using System.Collections;
using System.Globalization;
using System.Windows.Data;
using WpfHelpers.Enums;

namespace WpfHelpers.Converters;

public class ComparisonConverter : IMultiValueConverter
{
    #region Property

    /// <summary>
    /// Gets or sets the comparison type.
    /// </summary>
    public ComparisonOperator Operator { get; set; }

    #endregion

    #region Convert
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        switch (values.Length)
        {
            case 0:
                return false;

            case 1:
                return true;

            case 2:
            {
                int comparisonResult = Comparer.Default.Compare(values[0], values[1]);

                return Operator switch
                {
                    ComparisonOperator.NotEquals => comparisonResult != 0,
                    ComparisonOperator.GreaterThan => comparisonResult > 0,
                    ComparisonOperator.LessThan => comparisonResult < 0,
                    ComparisonOperator.GreaterThanOrEquals => comparisonResult >= 0,
                    ComparisonOperator.LessThanOrEquals => comparisonResult <= 0,
                    _ => comparisonResult == 0
                };
            }

            default:
                throw new ArgumentException($"{nameof(ComparisonConverter)} only supports two values to compare.");
        }
    }
    #endregion

    #region ConvertBack
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
    #endregion
}
