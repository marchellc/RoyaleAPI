using RoyaleAPI.Attributes;

using System;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;

namespace RoyaleAPI.Utilities
{
    public static class EnumTranslation
    {
        public static string ToValue(this Enum en)
        {
            if (en is null)
                throw new ArgumentNullException(nameof(en));

            var attribute = en.GetType().GetCustomAttribute<StringNameAttribute>();

            if (attribute is null)
                throw new ArgumentException($"Enum '{en.GetType().FullName}' does not have the StringNameAttribute", nameof(en));

            var key = en.ToString();

            if (!attribute.Processed.TryGetValue(key, out var translated))
                throw new ArgumentException($"Enum {en.GetType().FullName}'s StringNameAttribute does not contain a translation for member '{key}'", nameof(en));

            return translated;
        }

        public static T ToEnumKey<T>(this string str) where T : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentNullException(nameof(str));

            var attribute = typeof(T).GetCustomAttribute<StringNameAttribute>();

            if (attribute is null)
                throw new ArgumentException($"Enum '{typeof(T).FullName}' does not have the StringNameAttribute", nameof(T));

            var name = default(string);

            foreach (var pair in attribute.Processed)
            {
                if (pair.Value != str)
                    continue;

                name = pair.Key;
                break;
            }

            if (name is null)
                throw new ArgumentException($"Enum '{typeof(T).FullName}' does not have a defined member for value '{str}'", nameof(T));

            var members = Enum.GetValues(typeof(T)).Cast<T>();
            var member = members.First(x => x.ToString() == name);

            return member;
        }

        public static ProtocolType ToProtocol(int protocolType)
        {
            if (Enum.IsDefined(typeof(ProtocolType), protocolType))
                return (ProtocolType)protocolType;

            throw new Exception($"Protocol {protocolType} is not defined");
        }
    }
}