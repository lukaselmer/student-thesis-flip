#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    public class Metadata : IMetadata
    {
        private static readonly IDictionary<MetadataType, IDictionary<string, Metadata>> Metadatas =
            new Dictionary<MetadataType, IDictionary<string, Metadata>>(5);

        /// <summary>
        /// Initializes a new instance of the <see cref="Metadata"/> class.
        /// </summary>
        private Metadata(MetadataType type, string description)
        {
            Type = type;
            Description = description;
        }

        #region IMetadata Members

        public MetadataType Type { get; private set; }
        public string Description { get; private set; }

        public bool Match(IProjectNote projectNote)
        {
            if (!projectNote.Metadata.ContainsKey(Type)) return false;
            return projectNote.Metadata[Type].Any(m => m.Description == Description);
        }

        #endregion

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Metadata Get(MetadataType type, string description)
        {
            if (!Metadatas.ContainsKey(type)) Metadatas[type] = new Dictionary<string, Metadata>(100);
            if (!Metadatas[type].ContainsKey(description)) Metadatas[type][description] = new Metadata(type, description);
            return Metadatas[type][description];
        }
    }
}