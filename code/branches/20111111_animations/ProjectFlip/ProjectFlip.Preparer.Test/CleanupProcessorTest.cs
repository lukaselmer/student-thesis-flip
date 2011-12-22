#region

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.Test.Mock;

#endregion

namespace ProjectFlip.Preparer.Test
{
    /// <summary>
    ///This is a test class for CleanupProcessorTest and is intended
    ///to contain all CleanupProcessorTest Unit Tests
    ///</summary>
    [TestClass]
    public class CleanupProcessorTest
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
        ///A test for Process
        ///</summary>
        [TestMethod]
        public void ProcessTest()
        {
            var target = new CleanupProcessor();
            var projectNote = new ProjectNoteMock();
            Assert.AreEqual(false, target.Process(projectNote));
        }
    }
}