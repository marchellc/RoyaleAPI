using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Ip.Responses
{
    public class GetIPsResponse
    {
        [JsonPropertyName("ips")]
        public IpAddress[] IPs { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("pages")]
        public int Pages { get; set; }

        [JsonPropertyName("limit")]
        public int Limit { get; set; }
    }
}