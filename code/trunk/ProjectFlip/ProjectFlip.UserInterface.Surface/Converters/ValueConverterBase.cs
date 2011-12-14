#region

using System;
using System.Windows.Markup;

#endregion

namespace ProjectFlip.UserInterface.Surface.Converters
{
    /// <summary>
    /// The Value Converter Base. It is used by the BoolToVisibilityConverter and could also be used by other converters.
    /// </summary>
    /// <remarks></remarks>
    public class ValueConverterBase : MarkupExtension
    {
        #region Other

        /// <summary>
        /// Returns an object that is set as the value of the target property for this markup extension.
        /// </summary>
        /// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
        /// <returns>The object value to set on the property where the extension is applied.</returns>
        /// <remarks></remarks>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        #endregion
    }
}