using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Test.Mock;
using System.Linq;
using ProjectFlip.UserInterface.Surface;
using System;
using System.Windows.Data;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;

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
            Assert.AreEqual(5, target.ProjectNotes.Cast<IProjectNote>().Count());
        }


        /// <summary>
        ///A test for AddFilter and RemoveFilter
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void AddAndRemoveFilterTest()
        {
//            var projectNotesService = new ProjectNotesServiceMock(5);
//            var target = new OverviewWindowViewModel_Accessor(projectNotesService);
//            List<MetadataMock> filter = new List<MetadataMock>();
//            target.AddFilter(filter);
//            Assert.AreNotSame(target.ProjectNotes.Cast<IProjectNote>().Count(), 5);
        }

        /// <summary>
        ///A test for OnShowFilter
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void OnShowFilterTest()
        {
//            var projectNotesService = new ProjectNotesServiceMock(5);
//            var target = new OverviewWindowViewModel_Accessor(projectNotesService);
//            Assert.AreEqual(target.IsFilterViewVisible, false);
//            target.OnShowFilter(new object());
//            Assert.AreEqual(target.IsFilterViewVisible, true);
        }

        /// <summary>
        ///A test for FilterCallback
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void FilterCallbackTest()
        {
//            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
//            OverviewWindowViewModel_Accessor target = new OverviewWindowViewModel_Accessor(param0); // TODO: Initialize to an appropriate value
//            object projectNoteObj = null; // TODO: Initialize to an appropriate value
//            bool expected = false; // TODO: Initialize to an appropriate value
//            bool actual;
//            actual = target.FilterCallback(projectNoteObj);
//            Assert.AreEqual(expected, actual);
//            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MoxeToNext
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void MoveToNextTest()
        {
            IProjectNotesService projectNotesService = new ProjectNotesServiceMock(2);
            OverviewWindowViewModel_Accessor target = new OverviewWindowViewModel_Accessor(projectNotesService);
            target.MoveToNext();
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(1));
            target.MoveToNext();
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(0));
        }

        /// <summary>
        ///A test for MoveToPrevious
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void MoveToPreviousTest()
        {
            IProjectNotesService projectNotesService = new ProjectNotesServiceMock(2);
            OverviewWindowViewModel_Accessor target = new OverviewWindowViewModel_Accessor(projectNotesService);
            target.MoveToPrevious();
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(1));
            target.MoveToPrevious();
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(0));
        }
    }
}
