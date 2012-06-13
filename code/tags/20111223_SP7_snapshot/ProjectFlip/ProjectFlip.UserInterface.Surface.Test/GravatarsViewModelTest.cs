﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            IList<object> list = new[] { target.Persons.GetEnumerator() };
            Assert.AreEqual(2, list.Count);
            Assert.AreSame(gravatarService.Persons[0], list[0] as PersonMock);
            Assert.AreSame(gravatarService.Persons[1], list[1] as PersonMock);
        }

        private class GravatarServiceMock : IGravatarService
        {
            public IList<IPerson> Persons { get { return new List<IPerson> { new PersonMock(), new PersonMock() }; } }
        }

        private class PersonMock : IPerson
        {
            public string Name { get { return "Peter Muster"; } }
            public string Email { get { return "peter@muster.com"; } }
            public string ImageUrl { get { return "http://example.com"; } }
        }
    }

}