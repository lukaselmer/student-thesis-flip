using ProjectFlip.Preparer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ProjectFlip.Services;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Test.Mock;

namespace ProjectFlip.Preparer.Test
{
    
    
    /// <summary>
    ///This is a test class for CleanupProcessorTest and is intended
    ///to contain all CleanupProcessorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CleanupProcessorTest
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
        ///A test for Process
        ///</summary>
        [TestMethod()]
        public void ProcessTest()
        {
            CleanupProcessor target = new CleanupProcessor();
            IProjectNote projectNote = new ProjectNoteMock();
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Process(projectNote);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
