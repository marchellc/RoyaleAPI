using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Rules.Responses
{
    /// <summary>
    /// A response to the create rule endpoint.
    /// </summary>
    public class CreateRuleResponse
    {
        /// <summary>
        /// Gets the created rule's ID.
        /// </summary>
        [JsonPropertyName("rule_id")]
        public int RuleId { get; set; }
    }
}