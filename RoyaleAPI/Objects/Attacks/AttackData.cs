using System;
using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Attacks
{
    public class AttackData
    {
        [JsonPropertyName("attack_id")]
        public string Id { get; set; }

        [JsonPropertyName("dest")]
        public string Target { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("duration")]
        public int TotalDuration { get; set; }

        [JsonPropertyName("volume")]
        public int TotalVolume { get; set; }

        [JsonPropertyName("mbps")]
        public int Mbps { get; set; }

        [JsonPropertyName("pps")]
        public int Pps { get; set; }

        [JsonPropertyName("start_time")]
        public string EndedAtString { get; set; }

        [JsonPropertyName("event_time")]
        public string StartedAtString { get; set; }

        [JsonIgnore]
        public DateTime EndedAt => RoyaleClient.ParseDateTime(EndedAtString);

        [JsonIgnore]
        public DateTime StartedAt => RoyaleClient.ParseDateTime(StartedAtString);

        [JsonIgnore]
        public bool HasEnded => Status == "end";
    }
}