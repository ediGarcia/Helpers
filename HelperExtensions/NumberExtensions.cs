using System;
// ReSharper disable UnusedMember.Global

namespace HelperExtensions;

public static class NumberExtensions
{
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
