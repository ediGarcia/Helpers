using System;
using System.Collections.Generic;
using System.Linq;

namespace HelperMethods;

// ReSharper disable once UnusedMember.Global
public static class ArrayMethods
{
    #region Public Methods

    #region BinarySearch(Func<T, T, int>, T, params T[])
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="comparisonFunction">
    /// The comparison function must return:
    /// (0): if parameters are equal -
    /// (1): if the first parameters is greater than the second -
    /// (-1): if the first parameter is smaller that the second.</param>
    /// <param name="comparisonValue"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static int BinarySearch<T>(Func<T, T, int> comparisonFunction, T comparisonValue, params T[] values)
    {
        int startIndex = 0;
        int endIndex = values.Length;

        while (startIndex <= endIndex)
        {
            int middleIndex = (startIndex + endIndex) / 2;

            switch (comparisonFunction(values[middleIndex], comparisonValue))
            {
                case 0:
                    return middleIndex;

                case 1:
                    startIndex = middleIndex + 1;
                    break;

                case -1:
                    endIndex = middleIndex - 1;
                    break;

                default:
                    throw new InvalidOperationException("The comparison function must return 0, 1 or -1.");
            }
        }

        return -1;
    }
    #endregion

    #region BinarySearch(Func<T, int>, params T[])
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="comparisonFunction">
    /// The comparison function must return:
    /// (0): if values are equal -
    /// (1): if the parameter is greater than the search value -
    /// (-1): if the parameter is smaller than the search value.</param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static int BinarySearch<T>(Func<T, int> comparisonFunction, params T[] values)
    {
        int startIndex = 0;
        int endIndex = values.Length;

        while (startIndex <= endIndex)
        {
            int middleIndex = (startIndex + endIndex) / 2;

            switch (comparisonFunction(values[middleIndex]))
            {
                case 0:
                    return middleIndex;

                case 1:
                    startIndex = middleIndex + 1;
                    break;

                case -1:
                    endIndex = middleIndex - 1;
                    break;

                default:
                    throw new InvalidOperationException("The comparison function must return 0, 1 or -1.");
            }
        }

        return -1;
    }
    #endregion

    #region BinarySearch(IEnumerable<T>, Func<T, int>)
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="comparisonFunction">
    /// The comparison function must return:
    /// (0): if values are equal -
    /// (1): if the parameter is greater than the search value -
    /// (-1): if the parameter is smaller than the search value.</param>
    /// <param name="values"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static int BinarySearch<T>(IEnumerable<T> values, Func<T, int> comparisonFunction) =>
        BinarySearch(comparisonFunction, values.ToArray());
    #endregion

    #region BinarySearch(T, params T[])
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="searchValue"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static int BinarySearch<T>(T searchValue, params T[] values) where T : IComparable<T>
    {
        int startIndex = 0;
        int endIndex = values.Length;

        while (startIndex <= endIndex)
        {
            int middleIndex = (startIndex + endIndex) / 2;

            switch (values[middleIndex].CompareTo(searchValue))
            {
                case 0:
                    return middleIndex;

                case 1:
                    startIndex = middleIndex + 1;
                    break;

                case -1:
                    endIndex = middleIndex - 1;
                    break;

                default:
                    throw new InvalidOperationException("The comparison function must return 0, 1 or -1.");
            }
        }

        return -1;
    }
    #endregion

    #endregion
}