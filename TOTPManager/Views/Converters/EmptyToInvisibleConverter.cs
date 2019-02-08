using System;
using System.Windows;
using System.Windows.Data;

namespace TOTPManager.Views.Converters
{
    public class EmptyToInvisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var collectionCount = (int)value;
            return collectionCount != 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
