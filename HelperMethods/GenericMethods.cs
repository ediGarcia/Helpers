using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable UnusedMember.Global

namespace HelperMethods;

public static class GenericMethods
{
    #region Public Methods

    #region Average
    /// <summary>
    /// Retrieve the average value of the selected property.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="propertyName"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static double Average<T>(string propertyName, params T[] values) =>
        values.Length == 0
            ? throw new ArgumentException("No values to compare.")
            : values.Average(_ => GetDoubleValue(_, propertyName));
    #endregion

    #region AreAllNotNull
    /// <summary>
    /// Indicates whether every specified value is not null.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool AreAllNotNull(params object[] values) =>
        values.All(_ => _ is not null);
    #endregion

    #region AreAllNull
    /// <summary>
    /// Indicates whether every specified value is null.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool AreAllNull(params object[] values) =>
        values.All(_ => _ is null);
    #endregion

    #region AreEqual
    /// <summary>
    /// Indicates whether the specified properties of the given objects have the same values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj1"></param>
    /// <param name="obj2"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    public static bool AreEqual<T>(T obj1, T obj2, params string[] properties) =>
        properties.Select(typeof(T).GetProperty).All(_ => _.GetValue(obj1).Equals(_.GetValue(obj2)));
    #endregion

    #region Compare
    /// <summary>
    /// Performs a comparison of two objects of the same type and returns a value indicating whether x is less than, equal to, or greater than the y
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>A signed integer that indicates the relative values of value and other. If the return os less than zero, then x is less than y. If the return is zero, x is equal to y. And if the return is greater than zero, then x is greater than y.</returns>
    /// <exception cref="ArgumentException"><see cref="T"/> does not implement <see cref="IComparable{T}"/> or <see cref="IComparable"/>.</exception>
    public static int Compare<T>(T x, T y) =>
        Comparer<T>.Default.Compare(x, y);
    #endregion

    #region GetFirstMatching
    /// <summary>
    /// Returns the first value that matches the condition
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static T GetFirstMatching<T>(Func<T, bool> condition, params T[] values) =>
        values == null || values.Length == 0
            ? throw new ArgumentException("No values to compare.")
            : values.FirstOrDefault(condition);
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

    #region GetMatching
    /// <summary>
    /// Returns all the values that match the condition
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="condition"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static IEnumerable<T> GetMatching<T>(Func<T, bool> condition, params T[] values) =>
        values == null || values.Length == 0 ? throw new ArgumentException("No values to compare.") : values.Where(condition);
    #endregion

    #region IsAnyNotNull
    /// <summary>
    /// Indicates whether any of the specified values is not null.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool IsAnyNotNull(params object[] values) =>
        values.Any(_ => _ is not null);
    #endregion

    #region IsAnyNull
    /// <summary>
    /// Indicates whether any of the specified values is null.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool IsAnyNull(params object[] values) =>
        values.Any(_ => _ is null);
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
        IsGreater(value, minimum) && IsLess(value, maximum) || inclusive && (value.Equals(minimum) || value.Equals(maximum));
    #endregion

    #region IsGreater
    /// <summary>
    /// Indicates whether x is greater than y.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool IsGreater<T>(T x, T y) =>
        Compare(x, y) > 0;
    #endregion

    #region IsLess
    /// <summary>
    /// Indicates whether x is less than y.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool IsLess<T>(T x, T y) =>
        Compare(x, y) < 0;
    #endregion

    #region Max*

    #region Max(T, T)
    /// <summary>
    /// Returns the largest of two value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    public static T Max<T>(T value1, T value2) =>
        IsGreater(value1, value2) ? value1 : value2;
    #endregion

    #region Max(T, params T[])
    /// <summary>
    /// Returns the largest value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static T Max<T>(T value, params T[] values) =>
        values.Length == 0 ? value : Max(value, values.Max());
    #endregion

    #region Max (string, params T[])
    /// <summary>
    /// Returns all the objects with the larger property value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="propertyName"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static T[] Max<T>(string propertyName, params T[] values)
    {
        if (values.Length == 0)
            throw new ArgumentException("No values to compare.");

        List<T> selectedObjects = new(values.Length) { values[0] };
        double baseValue = GetDoubleValue(values[0], propertyName);

        for(int i = 1; i < values.Length; i++)
        {
            double currentValue = GetDoubleValue(values[i], propertyName);

            if (currentValue > baseValue)
            {
                baseValue = currentValue;
                selectedObjects.Clear();
                selectedObjects.Add(values[i]);
            }
            else if (currentValue == baseValue)
                selectedObjects.Add(values[i]);
        }

        return [.. selectedObjects];
    }
    #endregion

    #endregion

    #region Min*

    #region Min(T, T)
    /// <summary>
    /// Retrieves the smaller of two value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    public static T Min<T>(T value1, T value2) =>
        IsLess(value1, value2) ? value1 : value2;
    #endregion

    #region Min(T, params T[])
    /// <summary>
    /// Returns the smaller value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static T Min<T>(T value, params T[] values) =>
        values.Length == 0 ? value : Min(value, values.Min());
    #endregion

    #region Min(string, params T[])
    /// <summary>
    /// Returns all the objects with the smaller property value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="propertyName"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static T[] Min<T>(string propertyName, params T[] values)
    {
        if (values.Length == 0)
            throw new ArgumentException("No values to compare.");

        List<T> selectedObjects = new(values.Length) { values[0] };
        double baseValue = GetDoubleValue(values[0], propertyName);

        for (int i = 1; i < values.Length; i++)
        {
            double currentValue = GetDoubleValue(values[i], propertyName);

            if (currentValue < baseValue)
            {
                baseValue = currentValue;
                selectedObjects.Clear();
                selectedObjects.Add(values[i]);
            }
            else if (currentValue == baseValue)
                selectedObjects.Add(values[i]);
        }

        return [.. selectedObjects];
    }
    #endregion

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
    public static void Swap<T>(ref T v1, ref T v2) => 
        (v1, v2) = (v2, v1);

    #endregion

    #endregion

    #region Private Methods

    #region GetDoubleValue
    /// <summary>
    /// Returns the double value of a selected property.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    private static double GetDoubleValue<T>(T item, string propertyName) =>
        Convert.ToDouble(item.GetType().GetProperty(propertyName).GetValue(item));
    #endregion

    #endregion
}