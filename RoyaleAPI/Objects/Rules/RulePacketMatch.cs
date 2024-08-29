using RoyaleAPI.Objects.Enums;
using RoyaleAPI.Utilities;

using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Rules
{
    /// <summary>
    /// Represents packet matching in a rule.
    /// </summary>
    public class RulePacketMatch
    {
        [JsonPropertyName("match")]
        private string matchPart { get; set; } = "ack";

        [JsonPropertyName("match_type")]
        private string matchType { get; set; } = "eq";

        /// <summary>
        /// Gets or sets the packet matching part.
        /// </summary>
        [JsonIgnore]
        public PacketMatchPart Part
        {
            get => matchPart.ToEnumKey<PacketMatchPart>();
            set => matchPart = value.ToValue();
        }

        /// <summary>
        /// Gets or sets the packet matching behaviour.
        /// </summary>
        [JsonIgnore]
        public MatchType Type
        {
            get => matchType.ToEnumKey<MatchType>();
            set => matchType = value.ToValue();
        }

        /// <summary>
        /// Gets or sets the packet matching value (can be an integer or a boolean).
        /// </summary>
        [JsonPropertyName("match_value")]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the packet matching range (required if <see cref="Type"/> is set to <see cref="MatchType.Range"/>).
        /// </summary>
        [JsonPropertyName("range")]
        public MatchRange Range { get; set; }
    }
}