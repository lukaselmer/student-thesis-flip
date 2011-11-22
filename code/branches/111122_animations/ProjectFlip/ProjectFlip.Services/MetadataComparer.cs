using System;
using System.Collections.Generic;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    internal class MetadataComparer : IComparer<IMetadata> {
        public int Compare(IMetadata x, IMetadata y)
        {
            return StringComparer.OrdinalIgnoreCase.Compare(x.Description, y.Description);
        }
    }
}