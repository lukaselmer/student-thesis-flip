#region

using System.Windows;
using Microsoft.Practices.Unity;
using ProjectFlip.Services;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Services.Loader;
using ProjectFlip.Services.Loader.Interfaces;
using ProjectFlip.UserInterface.Surface;

#endregion

namespace ProjectFlip
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = new UnityContainer();
            ConfigureContainer(container);

            container.Resolve<ICultureHelper>().RegisterLanguage();

            var window = container.Resolve<OverviewWindow>();
            window.Show();
        }

        private static void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterType<IProjectNotesService, ProjectNotesService>();
            container.RegisterType<IProjectNotesLoader, ProjectNotesLoader>();
            container.RegisterType<ICultureHelper, CultureHelper>();
        }
    }
}