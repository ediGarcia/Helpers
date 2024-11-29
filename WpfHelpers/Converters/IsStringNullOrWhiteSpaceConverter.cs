using System.Globalization;
using System.Windows.Data;
using HelperExtensions;

namespace WpfHelpers.Converters;

public class IsStringNullOrWhiteSpaceConverter : IValueConverter
{
    #region Convert
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value?.ToString().IsNullOrWhiteSpace() ?? true;
    #endregion

    #region ConvertBack
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new InvalidOperationException();
    #endregion
}