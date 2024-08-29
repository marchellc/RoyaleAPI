using RoyaleAPI.Interfaces;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RoyaleAPI.Objects.Ip.Forms
{
    public class SetDnsForm : IForm
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("dns_name")]
        public string Dns { get; set; }

        public void ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(Address))
                throw new Exception("Address cannot be empty.");

            if (string.IsNullOrWhiteSpace(Dns))
                throw new Exception("The new DNS cannot be empty.");
        }

        public string ToJson()
            => JsonSerializer.Serialize(this);
    }
}