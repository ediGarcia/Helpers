using HelperExtensions;
using System.Globalization;
using System.Windows.Data;

namespace WpfHelpers.Converters;
public class EqualsConverter : IMultiValueConverter
{
    #region Convert
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.IsNullOrEmpty())
            return false;

        if (values.Length == 1)
            return true;

        object baseValue = values[0];
        for (int i = 1; i < values.Length; i++)
            if (!baseValue.Equals(values[i]))
                return false;

        return true;
    }
    #endregion

    #region ConvertBack
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
    #endregion
}
