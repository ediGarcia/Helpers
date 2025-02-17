using HelperExtensions;
using System.Globalization;
using System.Windows.Data;
// ReSharper disable UnusedMember.Global

namespace WpfHelpers.Converters;

public class IsStringNullOrEmptyConverter : IValueConverter
{
    #region Convert
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value?.ToString().IsNullOrEmpty() ?? true;
    #endregion

    #region ConvertBack
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new InvalidOperationException();
    #endregion
}
