#region

using System.ComponentModel;

#endregion

namespace ProjectFlip.UserInterface.Surface.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Declarations

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Other

        protected void Notify(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}