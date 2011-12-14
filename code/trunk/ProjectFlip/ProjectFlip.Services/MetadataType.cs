#region

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    /// <summary>
    /// The MetadataType object is a category for the metadata.
    /// This could be something like "Technologies", "Customers" or "Sectors"
    /// </summary>
    /// <remarks></remarks>
    public class MetadataType : IMetadataType
    {
        #region Declarations

        /// <summary>
        /// The dictionary of all created instances. Flyweight pattern.
        /// </summary>
        private static readonly Dictionary<string, MetadataType> MetadataTypes = new Dictionary<string, MetadataType>();

        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="MetadataType"/> class from being created.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <remarks></remarks>
        private MetadataType(string name)
        {
            Name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name
        /// </summary>
        /// <remarks></remarks>
        public string Name { get; private set; }

        #endregion

        #region Other

        /// <summary>
        /// Gets the metadata type with the specified type and description. To improve performance, a list
        /// of the created instances is maintained, and if a request with the same requirements is
        /// submitted, the reference of the already created metadata type is returned.
        /// 
        /// See Flyweight pattern.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static MetadataType Get(string name)
        {
            if (!MetadataTypes.ContainsKey(name)) MetadataTypes[name] = new MetadataType(name);
            return MetadataTypes[name];
        }

        #endregion
    }
}