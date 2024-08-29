using RoyaleAPI.Attributes;

namespace RoyaleAPI.Objects.Enums
{
    [StringName(
        "AllowCloudflare=allow-cf-web",
        "AllowCommonDns=allow-common-dns",

        "BlockSsh=block-ssh",
        "BlockRdp=block-rdp",

        "BlockCommonAttackVectors=block-common-vectors")]
    public enum PresetType
    {
        AllowClouflare,
        AllowCommonDns,

        BlockSsh,
        BlockRdp,

        BlockCommonAttackVectors
    }
}