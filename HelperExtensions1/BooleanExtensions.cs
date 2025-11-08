using System;

namespace HelperExtensions;

public static class BooleanExtensions
{
    #region Public Methods

    #region IsFalse
    /// <summary>
    /// Determines whether the current <see cref="Nullable{Boolean}"/> is false.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsFalse(this bool? value) =>
        value == false;
    #endregion

    #region IsNullOrFalse
    /// <summary>
    /// Determines whether the current <see cref="Nullable{Boolean}"/> is false or does not contain a valid value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNullOrFalse(this bool? value) =>
        value.IsNull() || value.IsFalse();
    #endregion

    #region IsNullOrTrue
    /// <summary>
    /// Determines whether the current <see cref="Nullable{Boolean}"/> is true or does not contain a valid value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNullOrTrue(this bool? value) =>
        value.IsNull() || value.IsTrue();
    #endregion

    #region IsTrue
    /// <summary>
    /// Determines whether the current <see cref="Nullable{Boolean}"/> is false.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsTrue(this bool? value) =>
        value == true;
    #endregion

    #endregion
}