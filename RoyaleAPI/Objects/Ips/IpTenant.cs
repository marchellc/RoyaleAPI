using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Ips
{
    public class IpTenant
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("display")]
        public string Display { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }
    }
}