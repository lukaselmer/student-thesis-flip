#region

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.Test.Mock;

#endregion

namespace ProjectFlip.Preparer.Test
{
    /// <summary>
    ///This is a test class for ImageExtractorProcessorTest and is intended
    ///to contain all ImageExtractorProcessorTest Unit Tests
    ///</summary>
    [TestClass]
    public class ImageExtractorProcessorTest
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
        [TestInitialize]
        public void MyTestInitialize()
        {
            Cleanup();
            File.ReadLines(ImagePath);
            Array.ForEach(new[] {PdfPath, XpsPath, ImagePath}, path => Assert.IsTrue(File.Exists(path)));
            Array.ForEach(new[] {TempPdfPath, TempXpsPath, TempImagePath, InvalidPath},
                path => Assert.IsFalse(File.Exists(path)));
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void MyTestCleanup()
        {
            Cleanup();
        }

        private static void Cleanup()
        {
            Array.ForEach(new[] {InvalidPath, TempPdfPath, TempXpsPath, TempImagePath},
                path => { if (File.Exists(path)) File.Delete(path); });
        }

        #endregion

        private const string PdfPath = @"..\..\..\Resources\Test\Pdf\test.pdf";
        private const string XpsPath = @"..\..\..\Resources\Test\Xps\test.xps";
        private const string ImagePath = @"..\..\..\Resources\Test\Images\test.bmp";

        private const string TempPdfPath = @"..\..\..\Resources\Test\Pdf\temp.pdf";
        private const string TempXpsPath = @"..\..\..\Resources\Test\Xps\temp.xps";
        private const string TempImagePath = @"..\..\..\Resources\Test\Images\temp.bmp";

        private const string InvalidPath = @"..\..\..\Resources\Test\thisFileShouldNotExist.bmp";

        /// <summary>
        ///A test for ExtractImage
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.Preparer.exe")]
        public void ExtractImageTest()
        {
            var pn = new ProjectNoteMock {FilepathXps = XpsPath, FilepathImage = TempImagePath};
            Assert.IsTrue(new ImageExtractorProcessor().Process(pn));
            Assert.IsTrue(File.Exists(TempImagePath));
            Assert.AreEqual(File.ReadAllText(ImagePath), File.ReadAllText(TempImagePath));
        }

        /// <summary>
        ///A test for ExtractImage with invalid path
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.Preparer.exe")]
        public void ExtractImageInvalidPathTest()
        {
            var pn = new ProjectNoteMock {FilepathXps = TempXpsPath, FilepathImage = TempImagePath};
            Assert.IsFalse(new ImageExtractorProcessor().Process(pn));
        }

        /// <summary>
        ///A test for ExtractImage with invalid image path
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.Preparer.exe")]
        public void ExtractImageInvalidImagePathTest()
        {
            var pn = new ProjectNoteMock {FilepathXps = XpsPath, FilepathImage = ImagePath};
            Assert.IsFalse(new ImageExtractorProcessor().Process(pn));
        }
    }
}