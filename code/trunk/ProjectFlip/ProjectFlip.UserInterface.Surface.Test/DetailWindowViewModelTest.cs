using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.Services;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.UserInterface.Surface.Test
{
    [TestClass]
    public class DetailWindowViewModelTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }


        [TestMethod]
        public void DetailWindowViewModelConstructorTest()
        {
           //_currentNOde
            IProjectNotesService service = new ProjectNotesServiceMock(4);
            LinkedListNode<IProjectNote> currentNode = new LinkedListNode<IProjectNote>(service.ProjectNotes[2]);
            IDocumentPaginatorSource doc = currentNode.Value.Document;
            DetailWindowViewModel target = new DetailWindowViewModel(service, service.ProjectNotes[2]);


           
            //Document
        }
    }
}
