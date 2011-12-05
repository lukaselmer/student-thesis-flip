#region

using System;
using System.Windows.Controls;

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

        private void CurrentDocViewerContainer_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            docViewer.FitToWidth();
            PreviousDocViewer.Width = docViewer.Width;
            NextDocViewer.Width = docViewer.Width;
        }
    }

}