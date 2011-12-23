#region

using System;
using System.Collections.Generic;
using System.Linq;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    /// <summary>
    /// This class is used to remove unwanted characters from the metadata and to split
    /// a line of metadata into single objects.
    /// </summary>
    /// <remarks></remarks>
    internal static class SharepointStringDeserializer
    {
        #region Other

        /// <summary>
        /// Deserializes the specified line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IEnumerable<IMetadata> Deserialize(string line, MetadataType type)
        {
            return ToStringList(line).Select(s => Metadata.Get(type, s)).Cast<IMetadata>().ToList();
        }

        /// <summary>
        /// Splits a line and replaces the unwanted characters.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        /// <remarks></remarks>
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