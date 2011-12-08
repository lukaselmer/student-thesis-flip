using System.Collections.Generic;
using System.Linq;

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