using RoyaleAPI.Attributes;

namespace RoyaleAPI.Objects.Enums
{
    [StringName(
        "AckBit=ack",
        "FinBit=fin",
        "PshBit=psh",
        "RstBit=rst",
        "SynBit=syn",
        "UrgBit=urg",
        
        "Dscp=dscp",
        "Ecn=ecn",
        
        "Protocol=proto",
        "Fragments=fragments",
        
        "Ttl=ttl")]
    public enum PacketMatchPart
    {
        AckBit,
        FinBit,
        PshBit,
        RstBit,
        SynBit,
        UrgBit,

        Dscp,
        Ecn,

        Protocol,
        Fragments,

        Ttl
    }
}