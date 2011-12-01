using ProjectFlip.Test.Mock;
using ProjectFlip.UserInterface.Surface.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ProjectFlip.UserInterface.Surface.Converter.Test
{
    
    
    /// <summary>
    ///This is a test class for ValueConverterBaseTest and is intended
    ///to contain all ValueConverterBaseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ValueConverterBaseTest
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
        ///A test for ProvideValue
        ///</summary>
        [TestMethod()]
        public void ProvideValueTest()
        {
            ValueConverterBase target = new ValueConverterBase();
            object returnvalue = target.ProvideValue(new ServiceProviderMock());
            Assert.AreEqual(returnvalue, target);
        }
    }
}
