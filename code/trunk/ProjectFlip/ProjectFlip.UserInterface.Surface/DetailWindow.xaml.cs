using System.Windows;
using Microsoft.Surface.Presentation.Controls;


namespace ProjectFlip.UserInterface.Surface
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : SurfaceWindow
    {
        public DetailWindow(DetailWindowViewModel detailWindowViewModel)
        {
            InitializeComponent();
            DataContext = detailWindowViewModel;
        }
     
       
    }
}
