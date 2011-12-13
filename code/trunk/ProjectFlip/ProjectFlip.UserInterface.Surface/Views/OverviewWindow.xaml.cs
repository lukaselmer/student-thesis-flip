namespace ProjectFlip.UserInterface.Surface
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
            //AddHandler(FrameworkElement.MouseDownEvent, (s,e) => {});
        }
    }
}