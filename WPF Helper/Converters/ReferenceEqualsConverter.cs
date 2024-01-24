using System.Globalization;
using System.Windows.Data;

namespace WPFHelper.Converters;

public class ReferenceEqualsConverter : IMultiValueConverter
{
    #region Convert
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) =>
        values.Length != 0 && values.All(_ => ReferenceEquals(_, values[0]));
    #endregion

    #region ConvertBack
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
    #endregion
}
