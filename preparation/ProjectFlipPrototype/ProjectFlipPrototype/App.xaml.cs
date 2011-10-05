using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using Microsoft.Practices.Unity;
using ProjectFlip.Services;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Services.Loader;
using ProjectFlip.UserInterface.Surface;

namespace ProjectFlip
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            RegisterLanguage();

            var container = new UnityContainer();
            ConfigureContainer(container);

            var window = container.Resolve<SurfaceWindow1>();
            window.Show();
        }

        private void RegisterLanguage()
        {
            var cultures = new List<CultureInfo>(CultureInfo.GetCultures(CultureTypes.AllCultures));
            if (cultures.Exists(c => c.Name == "und")) return;

            DoRegistration();
        }

        private void DoRegistration()
        {
            var cib = new CultureAndRegionInfoBuilder("und", CultureAndRegionModifiers.None);
            var ci = new CultureInfo("en-US");
            cib.LoadDataFromCultureInfo(ci);
            var ri = new RegionInfo("US");
            cib.LoadDataFromRegionInfo(ri);
            cib.Register();
        }

        private static void ConfigureContainer(UnityContainer container)
        {
            container.RegisterType<ILogService, ConsoleLogService>();
            container.RegisterType<IProjectNotesService, ProjectNotesService>();
            container.RegisterType<IProjectNotesLoader, ProjectNotesLoader>();
        }
    }
}