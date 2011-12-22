#region

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace ProjectFlip.Services.Test
{
    /// <summary>
    ///This is a test class for ProjectNotesServiceTest and is intended
    ///to contain all ProjectNotesServiceTest Unit Tests
    ///</summary>
    [TestClass]
    public class ProjectNotesServiceTest
    {
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
        ///A test for ProjectNotesService Constructor with a valid path
        ///</summary>
        [TestMethod]
        public void ProjectNotesServicewithPnTest()
        {
            ProjectNote.FilepathFolder = @"..\..\..\Resources\Test";
            createAndTestProjectNotesService(1);
        }

        /// <summary>
        ///A test for ProjectNotesService Constructor without a valid path
        ///</summary>
        [TestMethod]
        public void ProjectNotesServiceWithoutPnTest()
        {
            ProjectNote.FilepathFolder = @"..\..\..\Resources\";
            createAndTestProjectNotesService(0);
        }

        private void createAndTestProjectNotesService(int result)
        {
            var projectNotesLoader = new ProjectNotesLoaderMock();
            var target = new ProjectNotesService(projectNotesLoader);
            Assert.AreEqual(result, target.ProjectNotes.Count);
        }
    }
}