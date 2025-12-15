namespace HelperMethods;

public static class BooleanHelper
{
    #region Public Methods

    #region Reverse
    /// <summary>
    /// Reverses the value of the specified boolean.
    /// </summary>
    /// <param name="b"></param>
    public static void Reverse(ref bool b) => b = !b;
    #endregion

    #endregion
}
