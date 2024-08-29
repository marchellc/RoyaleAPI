using RoyaleAPI.Objects.Enums;
using RoyaleAPI.Utilities;

using System;
using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Attacks
{
    public class AttackInfo
    {
        [JsonPropertyName("duration")]
        private int durationSeconds { get; set; }

        [JsonPropertyName("status")]
        private string attackStatus { get; set; }

        [JsonPropertyName("volume")]
        public int Dropped { get; set; }

        [JsonPropertyName("pps")]
        public int PacketsPerSecond { get; set; }

        [JsonPropertyName("mbps")]
        public int MegaBitsPerSecond { get; set; }

        [JsonPropertyName("attack_id")]
        public string Id { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("dest")]
        public string Destination { get; set; }

        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("event_time")]
        public DateTime EventTime { get; set; }

        [JsonIgnore]
        public AttackStatus Status
        {
            get => attackStatus.ToEnumKey<AttackStatus>();
            set => attackStatus = value.ToValue();
        }

        [JsonIgnore]
        public TimeSpan Duration
        {
            get => TimeSpan.FromSeconds(durationSeconds);
            set => durationSeconds = (int)Math.Floor((double)durationSeconds);
        }
    }
}