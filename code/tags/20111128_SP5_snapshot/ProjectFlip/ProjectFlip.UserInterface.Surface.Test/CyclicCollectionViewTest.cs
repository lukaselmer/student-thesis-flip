﻿#region

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.Services;
using ProjectFlip.Services.Interfaces;

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
        private readonly IList<string> _referenceList = new[] {"a", "b", "c", "d", "e"};

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
            ICyclicCollectionView<string> target = new CyclicCollectionView<string>(_referenceList);
            Assert.AreEqual(_referenceList[0], target.CurrentItem);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[1], target.CurrentItem);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[2], target.CurrentItem);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[3], target.CurrentItem);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[4], target.CurrentItem);
            target.MoveCurrentToNext();

            Assert.AreEqual(_referenceList[0], target.CurrentItem);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[1], target.CurrentItem);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[2], target.CurrentItem);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[3], target.CurrentItem);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[4], target.CurrentItem);
            target.MoveCurrentToNext();
        }

        /// <summary>
        ///A test for MoveCurrentToPrevious
        ///</summary>
        [TestMethod]
        public void MoveCurrentToPreviousTest()
        {
            ICyclicCollectionView<string> target = new CyclicCollectionView<string>(_referenceList);
            Assert.AreEqual(_referenceList[0], target.CurrentItem);
            target.MoveCurrentToPrevious();
            Assert.AreEqual(_referenceList[4], target.CurrentItem);
            target.MoveCurrentToPrevious();
            Assert.AreEqual(_referenceList[3], target.CurrentItem);
            target.MoveCurrentToPrevious();
            Assert.AreEqual(_referenceList[2], target.CurrentItem);
            target.MoveCurrentToPrevious();
            Assert.AreEqual(_referenceList[1], target.CurrentItem);
            target.MoveCurrentToPrevious();

            Assert.AreEqual(_referenceList[0], target.CurrentItem);
            target.MoveCurrentToPrevious();
            Assert.AreEqual(_referenceList[4], target.CurrentItem);
            target.MoveCurrentToPrevious();
            Assert.AreEqual(_referenceList[3], target.CurrentItem);
            target.MoveCurrentToPrevious();
            Assert.AreEqual(_referenceList[2], target.CurrentItem);
            target.MoveCurrentToPrevious();
            Assert.AreEqual(_referenceList[1], target.CurrentItem);
            target.MoveCurrentToPrevious();
        }

        /// <summary>
        ///A test for Next
        ///</summary>
        [TestMethod]
        public void NextTest()
        {
            ICyclicCollectionView<string> target = new CyclicCollectionView<string>(_referenceList);
            Assert.AreEqual(_referenceList[0], target.CurrentItem);
            Assert.AreEqual(_referenceList[1], target.Next);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[2], target.Next);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[3], target.Next);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[4], target.Next);
            target.MoveCurrentToNext();

            Assert.AreEqual(_referenceList[0], target.Next);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[1], target.Next);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[2], target.Next);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[3], target.Next);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[4], target.Next);
            target.MoveCurrentToNext();
        }

        /// <summary>
        ///A test for Previous
        ///</summary>
        [TestMethod]
        public void PreviousTest()
        {
            ICyclicCollectionView<string> target = new CyclicCollectionView<string>(_referenceList);
            Assert.AreEqual(_referenceList[0], target.CurrentItem);
            Assert.AreEqual(_referenceList[4], target.Previous);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[0], target.Previous);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[1], target.Previous);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[2], target.Previous);
            target.MoveCurrentToNext();

            Assert.AreEqual(_referenceList[3], target.Previous);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[4], target.Previous);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[0], target.Previous);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[1], target.Previous);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[2], target.Previous);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[3], target.Previous);
            target.MoveCurrentToNext();
            Assert.AreEqual(_referenceList[4], target.Previous);
            target.MoveCurrentToNext();
        }
    }
}