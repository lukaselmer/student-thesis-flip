#region

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.Converter.Pdf;
using ProjectFlip.Converter.Test.Properties;

#endregion

namespace ProjectFlip.Converter.Test
{
    /// <summary>
    ///This is a test class for PdfConverterTest and is intended
    ///to contain all PdfConverterTest Unit Tests
    ///</summary>
    [TestClass]
    public class PdfConverterTest
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
        private static readonly bool RunPdfConverterTests = (bool) Settings.Default["RunPdfConverterTests"];

        /// <summary>
        ///A test for RequirementsNotOk -> Missing Adobe Reader
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.Converter.Pdf.exe")]
        public void TestRequirementsNotOkTestMissingAdobeReader()
        {
            var target = new PdfConverter_Accessor {AcrobatLocation = InvalidPath};
            Assert.AreEqual(false, target.RequirementsOk(PdfPath, TempXpsPath));
        }

        /// <summary>
        ///A test for RequirementsNotOk -> Invalid From Path
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.Converter.Pdf.exe")]
        public void TestRequirementsNotOkTestInvalidFromPath()
        {
            var target = new PdfConverter_Accessor();
            Assert.AreEqual(false, target.RequirementsOk(TempPdfPath, TempXpsPath));
        }

        /// <summary>
        ///A test for RequirementsNotOk -> Invalid To Path
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.Converter.Pdf.exe")]
        public void TestRequirementsNotOkTestInvalidToPath()
        {
            var target = new PdfConverter_Accessor();
            Assert.AreEqual(false, target.RequirementsOk(PdfPath, XpsPath));
        }

        /// <summary>
        ///A test for RequirementsOk
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.Converter.Pdf.exe")]
        public void TestRequirementsOk()
        {
            var target = new PdfConverter_Accessor();
            Assert.AreEqual(true, target.RequirementsOk(PdfPath, TempXpsPath));
        }

        /// <summary>
        ///A test for Convert
        ///</summary>
        [TestMethod]
        public void ConvertTest()
        {
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (!RunPdfConverterTests) return;
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            // ReSharper disable CSharpWarnings::CS0162
            // ReSharper disable HeuristicUnreachableCode
            PdfConverter.SecondsToWait = 4;
            var target = new PdfConverter();
            var expected = target.Convert(PdfPath, TempXpsPath);
            Assert.AreEqual(true, expected);
            Assert.IsTrue(File.Exists(TempXpsPath));

            var xpsFileInfo = new FileInfo(XpsPath);
            var tempXpsFileInfo = new FileInfo(XpsPath);
            Assert.IsTrue(xpsFileInfo.Length*1.1 > tempXpsFileInfo.Length);
            Assert.IsTrue(xpsFileInfo.Length*0.9 < tempXpsFileInfo.Length);
            // ReSharper restore HeuristicUnreachableCode
            // ReSharper restore CSharpWarnings::CS0162
        }

        /// <summary>
        ///A test for Convert with bad requirements
        ///</summary>
        [TestMethod]
        public void ConvertTestWithBadRequirements()
        {
            var target = new PdfConverter();
            Assert.AreEqual(false, target.Convert("", ""));
        }
    }
}