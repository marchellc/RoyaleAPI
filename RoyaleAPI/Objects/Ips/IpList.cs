using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Ips
{
    public class IpList
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("pages")]
        public int Pages { get; set; }

        [JsonPropertyName("ips")]
        public IpObject[] Ips { get; set; }
    }
}