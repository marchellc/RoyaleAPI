namespace RoyaleAPI
{
    public static class RoyaleEndpoints
    {
        public const string BaseUrl = "https://shield.royalehosting.net/api/v2";

        public static string GetIpsUrl => BaseUrl + "/ips";
        public static string GetRulesUrl => BaseUrl + "/rules";
        public static string GetAttacksUrl => BaseUrl + "/attacks";

        public static string GetAnalyticsUrl => BaseUrl + "/analytics/traffic";

        public static string PresetsUrl => GetRulesUrl + "/presets";

        public static string GetAttackUrl(string attackId)
            => GetAttacksUrl + $"/{attackId}";

        public static string GetDeleteRuleUrl(uint ruleId)
            => GetRulesUrl + $"{ruleId}";

        public static string GetDeleteGroupUrl(uint groupId)
            => GetRulesUrl + $"/groups/{groupId}";
    }
}