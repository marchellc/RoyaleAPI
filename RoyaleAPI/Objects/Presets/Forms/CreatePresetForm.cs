using RoyaleAPI.Interfaces;
using RoyaleAPI.Objects.Enums;
using RoyaleAPI.Utilities;

using System.Text.Json.Nodes;
using System;

namespace RoyaleAPI.Objects.Presets
{
    public class CreatePresetForm : IForm
    {
        public int Id { get; set; } = -1;

        public string DestinationMask { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;

        public string[] IPs { get; set; }

        public PresetType Type { get; set; } = PresetType.AllowClouflare;

        public void ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(DestinationMask))
                throw new Exception($"Destination mask has not been set");
        }

        public string ToJson()
        {
            var obj = new JsonObject();

            obj.Add("destination", DestinationMask);
            obj.Add("preset", Type.ToValue());

            if (!string.IsNullOrWhiteSpace(Note))
                obj.Add("note", Note);

            if (Id > 0)
                obj.Add("id", Id);

            if (IPs != null && IPs.Length > 0)
            {
                var array = new JsonArray();

                for (int i = 0; i < IPs.Length; i++)
                    array.Add(IPs[i]);

                obj.Add("ips", array);
            }

            return obj.ToJsonString();
        }
    }
}