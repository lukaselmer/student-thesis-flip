#region

using System;
using System.Collections.Generic;
using System.Windows.Documents;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.UserInterface.Surface
{
    public class DetailWindowViewModel : ViewModelBase
    {
        private LinkedListNode<IProjectNote> _currentNode;

        public DetailWindowViewModel(IProjectNotesService projectNotesService, IProjectNote projectNote)
        {
            ProjectNotes = new LinkedList<IProjectNote>(projectNotesService.ProjectNotes);
            _currentNode = ProjectNotes.Find(projectNote);
            Document = _currentNode.Value.Document;
            NavigateToLeftCommand = new Command(NavigateToLeft);
            NavigateToRightCommand = new Command(NavigateToRight);
            CloseWindowCommand = new Command(sender => { if (CloseWindow != null) CloseWindow(); });
        }

        // ReSharper disable MemberCanBePrivate.Global
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public IDocumentPaginatorSource Document { get; private set; }
        public Command NavigateToLeftCommand { get; private set; }
        public Command NavigateToRightCommand { get; private set; }
        public Command CloseWindowCommand { get; private set; }
        private LinkedList<IProjectNote> ProjectNotes { get; set; }
        public event Action CloseWindow;
        // ReSharper restore UnusedAutoPropertyAccessor.Global
        // ReSharper restore MemberCanBePrivate.Global

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