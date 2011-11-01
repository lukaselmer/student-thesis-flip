using System;
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
        public OverviewWindow(OverviewWindowViewModel surfaceWindowViewModel)
        {
            InitializeComponent();
            DataContext = surfaceWindowViewModel;
        }
    }
}