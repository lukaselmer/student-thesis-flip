using System.ComponentModel;

namespace ProjectFlip.UserInterface.Surface
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual event PropertyChangedEventHandler PropertyChanged;
    }
}