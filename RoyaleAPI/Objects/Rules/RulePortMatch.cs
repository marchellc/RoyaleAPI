using RoyaleAPI.Objects.Enums;
using RoyaleAPI.Utilities;

using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Rules
{
    /// <summary>
    /// Represents a rule port match when creating a new rule.
    /// </summary>
    public class RulePortMatch
    {
        [JsonPropertyName("match_type")]
        public string matchType { get; set; } = "eq";

        /// <summary>
        /// Gets the match type.
        /// </summary>
        [JsonIgnore]
        public MatchType MatchType
        {
            get => matchType.ToEnumKey<MatchType>();
            set => matchType = value.ToValue();
        }

        /// <summary>
        /// Gets the match list (required if <see cref="MatchType"/> is NOT set to <see cref="MatchType.Range"/>).
        /// </summary>
        [JsonPropertyName("match_value")]
        public ushort[] MatchValues { get; set; }

        /// <summary>
        /// Gets the match range (required if <see cref="MatchType"/> is set to <see cref="MatchType.Range"/>).
        /// </summary>
        public MatchRange MatchRange { get; set; }
    }
}