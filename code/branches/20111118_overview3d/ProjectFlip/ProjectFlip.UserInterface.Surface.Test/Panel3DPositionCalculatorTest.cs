using ProjectFlip.UserInterface.Surface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;

namespace ProjectFlip.UserInterface.Surface.Test
{
    
    
    /// <summary>
    ///This is a test class for Panel3DPositionCalculatorTest and is intended
    ///to contain all Panel3DPositionCalculatorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Panel3DPositionCalculatorTest
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
        ///A test for Calculate
        ///</summary>
        [TestMethod]
        public void CalculateTest1()
        {
            var target = new Panel3DPositionCalculator(new Size(2, 3), new Size(20, 20), 0, new Panel3DLinearScaleFunction(1, 1), 10);

            var actual = target.Calculate(0, 0);
            Assert.AreEqual(0, actual.X);
            Assert.AreEqual(0, actual.Y);

            actual = target.Calculate(1, 1);
            Assert.AreEqual(2, actual.X);
            Assert.AreEqual(3, actual.Y);

            actual = target.Calculate(2, 2);
            Assert.AreEqual(4, actual.X);
            Assert.AreEqual(6, actual.Y);

            actual = target.Calculate(3, 3);
            Assert.AreEqual(6, actual.X);
            Assert.AreEqual(9, actual.Y);

            actual = target.Calculate(0, 1);
            Assert.AreEqual(2, actual.X);
            Assert.AreEqual(0, actual.Y);

            actual = target.Calculate(0, 2);
            Assert.AreEqual(4, actual.X);
            Assert.AreEqual(0, actual.Y);

            actual = target.Calculate(0, 3);
            Assert.AreEqual(6, actual.X);
            Assert.AreEqual(0, actual.Y);

            actual = target.Calculate(1, 0);
            Assert.AreEqual(0, actual.X);
            Assert.AreEqual(3, actual.Y);

            actual = target.Calculate(2, 0);
            Assert.AreEqual(0, actual.X);
            Assert.AreEqual(6, actual.Y);

            actual = target.Calculate(3, 0);
            Assert.AreEqual(0, actual.X);
            Assert.AreEqual(9, actual.Y);
        }


        /// <summary>
        ///A test for Calculate
        ///</summary>
        [TestMethod]
        public void CalculateTest2()
        {
            var target = new Panel3DPositionCalculator(new Size(400, 600), new Size(100000, 100000), 0, new Panel3DLinearScaleFunction(0.5, 1), 1000);
            target.LeftAligned = true;

            var actual = target.Calculate(0, 0);
            Assert.AreEqual(0, actual.X);
            Assert.AreEqual(0, actual.Y);

            actual = target.Calculate(1, 1);
            Assert.AreEqual(400, actual.X);
            Assert.AreEqual(600, actual.Y);

            actual = target.Calculate(2, 2);
            Assert.AreEqual(800, actual.X);
            Assert.AreEqual(900, actual.Y);

            actual = target.Calculate(3, 3);
            Assert.AreEqual(1200, actual.X);
            Assert.AreEqual(1050, actual.Y);

            actual = target.Calculate(4, 4);
            Assert.AreEqual(1600, actual.X);
            Assert.AreEqual(1125, actual.Y);
        }
    }
}
