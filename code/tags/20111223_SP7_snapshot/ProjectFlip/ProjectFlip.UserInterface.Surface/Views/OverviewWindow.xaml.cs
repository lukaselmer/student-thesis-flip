#region

using ProjectFlip.UserInterface.Surface.ViewModels;

#endregion

namespace ProjectFlip.UserInterface.Surface.Views
{
    /// <summary>
    /// Interaction logic for OverviewWindow.xaml
    /// </summary>
    public partial class OverviewWindow
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public OverviewWindow(OverviewWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}