using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace TOTPManager
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public bool IsInDesignMode { get; } = (bool) DependencyPropertyDescriptor
            .FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement))
            .Metadata.DefaultValue;

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
