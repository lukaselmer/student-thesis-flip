using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Surface;
using System.IO;
using System.Windows.Xps.Packaging;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;

namespace SurfaceDarstellung
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            //Language = XmlLanguage.GetLanguage(System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag);  
            InitializeComponent();
            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
            Console.WriteLine(Language);
            //add buttons
            for (int i = 0; i < 5; i++)
            {
                SurfaceButton sb = new SurfaceButton();
                sb.Content = "test" + i +".xps";
                docContainer.Items.Add(sb);
                sb.AddHandler(SurfaceButton.ClickEvent, new RoutedEventHandler(OnButtonClick));
            }
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            SurfaceButton b = sender as SurfaceButton;
            DocumentViewer docViewer = new DocumentViewer();
            docViewer.Margin = new Thickness(20);
            docViewer.Language = Language;
            XpsDocument doc = new XpsDocument(@"D:\Flip Project 2.0\preparation\ProjectFlipXPSPrototype\SurfaceDarstellung\xpss\" + b.Content.ToString(), FileAccess.ReadWrite);
            docViewer.Document = doc.GetFixedDocumentSequence();
            docViewer.FitToWidth();
            //docViewer.AddHandler();
            doc.Close();
            docContainer.Items.Add(docViewer);
            docContainer.Items.Remove(b);
        }

        private void DocumentViewerSizeChanged(object sender, RoutedEventArgs e)
        {
            DocumentViewer docViewer = sender as DocumentViewer;
            docViewer.FitToWidth();
        }

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }

    }
}