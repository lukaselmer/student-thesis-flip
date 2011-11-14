using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    public class MetadataType : IMetadataType
    {
        private static readonly Dictionary<string, MetadataType> MetadataTypes = new Dictionary<string, MetadataType>();

        public string Name { get; private set; }

        private MetadataType(string name)
        {
            Name = name;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static MetadataType Get(string name)
        {
            if (!MetadataTypes.ContainsKey(name)) MetadataTypes[name] = new MetadataType(name);
            return MetadataTypes[name];
        }
    }
}
