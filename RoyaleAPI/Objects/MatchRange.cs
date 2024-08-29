using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects
{
    /// <summary>
    /// A rule port range used when creating a new rule.
    /// </summary>
    public class MatchRange
    {
        /// <summary>
        /// Gets the start of the range.
        /// </summary>
        [JsonPropertyName("start")]
        public ushort Start { get; set; }

        /// <summary>
        /// Gets the end of the range.
        /// </summary>
        [JsonPropertyName("end")]
        public ushort End { get; set; }
    }
}