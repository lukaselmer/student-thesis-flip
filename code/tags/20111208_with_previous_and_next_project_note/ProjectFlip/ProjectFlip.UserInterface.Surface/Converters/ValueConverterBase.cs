#region

using System;
using System.Windows.Markup;

#endregion

namespace ProjectFlip.UserInterface.Surface.Converters
{
    public class ValueConverterBase : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}