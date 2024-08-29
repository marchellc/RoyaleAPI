using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects
{
    public class BaseResponse
    {
        [JsonPropertyName("data")]
        public JsonObject Data { get; set; }

        [JsonPropertyName("success")]
        public bool IsSuccess { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}