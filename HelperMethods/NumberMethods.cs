using System;
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

    #region GetRandomInt
    /// <summary>
    /// Returns a random int value between [minimum] and [maximum].
    /// </summary>
    /// <param name="minimum"></param>
    /// <param name="maximum"></param>
    /// <param name="isInclusive"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static int GetRandomInt(int minimum = Int32.MinValue, int maximum = Int32.MaxValue, bool isInclusive = false)
    {
        if (isInclusive && maximum < minimum)
            maximum++;

        return Rand.Next(minimum, maximum);
    }
    #endregion

    #endregion
}