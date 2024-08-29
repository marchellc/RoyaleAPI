using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Rules.Responses
{
    /// <summary>
    /// A response to the rules endpoint.
    /// </summary>
    public class GetRulesResponse
    {
        /// <summary>
        /// Gets the per-page rule count limit.
        /// </summary>
        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        /// <summary>
        /// Gets the total amount of included rules.
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets the page number.
        /// </summary>
        [JsonPropertyName("Page")]
        public int Page { get; set; }

        /// <summary>
        /// Gets the included rules.
        /// </summary>
        [JsonPropertyName("rules")]
        public RuleInfo[] Rules { get; set; }
    }
}