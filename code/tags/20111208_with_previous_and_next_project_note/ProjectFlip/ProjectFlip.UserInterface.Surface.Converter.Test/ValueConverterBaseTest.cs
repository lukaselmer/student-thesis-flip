#region

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.Test.Mock;
using ProjectFlip.UserInterface.Surface.Converters;

#endregion

namespace ProjectFlip.UserInterface.Surface.Converter.Test
{
    /// <summary>
    ///This is a test class for ValueConverterBaseTest and is intended
    ///to contain all ValueConverterBaseTest Unit Tests
    ///</summary>
    [TestClass]
    public class ValueConverterBaseTest
    {
        /// <summary>
        ///A test for ProvideValue
        ///</summary>
        [TestMethod]
        public void ProvideValueTest()
        {
            var target = new ValueConverterBase();
            var returnvalue = target.ProvideValue(new ServiceProviderMock());
            Assert.AreEqual(returnvalue, target);
        }
    }
}