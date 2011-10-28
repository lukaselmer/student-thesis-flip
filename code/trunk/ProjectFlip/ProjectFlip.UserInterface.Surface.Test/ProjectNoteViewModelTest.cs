using ProjectFlip.UserInterface.Surface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ProjectFlip.Services.Interfaces;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace ProjectFlip.UserInterface.Surface.Test
{
    
    
    /// <summary>
    ///This is a test class for ProjectNoteViewModelTest and is intended
    ///to contain all ProjectNoteViewModelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProjectNoteViewModelTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ProjectNoteViewModel Constructor
        ///</summary>
        [TestMethod()]
        public void ProjectNoteViewModelConstructorTest()
        {
            IProjectNotesService service = new ProjectNotesServiceMock(3); // TODO: Initialize to an appropriate value
            IProjectNote projectNote = service.ProjectNotes[0]; // TODO: Initialize to an appropriate value
            ProjectNoteViewModel target = new ProjectNoteViewModel(service, projectNote);
            Assert.AreEqual(target.ProjectNoteService, service);
            Assert.AreEqual(target.ProjectNote, projectNote);
        }

        /// <summary>
        ///A test for OpenNewWindow
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void OpenNewWindowTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ProjectNoteViewModel_Accessor target = new ProjectNoteViewModel_Accessor(param0); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            target.OpenNewWindow(obj);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Image
        ///</summary>
        [TestMethod()]
        public void ImageTest()
        {
            IProjectNotesService service = null; // TODO: Initialize to an appropriate value
            IProjectNote projectNote = null; // TODO: Initialize to an appropriate value
            ProjectNoteViewModel target = new ProjectNoteViewModel(service, projectNote); // TODO: Initialize to an appropriate value
            BitmapImage actual;
            actual = target.Image;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        ///// <summary>
        /////A test for OpenWindowCommand
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        //public void OpenWindowCommandTest()
        //{
        //    PrivateObject param0 = null; // TODO: Initialize to an appropriate value
        //    ProjectNoteViewModel_Accessor target = new ProjectNoteViewModel_Accessor(param0); // TODO: Initialize to an appropriate value
        //    ICommand expected = null; // TODO: Initialize to an appropriate value
        //    ICommand actual;
        //    target.OpenWindowCommand = expected;
        //    actual = target.OpenWindowCommand;
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for ProjectNote
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        //public void ProjectNoteTest()
        //{
        //    PrivateObject param0 = null; // TODO: Initialize to an appropriate value
        //    ProjectNoteViewModel_Accessor target = new ProjectNoteViewModel_Accessor(param0); // TODO: Initialize to an appropriate value
        //    IProjectNote expected = null; // TODO: Initialize to an appropriate value
        //    IProjectNote actual;
        //    target.ProjectNote = expected;
        //    actual = target.ProjectNote;
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for ProjectNoteService
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        //public void ProjectNoteServiceTest()
        //{
        //    PrivateObject param0 = null; // TODO: Initialize to an appropriate value
        //    ProjectNoteViewModel_Accessor target = new ProjectNoteViewModel_Accessor(param0); // TODO: Initialize to an appropriate value
        //    IProjectNotesService expected = null; // TODO: Initialize to an appropriate value
        //    IProjectNotesService actual;
        //    target.ProjectNoteService = expected;
        //    actual = target.ProjectNoteService;
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for Title
        ///</summary>
        [TestMethod()]
        public void TitleTest()
        {
            IProjectNotesService service = null; // TODO: Initialize to an appropriate value
            IProjectNote projectNote = null; // TODO: Initialize to an appropriate value
            ProjectNoteViewModel target = new ProjectNoteViewModel(service, projectNote); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Title;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
