#region

using System.Collections.Generic;
using System.Globalization;

#endregion

namespace ProjectFlip.Services
{
    public class CultureHelper : ICultureHelper
    {
        #region ICultureHelper Members

        public void RegisterLanguage()
        {
            var cultures = new List<CultureInfo>(CultureInfo.GetCultures(CultureTypes.AllCultures));
            if (cultures.Exists(c => c.Name == "und")) return;

            DoRegistration();
        }

        #endregion

        private void DoRegistration()
        {
            var cib = new CultureAndRegionInfoBuilder("und", CultureAndRegionModifiers.None);
            var ci = new CultureInfo("en-US");
            cib.LoadDataFromCultureInfo(ci);
            var ri = new RegionInfo("US");
            cib.LoadDataFromRegionInfo(ri);
            cib.Register();
        }
    }
}