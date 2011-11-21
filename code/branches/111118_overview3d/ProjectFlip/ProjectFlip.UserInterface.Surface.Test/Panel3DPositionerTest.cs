using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;

namespace ProjectFlip.UserInterface.Surface.Test
{


    /// <summary>
    ///This is a test class for Panel3DPositionerTest and is intended
    ///to contain all Panel3DPositionerTest Unit Tests
    ///</summary>
    [TestClass]
    public class Panel3DPositionerTest
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
        ///A test for CalculateCurrentPosition
        ///</summary>
        [TestMethod]
        public void CalculateCurrentPositionTest()
        {
            var expectedPositions = new[]
                {
                    new double[]{0,0},
                    new double[]{2,0},
                    new double[]{4,0},
                    new double[]{6,0},
                    new double[]{8,0},
                    
                    new double[]{0,3},
                    new double[]{2,3},
                    new double[]{4,3},
                    new double[]{6,3},
                    new double[]{8,3},

                    new double[]{0,6},
                    new double[]{2,6},
                    new double[]{4,6},
                    new double[]{6,6},
                    new double[]{8,6},

                    new double[]{0,9},
                    new double[]{2,9},
                    new double[]{4,9},
                    new double[]{6,9},
                    new double[]{8,9}
                };
            CalculateCurrentPositionTest(new Size(10, 100), new Size(2, 3), expectedPositions, new Panel3DLinearScaleFunction(1));
        }

        private static void CalculateCurrentPositionTest(Size windowSize, Size elSize, IEnumerable<double[]> expectedPositionsArray, IPanel3DScaleFunction scaleFunction)
        {
            var positioner = new Panel3DPositioner(windowSize, elSize, 0, scaleFunction);
            foreach (var expectedPosition in expectedPositionsArray.Select(pos => new Position3D(pos[0], pos[1], 1)))
            {
                var calculatedPosition = positioner.CalculateCurrentPosition();
                Assert.AreEqual(expectedPosition.X, calculatedPosition.X, 0.25);
                Assert.AreEqual(expectedPosition.Y, calculatedPosition.Y, 0.25);
                positioner.MoveToNext();
            }
        }

        /// <summary>
        ///A test for MoveToNext
        ///</summary>
        [TestMethod]
        public void MoveToNextTest()
        {
            Size windowSize = new Size(10, 10);
            Size elementSize = new Size(2, 2);
            Panel3DPositioner target = new Panel3DPositioner(windowSize, elementSize, 0);
            Assert.AreEqual(target.Index, 0);
            target.MoveToNext();
            Assert.AreEqual(target.Index, 1);
            target.MoveToNext();
            Assert.AreEqual(target.Index, 2);
            target.MoveToNext();
            Assert.AreEqual(target.Index, 3);
        }

        /// <summary>
        ///A test for CurrentRow
        ///</summary>
        [TestMethod]
        public void CurrentRowTest()
        {
            var target = new Panel3DPositioner(new Size(6, 100), new Size(2, 2), 0);
            Assert.AreEqual(0, target.CurrentRow);
            target.MoveToNext();
            Assert.AreEqual(0, target.CurrentRow);
            target.MoveToNext();
            Assert.AreEqual(0, target.CurrentRow);
            target.MoveToNext();
            Assert.AreEqual(1, target.CurrentRow);
            target.MoveToNext();
            Assert.AreEqual(1, target.CurrentRow);
            target.MoveToNext();
            Assert.AreEqual(1, target.CurrentRow);
            target.MoveToNext();
            Assert.AreEqual(2, target.CurrentRow);
            target.MoveToNext();
            Assert.AreEqual(2, target.CurrentRow);
            target.MoveToNext();
            Assert.AreEqual(2, target.CurrentRow);
            target.MoveToNext();
            Assert.AreEqual(3, target.CurrentRow);
            target.MoveToNext();
            Assert.AreEqual(3, target.CurrentRow);
            target.MoveToNext();
            Assert.AreEqual(3, target.CurrentRow);
            target.MoveToNext();
            Assert.AreEqual(4, target.CurrentRow);
            target.MoveToNext();
            Assert.AreEqual(4, target.CurrentRow);
            target.MoveToNext();
            Assert.AreEqual(4, target.CurrentRow);
            target.MoveToNext();
        }

        /// <summary>
        ///A test for CurrentCol
        ///</summary>
        [TestMethod]
        public void CurrentColTest()
        {
            var target = new Panel3DPositioner(new Size(6, 100), new Size(2, 2), 0);
            Assert.AreEqual(0, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(1, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(2, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(0, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(1, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(2, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(0, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(1, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(2, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(0, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(1, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(2, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(0, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(1, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(2, target.CurrentCol);
            target.MoveToNext();
        }


        /// <summary>
        ///A test for CurrentCol
        ///</summary>
        [TestMethod]
        public void CurrentColTest2()
        {
            var target = new Panel3DPositioner(new Size(10, 100), new Size(1, 1), 0);
            Assert.AreEqual(0, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(1, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(2, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(3, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(4, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(5, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(6, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(7, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(8, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(9, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(0, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(1, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(2, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(3, target.CurrentCol);
            target.MoveToNext();
            Assert.AreEqual(4, target.CurrentCol);
            target.MoveToNext();
        }


        /// <summary>
        ///A test for ElementsPerLine
        ///</summary>
        [TestMethod]
        public void ElementsPerLineTest()
        {
            var target = new Panel3DPositioner(new Size(6, 100), new Size(2, 2), 0);
            Assert.AreEqual(3, target.ElementsPerLine());
            target = new Panel3DPositioner(new Size(2, 100), new Size(2, 2), 0);
            Assert.AreEqual(1, target.ElementsPerLine());
            target = new Panel3DPositioner(new Size(3, 100), new Size(2, 2), 0);
            Assert.AreEqual(1, target.ElementsPerLine());
            target = new Panel3DPositioner(new Size(3.9, 100), new Size(2, 2), 0);
            Assert.AreEqual(1, target.ElementsPerLine());
            target = new Panel3DPositioner(new Size(4.1, 100), new Size(2, 2), 0);
            Assert.AreEqual(2, target.ElementsPerLine());
            target = new Panel3DPositioner(new Size(4, 100), new Size(2, 2), 0);
            Assert.AreEqual(2, target.ElementsPerLine());
            target = new Panel3DPositioner(new Size(0, 0), new Size(2, 2), 0);
            Assert.AreEqual(1, target.ElementsPerLine());
            target = new Panel3DPositioner(new Size(1, 1), new Size(2, 2), 0);
            Assert.AreEqual(1, target.ElementsPerLine());
            target = new Panel3DPositioner(new Size(3, 3), new Size(2, 2), 0);
            Assert.AreEqual(1, target.ElementsPerLine());
        }
    }
}
