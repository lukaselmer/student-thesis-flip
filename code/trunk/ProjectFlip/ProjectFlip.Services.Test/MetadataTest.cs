using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services.Test
{
    
    
    /// <summary>
    ///This is a test class for MetadataTest and is intended
    ///to contain all MetadataTest Unit Tests
    ///</summary>
    [TestClass]
    public class MetadataTest
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
        ///A test for Get
        ///</summary>
        [TestMethod]
        public void GetTest()
        {
            Assert.AreSame(MetadataType.Get("Sector"), MetadataType.Get("Sector"));
            Assert.AreSame(Metadata.Get(MetadataType.Get("Sector"), "bla"), Metadata.Get(MetadataType.Get("Sector"), "bla"));
        }
    }
}
