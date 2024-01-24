using HelperMethods;
using System.Globalization;
using System.Windows.Data;
#pragma warning disable CS8603

namespace WPFHelper.Converters;

public class PathToIconConverter : IValueConverter
{
    #region Convert
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is string path ? SystemMethods.GetIcon(path, true) : null;
    #endregion

    #region ConvertBack
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
    #endregion
}
