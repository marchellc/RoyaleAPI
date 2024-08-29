using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Ip
{
    public class IpAddress
    {
        [JsonPropertyName("address")]
        public string Mask { get; set; }

        [JsonPropertyName("dns_name")]
        public string Dns { get; set; }

        [JsonPropertyName("family")]
        public IpFamily Family { get; set; }
    }
}