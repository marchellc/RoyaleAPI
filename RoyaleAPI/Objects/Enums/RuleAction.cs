using RoyaleAPI.Attributes;

namespace RoyaleAPI.Objects.Enums
{
    [StringName(
        "Permit=permit",
        "Deny=deny")]
    public enum RuleAction
    {
        Permit,
        Deny
    }
}