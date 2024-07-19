using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects
{
    public class ApiResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}