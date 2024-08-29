using System;
using System.Collections.Generic;

namespace RoyaleAPI.Attributes
{
    /// <summary>
    /// Used for enum translation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public class StringNameAttribute : Attribute
    {
        /// <summary>
        /// Paired enum members.
        /// </summary>
        public string[] Names { get; }

        /// <summary>
        /// Processed enum members.
        /// </summary>
        public Dictionary<string, string> Processed { get; }

        /// <summary>
        /// Creates a new <see cref="StringNameAttribute"/> instance.
        /// </summary>
        /// <param name="names">Enum members split with a = sign.</param>
        public StringNameAttribute(params string[] names)
        {
            Names = names;
            Processed = new Dictionary<string, string>(names.Length);

            for (int i = 0; i < names.Length; i++)
            {
                var split = names[i].Split('=');

                if (split.Length != 2)
                    continue;

                Processed[split[0].Trim()] = split[1].Trim();
            }
        }
    }
}