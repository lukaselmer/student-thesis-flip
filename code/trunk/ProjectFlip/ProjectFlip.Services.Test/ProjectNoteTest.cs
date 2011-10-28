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
        ///A test for Document
        ///</summary>
        [TestMethod()]
        public void DocumentTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor();
            target.FilepathXps = @"..\..\..\ProjectFlip.Services.Test\Resources\Xps\pn_test.xps";
            IDocumentPaginatorSource document;
            document = target.Document;
            Assert.IsNotNull(document);
        }
 
        /// <summary>
        ///A test for Image
        ///</summary>
        [TestMethod()]
        public void ImageTest()
        {
            ProjectNote_Accessor target = new ProjectNote_Accessor();
            target.FilepathImage = @"..\..\..\ProjectFlip.Services.Test\Resources\Image\pn_test.bmp";
            BitmapImage image;
            image = target.Image;
            Assert.IsNotNull(image);
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
    }
}
