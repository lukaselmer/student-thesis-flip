#region

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Test.Mock;

#endregion

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
            Assert.AreSame(Metadata.Get(MetadataType.Get("Sector"), "bla"),
                Metadata.Get(MetadataType.Get("Sector"), "bla"));
        }

        /// <summary>
        ///A test for Match
        ///</summary>
        [TestMethod]
        public void MatchTest()
        {
            var t1 = new MetadataTypeMock {Name = "Bla1"};
            var t2 = new MetadataTypeMock {Name = "Bla2"};
            var t3 = new MetadataTypeMock {Name = "Bla3"};

            var t1M1 = Metadata.Get(t1, "Val1");
            var t1M2 = Metadata.Get(t1, "Val2");
            var t1M3 = Metadata.Get(t1, "Val3");
            var t2M1 = Metadata.Get(t2, "Blu1");
            var t2M2 = Metadata.Get(t2, "Blu2");
            var t3M1 = Metadata.Get(t3, "Blbb1");

            var pn = new ProjectNoteMock
                     {
                         Metadata =
                             new Dictionary<IMetadataType, ICollection<IMetadata>>
                             {{t1, new List<IMetadata> {t1M1, t1M2}}, {t2, new List<IMetadata> {t2M1, t2M2}},}
                     };

            Assert.IsTrue(t1M1.Match(pn));
            Assert.IsTrue(t1M2.Match(pn));
            Assert.IsTrue(t2M1.Match(pn));
            Assert.IsTrue(t2M2.Match(pn));
            Assert.IsFalse(t1M3.Match(pn));
            Assert.IsFalse(t1M3.Match(pn));
            Assert.IsFalse(t3M1.Match(pn));
        }
    }
}