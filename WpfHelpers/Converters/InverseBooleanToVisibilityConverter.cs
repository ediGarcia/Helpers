using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfHelpers.Converters;

public class InverseBooleanToVisibilityConverter : IValueConverter
{
    #region Convert
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is true ? Visibility.Collapsed : Visibility.Visible;
    #endregion

    #region ConvertBack
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is not Visibility.Visible;
    #endregion
}