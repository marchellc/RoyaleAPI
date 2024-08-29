using RoyaleAPI.Interfaces;
using RoyaleAPI.Utilities;

using System;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RoyaleAPI.Objects.Rules
{
    /// <summary>
    /// Represents a form used to create a new rule.
    /// </summary>
    public class CreateRuleForm : IForm
    {
        /// <summary>
        /// Gets or sets the rule's basic data.
        /// </summary>
        public RuleInfo Info { get; set; }

        public RulePortMatch DestinationPort { get; set; }

        public RulePortMatch SourcePort { get; set; }

        /// <summary>
        /// Gets or sets the rule's packet matching behaviours
        /// </summary>
        public RulePacketMatch[] PacketMatches { get; set; }

        public void ValidateForm()
        {
            if (Info is null)
                throw new Exception($"Info has to be defined");

            if (string.IsNullOrWhiteSpace(Info.DestinationMask))
                throw new Exception($"Destination cannot be empty");

            if (Info.Position < 0)
                throw new Exception("Position cannot be less than zero");

            if (Info.Position > 10000)
                throw new Exception("Position cannot be higher than ten thousand");
        }

        public string ToJson()
        {
            var obj = new JsonObject();

            obj.Add("destination", Info.DestinationMask);
            obj.Add("action", Info.Action.ToValue());
            obj.Add("protocol", (int)Info.Protocol);
            obj.Add("order", Info.Position);

            if (Info.Id > 0)
                obj.Add("id", Info.Id);

            if (!string.IsNullOrWhiteSpace(Info.Note))
                obj.Add("note", Info.Note);

            if (!string.IsNullOrWhiteSpace(Info.SourceMask))
                obj.Add("source", Info.SourceMask);

            if (DestinationPort != null)
                obj.Add("destination_port", JsonSerializer.Serialize(DestinationPort));

            if (SourcePort != null)
                obj.Add("source_port", JsonSerializer.Serialize(SourcePort));

            if (PacketMatches != null && PacketMatches.Length > 0)
            {
                var array = new JsonArray();

                for (int i = 0; i < PacketMatches.Length; i++)
                    array.Add(JsonSerializer.Serialize(PacketMatches[i]));

                obj.Add("matches", array);
            }

            return obj.ToJsonString();
        }
    }
}