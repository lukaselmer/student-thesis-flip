#region

using Microsoft.Surface.Presentation.Controls;

#endregion

namespace ProjectFlip.UserInterface.Surface
{
    /// <summary>
    /// Interaction logic for OverviewWindow.xaml
    /// </summary>
    public partial class OverviewWindow : SurfaceWindow
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public OverviewWindow(OverviewWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            //AddHandler(FrameworkElement.MouseDownEvent, (s,e) => {});
        }
    }
}