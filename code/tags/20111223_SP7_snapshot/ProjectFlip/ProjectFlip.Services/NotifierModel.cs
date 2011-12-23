#region

using System.ComponentModel;

#endregion

namespace ProjectFlip.Services
{
    /// <summary>
    /// The notifier model. It implements INotifyPropertyChanged interface.
    /// </summary>
    /// <remarks></remarks>
    public class NotifierModel : INotifyPropertyChanged
    {
        #region Declarations

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        /// <remarks></remarks>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Other

        /// <summary>
        /// Notifies if the element with the specified name has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <remarks></remarks>
        protected void Notify(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}