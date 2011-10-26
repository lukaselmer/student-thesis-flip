using System.Windows;
using System.Windows.Xps.Packaging;


namespace ProjectFlip.UserInterface.Surface
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        public DetailWindow(DetailWindowViewModel detailWindowViewModel)
        {
            InitializeComponent();
            DataContext = detailWindowViewModel;
        }
     
       
    }
}
