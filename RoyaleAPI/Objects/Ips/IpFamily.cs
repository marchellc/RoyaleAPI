using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Ips
{
    public class IpFamily
    {
        [JsonPropertyName("value")]
        public int Type { get; set; }

        [JsonPropertyName("label")]
        public string Name { get; set; }
    }
}