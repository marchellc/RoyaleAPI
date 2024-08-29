using RoyaleAPI.Attributes;

namespace RoyaleAPI.Objects.Enums
{
    [StringName(
        "MegaBitsPerSecond=mbps",
        "PacketsPerSecond=pps")]
    public enum AnalyticsUnit
    {
        MegaBitsPerSecond,
        PacketsPerSecond
    }
}