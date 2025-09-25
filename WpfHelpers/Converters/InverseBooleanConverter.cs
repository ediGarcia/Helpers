namespace WpfHelpers.Converters;

public class InverseBooleanConverter : BooleanConverter
{
    #region Properties

    /// <inheritdoc cref="BooleanConverter.FalseValue" />
    public new object FalseValue => true;

    /// <inheritdoc cref="BooleanConverter.TrueValue" />
    public new object TrueValue => false;

    #endregion
}
