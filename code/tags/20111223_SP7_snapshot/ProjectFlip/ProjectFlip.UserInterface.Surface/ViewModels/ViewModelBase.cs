#region

using System.ComponentModel;

#endregion

namespace ProjectFlip.UserInterface.Surface.ViewModels
{
    /// <summary>
    /// The view model base.
    /// </summary>
    /// <remarks></remarks>
    public abstract class ViewModelBase : INotifyPropertyChanged
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
        /// Notifies that the property with the specified name has changed.
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