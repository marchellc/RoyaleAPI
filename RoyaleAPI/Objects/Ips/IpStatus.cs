using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Ips
{
    public class IpStatus
    {
        [JsonPropertyName("value")]
        public string Text { get; set; }

        [JsonPropertyName("label")]
        public string Name { get; set; }
    }
}