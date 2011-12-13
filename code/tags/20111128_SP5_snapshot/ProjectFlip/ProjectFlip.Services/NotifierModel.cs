#region

using System.ComponentModel;

#endregion

namespace ProjectFlip.Services
{
    public class NotifierModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected void Notify(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}