using ProjectFlip.Services;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.UserInterface.Surface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.Test.Mock;
using System;

namespace ProjectFlip.UserInterface.Surface.Test
{
    
    
    /// <summary>
    ///This is a test class for DetailWindowTest and is intended
    ///to contain all DetailWindowTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DetailWindowTest
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


        ///// <summary>
        /////A test for DetailWindow Constructor
        /////</summary>
        //[TestMethod()]
        //public void DetailWindowConstructorTest()
        //{
        //    Assert.Inconclusive("TODO: Fix this!"); // TODO: Fix this!
        //    IProjectNotesService service = new ProjectNotesServiceMock(4);
        //    IProjectNote projectNote = service.ProjectNotes[2];
        //    DetailWindowViewModel detailWindowViewModel = new DetailWindowViewModel(service,projectNote);
        //    DetailView target = new DetailView(detailWindowViewModel);
        //    Assert.AreSame(target.DataContext, detailWindowViewModel);
        //}
    }
}
