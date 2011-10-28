using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.Services;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.UserInterface.Surface;

namespace ProjectFlip.UserInterface.Surface.Test
{
    [TestClass]
    public class DetailWindowViewModelTest
    {
        [TestMethod]
        public void DetailWindowViewModelConstructorTest()
        {
           //_currentNOde
            IProjectNotesService service = new ProjectNotesServiceMock(4);
            LinkedListNode<IProjectNote> currentNode = new LinkedListNode<IProjectNote>(service.ProjectNotes[2]);
            IDocumentPaginatorSource doc = currentNode.Value.Document;
            DetailWindowViewModel target = new DetailWindowViewModel(service, service.ProjectNotes[2]);
        }

        /// <summary>
        ///A test for NavigateToLeft
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void NavigateToLeftTest()
        {
            IProjectNotesService service = new ProjectNotesServiceMock(4);
            IProjectNote currentPN = (service.ProjectNotes[2]);
            IProjectNote previousPN = (service.ProjectNotes[1]);
            DetailWindowViewModel_Accessor target = new DetailWindowViewModel_Accessor(service, currentPN);
            object parameter = new object(); // TODO: Initialize to an appropriate value
            target.NavigateToLeft(parameter);
            Assert.AreEqual(previousPN.Document,currentPN.Document);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for NavigateToRight
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void NavigateToRightTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            DetailWindowViewModel_Accessor target = new DetailWindowViewModel_Accessor(param0); // TODO: Initialize to an appropriate value
            object parameter = null; // TODO: Initialize to an appropriate value
            target.NavigateToRight(parameter);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
