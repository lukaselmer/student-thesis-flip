using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Microsoft.Surface.Presentation.Controls;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.UserInterface.Surface
{
    public class SurfaceWindowViewModel : INotifyPropertyChanged
    {

        private readonly FrameworkElementFactory _projectNoteButtonTemplate;
        private readonly FrameworkElementFactory _projectNoteTemplate ;
        private DataTemplate _dataTemplate;

        public SurfaceWindowViewModel(IProjectNotesService projectNotesService)
        {
            ProjectNotes = projectNotesService.ProjectNotes;
            _projectNoteButtonTemplate = CreateProjectNoteButtonTemplate();
            _projectNoteTemplate = CreateProjectNoteTemplate();
            ProjectNoteTemplate = new DataTemplate();
            ProjectNoteTemplate.VisualTree = _projectNoteButtonTemplate;
        }

        public Command OpenProjectNoteCommand { get; private set; }

        private void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
         }

        public List<IProjectNote> ProjectNotes { get; private set; }

        public DataTemplate ProjectNoteTemplate
        {
            get { return _dataTemplate; }
            set
            {
                _dataTemplate = value;
                Notify("ProjectNoteTemplate");
            }
        }


        private void ChangeTemplate(object parameter)
        {
            var temp = new DataTemplate();
            temp.VisualTree = _projectNoteTemplate;
            ProjectNoteTemplate = temp;
        }

        private FrameworkElementFactory CreateProjectNoteButtonTemplate()
        {
            var surfacebutton = new FrameworkElementFactory(typeof (SurfaceButton));

            var stackPanel = new FrameworkElementFactory(typeof (StackPanel));

            var image = new FrameworkElementFactory(typeof(Image));
            image.SetBinding(Image.SourceProperty, new Binding("Image"));
            image.SetValue(FrameworkElement.WidthProperty, 100.0);
            image.SetValue(FrameworkElement.HeightProperty, 100.0);
            var textBlock = new FrameworkElementFactory(typeof (TextBlock));
            textBlock.SetBinding(TextBlock.TextProperty, new Binding("Title"));

            stackPanel.AppendChild(image);
            stackPanel.AppendChild(textBlock);

            surfacebutton.AppendChild(stackPanel);
            surfacebutton.SetValue(ButtonBase.CommandProperty, new Command(ChangeTemplate));
            return surfacebutton;
        }

        private FrameworkElementFactory CreateProjectNoteTemplate()
        {
            var documentViewer = new FrameworkElementFactory(typeof (DocumentViewer));
            documentViewer.SetBinding(DocumentViewerBase.DocumentProperty, new Binding("Document"));
            return documentViewer;
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
