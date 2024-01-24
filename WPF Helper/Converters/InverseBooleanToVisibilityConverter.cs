using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFHelper.Converters;

public class InverseBooleanToVisibilityConverter : IValueConverter
{
    #region Convert
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        (bool)value ? Visibility.Collapsed : Visibility.Visible;
    #endregion

    #region ConvertBack
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        (Visibility)value == Visibility.Collapsed;
    #endregion
}
