#region

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.Test.Mock;

#endregion

namespace ProjectFlip.Services.Test
{
    /// <summary>
    ///This is a test class for ProjectNoteTest and is intended
    ///to contain all ProjectNoteTest Unit Tests
    ///</summary>
    [TestClass]
    public class ProjectNoteTest
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

        ///// <summary>
        /////A test for ConvertToList
        /////</summary>
        //[TestMethod, DeploymentItem("ProjectFlip.Services.dll")]

        //public void ConvertToListTest()
        //{
        //    const string line = "test";
        //    var actual = ProjectNote_Accessor.ConvertToList(line);
        //    Assert.AreEqual(1, actual.Count);
        //    Assert.AreEqual(line, actual[0]);
        //}

        ///// <summary>
        /////A test for Document
        /////</summary>
        //[TestMethod]
        //public void DocumentTest()
        //{
        //    var target = new ProjectNote_Accessor { FilepathXps = @"..\..\..\Resources\Test\Xps\test.xps" };
        //    var document = target.Document;
        //    Assert.IsNotNull(document);
        //}

        ///// <summary>
        /////A test for Url
        /////</summary>
        //[TestMethod]
        //public void UrlTest()
        //{
        //    var target = new ProjectNote_Accessor();
        //    const string filename = "filename";
        //    target.Filename = filename;
        //    Assert.AreEqual("http://www.zuehlke.com/uploads/tx_zepublications/" + filename, target.Url);
        //}

        ///// <summary>
        /////A test for Image
        /////</summary>
        //[TestMethod]
        //public void ImageTest()
        //{
        //    var image = GetBitmapImage(@"..\..\..\Resources\Test\Images\test.bmp");
        //    Assert.IsNotNull(image);
        //}

        ///// <summary>
        /////A test for Image with invalid file path
        /////</summary>
        //[TestMethod]
        //public void NoImageTest()
        //{
        //    var image = GetBitmapImage(@"..\");
        //    Assert.IsNull(image);
        //}

        //private static BitmapImage GetBitmapImage(string path)
        //{
        //    var target = new ProjectNote_Accessor { FilepathImage = path };
        //    var image = target.Image;
        //    return image;
        //}

        /// <summary>
        ///A test for Line
        ///</summary>
        [TestMethod]
        public void LineTest()
        {
            const string filename = "Filename";
            var line = new List<string>(19)
                       {
                           "1", "Title", "Text", "Sector", "Customer", "Focus", "Services", "Technology", "Application",
                           "Tools", "15.10.2011", "text", "text", filename + ".pdf", "text", "text", "text", "text",
                           "text"
                       };
            var aggregator = new AggregatorMock();
            var target = new ProjectNote { Aggregator = aggregator, Line = line };
            Assert.AreEqual(Convert.ToInt32(line[0]), target.Id);
            Assert.AreEqual(line[1], target.Title);
            Assert.AreEqual(line[2], target.Text);
            Assert.AreEqual(line[3], target.Metadata[MetadataType.Get("Sector")][0].Description);
            Assert.AreEqual(line[4], target.Metadata[MetadataType.Get("Customer")][0].Description);
            Assert.AreEqual(line[5], target.Metadata[MetadataType.Get("Focus")][0].Description);
            Assert.AreEqual(line[6], target.Metadata[MetadataType.Get("Services")][0].Description);
            Assert.AreEqual(line[7], target.Metadata[MetadataType.Get("Technologies")][0].Description);
            Assert.AreEqual(line[8], target.Metadata[MetadataType.Get("Applications")][0].Description);
            Assert.AreEqual(line[9], target.Metadata[MetadataType.Get("Tools")][0].Description);
            Assert.AreEqual(Convert.ToDateTime(line[10]), target.Published);
            Assert.AreEqual(line[13], target.Filename);
            Assert.AreEqual((ProjectNote.FilepathFolder + @"\Pdf\" + line[13]), target.FilepathPdf);
            Assert.AreEqual((ProjectNote.FilepathFolder + @"\Xps\" + filename + ".xps"), target.FilepathXps);
            Assert.AreEqual((ProjectNote.FilepathFolder + @"\Images\" + filename + ".bmp"), target.FilepathImage);

            Assert.AreSame(Metadata.Get(MetadataType.Get("Sector"), line[3]), target.Metadata[MetadataType.Get("Sector")][0]);
            Assert.AreSame(Metadata.Get(MetadataType.Get("Customer"), line[4]), target.Metadata[MetadataType.Get("Customer")][0]);
            Assert.AreSame(Metadata.Get(MetadataType.Get("Focus"), line[5]), target.Metadata[MetadataType.Get("Focus")][0]);
            Assert.AreSame(Metadata.Get(MetadataType.Get("Services"), line[6]), target.Metadata[MetadataType.Get("Services")][0]);
            Assert.AreSame(Metadata.Get(MetadataType.Get("Technologies"), line[7]), target.Metadata[MetadataType.Get("Technologies")][0]);
            Assert.AreSame(Metadata.Get(MetadataType.Get("Applications"), line[8]), target.Metadata[MetadataType.Get("Applications")][0]);
            Assert.AreSame(Metadata.Get(MetadataType.Get("Tools"), line[9]), target.Metadata[MetadataType.Get("Tools")][0]);
        }
    }
}