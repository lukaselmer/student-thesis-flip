using ProjectFlip.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ProjectFlip.Services.Loader.Interfaces;
using ProjectFlip.Services.Interfaces;
using System.Collections.Generic;

namespace ProjectFlip.Services.Test
{


    /// <summary>
    ///This is a test class for ProjectNotesServiceTest and is intended
    ///to contain all ProjectNotesServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProjectNotesServiceTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
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
        ///A test for ProjectNotesService Constructor
        ///</summary>
        [TestMethod()]
        public void ProjectNotesServiceConstructorTest()
        {
            IProjectNotesLoader projectNotesLoader = new ProjectNotesLoaderMock();
            ProjectNote.FilepathFolder = @"..\..\..\Resources\Test";
            ProjectNotesService target = new ProjectNotesService(projectNotesLoader);
            Assert.AreEqual(1, target.ProjectNotes.Count);
        }
    }
}
