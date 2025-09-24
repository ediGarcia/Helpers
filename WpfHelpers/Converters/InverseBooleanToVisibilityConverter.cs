// ReSharper disable UnusedMember.Global

using System.Windows;

namespace WpfHelpers.Converters;

public class InverseBooleanToVisibilityConverter : BooleanConverter
{
    #region Properties

    /// <inheritdoc cref="BooleanConverter.FalseValue" />
    public new object FalseValue { get; } = Visibility.Visible;

    /// <inheritdoc cref="BooleanConverter.TrueValue" />
    public new object TrueValue { get; } = Visibility.Collapsed;

    #endregion
}