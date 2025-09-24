// ReSharper disable UnusedMember.Global

namespace WpfHelpers.Converters;

public class InverseBooleanConverter : BooleanConverter
{
    #region Properties

    /// <inheritdoc cref="BooleanConverter.FalseValue" />
    public new object FalseValue { get; } = true;

    /// <inheritdoc cref="BooleanConverter.TrueValue" />
    public new object TrueValue { get; } = false;

    #endregion
}
