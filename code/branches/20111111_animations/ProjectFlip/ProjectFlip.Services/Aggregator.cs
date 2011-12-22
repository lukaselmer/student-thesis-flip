using System;
using System.Collections.Generic;
using System.Linq;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    internal static class Aggregator
    {
        static Dictionary<string, IMetadata> _mapping = new Dictionary<string, IMetadata>();

        public static void LoadMapping()
        {
            _mapping = new Dictionary<string, IMetadata>();
        }

        public static IMetadata AggregateMetadata(IMetadata metadata)
        {
            return _mapping.ContainsKey(metadata.Description) ? _mapping[metadata.Description] : metadata;
        }
    }
}