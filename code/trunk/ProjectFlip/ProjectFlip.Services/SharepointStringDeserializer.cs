#region

using System;
using System.Collections.Generic;
using System.Linq;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    internal static class SharepointStringDeserializer
    {
        #region Other

        public static IEnumerable<IMetadata> Deserialize(string line, MetadataType type)
        {
            return ToStringList(line).Select(s => Metadata.Get(type, s)).Cast<IMetadata>().ToList();
        }

        public static IEnumerable<string> ToStringList(string line)
        {
            return
                line.TrimStart(new[] {'"'}).TrimEnd(new[] {'"'}).Replace("\"\"", "\"").Replace("#;#", "##;").Split(
                    new[] {"#;", ";#___", ";#__", ";#_ ", ";#", ";", ","}, StringSplitOptions.RemoveEmptyEntries).ToList
                    ().Select(s => s.Replace("_", "")).Select(s => s.Trim());
        }

        #endregion
    }
}