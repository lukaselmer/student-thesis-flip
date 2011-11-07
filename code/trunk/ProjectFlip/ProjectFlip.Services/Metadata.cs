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
            switch (Type)
            {
                case MetadataType.Sector:
                    return projectNote.Sector.Description == Description;
                case MetadataType.Technologies:
                    return projectNote.Technologies.Any(d => d.Description == Description);
                case MetadataType.Services:
                    return projectNote.Services.Any(d => d.Description == Description);
                case MetadataType.Tools:
                    return projectNote.Tools.Any(d => d.Description == Description);
                case MetadataType.Customer:
                    return projectNote.Customer.Description == Description;
                case MetadataType.Focus:
                    return projectNote.Focus.Any(d => d.Description == Description);
                case MetadataType.Applications:
                    return projectNote.Applications.Any(d => d.Description == Description);
                default:
                    throw new ArgumentOutOfRangeException();
            }
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