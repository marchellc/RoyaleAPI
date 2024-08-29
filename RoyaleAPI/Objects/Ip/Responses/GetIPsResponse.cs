using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Ip.Responses
{
    public class GetIPsResponse
    {
        [JsonPropertyName("ips")]
        public IpAddress[] IPs { get; set; }

        [JsonPropertyName("count")]
        public int Total { get; set; }

        [JsonPropertyName("pages")]
        public int Pages { get; set; }
    }
}