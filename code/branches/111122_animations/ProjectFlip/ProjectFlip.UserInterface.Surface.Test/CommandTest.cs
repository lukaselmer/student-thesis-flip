using ProjectFlip.UserInterface.Surface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ProjectFlip.UserInterface.Surface.Test
{
    
    
    /// <summary>
    ///This is a test class for CommandTest and is intended
    ///to contain all CommandTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CommandTest
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
        ///A test for CanExecute
        ///</summary>
        [TestMethod()]
        public void CanExecuteTest()
        {
            Action<object> execute = o => { };
            Predicate<object> canExecute = o => o != null;
            Command target = new Command(execute, canExecute);
            bool actual = target.CanExecute(new object());
            Assert.AreEqual(true, actual);
            }

        /// <summary>
        ///A test for CanExecute and Execute
        ///</summary>
        [TestMethod()]
        public void ExecuteTest()
        {
            var success = false;
            Action<object> execute = o => success = true;
            Command target = new Command(execute);
            bool actual = target.CanExecute(new object());
            Assert.AreEqual(true, actual);
            object parameter = new object();
            target.Execute(parameter);
            Assert.AreEqual(true, success);
        }

        /// <summary>
        ///A test for RaiseCanExecuteChanged
        ///</summary>
        [TestMethod()]
        public void RaiseCanExecuteChangedTest()
        {
            var success = false;
            var target = new Command((sender) => {});
            target.CanExecuteChanged += (sender, eventArgs) => success = true;
            target.RaiseCanExecuteChanged();
            Assert.IsTrue(success);
        }
    }
}
