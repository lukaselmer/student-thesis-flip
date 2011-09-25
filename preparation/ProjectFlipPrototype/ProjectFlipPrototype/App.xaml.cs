using System.Windows;
using Microsoft.Practices.Unity;
using ProjectFlip.Services;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.UserInterface;

namespace ProjectFlip
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = new UnityContainer();
            
            ConfigueContainer(container);

            var window = container.Resolve<MainWindow>();
            window.Show();
        }

        private static void ConfigueContainer(UnityContainer container)
        {
            container.RegisterType<ILogService, ConsoleLogService>();
            container.RegisterType<IProjectNotesService, ProjectNotesService>();
        }
    }
}
