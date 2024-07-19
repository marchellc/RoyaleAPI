using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Attacks
{
    public class AttackResponse
    {
        [JsonPropertyName("attack")]
        public AttackData Attack { get; set; }

        [JsonPropertyName("info")]
        public AttackInfo Info { get; set; }
    }
}