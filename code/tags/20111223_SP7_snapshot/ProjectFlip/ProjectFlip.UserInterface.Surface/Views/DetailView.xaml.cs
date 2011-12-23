#region

using System.Windows;

#endregion

namespace ProjectFlip.UserInterface.Surface.Views
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailView
    {
        public DetailView()
        {
            InitializeComponent();
        }

        private void CurrentDocViewerContainerSizeChanged(object sender, SizeChangedEventArgs e)
        {
            DocViewer.FitToWidth();
        }
    }
}