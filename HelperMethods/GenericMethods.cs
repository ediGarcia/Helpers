using System;
using System.Collections.Generic;
using System.Linq;
using HelperExtensions;

namespace HelperMethods
{
    // ReSharper disable once UnusedMember.Global
    public static class GenericMethods
    {
        #region Public Methods

        #region Average (string, params T[])
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

        #region Compare
        /// <summary>
        /// Returns a single value according to the comparison method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comparison"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static T Compare<T>(Func<T, T, bool> comparison, params T[] values)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentException("No values to compare.");

            T selectedValue = values[0];
            values.ArrayForEach(item =>
            {
                if (comparison(item, selectedValue))
                    selectedValue = item;
            }, 1);

            return selectedValue;
        }
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

            values.ArrayForEach(item =>
            {
                double currentValue = GetDoubleValue(item, propertyName);

                if (currentValue > baseValue)
                {
                    baseValue = currentValue;
                    selectedObjects.Clear();
                    selectedObjects.Add(item);
                }
                else if (currentValue == baseValue)
                    selectedObjects.Add(item);
            }, 1);

            return selectedObjects.ToArray();
        }
        #endregion

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

            values.ArrayForEach(item =>
            {
                double currentValue = GetDoubleValue(item, propertyName);

                if (currentValue < baseValue)
                {
                    baseValue = currentValue;
                    selectedObjects.Clear();
                    selectedObjects.Add(item);
                }
                else if (currentValue == baseValue)
                    selectedObjects.Add(item);
            }, 1);

            return selectedObjects.ToArray();
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
}
