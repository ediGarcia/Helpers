using System.Globalization;
using System.Windows.Data;

namespace WpfHelpers.Converters;

// ReSharper disable once UnusedMember.Global
public class FlagToBooleanConverter : IValueConverter
{
    #region Convert
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Enum enumValue)
            return false;

        Enum flag;

        switch (parameter)
        {
            case Enum flagParameter:
                flag = flagParameter;
                break;

            case string flagName:
                flag = (Enum)Enum.Parse(enumValue.GetType(), flagName);
                break;

            default:
                return false;
        }

        return enumValue.HasFlag(flag);
    }
    #endregion

    #region ConvertBack
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
    #endregion
}