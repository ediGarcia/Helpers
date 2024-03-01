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

    #region Max*

    #region Max (params T[])
    /// <summary>
    /// Retrieves the larger T value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static T Max<T>(params T[] values) =>
        values.Length == 0 ? throw new ArgumentException("No values to compare.") : values.Max();
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

    #region Min (params T[])
    /// <summary>
    /// Retrieves the smaller T value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static T Min<T>(params T[] values) =>
        values.Length == 0 ? throw new ArgumentException("No values to compare.") : values.Min();
    #endregion

    #region Min (string, params T[])
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
    public static void Swap<T>(ref T v1, ref T v2)
    {
        T swap = v1;
        v1 = v2;
        v2 = swap;
    }
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