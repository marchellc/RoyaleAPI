using RoyaleAPI.Attributes;

namespace RoyaleAPI.Objects.Enums
{
    [StringName(
        "Ended=end",
        "InProgress=ongoing",
        "Started=start")]
    public enum AttackStatus
    {
        Ended,
        InProgress,
        Started
    }
}