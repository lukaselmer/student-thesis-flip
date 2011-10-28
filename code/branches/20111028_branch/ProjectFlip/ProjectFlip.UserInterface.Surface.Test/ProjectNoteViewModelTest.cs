using System.Windows;
using ProjectFlip.UserInterface.Surface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ProjectFlip.Services.Interfaces;

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
        ///A test for OpenNewWindow
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void OpenNewWindowTest()
        {
            var service = new ProjectNotesServiceMock(5);
            var target = new ProjectNoteViewModel_Accessor(service, service.ProjectNotes[0]);
            string test = "test";
            target.OpenNewWindow(test);
            var window = Application.Current.MainWindow;
            Assert.IsNotNull(window);
        }
    }
}
