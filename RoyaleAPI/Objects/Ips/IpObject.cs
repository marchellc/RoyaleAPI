using System;
using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Ips
{
    public class IpObject
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("display")]
        public string Display { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("dns_name")]
        public string DnsName { get; set; }

        [JsonPropertyName("created")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("family")]
        public IpFamily Family { get; set; }

        [JsonPropertyName("tenant")]
        public IpTenant Tenant { get; set; }

        [JsonPropertyName("status")]
        public IpStatus Status { get; set; }
    }
}