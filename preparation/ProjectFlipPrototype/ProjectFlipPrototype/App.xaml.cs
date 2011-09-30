using System.Globalization;
using System.Windows;
using Microsoft.Practices.Unity;
using ProjectFlip.Services;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.UserInterface;
using ProjectFlip.UserInterface.Surface;

namespace ProjectFlip
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            InitLanguage();
            var container = new UnityContainer();
            
            ConfigueContainer(container);

            var window = container.Resolve<SurfaceWindow1>();
            window.Show();
        }

        private void InitLanguage()
        {
            if (false)
            {
                CultureAndRegionInfoBuilder cib = new CultureAndRegionInfoBuilder("und", CultureAndRegionModifiers.None);
                CultureInfo ci = new CultureInfo("en-US");
                cib.LoadDataFromCultureInfo(ci);
                RegionInfo ri = new RegionInfo("US");
                cib.LoadDataFromRegionInfo(ri);
                cib.Register();
            }
        }

        private static void ConfigueContainer(UnityContainer container)
        {
            container.RegisterType<ILogService, ConsoleLogService>();
            container.RegisterType<IProjectNotesService, ProjectNotesService>();
        }
    }
}
