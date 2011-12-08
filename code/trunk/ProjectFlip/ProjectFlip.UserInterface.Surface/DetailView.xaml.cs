#region

using System.Windows;

#endregion


namespace ProjectFlip.UserInterface.Surface
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