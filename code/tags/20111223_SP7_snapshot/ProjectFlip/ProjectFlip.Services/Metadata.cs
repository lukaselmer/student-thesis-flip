#region

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    /// <summary>
    /// The Metadata object is a addition to a project note.
    /// This could be something like "Java", "Credit Suisse" or "Energy Sector"
    /// </summary>
    /// <remarks></remarks>
    public class Metadata : IMetadata
    {
        #region Declarations

        /// <summary>
        /// The dictionary of all created instances. Flyweight pattern.
        /// </summary>
        private static readonly IDictionary<IMetadataType, IDictionary<string, Metadata>> Metadatas =
            new Dictionary<IMetadataType, IDictionary<string, Metadata>>();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Metadata"/> class.
        /// </summary>
        private Metadata(IMetadataType type, string description)
        {
            Type = type;
            Description = description;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <remarks></remarks>
        public IMetadataType Type { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <remarks></remarks>
        public string Description { get; private set; }

        #endregion

        #region Other

        /// <summary>
        /// Determindes if the metadata matches one of the metadatas of the specified project note.
        /// </summary>
        /// <param name="projectNote">The project note.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Match(IProjectNote projectNote)
        {
            return projectNote.Metadata.ContainsKey(Type) && projectNote.Metadata[Type].Contains(this);
        }

        /// <summary>
        /// Gets the metadata with the specified type and description. To improve performance, a list
        /// of the created instances is maintained, and if a request with the same requirements is
        /// submitted, the reference of the already created metadata is returned.
        /// 
        /// See Flyweight pattern.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Metadata Get(IMetadataType type, string description)
        {
            if (!Metadatas.ContainsKey(type)) Metadatas[type] = new Dictionary<string, Metadata>(100);
            if (!Metadatas[type].ContainsKey(description)) Metadatas[type][description] = new Metadata(type, description);
            return Metadatas[type][description];
        }

        #endregion
    }
}