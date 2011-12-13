#region

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    public class MetadataType : IMetadataType
    {
        #region Declarations

        private static readonly Dictionary<string, MetadataType> MetadataTypes = new Dictionary<string, MetadataType>();

        #endregion

        #region Constructor

        private MetadataType(string name)
        {
            Name = name;
        }

        #endregion

        #region Properties

        public string Name { get; private set; }

        #endregion

        #region Other

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static MetadataType Get(string name)
        {
            if (!MetadataTypes.ContainsKey(name)) MetadataTypes[name] = new MetadataType(name);
            return MetadataTypes[name];
        }

        #endregion
    }
}