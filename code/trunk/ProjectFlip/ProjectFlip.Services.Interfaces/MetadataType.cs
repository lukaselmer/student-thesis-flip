﻿using System.Collections.Concurrent;
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
    }
}
