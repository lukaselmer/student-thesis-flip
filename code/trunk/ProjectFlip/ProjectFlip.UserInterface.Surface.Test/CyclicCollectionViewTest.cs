#region

using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace ProjectFlip.UserInterface.Surface.Test
{
    /// <summary>
    ///This is a test class for CyclicCollectionViewTest and is intended
    ///to contain all CyclicCollectionViewTest Unit Tests
    ///</summary>
    [TestClass]
    public class CyclicCollectionViewTest
    {
        readonly IList _referenceList = new[] { "a", "b", "c", "d", "e" };

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
        ///A test for MoveCurrentToNext
        ///</summary>
        [TestMethod]
        public void MoveCurrentToNextTest()
        {
            var target = new CyclicCollectionView(_referenceList);
            Assert.AreEqual(_referenceList[0], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[1], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[2], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[3], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[4], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToNext());

            Assert.AreEqual(_referenceList[0], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[1], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[2], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[3], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[4], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToNext());
        }
        
        /// <summary>
        ///A test for MoveCurrentToPrevious
        ///</summary>
        [TestMethod]
        public void MoveCurrentToPreviousTest()
        {
            var target = new CyclicCollectionView(_referenceList);
            Assert.AreEqual(_referenceList[0], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToPrevious());
            Assert.AreEqual(_referenceList[4], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToPrevious());
            Assert.AreEqual(_referenceList[3], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToPrevious());
            Assert.AreEqual(_referenceList[2], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToPrevious());
            Assert.AreEqual(_referenceList[1], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToPrevious());

            Assert.AreEqual(_referenceList[0], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToPrevious());
            Assert.AreEqual(_referenceList[4], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToPrevious());
            Assert.AreEqual(_referenceList[3], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToPrevious());
            Assert.AreEqual(_referenceList[2], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToPrevious());
            Assert.AreEqual(_referenceList[1], target.CurrentItem);
            Assert.IsTrue(target.MoveCurrentToPrevious());
        }

        /// <summary>
        ///A test for Next
        ///</summary>
        [TestMethod]
        public void NextTest()
        {
            var target = new CyclicCollectionView(_referenceList);
            Assert.AreEqual(_referenceList[0], target.CurrentItem);
            Assert.AreEqual(_referenceList[1], target.Next);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[2], target.Next);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[3], target.Next);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[4], target.Next);
            Assert.IsTrue(target.MoveCurrentToNext());

            Assert.AreEqual(_referenceList[0], target.Next);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[1], target.Next);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[2], target.Next);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[3], target.Next);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[4], target.Next);
            Assert.IsTrue(target.MoveCurrentToNext());
        }

        /// <summary>
        ///A test for Previous
        ///</summary>
        [TestMethod]
        public void PreviousTest()
        {
            var target = new CyclicCollectionView(_referenceList);
            Assert.AreEqual(_referenceList[0], target.CurrentItem);
            Assert.AreEqual(_referenceList[4], target.Previous);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[0], target.Previous);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[1], target.Previous);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[2], target.Previous);
            Assert.IsTrue(target.MoveCurrentToNext());

            Assert.AreEqual(_referenceList[3], target.Previous);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[4], target.Previous);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[0], target.Previous);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[1], target.Previous);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[2], target.Previous);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[3], target.Previous);
            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(_referenceList[4], target.Previous);
            Assert.IsTrue(target.MoveCurrentToNext());
        }
    }
}