using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects
{
    public class PropertyData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }
    }
}