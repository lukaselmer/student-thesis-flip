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

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        #endregion
    }
}