using RoyaleAPI.Objects.Enums;
using RoyaleAPI.Utilities;

using System;
using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Attacks
{
    public class AttackInfo
    {
        [JsonPropertyName("start_time")]
        public string startString { get; set; }

        [JsonPropertyName("event_time")]
        public string eventString { get; set; }

        [JsonPropertyName("duration")]
        public int durationSeconds { get; set; }

        [JsonPropertyName("status")]
        public string attackStatus { get; set; }

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

        [JsonIgnore]
        public DateTime StartTime => StupidDateTimeFormatParser.ParseDateTime(startString);

        [JsonIgnore]
        public DateTime EventTime => StupidDateTimeFormatParser.ParseDateTime(eventString);

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