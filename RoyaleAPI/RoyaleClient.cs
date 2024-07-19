using RoyaleAPI.Endpoints;

using RoyaleAPI.Objects.Attacks;
using RoyaleAPI.Objects.Ips;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace RoyaleAPI
{
    /// <summary>
    /// A client used to send requests to the RoyaleHosting API.
    /// </summary>
    public class RoyaleClient : IDisposable
    {
        private HttpClient _client;
        private Action<string> _logger;

        public string Token { get; set; }

        /// <summary>
        /// Creates a new <see cref="RoyaleClient"></see> instance. You <b>MUST</b> send the <see cref="Token"/> property later.
        /// </summary>
        public RoyaleClient() { }

        /// <summary>
        /// Creates a new <see cref="RoyaleClient"/> instance with an API token.
        /// </summary>
        /// <param name="token">The API token to use for requests.</param>
        public RoyaleClient(string token) => Token = token;

        /// <summary>
        /// Initializes this client. Calling this method is <b>required</b>!
        /// </summary>
        /// <param name="client">A custom <see cref="HttpClient"/> to send requests by.</param>
        /// <param name="logger">A custom message logger.</param>
        public void InitializeClient(HttpClient client = null, Action<string> logger = null)
        {
            _client = client ?? new HttpClient();
            _logger = logger;
        }

        public async Task<AttackResponse> GetAttackAsync(string attackId)
        {
            InternalValidate();

            if (string.IsNullOrWhiteSpace(attackId))
                throw new ArgumentNullException(nameof(attackId));

            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{VersionTwoEndpoints.Attacks}/{attackId}"))
            {
                request.Headers.Add("token", Token);

                using (var response = await _client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();

                    var apiResponseString = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonSerializer.Deserialize<JsonObject>(apiResponseString);

                    if (!apiResponse.TryGetPropertyValue("success", out var successNode))
                        throw new Exception($"Missing success node");

                    var success = successNode.Deserialize<bool>();

                    if (!success)
                    {
                        if (apiResponse.TryGetPropertyValue("message", out var messageNode))
                            throw new Exception(messageNode.Deserialize<string>());
                        else
                            throw new Exception("Unknown failure");
                    }

                    if (!apiResponse.TryGetPropertyValue("data", out var dataNode))
                        throw new Exception("Missing data node");

                    return dataNode.Deserialize<AttackResponse>();
                }
            }
        }

        public async Task<AttackList> GetAttacksAsync(int chunk = 0)
        {
            InternalValidate();

            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{VersionTwoEndpoints.Attacks}?chunk={chunk}"))
            {
                request.Headers.Add("token", Token);

                using (var response = await _client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();

                    var apiResponseString = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonSerializer.Deserialize<JsonObject>(apiResponseString);

                    if (!apiResponse.TryGetPropertyValue("success", out var successNode))
                        throw new Exception($"Missing success node");

                    var success = successNode.Deserialize<bool>();

                    if (!success)
                    {
                        if (apiResponse.TryGetPropertyValue("message", out var messageNode))
                            throw new Exception(messageNode.Deserialize<string>());
                        else
                            throw new Exception("Unknown failure");
                    }

                    if (!apiResponse.TryGetPropertyValue("data", out var dataNode))
                        throw new Exception("Missing data node");

                    return dataNode.Deserialize<AttackList>();
                }
            }
        }

        public async Task<IpList> GetIpsAsync()
        {
            InternalValidate();

            using (var request = new HttpRequestMessage(HttpMethod.Get, VersionOneEndpoints.Ips))
            {
                request.Headers.Add("token", Token);

                using (var response = await _client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();

                    var apiResponseString = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonSerializer.Deserialize<JsonObject>(apiResponseString);

                    if (!apiResponse.TryGetPropertyValue("success", out var successNode))
                        throw new Exception($"Missing success node");

                    var success = successNode.Deserialize<bool>();

                    if (!success)
                    {
                        if (apiResponse.TryGetPropertyValue("message", out var messageNode))
                            throw new Exception(messageNode.Deserialize<string>());
                        else
                            throw new Exception("Unknown failure");
                    }

                    if (!apiResponse.TryGetPropertyValue("data", out var dataNode))
                        throw new Exception("Missing data node");

                    return dataNode.Deserialize<IpList>();
                }
            }
        }

        public void Dispose()
        {
            if (_client != null)
            {
                _client.Dispose();
                _client = null;
            }

            _logger = null;
        }

        private void InternalLog(object log)
            => _logger?.Invoke(log.ToString());

        private void InternalValidate()
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new InvalidOperationException($"You need to set the API token before sending a request.");
        }

        // Why in god's name did they choose a different format for a singular path
        internal static DateTime ParseDateTime(string dateTime)
        {
            var split = dateTime.Split(' ');

            var date = split[0];
            var dateSplit = date.Split('-');

            var time = split[1];
            var timeSplit = time.Split(':');

            return new DateTime(
                int.Parse(dateSplit[0]),
                int.Parse(dateSplit[1]),
                int.Parse(dateSplit[2]),

                int.Parse(timeSplit[0]),
                int.Parse(timeSplit[1]),
                int.Parse(timeSplit[2]));
        }
    }
}