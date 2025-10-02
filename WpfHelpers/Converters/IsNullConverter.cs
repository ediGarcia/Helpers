using System.Globalization;
using System.Windows.Data;

namespace WpfHelpers.Converters;

public class IsNullConverter : IValueConverter
{
    #region Convert
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is null;
    #endregion

    #region ConvertBack
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
    #endregion
}
