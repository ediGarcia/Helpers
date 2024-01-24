using System.Globalization;
using System.IO;
using System.Windows.Data;
#pragma warning disable CS8603

namespace WPFHelper.Converters;

public class PathToFileNameConverter : IValueConverter
{
    #region Convert
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is string path ? Path.GetFileName(path) : null;
    #endregion

    #region ConvertBack
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
    #endregion
}
