using System;
using System.Windows;
using Microsoft.Surface;
using Microsoft.Surface.Presentation.Controls;

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