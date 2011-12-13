#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace ProjectFlip.Services.Loader.Test
{
    /// <summary>
    ///This is a test class for ProjectNotesLoaderTest and is intended
    ///to contain all ProjectNotesLoaderTest Unit Tests
    ///</summary>
    [TestClass]
    public class ProjectNotesLoaderTest
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
        ///A test for Import
        ///</summary>
        [TestMethod]
        public void ImportTest()
        {
            const string filename = @"..\..\..\ProjectFlip.Services.Loader.Test\testdata.txt";
            Assert.IsTrue(File.Exists(filename), "Testfile " + filename + " does not exist!");
            var target = new ProjectNotesLoader {Filename = filename};
            var listFromAtoS = Enumerable.Range('a', 19).Select(c => "" + (char) c).ToList();
            var expected = new List<List<string>> {listFromAtoS};
            var actual = target.Import();
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.IsTrue(expected[0].SequenceEqual(actual[0]));
            Assert.IsTrue(actual[0].SequenceEqual(expected[0]));
        }
    }
}