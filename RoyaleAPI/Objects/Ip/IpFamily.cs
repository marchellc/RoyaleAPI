using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Ip
{
    public class IpFamily
    {
        [JsonPropertyName("value")]
        public int Value { get; set; }
    }
}