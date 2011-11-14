using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ProjectFlip.Services.Interfaces
{
    public class MetadataType
    {
        private static readonly Dictionary<string, MetadataType> MetadataTypes = new Dictionary<string, MetadataType>();

        public string Name { get; private set; }

        private MetadataType(string name)
        {
            Name = name;
        }

        public static MetadataType Get(string name)
        {
            if (!MetadataTypes.ContainsKey(name)) MetadataTypes[name] = new MetadataType(name);
            return MetadataTypes[name];
        }

        public static MetadataType Sector { get { return Get("Sector"); } }
        public static MetadataType Technologies { get { return Get("Technologies"); } }
        public static MetadataType Services { get { return Get("Services"); } }
        public static MetadataType Tools { get { return Get("Tools"); } }
        public static MetadataType Customer { get { return Get("Customer"); } }
        public static MetadataType Focus { get { return Get("Focus"); } }
        public static MetadataType Applications { get { return Get("Applications"); } }
        public static MetadataType Unknown { get { return Get("Unknown"); } }
    }
}
