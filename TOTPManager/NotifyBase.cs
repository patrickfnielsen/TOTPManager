using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TOTPManager
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void Set<T>(ref T propertyNameValue, T value, [CallerMemberName] string propertyName = null)
        {
            propertyNameValue = value;
            if (propertyName != null) OnPropertyChanged(propertyName);
        }
    }
}
