namespace HelperMethods;

public static class GenericHelper
{
    #region Public Methods

    #region AreAllNotNull
    /// <summary>
    /// Indicates whether every specified value is not null.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool AreAllNotNull(params object[] values) => values.All(_ => _ is not null);
    #endregion

    #region AreAllNull
    /// <summary>
    /// Indicates whether every specified value is null.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool AreAllNull(params object[] values) => values.All(_ => _ is null);
    #endregion

    #region AreEqual
    /// <summary>
    /// Indicates whether every specified value is equal, based on a custom comparison function.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="customComparer"></param>
    /// <returns></returns>
    public static bool AreEqual<T>(IList<T> values, Func<T, T, int> customComparer)
    {
        switch (values.Count)
        {
            case 0:
                return false;

            case 1:
                return true;

            default:
                for (int i = 0; i < values.Count - 1; i++)
                {
                    int result = customComparer(values[i], values[i + 1]);
                    if (result != 0)
                        return false;
                }

                return true;
        }
    }
    #endregion

    #region Coerce
    /// <summary>
    /// Coerces a value to be within the specified minimum and maximum range.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="minimum"></param>
    /// <param name="maximum"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"><see cref="minimum"/> is greater than <see cref="maximum"/>,
    /// or <see cref="T"/> does not implement <see cref="IComparable{T}"/> or <see cref="IComparable"/>.</exception>
    public static T Coerce<T>(T value, T minimum, T maximum)
    {
        if (Compare(minimum, maximum) > 0)
            throw new ArgumentException("Minimum value cannot be greater than maximum value.");

        if (Compare(value, minimum) < 0)
            return minimum;

        if (Compare(value, maximum) > 0)
            return maximum;

        return value;
    }
    #endregion

    #region Compare
    /// <summary>
    /// Performs a comparison of two objects of the same type and returns a value indicating whether x is less than, equal to, or greater than the y
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>A signed integer that indicates the relative values of value and other. If the return os less than zero, then x is less than y.
    /// If the return is zero, x is equal to y. And if the return is greater than zero, then x is greater than y.</returns>
    /// <exception cref="ArgumentException"><see cref="T"/> does not implement <see cref="IComparable{T}"/> or <see cref="IComparable"/>.</exception>
    public static int Compare<T>(T x, T y) => Comparer<T>.Default.Compare(x, y);
    #endregion

    #region Equals
    /// <summary>
    /// Determines whether two specified objects of type <typeparamref name="T"/> are equal.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns><see langword="true"/> if the two objects are considered equal; otherwise, <see langword="false"/>.</returns>
    public static bool Equals<T>(T x, T y) => Compare(x, y) == 0;
    #endregion

    #region EvaluateChance
    /// <summary>
    /// Returns true based on a given probability.
    /// </summary>
    /// <param name="probability"></param>
    /// <returns></returns>
    public static bool EvaluateChance(int probability) =>
        NumberHelper.GetRandomInt(100) < probability;
    #endregion

    #region GetFirstNotNull
    /// <summary>
    /// Returns the first not null element of the sequence.
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public static object GetFirstNotNull(params object[] items) =>
        items.FirstOrDefault(_ => _ is not null);
    #endregion

    #region IsAnyNotNull
    /// <summary>
    /// Indicates whether any of the specified values is not null.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool IsAnyNotNull(params object[] values) => values.Any(_ => _ is not null);
    #endregion

    #region IsAnyNull
    /// <summary>
    /// Indicates whether any of the specified values is null.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool IsAnyNull(params object[] values) => values.Any(_ => _ is null);
    #endregion

    #region IsBetween
    /// <summary>
    /// Indicates whether the specified value is between two thresholds.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="minimum"></param>
    /// <param name="maximum"></param>
    /// <param name="inclusive"></param>
    /// <returns></returns>
    public static bool IsBetween<T>(T value, T minimum, T maximum, bool inclusive) =>
        IsGreater(value, minimum) && IsLess(value, maximum)
        || inclusive && (value.Equals(minimum) || value.Equals(maximum));
    #endregion

    #region IsGreater
    /// <summary>
    /// Indicates whether x is greater than y.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool IsGreater<T>(T x, T y) => Compare(x, y) > 0;
    #endregion

    #region IsLess
    /// <summary>
    /// Indicates whether x is less than y.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool IsLess<T>(T x, T y) => Compare(x, y) < 0;
    #endregion

    #region Max
    /// <summary>
    /// Returns the largest of two value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    public static T Max<T>(T value1, T value2) => IsGreater(value1, value2) ? value1 : value2;
    #endregion

    #region Min
    /// <summary>
    /// Retrieves the smaller of two value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    public static T Min<T>(T value1, T value2) => IsLess(value1, value2) ? value1 : value2;
    #endregion

    #region ReturnFirstNotNull
    /// <summary>
    /// Gets the first <see cref="Func{TResult}"/> that returns a not null value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
    public static T ReturnFirstNotNull<T>(params Func<T>[] items)
    {
        foreach (Func<T> function in items)
        {
            T result = function();

            if (result is not null)
                return result;
        }

        return default;
    }
    #endregion

    #region Swap
    /// <summary>
    /// Swaps values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    // ReSharper disable once UnusedMember.Global
    public static void Swap<T>(ref T v1, ref T v2) => (v1, v2) = (v2, v1);
    #endregion

    #endregion
}
