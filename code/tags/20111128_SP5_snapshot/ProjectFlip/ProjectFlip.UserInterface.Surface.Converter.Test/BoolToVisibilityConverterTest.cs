#region

using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.UserInterface.Surface.Converters;

#endregion

namespace ProjectFlip.UserInterface.Surface.Converter.Test
{
    /// <summary>
    ///This is a test class for BoolToVisibilityConverterTest and is intended
    ///to contain all BoolToVisibilityConverterTest Unit Tests
    ///</summary>
    [TestClass]
    public class BoolToVisibilityConverterTest
    {
        /// <summary>
        ///A test for Convert
        ///</summary>
        [TestMethod]
        public void ConvertTest()
        {
            var target = new BoolToVisibilityConverter();
            Assert.AreEqual(Visibility.Visible, target.Convert(true, null, null, null));
            Assert.AreEqual(Visibility.Collapsed, target.Convert(false, null, null, null));
        }

        /// <summary>
        ///A test for ConvertBack
        ///</summary>
        [TestMethod]
        public void ConvertBackTest()
        {
            var target = new BoolToVisibilityConverter();
            Assert.AreEqual(true, target.ConvertBack(Visibility.Visible, null, null, null));
            Assert.AreEqual(false, target.ConvertBack(Visibility.Collapsed, null, null, null));
        }
    }
}