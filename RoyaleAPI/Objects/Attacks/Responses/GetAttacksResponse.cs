using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Attacks.Responses
{
    public class GetAttacksResponse
    {
        [JsonPropertyName("attacks")]
        public AttackInfo[] Attacks { get; set; }
    }
}