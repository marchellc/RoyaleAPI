using RoyaleAPI.Objects.Enums;
using RoyaleAPI.Utilities;

using System;
using System.Net.Sockets;
using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Rules
{
    /// <summary>
    /// A rule object.
    /// </summary>
    public class RuleInfo
    {
        [JsonPropertyName("action")]
        public string action { get; set; }

        [JsonPropertyName("src_port_type")]
        public string srcPortMatch { get; set; }

        [JsonPropertyName("dst_port_type")]
        public string dstPostMatch { get; set; }

        [JsonPropertyName("protocol")]
        public int protocol { get; set; }

        /// <summary>
        /// Gets the rule's ID.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets the rule's group ID. Will be zero if not a part of a group.
        /// </summary>
        [JsonPropertyName("group_id")]
        public int GroupId { get; set; }

        /// <summary>
        /// Gets the rule's position.
        /// </summary>
        [JsonPropertyName("order")]
        public int Position { get; set; }

        /// <summary>
        /// Gets the amount of packets that matched this rule.
        /// </summary>
        [JsonPropertyName("matched_packets")]
        public int Packets { get; set; }

        /// <summary>
        /// Gets the rule's destination IP as a CIDR mask.
        /// </summary>
        [JsonPropertyName("destination")]
        public string DestinationMask { get; set; }

        /// <summary>
        /// Gets the rule's source IP as a CIDR mask.
        /// </summary>
        [JsonPropertyName("source")]
        public string SourceMask { get; set; }

        /// <summary>
        /// Gets the rule's note.
        /// </summary>
        [JsonPropertyName("note")]
        public string Note { get; set; }

        /// <summary>
        /// Gets a boolean indicating whether or not the rule is active.
        /// </summary>
        [JsonPropertyName("disabled")]
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Gets the rule's last edit time.
        /// </summary>
        [JsonPropertyName("last_rule_update")]
        public DateTime LastEditAt { get; set; }

        /// <summary>
        /// Gets the rule's last data update time.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the source port matching type.
        /// </summary>
        [JsonIgnore]
        public MatchType SourcePortMatching
        {
            get => srcPortMatch.ToEnumKey<MatchType>();
            set => srcPortMatch = value.ToValue();
        }

        /// <summary>
        /// Gets or sets the destination port matching type.
        /// </summary>
        [JsonIgnore]
        public MatchType DestinationPortMatching
        {
            get => dstPostMatch.ToEnumKey<MatchType>();
            set => dstPostMatch = value.ToValue();
        }

        /// <summary>
        /// Gets or sets the rule's action.
        /// </summary>
        [JsonIgnore]
        public RuleAction Action
        {
            get => action.ToEnumKey<RuleAction>();
            set => action = value.ToValue();
        }

        /// <summary>
        /// Gets or sets the rule's protocol.
        /// </summary>
        [JsonIgnore]
        public ProtocolType Protocol
        {
            get => EnumTranslation.ToProtocol(protocol);
            set => protocol = (int)value;
        }
    }
}
