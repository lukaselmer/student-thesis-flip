#region

using System;
using System.Collections.Generic;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    /// <summary>
    /// Compares two metadatas, ignores case.
    /// </summary>
    /// <remarks></remarks>
    internal class MetadataComparer : IComparer<IMetadata>
    {
        #region Other

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>Value Condition Less than zerox is less than y.Zerox equals y.Greater than zerox is greater than y.</returns>
        /// <remarks></remarks>
        public int Compare(IMetadata x, IMetadata y)
        {
            return StringComparer.OrdinalIgnoreCase.Compare(x.Description, y.Description);
        }

        #endregion
    }
}