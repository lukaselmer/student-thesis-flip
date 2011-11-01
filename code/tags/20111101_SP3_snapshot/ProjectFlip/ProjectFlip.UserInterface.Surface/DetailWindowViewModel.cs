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
        public Command NavigateToLeftCommand { get; private set; }
        public Command NavigateToRightCommand { get; private set; }
        public Command CloseWindowCommand { get; private set; }
        private LinkedList<IProjectNote> ProjectNotes { get; set; }
        private LinkedListNode<IProjectNote> _currentNode;
        public event Action CloseWindow;

        public DetailWindowViewModel(IProjectNotesService projectNotesService, IProjectNote projectNote)
        {
            ProjectNotes = new LinkedList<IProjectNote>(projectNotesService.ProjectNotes);
            _currentNode = ProjectNotes.Find(projectNote);
            Document = _currentNode.Value.Document;
            NavigateToLeftCommand = new Command(NavigateToLeft);
            NavigateToRightCommand = new Command(NavigateToRight);
            CloseWindowCommand = new Command((sender) => { if (CloseWindow != null) CloseWindow(); });
        }

        private void NavigateToLeft(object parameter)
        {
            _currentNode = _currentNode.Previous ?? ProjectNotes.Last;
            UpdateDocument();
        }

        private void NavigateToRight(object parameter)
        {
            _currentNode = _currentNode.Next ?? ProjectNotes.First;
            UpdateDocument();
        }

        private void UpdateDocument()
        {
            Document = _currentNode.Value.Document;
            Notify("Document");
        }
    }

}
