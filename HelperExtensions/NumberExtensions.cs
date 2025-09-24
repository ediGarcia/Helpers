using System;
// ReSharper disable UnusedMember.Global

namespace HelperExtensions;

public static class NumberExtensions
{
    #region IsBetween*

    #region IsBetween(int)
    /// <summary>
    /// Indicates whether a value is within a specified range.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="inclusive"></param>
    /// <returns></returns>
    public static bool IsBetween(this int value, int min, int max, bool inclusive = true) =>
        value >= min && (inclusive && value <= max || !inclusive && value < max);
    #endregion

    #region IsBetween(long)
    /// <summary>
    /// Indicates whether a value is within a specified range.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="inclusive"></param>
    /// <returns></returns>
    public static bool IsBetween(this long value, long min, long max, bool inclusive = true) =>
        value >= min && (inclusive && value <= max || !inclusive && value < max);
    #endregion

    #region IsBetween(double)
    /// <summary>
    /// Indicates whether a value is within a specified range.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="inclusive"></param>
    /// <returns></returns>
    public static bool IsBetween(this double value, double min, double max, bool inclusive = true) =>
        value >= min && (inclusive && value <= max || !inclusive && value < max);
    #endregion

    #endregion

    #region IsInfinity
    ///<inheritdoc cref="Double.IsInfinity"/>
    public static bool IsInfinity(this double value) =>
        Double.IsInfinity(value);
    #endregion

    #region IsNaN
    ///<inheritdoc cref="Double.IsNaN"/>
    public static bool IsNaN(this double value) =>
        Double.IsNaN(value);
    #endregion

    #region IsNegativeInfinity
    ///<inheritdoc cref="Double.IsNegativeInfinity"/>
    public static bool IsNegativeInfinity(this double value) =>
        Double.IsNegativeInfinity(value);
    #endregion

    #region IsPositiveInfinity
    ///<inheritdoc cref="Double.IsPositiveInfinity"/>
    public static bool IsPositiveInfinity(this double value) =>
        Double.IsPositiveInfinity(value); 
    #endregion
}
