using RoyaleAPI.Objects.Attacks;
using RoyaleAPI.Objects.Ips;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace RoyaleAPI
{
    public class AttackWatcher : IDisposable
    {
        private Timer _timer;

        private IpList _ips;
        private AttackList _attacks;

        private RoyaleClient _client;

        private DateTime _startTime;
        private DateTime _refreshTime;

        private double _refreshInterval;

        private List<AttackResponse> _attacksInProgress = new List<AttackResponse>();

        public bool IsRunning
        {
            get => _timer.Enabled;
            set => _timer.Enabled = value;
        }

        public IReadOnlyList<AttackResponse> InProgress => _attacksInProgress;

        public event Action<AttackResponse> OnAttackDetected;
        public event Action<AttackResponse> OnAttackEnded;

        public event Action<IpList> OnIpListRefreshed;
        public event Action<AttackList> OnAttackListRefreshed;

        public AttackWatcher(RoyaleClient client, double interval = 5000, double ipRefreshInterval = 60000)
        {
            _client = client;

            _startTime = DateTime.Now;

            _refreshTime = DateTime.MinValue;
            _refreshInterval = ipRefreshInterval;

            _timer = new Timer(interval);
            _timer.Elapsed += OnElapsed;
            _timer.Start();
        }

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Stop();

                _timer.Elapsed -= OnElapsed;

                _timer.Dispose();
                _timer = null;
            }

            _ips = null;
            _attacks = null;
            _client = null;

            _refreshTime = DateTime.MinValue;
            _refreshInterval = 0;

            _attacksInProgress.Clear();
            _attacksInProgress = null;
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            Task.Run(async () =>
            {
                if (_ips is null || (_refreshInterval > 0 && (DateTime.Now - _refreshTime).TotalMilliseconds <= _refreshInterval))
                {
                    _ips = await _client.GetIpsAsync();
                    _refreshTime = DateTime.Now;

                    OnIpListRefreshed?.Invoke(_ips);
                }

                _attacks = await _client.GetAttacksAsync();

                OnAttackListRefreshed?.Invoke(_attacks);

                foreach (var attack in _attacks.Attacks)
                {
                    if (attack.HasEnded)
                    {
                        foreach (var attackInfo in _attacksInProgress)
                        {
                            if (attackInfo.Attack.Id == attack.Id)
                                OnAttackEnded?.Invoke(attackInfo);
                        }

                        _attacksInProgress.RemoveAll(a => a.Attack.Id == attack.Id);
                        continue;
                    }

                    if (_attacksInProgress.Any(a => a.Attack.Id == attack.Id))
                        continue;

                    var info = await _client.GetAttackAsync(attack.Id);

                    _attacksInProgress.Add(info);
                    OnAttackDetected?.Invoke(info);
                }
            });
        }
    }
}
