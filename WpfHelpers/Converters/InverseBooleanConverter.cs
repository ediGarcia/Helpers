namespace WpfHelpers.Converters;

public class InverseBooleanConverter : BooleanConverter
{
    #region Properties

    /// <inheritdoc cref="BooleanConverter.FalseValue" />
    public override object FalseValue => true;

    /// <inheritdoc cref="BooleanConverter.TrueValue" />
    public override object TrueValue => false;

    #endregion
}
