using System.Linq;
using ProjectFlip.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ProjectFlip.Services.Test
{


    /// <summary>
    ///This is a test class for SharepointStringDeserializerTest and is intended
    ///to contain all SharepointStringDeserializerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SharepointStringDeserializerTest
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
        ///A test for ToList
        ///</summary>
        [TestMethod]
        public void ToListTests()
        {
            TestString("", new[] { "" });
            TestString("test", new[] { "test" });
        }

        private static void TestString(string input, IEnumerable<string> expectedArray)
        {
            var actual = SharepointStringDeserializer.ToList(input);
            Assert.IsTrue(expectedArray.SequenceEqual(actual), "Lists are not equal");
        }
    }
}
