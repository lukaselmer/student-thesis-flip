using System.Collections.Generic;
using ProjectFlip.UserInterface.Surface.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.UserInterface.Surface.Test
{

    /// <summary>
    ///This is a test class for GravatarsViewModelTest and is intended
    ///to contain all GravatarsViewModelTest Unit Tests
    ///</summary>
    [TestClass]
    public class GravatarsViewModelTest
    {
        /// <summary>
        ///A test for GravatarsViewModel Constructor
        ///</summary>
        [TestMethod]
        public void GravatarsViewModelConstructorTest()
        {
            var gravatarService = new GravatarServiceMock();
            var target = new GravatarsViewModel(gravatarService);
            Assert.AreEqual(1, target.Persons.Count);
            var person = target.Persons[0];
            Assert.AreEqual("Peter Muster", person.Name);
            Assert.AreEqual("peter@muster.com", person.Email);
            Assert.AreEqual("http://example.com", person.ImageUrl);
        }

        private class GravatarServiceMock : IGravatarService
        {
            public IList<IPerson> Persons { get { return new List<IPerson> { new PersonMock() }; } }
        }

        private class PersonMock : IPerson
        {
            public string Name { get { return "Peter Muster"; } }
            public string Email { get { return "peter@muster.com"; } }
            public string ImageUrl { get { return "http://example.com"; } }
        }
    }

}
