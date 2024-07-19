using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Attacks
{
    public class AttackInfo
    {
        [JsonPropertyName("dport")]
        public AttackValue[] DestinationPorts { get; set; }

        [JsonPropertyName("plen")]
        public AttackValue[] Packets { get; set; }

        [JsonPropertyName("prot")]
        public AttackValue[] Protocols { get; set; }

        [JsonPropertyName("src-asn")]
        public AttackValue[] SourceAsns { get; set; }

        [JsonPropertyName("src-country")]
        public AttackValue[] SourceCountries { get; set; }

        [JsonPropertyName("sip")]
        public AttackValue[] SourceIps { get; set; }

        [JsonPropertyName("sport")]
        public AttackValue[] SourcePorts { get; set; }
    }
}