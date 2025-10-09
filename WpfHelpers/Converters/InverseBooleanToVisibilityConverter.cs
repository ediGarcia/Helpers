using System.Windows;

namespace WpfHelpers.Converters;

public class InverseBooleanToVisibilityConverter : BooleanConverter
{
    #region Properties

    /// <inheritdoc cref="BooleanConverter.FalseValue" />
    public override object FalseValue => Visibility.Visible;

    /// <inheritdoc cref="BooleanConverter.TrueValue" />
    public override object TrueValue => Visibility.Collapsed;

    #endregion
}