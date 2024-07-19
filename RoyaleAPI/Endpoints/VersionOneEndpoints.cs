namespace RoyaleAPI.Endpoints
{
    public static class VersionOneEndpoints
    {
        public const string BaseUrl = "https://shield.royalehosting.net/api/v1";

        public static string Ips => $"{BaseUrl}/ips";
    }
}