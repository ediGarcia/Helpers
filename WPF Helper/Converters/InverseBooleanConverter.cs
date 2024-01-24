using System.Globalization;
using System.Windows.Data;

namespace WPFHelper.Converters;

public class InverseBooleanConverter : IValueConverter
{
    #region Convert
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        !(bool)value;
    #endregion

    #region ConvertBack
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        !(bool)value;
    #endregion
}
