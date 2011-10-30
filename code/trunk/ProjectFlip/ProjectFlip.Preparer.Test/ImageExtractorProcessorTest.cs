using System.IO;
using ProjectFlip.Preparer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ProjectFlip.Preparer.Test
{
    
    
    /// <summary>
    ///This is a test class for ImageExtractorProcessorTest and is intended
    ///to contain all ImageExtractorProcessorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ImageExtractorProcessorTest
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
        ///A test for ExtractImage
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.Preparer.exe")]
        public void ExtractImageTest()
        {
            const string xpsPath = @"..\..\..\Resources\Test\test.xps";
            const string tempImagePath = @"..\..\..\Resources\Test\temp.bmp";
            const string referenceImagePath = @"..\..\..\Resources\Test\test.bmp";

            Assert.IsTrue(File.Exists(referenceImagePath));

            if (File.Exists(tempImagePath)) File.Delete(tempImagePath);
            Assert.IsFalse(File.Exists(tempImagePath));

            ImageExtractorProcessor_Accessor.ExtractImage(xpsPath, tempImagePath);

            Assert.IsTrue(File.Exists(tempImagePath));
            Assert.AreEqual(File.ReadAllText(referenceImagePath), File.ReadAllText(tempImagePath));
            var x = File.ReadAllText(referenceImagePath);
            var y = File.ReadAllText(referenceImagePath);
            //File.Delete(tempImagePath);
        }
    }
}
