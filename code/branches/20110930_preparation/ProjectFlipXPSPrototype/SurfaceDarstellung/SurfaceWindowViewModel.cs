using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Surface;
using System.Windows.Controls;
using Microsoft.Surface.Presentation.Controls;
using System.IO;
using System.Windows.Xps.Packaging;

namespace SurfaceDarstellung
{
    class SurfaceWindowViewModel
    {

        private ScatterView _view;
        public Command OpenXPSCommand { get; private set; }

        public SurfaceWindowViewModel(ScatterView view)
        {
            _view = view;
            OpenXPSCommand = new Command(OpenXPS);
        }

        private void OpenXPS(object parameter)
        {
            DocumentViewer docViewer = new DocumentViewer();
            XpsDocument doc = new XpsDocument(@"D:\Flip Project 2.0\preparation\ProjectFlipXPSPrototype\SurfaceDarstellung\xpss\test.xps", FileAccess.Read);
            docViewer.Document = doc.GetFixedDocumentSequence();
            docViewer.FitToWidth();
            doc.Close();
            _view.Items.Add(docViewer);
        }

    }
}
