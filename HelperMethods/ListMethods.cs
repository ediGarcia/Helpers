using System;
using System.Collections.Generic;
// ReSharper disable UnusedMember.Global

namespace HelperMethods;

public static class ListMethods
{
    #region Public Methods

    #region BinarySearch*

    #region BinarySearch<T>(IList<T>, T)
    /// <summary>
    /// Performs a binary search and retrieves the index of the target item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="targetValue"></param>
    /// <returns>The index of the target item, if it exists within the specified collection. -1, otherwise.</returns>
    public static int BinarySearch<T>(IList<T> values, T targetValue) where T : IComparable<T> =>
        BinarySearch(values, targetValue, (t1, t2) => t1.CompareTo(t2));
    #endregion

    #region BinarySearch<T>(IList<T>, T, Func<T, T, int>)
    /// <summary>
    /// Performs a binary search and retrieves the index of the target item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="targetValue"></param>
    /// <param name="comparisonFunction">A comparison function that should return: 0, if both values are equal; 
    /// 1 if the first value is greater than the second; -1 if the first value is less than the second.</param>
    /// <returns>The index of the target item, if it exists within the specified collection. -1, otherwise.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static int BinarySearch<T>(IList<T> values, T targetValue, Func<T, T, int> comparisonFunction)
    {
        int startIndex = 0;
        int finalIndex = values.Count - 1;

        while (startIndex <= finalIndex)
        {
            int middleIndex = (int)Math.Round((startIndex + finalIndex) / 2d);

            switch (comparisonFunction(values[middleIndex], targetValue))
            {
                case 0:
                    return middleIndex;

                case 1:
                    startIndex = middleIndex + 1;
                    break;

                case -1:
                    finalIndex = middleIndex - 1;
                    break;

                default:
                    throw new InvalidOperationException("The comparison function must return 0, 1 or -1.");
            }
        }

        return -1;
    }
    #endregion

    #endregion

    #endregion
}