using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectFlip.Services.Test
{


    /// <summary>
    ///This is a test class for GravatarServiceTest and is intended
    ///to contain all GravatarServiceTest Unit Tests
    ///</summary>
    [TestClass]
    public class GravatarServiceTest
    {


        /// <summary>
        ///A test for GravatarService Constructor
        ///</summary>
        [TestMethod]
        public void GravatarServiceConstructorTest()
        {
            GravatarService target = new GravatarService();
            Assert.AreEqual(3, target.Persons.Count);
            var persons = target.Persons;
            var lukas = persons.First(p => p.Email == "lelmer@hsr.ch");
            Assert.AreEqual("Lukas Elmer", lukas.Name);
            var christina = persons.First(p => p.Email == "cheidt@hsr.ch");
            Assert.AreEqual("Christina Heidt", christina.Name);
            var delia = persons.First(p => p.Email == "dtreichl@hsr.ch");
            Assert.AreEqual("Delia Treichler", delia.Name);
        }
    }
}
