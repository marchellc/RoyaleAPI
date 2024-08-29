using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Attacks.Responses
{
    public class GetAttackResponse
    {
        [JsonPropertyName("attack")]
        public AttackInfo BaseInfo { get; set; }

        [JsonPropertyName("info")]
        public AttackExtendedInfo ExtendedInfo { get; set; }
    }
}