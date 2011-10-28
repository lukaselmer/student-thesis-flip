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
            var line = new List<string>(19) { "1", "Title", "Text", "Sector", "Customer", "Focus", "Services", "Technology", "Application", "Tools", "15.10.2011", "text", "text", "Filename", "text", "text", "text", "text", "text" };
            ProjectNote target = new ProjectNote();
            target.Line = line;
            Assert.AreEqual(Convert.ToInt32(line[0]), target.Id);
            Assert.AreEqual(line[1], target.Title);
            Assert.AreEqual(line[2], target.Text);
            Assert.AreEqual(line[3], target.Sector);
            Assert.AreEqual(line[4], target.Customer);
            Assert.AreEqual(line[5], target.Focus[0]);
            Assert.AreEqual(line[6], target.Services[0]);
            Assert.AreEqual(line[7], target.Technologies[0]);
            Assert.AreEqual(line[8], target.Applications[0]);
            Assert.AreEqual(line[9], target.Tools[0]);
            Assert.AreEqual(Convert.ToDateTime(line[10]), target.Published);
            Assert.AreEqual(line[13], target.Filename);
            Assert.AreEqual((ProjectNote.FilepathFolder + @"\Pdf\" + line[13]), target.FilepathPdf);
            Assert.AreEqual((ProjectNote.FilepathFolder + @"\Xps\" + line[13]), target.FilepathXps);
            Assert.AreEqual((ProjectNote.FilepathFolder + @"\Images\" + line[13]), target.FilepathImage);
        }
    }
}
