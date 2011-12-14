#region

using System.Collections.Generic;
using System.Globalization;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    /// <summary>
    /// Registers the language "und". This is used to read the XPS files.
    /// </summary>
    /// <remarks></remarks>
    public class CultureHelper : ICultureHelper
    {
        #region Other

        /// <summary>
        /// Registers the language "und" if it is not registered yet.
        /// Necessary because XPS documents use the language "und" which causes an exception when it's not registered.
        /// </summary>
        /// <remarks></remarks>
        public void RegisterLanguage()
        {
            var cultures = new List<CultureInfo>(CultureInfo.GetCultures(CultureTypes.AllCultures));
            if (cultures.Exists(c => c.Name == "und")) return;

            DoRegistration();
        }

        /// <summary>
        /// Does the registration.
        /// </summary>
        /// <remarks></remarks>
        private void DoRegistration()
        {
            var cib = new CultureAndRegionInfoBuilder("und", CultureAndRegionModifiers.None);
            var ci = new CultureInfo("en-US");
            cib.LoadDataFromCultureInfo(ci);
            var ri = new RegionInfo("US");
            cib.LoadDataFromRegionInfo(ri);
            cib.Register();
        }

        #endregion
    }
}