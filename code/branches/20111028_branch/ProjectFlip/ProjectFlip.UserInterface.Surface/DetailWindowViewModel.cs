using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Xps.Packaging;
using ProjectFlip.Services.Interfaces;


namespace ProjectFlip.UserInterface.Surface
{
    public class DetailWindowViewModel : ViewModelBase
    {
        public IDocumentPaginatorSource Document { get; private set; }
        public UserInterface.Command NavigateToLeftCommand { get; private set; }
        public UserInterface.Command NavigateToRightCommand { get; private set; }
        public UserInterface.Command CloseWindowCommand { get; private set; }
        private LinkedList<IProjectNote> ProjectNotes { get; set; }
        private LinkedListNode<IProjectNote> _currentNode;

        public DetailWindowViewModel(IProjectNotesService projectNotesService, IProjectNote projectNote)
        {
            ProjectNotes = new LinkedList<IProjectNote>(projectNotesService.ProjectNotes);
            _currentNode = ProjectNotes.Find(projectNote);
            Document = _currentNode.Value.Document;
            NavigateToLeftCommand = new UserInterface.Command(NavigateToLeft);
            NavigateToRightCommand = new UserInterface.Command(NavigateToRight);
            CloseWindowCommand = new UserInterface.Command(CloseWindow);
        }

        private void NavigateToLeft(object parameter)
        {
            _currentNode = _currentNode.Previous ?? ProjectNotes.Last;
            SetDocument(_currentNode.Value);
        }

        private void NavigateToRight(object parameter)
        {
            _currentNode = _currentNode.Next ?? ProjectNotes.First;
            SetDocument(_currentNode.Value);
        } 
        
        private void CloseWindow(object sender)
        {
            Application.Current.MainWindow.Close();
        }

        private void SetDocument(IProjectNote doc)
        {
            Document = doc.Document;
            Notify("Document");
        }
    }

}
