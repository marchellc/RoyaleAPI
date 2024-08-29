using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Attacks
{
    public class AttackExtendedInfo
    {
        [JsonPropertyName("dport")]
        public PropertyData[] DestinationPorts { get; set; }

        [JsonPropertyName("plen")]
        public PropertyData[] PacketLengths { get; set; }

        [JsonPropertyName("sport")]
        public PropertyData[] SourcePorts { get; set; }

        [JsonPropertyName("src-asn")]
        public PropertyData[] SourceASNs { get; set; }

        [JsonPropertyName("src-country")]
        public PropertyData[] SourceCountries { get; set; }

        [JsonPropertyName("sip")]
        public PropertyData[] SourceIPs { get; set; }
    }
}