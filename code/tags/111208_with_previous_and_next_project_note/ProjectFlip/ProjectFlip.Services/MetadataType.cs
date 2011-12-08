#region

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    public class MetadataType : IMetadataType
    {
        private static readonly Dictionary<string, MetadataType> MetadataTypes = new Dictionary<string, MetadataType>();

        private MetadataType(string name)
        {
            Name = name;
        }

        #region IMetadataType Members

        public string Name { get; private set; }

        #endregion

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static MetadataType Get(string name)
        {
            if (!MetadataTypes.ContainsKey(name)) MetadataTypes[name] = new MetadataType(name);
            return MetadataTypes[name];
        }
    }
}