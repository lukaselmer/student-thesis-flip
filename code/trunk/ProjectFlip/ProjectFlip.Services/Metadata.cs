#region

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    public class Metadata : IMetadata
    {
        private static readonly IDictionary<IMetadataType, IDictionary<string, Metadata>> Metadatas =
            new Dictionary<IMetadataType, IDictionary<string, Metadata>>(5);

        /// <summary>
        /// Initializes a new instance of the <see cref="Metadata"/> class.
        /// </summary>
        private Metadata(IMetadataType type, string description)
        {
            Type = type;
            Description = description;
        }

        #region IMetadata Members

        public IMetadataType Type { get; private set; }
        public string Description { get; private set; }

        public bool Match(IProjectNote projectNote)
        {
            return projectNote.Metadata.ContainsKey(Type) && projectNote.Metadata[Type].Contains(this);
        }

        #endregion

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Metadata Get(IMetadataType type, string description)
        {
            if (!Metadatas.ContainsKey(type)) Metadatas[type] = new Dictionary<string, Metadata>(100);
            if (!Metadatas[type].ContainsKey(description)) Metadatas[type][description] = new Metadata(type, description);
            return Metadatas[type][description];
        }
    }
}