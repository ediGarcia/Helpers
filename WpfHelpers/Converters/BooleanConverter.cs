using System.Globalization;
using System.Windows.Data;

namespace WpfHelpers.Converters;

public class BooleanConverter : IValueConverter
{
    #region Properties

    /// <summary>
    /// Gets or sets the value to return when the input is false.
    /// </summary>
    public virtual object FalseValue { get; set; } = false;

    /// <summary>
    /// Gets or sets the value to return when the input is true.
    /// </summary>
    public virtual object TrueValue { get; set; } = true;

    #endregion

    #region Convert
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not bool booleanValue)
            throw new ArgumentException("Invalid boolean value", nameof(value));

        return booleanValue ? TrueValue : FalseValue;
    }
    #endregion

    #region ConvertBack
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == TrueValue)
            return true;

        if (value == FalseValue)
            return false;

        throw new ArgumentException("Invalid value", nameof(value));
    }
    #endregion
}
