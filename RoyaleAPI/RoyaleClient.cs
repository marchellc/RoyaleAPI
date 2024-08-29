using RoyaleAPI.Interfaces;

using RoyaleAPI.Objects;
using RoyaleAPI.Objects.Analytics;
using RoyaleAPI.Objects.Attacks.Responses;
using RoyaleAPI.Objects.Enums;
using RoyaleAPI.Objects.Ip.Forms;
using RoyaleAPI.Objects.Ip.Responses;
using RoyaleAPI.Objects.Presets;
using RoyaleAPI.Objects.Rules;
using RoyaleAPI.Objects.Rules.Responses;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoyaleAPI
{
    /// <summary>
    /// A client used to send requests to the RoyaleHosting API.
    /// </summary>
    public class RoyaleClient : IDisposable
    {
        private static readonly MediaTypeHeaderValue JsonHeader = new MediaTypeHeaderValue("application/json");
        private static readonly HttpMethod PatchMethod = new HttpMethod("PATCH");

        private HttpClient _client;
        private Action<string> _logger;

        /// <summary>
        /// Gets or sets the client's API token.
        /// </summary>
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

        /// <inheritdoc/>
        public void Dispose()
        {
            if (_client != null)
            {
                _client.Dispose();
                _client = null;
            }

            _logger = null;
        }

        public async Task DeleteRuleAsync(uint ruleId)
            => await InternalSendAsync(HttpMethod.Delete, RoyaleEndpoints.GetDeleteRuleUrl(ruleId));

        public async Task DeleteGroupAsync(uint groupId)
            => await InternalSendAsync(HttpMethod.Delete, RoyaleEndpoints.GetDeleteGroupUrl(groupId));

        public async Task<CreateRuleResponse> CreateRuleAsync(CreateRuleForm form)
        {
            if (form is null)
                throw new ArgumentNullException(nameof(form));

            return await InternalSubmitFormAsync<CreateRuleResponse>(RoyaleEndpoints.GetRulesUrl, form, HttpMethod.Post);
        }

        public async Task<CreatePresetForm> CreatePresetAsync(CreatePresetForm form)
        {
            if (form is null)
                throw new ArgumentNullException(nameof(form));

            return await InternalSubmitFormAsync<CreatePresetForm>(RoyaleEndpoints.PresetsUrl, form, HttpMethod.Post);
        }

        public async Task SetDnsAsync(SetDnsForm form)
        {
            if (form is null)
                throw new ArgumentNullException(nameof(form));

            await InternalSubmitFormAsync(RoyaleEndpoints.GetIpsUrl, form, PatchMethod);
        }

        public async Task<AnalyticsData> GetAnalyticsAsync(string ipMask, AnalyticsUnit unit, TimeSpan time)
        {
            if (string.IsNullOrWhiteSpace(ipMask))
                throw new ArgumentNullException(nameof(ipMask));

            if (time <= TimeSpan.Zero)
                throw new ArgumentException($"Time has to be more than zero.", nameof(time));

            var url = RoyaleEndpoints.GetAnalyticsUrl + $"?ip={ipMask}?time={Math.Floor((double)time.TotalMinutes)}";

            if (unit is AnalyticsUnit.MegaBitsPerSecond)
                url += "?unit=mbps";
            else
                url += "?unit=pps";

            return await InternalGetAsync<AnalyticsData>(url);
        }

        public async Task<GetIPsResponse> GetIPsAsync(int? page = null, string filter = null, int? limit = null)
        {
            var url = RoyaleEndpoints.GetIpsUrl;

            if (page.HasValue && page.Value > 0)
                url += $"?page={page.Value}";

            if (limit.HasValue && limit.Value > 0)
                url += $"?limit={limit.Value}";

            if (!string.IsNullOrWhiteSpace(filter))
                url += $"?filter={filter}";

            return await InternalGetAsync<GetIPsResponse>(url);
        }

        public async Task<GetAttackResponse> GetAttackAsync(string attackId)
        {
            if (string.IsNullOrWhiteSpace(attackId))
                throw new ArgumentNullException(nameof(attackId));

            return await InternalGetAsync<GetAttackResponse>(RoyaleEndpoints.GetAttackUrl(attackId));
        }

        public async Task<GetAttacksResponse> GetAttacksAsync(uint chunk, string filter = null, int? limit = null)
        {
            var url = RoyaleEndpoints.GetAttacksUrl + $"?chunk={chunk}";

            if (limit.HasValue && limit.Value > 0)
                url += $"?limit={limit.Value}";

            if (!string.IsNullOrWhiteSpace(filter))
                url += $"?filter={filter}";

            return await InternalGetAsync<GetAttacksResponse>(url);
        }

        public async Task<GetRulesResponse> GetRulesAsync(int? page = null, string filter = null, int? limit = null)
        {
            var url = RoyaleEndpoints.GetRulesUrl;

            if (page.HasValue && page.Value > 0)
                url += $"?page={page.Value}";

            if (limit.HasValue && limit.Value > 0)
                url += $"?limit={limit.Value}";

            if (!string.IsNullOrWhiteSpace(filter))
                url += $"?filter={filter}";

            return await InternalGetAsync<GetRulesResponse>(url);
        }

        internal void InternalLog(object log)
            => _logger?.Invoke(log.ToString());

        private void InternalValidate()
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new InvalidOperationException($"You need to set the API token before sending a request.");
        }

        private async Task InternalSubmitFormAsync(string url, IForm form, HttpMethod method)
        {
            form.ValidateForm();

            await InternalSendAsync(method, url, r =>
            {
                r.Content = new StringContent(form.ToJson());
                r.Content.Headers.ContentType = JsonHeader;
            });
        }

        private async Task<T> InternalSubmitFormAsync<T>(string url, IForm form, HttpMethod method)
        {
            form.ValidateForm();

            return (await InternalSendAsync(method, url, r =>
            {
                r.Content = new StringContent(form.ToJson());
                r.Content.Headers.ContentType = JsonHeader;
            })).Data.Deserialize<T>();
        }

        private async Task<T> InternalGetAsync<T>(string url)
            => (await InternalSendAsync(HttpMethod.Get, url)).Data.Deserialize<T>();

        private async Task<BaseResponse> InternalSendAsync(HttpMethod method, string url, Action<HttpRequestMessage> requestSetup = null)
        {
            InternalValidate();

            using (var request = new HttpRequestMessage(method, url))
            {
                request.Headers.Add("token", Token);

                if (requestSetup != null)
                    requestSetup(request);

                using (var response = await _client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var msg = JsonSerializer.Deserialize<BaseResponse>(json);

                    if (!msg.IsSuccess)
                        throw new Exception($"The API has returned an error: {msg.Message}");

                    return msg;
                }
            }
        }
    }
}