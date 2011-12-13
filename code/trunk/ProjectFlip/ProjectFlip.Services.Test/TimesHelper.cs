#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace ProjectFlip.Services.Test
{
    public static class TimesHelper
    {
        public static IEnumerable<int> Times(this int source)
        {
            return Enumerable.Range(0, source);
        }
    }
}