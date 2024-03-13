using System;
using System.Linq;

// ReSharper disable UnusedMember.Global

namespace HelperMethods;

public static class NumberMethods
{
    #region Properties

    /// <summary>
    /// Random number generator.
    /// </summary>
    public static Random Rand { get; } = new();

    #endregion

    #region Public Methods

    #region Between
    /// <summary>
    /// Retrieves the value if its between the specified minimum and maximum. Otherwise, returns either minimum or maximum.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static T Between<T>(T value, T min, T max) =>
        Max(Min(value, max), min);
    #endregion

    #region GetRandomDouble
    /// <summary>
    /// Returns a random double value between [minimum] and [maximum].
    /// </summary>
    /// <param name="minimum"></param>
    /// <param name="maximum"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static double GetRandomDouble(double minimum = Double.MinValue, double maximum = Double.MaxValue) =>
        maximum < minimum
            ? throw new ArgumentOutOfRangeException($"{nameof(maximum)} must be equal or greater than {nameof(minimum)}.")
            : Rand.NextDouble() * (maximum - minimum) + minimum;
    #endregion

    #region GetRandomInt*

    #region GetRandomInt()
    /// <summary>
    /// Returns a random int value.
    /// </summary>
    /// <param name="allowNegative">Indicates whether the method may return negative values.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static int GetRandomInt(bool allowNegative = false) =>
        Rand.Next(allowNegative ? Int32.MinValue : 0, Int32.MaxValue);
    #endregion

    #region GetRandomInt(int, [bool])
    /// <summary>
    /// Returns a random int value between zero and [maximum].
    /// </summary>
    /// <param name="maximum"></param>
    /// <param name="isInclusive"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static int GetRandomInt(int maximum, bool isInclusive = false)
    {
        if (isInclusive)
            maximum++;

        return Rand.Next(0, maximum);
    }
    #endregion

    #region GetRandomInt(int, int, [bool])
    /// <summary>
    /// Returns a random int value between [minimum] and [maximum].
    /// </summary>
    /// <param name="minimum"></param>
    /// <param name="maximum"></param>
    /// <param name="isInclusive"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static int GetRandomInt(int minimum, int maximum, bool isInclusive = false)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (isInclusive && maximum < minimum && maximum < Int32.MaxValue)
            maximum++;

        return Rand.Next(minimum, maximum);
    }
    #endregion

    #endregion

    #region Max*

    #region Max(T, T)
    /// <summary>
    /// Returns the largest of two numbers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    public static T Max<T>(T value1, T value2) =>
        GenericMethods.Max(value1, value2);
    #endregion

    #region Max(T, params T[])
    /// <summary>
    /// Returns the largest number.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static T Max<T>(T value, params T[] values) =>
        GenericMethods.Max(value, values.Max());
    #endregion

    #endregion

    #region Min*

    #region Min(T, T)
    /// <summary>
    /// Returns the smalled of two numbers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    public static T Min<T>(T value1, T value2) =>
        GenericMethods.Min(value1, value2);
    #endregion

    #region Min(T, params T[])
    /// <summary>
    /// Returns the smalled number.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static T Min<T>(T value, params T[] values) =>
        GenericMethods.Min(value, values.Min());
    #endregion

    #endregion

    #endregion
}