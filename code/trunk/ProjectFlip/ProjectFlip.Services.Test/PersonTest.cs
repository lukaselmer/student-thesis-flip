using ProjectFlip.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ProjectFlip.Services.Test
{


    /// <summary>
    ///This is a test class for PersonTest and is intended
    ///to contain all PersonTest Unit Tests
    ///</summary>
    [TestClass]
    public class PersonTest
    {

        /// <summary>
        ///A test for Person Constructor
        ///</summary>
        [TestMethod]
        public void PersonConstructorTest()
        {
            const string name = "Lukas Elmer";
            const string email = "lelmer@hsr.ch";
            var target = new Person(name, email);
            Assert.AreEqual(name, target.Name);
            Assert.AreEqual(email, target.Email);
        }

        /// <summary>
        ///A test for ImageUrl
        ///</summary>
        [TestMethod]
        public void ImageUrlTest()
        {
            const string name = "Lukas Elmer";
            const string email = "lelmer@hsr.ch";
            var target = new Person(name, email);
            Assert.AreEqual("http://www.gravatar.com/avatar/3230c1430c82059fabc5dbd2bd19828f?s=150", target.ImageUrl);
        }
    }
}
