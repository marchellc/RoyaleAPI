using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Attacks
{
    public class AttackList
    {
        [JsonPropertyName("attacks")]
        public AttackData[] Attacks { get; set; }
    }
}