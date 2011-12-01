using ProjectFlip.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Test.Mock;

namespace ProjectFlip.Services.Test
{
    
    
    /// <summary>
    ///This is a test class for MetadataComparerTest and is intended
    ///to contain all MetadataComparerTest Unit Tests
    ///</summary>
    [TestClass]
    public class MetadataComparerTest
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
        ///A test for Compare
        ///</summary>
        [TestMethod]
        public void CompareTest()
        {
            var target = new MetadataComparer();
            
            Assert.AreEqual(0, target.Compare(Metadata.Get(new MetadataTypeMock(), "aaa"),
                Metadata.Get(new MetadataTypeMock(), "aaa")));
            Assert.AreEqual(-1, target.Compare(Metadata.Get(new MetadataTypeMock(), "aaa"),
                Metadata.Get(new MetadataTypeMock(), "bbb")));
            Assert.AreEqual(1, target.Compare(Metadata.Get(new MetadataTypeMock(), "bbb"),
                Metadata.Get(new MetadataTypeMock(), "aaa")));

            Assert.AreEqual(0, target.Compare(Metadata.Get(new MetadataTypeMock(), "AAA"),
                Metadata.Get(new MetadataTypeMock(), "aaa")));
            Assert.AreEqual(-1, target.Compare(Metadata.Get(new MetadataTypeMock(), "AAA"),
                Metadata.Get(new MetadataTypeMock(), "bbb")));
            Assert.AreEqual(1, target.Compare(Metadata.Get(new MetadataTypeMock(), "BBB"),
                Metadata.Get(new MetadataTypeMock(), "aaa")));
        }
    }
}
