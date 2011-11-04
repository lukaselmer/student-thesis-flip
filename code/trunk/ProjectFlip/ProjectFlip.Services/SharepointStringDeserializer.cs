using System;
using System.Collections.Generic;
using System.Linq;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    internal static class SharepointStringDeserializer
    {
        public static IList<IMetadata> ToList(string line, MetadataType type)
        {
            return ToStringList(line).Select(s => new Metadata(type, s)).Cast<IMetadata>().ToList();
        }

        public static IEnumerable<string> ToStringList(string line)
        {
            return line.TrimStart(new[] { '"' }).TrimEnd(new[] { '"' }).Replace("\"\"", "\"").Replace("#;#", "##;").
                Split(new[] { "#;", ";#___", ";#__", ";#_ ", ";#", ";" }, StringSplitOptions.RemoveEmptyEntries).
                ToList().Select(s => s.Trim());
        }
    }
}