using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.UserInterface.Surface.Test
{


    /// <summary>
    ///This is a test class for OverviewWindowViewModelTest and is intended
    ///to contain all OverviewWindowViewModelTest Unit Tests
    ///</summary>
    [TestClass]
    public class OverviewWindowViewModelTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

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
        ///A test for OverviewWindowViewModel Constructor & ProjectNotes Property
        ///</summary>
        [TestMethod]
        public void OverviewWindowViewModelProjectNotesTest()
        {
            var projectNotesService = new ProjectNotesServiceMock(5);
            var target = new OverviewWindowViewModel(projectNotesService);
            Assert.AreEqual(5, target.ProjectNotes.Count);
        }

        /// <summary>
        ///A test for Notify
        ///</summary>
        [TestMethod]
        [DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void NotifyTest()
        {
            IProjectNotesService projectNotesService = new ProjectNotesServiceMock(3);
            var target = new OverviewWindowViewModel_Accessor(projectNotesService);

            var success = false;
            target.add_PropertyChanged((sender, e) => success = true);
            Assert.IsFalse(success);
            target.Notify("Title");
            Assert.IsTrue(success);

            var counter = 0;
            // ReSharper disable AccessToModifiedClosure
            target.add_PropertyChanged((sender, e) => counter += 1);
            // ReSharper restore AccessToModifiedClosure
            target.add_PropertyChanged((sender, e) => counter += 1);
            Assert.AreEqual(counter, 0);
            target.Notify("Title");
            Assert.AreEqual(counter, 2);
            target.Notify("Title");
            Assert.AreEqual(counter, 4);
        }
    }
}
