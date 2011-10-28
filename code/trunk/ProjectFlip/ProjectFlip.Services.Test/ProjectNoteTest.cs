using ProjectFlip.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace ProjectFlip.Services.Test
{
    
    
    /// <summary>
    ///This is a test class for ProjectNoteTest and is intended
    ///to contain all ProjectNoteTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProjectNoteTest
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
        ///A test for ProjectNote Constructor
        ///</summary>
        [TestMethod()]
        public void ProjectNoteConstructorTest()
        {
            ProjectNote target = new ProjectNote();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for ConvertToList
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Services.dll")]
        public void ConvertToListTest()
        {
            string line = "test";
            IList<string> expected = new List<string>();
            expected.Add(line);
            IList<string> actual;
            actual = ProjectNote_Accessor.ConvertToList(line);
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[0], actual[i]);
            }
        }

        /// <summary>
        ///A test for Applications
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Services.dll")]
        public void ApplicationsTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor(); // TODO: Initialize to an appropriate value
            IList<string> expected = new List<string>();
            expected.Add("test application");
            IList<string> actual;
            target.Applications = expected;
            actual = target.Applications;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Customer
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Services.dll")]
        public void CustomerTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Customer = expected;
            actual = target.Customer;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Document
        ///</summary>
        [TestMethod()]
        public void DocumentTest()
        {
            ProjectNote target = new ProjectNote(); // TODO: Initialize to an appropriate value
            IDocumentPaginatorSource actual;
            actual = target.Document;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Filename
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Services.dll")]
        public void FilenameTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Filename = expected;
            actual = target.Filename;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FilepathImage
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Services.dll")]
        public void FilepathImageTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.FilepathImage = expected;
            actual = target.FilepathImage;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FilepathPdf
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Services.dll")]
        public void FilepathPdfTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.FilepathPdf = expected;
            actual = target.FilepathPdf;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FilepathXps
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Services.dll")]
        public void FilepathXpsTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.FilepathXps = expected;
            actual = target.FilepathXps;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Focus
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Services.dll")]
        public void FocusTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor(); // TODO: Initialize to an appropriate value
            IList<string> expected = null; // TODO: Initialize to an appropriate value
            IList<string> actual;
            target.Focus = expected;
            actual = target.Focus;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Id
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Services.dll")]
        public void IdTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.Id = expected;
            actual = target.Id;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Image
        ///</summary>
        [TestMethod()]
        public void ImageTest()
        {
            ProjectNote target = new ProjectNote(); // TODO: Initialize to an appropriate value
            BitmapImage actual;
            actual = target.Image;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Line
        ///</summary>
        [TestMethod()]
        public void LineTest()
        {
            ProjectNote target = new ProjectNote(); // TODO: Initialize to an appropriate value
            IList<string> expected = null; // TODO: Initialize to an appropriate value
            target.Line = expected;
            Assert.Inconclusive("Write-only properties cannot be verified.");
        }

        /// <summary>
        ///A test for Published
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Services.dll")]
        public void PublishedTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor(); // TODO: Initialize to an appropriate value
            DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime actual;
            target.Published = expected;
            actual = target.Published;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Sector
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Services.dll")]
        public void SectorTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Sector = expected;
            actual = target.Sector;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Services
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Services.dll")]
        public void ServicesTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor(); // TODO: Initialize to an appropriate value
            IList<string> expected = null; // TODO: Initialize to an appropriate value
            IList<string> actual;
            target.Services = expected;
            actual = target.Services;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Technologies
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Services.dll")]
        public void TechnologiesTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor(); // TODO: Initialize to an appropriate value
            IList<string> expected = null; // TODO: Initialize to an appropriate value
            IList<string> actual;
            target.Technologies = expected;
            actual = target.Technologies;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Text
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Services.dll")]
        public void TextTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Text = expected;
            actual = target.Text;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Title
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Services.dll")]
        public void TitleTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Title = expected;
            actual = target.Title;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Tools
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Services.dll")]
        public void ToolsTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor(); // TODO: Initialize to an appropriate value
            IList<string> expected = null; // TODO: Initialize to an appropriate value
            IList<string> actual;
            target.Tools = expected;
            actual = target.Tools;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Url
        ///</summary>
        [TestMethod()]
        public void UrlTest()
        {
            ProjectNote target = new ProjectNote(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Url;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ViewCount
        ///</summary>
        [TestMethod()]
        public void ViewCountTest()
        {
            ProjectNote target = new ProjectNote(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.ViewCount = expected;
            actual = target.ViewCount;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
