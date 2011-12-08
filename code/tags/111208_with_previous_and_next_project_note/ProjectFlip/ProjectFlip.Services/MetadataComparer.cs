#region

using System;
using System.Collections.Generic;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    internal class MetadataComparer : IComparer<IMetadata>
    {
        #region IComparer<IMetadata> Members

        public int Compare(IMetadata x, IMetadata y)
        {
            return StringComparer.OrdinalIgnoreCase.Compare(x.Description, y.Description);
        }

        #endregion
    }
}