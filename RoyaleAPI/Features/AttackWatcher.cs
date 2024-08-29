using System;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;
using System.Collections.Generic;

using RoyaleAPI.Objects.Attacks.Responses;
using RoyaleAPI.Objects.Attacks;
using RoyaleAPI.Objects.Enums;

namespace RoyaleAPI.Features
{
    /// <summary>
    /// A feature used to monitor incoming attacks and their end.
    /// </summary>
    public class AttackWatcher : IDisposable
    {
        private Timer _timer;
        private RoyaleClient _client;
        private GetAttacksResponse _attacks;

        private List<GetAttackResponse> _attacksInProgress = new List<GetAttackResponse>();
        private List<AttackInfo> _attackCache = new List<AttackInfo>();

        /// <summary>
        /// Gets or sets a value indicating whether or not the watcher is checking for attacks.
        /// </summary>
        public bool IsRunning
        {
            get => _timer.Enabled;
            set => _timer.Enabled = value;
        }

        /// <summary>
        /// Gets a list of all attacks that are currently in progress.
        /// </summary>
        public IReadOnlyList<GetAttackResponse> InProgress => _attacksInProgress;

        /// <summary>
        /// Gets called once an attack starts.
        /// </summary>
        public event Action<GetAttackResponse> OnAttackDetected;

        /// <summary>
        /// Gets caled once an attack ends.
        /// </summary>
        public event Action<GetAttackResponse> OnAttackEnded;

        /// <summary>
        /// Gets called once an error occurs.
        /// </summary>
        public event Action<Exception> OnError;

        /// <summary>
        /// Creates a new watcher with the specified client.
        /// </summary>
        /// <param name="client">The client to use for API communication.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public AttackWatcher(RoyaleClient client)
        {
            if (client is null)
                throw new ArgumentNullException(nameof(client));

            _client = client;
        }

        /// <summary>
        /// Starts the watcher with the specified interval.
        /// </summary>
        /// <param name="interval">The check interval (in milliseconds).</param>
        public void Start(double interval = 5000)
        {
            if (_timer != null)
            {
                _timer.Stop();

                _timer.Elapsed -= OnElapsed;

                _timer.Dispose();
                _timer = null;
            }

            _timer = new Timer(interval);
            _timer.Elapsed += OnElapsed;

            _timer.Start();
        }

        /// <summary>
        /// Stops the watcher and diposes the client.
        /// </summary>
        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Stop();

                _timer.Elapsed -= OnElapsed;

                _timer.Dispose();
                _timer = null;
            }

            _attacks = null;
            _client = null;

            _attacksInProgress.Clear();
            _attacksInProgress = null;

            _attackCache.Clear();
            _attackCache = null;
        }

        private void OnElapsed(object sender, ElapsedEventArgs _)
        {
            Task.Run(async () =>
            {
                try
                {
                    _attacks = await _client.GetAttacksAsync(0);
                    _attackCache.Clear();

                    foreach (var attack in _attacks.Attacks)
                    {
                        if (attack.Status is AttackStatus.Ended)
                        {
                            foreach (var attackInfo in _attacksInProgress)
                            {
                                if (attackInfo.BaseInfo.Id == attack.Id)
                                    _attackCache.Add(attack);
                            }

                            _attacksInProgress.RemoveAll(a => a.BaseInfo.Id == attack.Id);
                            continue;
                        }

                        if (_attacksInProgress.Any(a => a.BaseInfo.Id == attack.Id))
                            continue;

                        var info = await _client.GetAttackAsync(attack.Id);

                        if (info is null)
                            _client.InternalLog("info is null");

                        _attacksInProgress.Add(info);
                        OnAttackDetected?.Invoke(info);
                    }

                    foreach (var data in _attackCache)
                    {
                        _attacksInProgress.RemoveAll(x => x.BaseInfo.Id == data.Id);

                        var info = await _client.GetAttackAsync(data.Id);

                        OnAttackEnded?.Invoke(info);
                    }
                }
                catch (Exception ex)
                {
                    OnError?.Invoke(ex);
                }
            });
        }
    }
}
