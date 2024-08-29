using RoyaleAPI.Attributes;

namespace RoyaleAPI.Objects.Enums
{
    [StringName(
        "Equal=eq",
        "NotEqual=neq",

        "GreaterThan=gt",
        "LowerThan=lt",

        "Range=range")]
    public enum MatchType
    {
        Equal,
        NotEqual,

        GreaterThan,
        LowerThan,

        Range
    }
}