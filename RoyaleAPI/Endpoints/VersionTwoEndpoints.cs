namespace RoyaleAPI.Endpoints
{
    public static class VersionTwoEndpoints
    {
        public const string BaseUrl = "https://shield.royalehosting.net/api/v2";

        public static string Attacks => $"{BaseUrl}/attacks";
        public static string AttackId => $"{BaseUrl}/attacks/attack_id";
    }
}