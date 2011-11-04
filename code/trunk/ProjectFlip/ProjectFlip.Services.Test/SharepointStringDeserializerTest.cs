using System.Linq;
using ProjectFlip.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ProjectFlip.Services.Test
{


    /// <summary>
    ///This is a test class for SharepointStringDeserializerTest and is intended
    ///to contain all SharepointStringDeserializerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SharepointStringDeserializerTest
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
        ///A test for ToList
        ///</summary>
        [TestMethod]
        public void ToListTests()
        {
            ToListTest("", new string[0]);
            ToListTest("test", new[] { "test" });
            ToListTest("Perl;#Struts;#Windows", new[] { "Perl", "Struts", "Windows" });
            ToListTest("\"test\"", new[] { "test" });
            ToListTest("\"Consulting;#_ Technology Consulting;#__ Technology Consultation;#_ Methodology;#__ Software-Testing;#Development;#_ Software solutions;#__ Bespoke Solutions;#_ Product innovation;#__ Test Engineering\"",
                new[] { "Consulting", "Technology Consulting", "Technology Consultation", "Methodology", "Software-Testing", "Development", "Software solutions", "Bespoke Solutions", "Product innovation", "Test Engineering" });
            ToListTest("Real-Time and Embedded Software", new[] { "Real-Time and Embedded Software" });
            ToListTest("\"Embedded Systems;#Control Systems;#Machine Control Systems\"",
                new[] { "Embedded Systems", "Control Systems", "Machine Control Systems" });
            ToListTest("C++;#LabVIEW;#MS Excel;#UML;#CAD - ProE; MS Visual Studio; SW Analyse Tools; GOOP",
                new[] { "C++", "LabVIEW", "MS Excel", "UML", "CAD - ProE", "MS Visual Studio", "SW Analyse Tools", "GOOP" });
            ToListTest("\"Embedded Systems;#Sensors/Signal Processing\"",
                new[] { "Embedded Systems", "Sensors/Signal Processing" });
            ToListTest(@"So kommt der ""Aeroccino"" termingerecht auf den Weltmarkt.",
                new[] { "So kommt der \"Aeroccino\" termingerecht auf den Weltmarkt." });
            ToListTest("C#;#MS SQL;#SAP;#UML;#Windows;#XML;#Axapta; Windows Share Point Services",
                new[] { "C#", "MS SQL", "SAP", "UML", "Windows", "XML", "Axapta", "Windows Share Point Services" });
        }

        private static void ToListTest(string input, IEnumerable<string> expectedArray)
        {
            var expected = new List<string>(expectedArray);
            var actual = SharepointStringDeserializer.ToStringList(input).ToList();
            Assert.IsTrue(expected.SequenceEqual(actual), "Lists are not equal, input: #{0}", input);
        }
    }
}
