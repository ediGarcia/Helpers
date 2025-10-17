using HelperExtensions;
using System.Globalization;
using System.Windows.Data;

namespace WpfHelpers.Converters;

public class TupleConverter : IMultiValueConverter
{
    #region Convert
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.IsNullOrEmpty())
            return null;

        return values.Length switch
        {
            1 => Tuple.Create(values[0]),
            2 => Tuple.Create(values[0], values[1]),
            3 => Tuple.Create(values[0], values[1], values[2]),
            4 => Tuple.Create(values[0], values[1], values[2], values[3]),
            5 => Tuple.Create(values[0], values[1], values[2], values[3], values[4]),
            6 => Tuple.Create(values[0], values[1], values[2], values[3], values[4], values[5]),
            7 => Tuple.Create(values[0], values[1], values[2], values[3], values[4], values[5], values[6]),
            8 => Tuple.Create(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7]),
            _ => throw new NotSupportedException("Tuples with more than 8 items are not supported.")
        };
    }
    #endregion

    #region ConvertBack
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
    #endregion
}