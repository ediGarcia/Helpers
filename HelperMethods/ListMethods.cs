using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// ReSharper disable UnusedMember.Global

namespace HelperMethods;

public static class ListMethods
{
    #region Public Methods

    #region IList<T>

    #region BinarySearch*

    #region BinarySearch<T>(IList<T>, T)
    /// <summary>
    /// Performs a binary search and retrieves the index of the target item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="targetValue"></param>
    /// <returns>The index of the target item, if it exists within the specified collection. -1, otherwise.</returns>
    public static int BinarySearch<T>(IList<T> values, T targetValue)
        where T : IComparable<T> => BinarySearch(values, targetValue, (t1, t2) => t1.CompareTo(t2));
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
    public static int BinarySearch<T>(
        IList<T> values,
        T targetValue,
        Func<T, T, int> comparisonFunction
    )
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
                    finalIndex = middleIndex - 1;
                    break;

                case -1:
                    startIndex = middleIndex + 1;
                    break;

                default:
                    throw new InvalidOperationException(
                        "The comparison function must return 0, 1 or -1."
                    );
            }
        }

        return -1;
    }
    #endregion

    #region BinarySearch<T>(IList<T>, string, object)
    /// <summary>
    /// Performs a binary search and retrieves the index of the target item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="propertyName"></param>
    /// <param name="targetObject"></param>
    /// <returns>The index of the target item, if it exists within the specified collection. -1, otherwise.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static int BinarySearch<T>(IList<T> values, string propertyName, T targetObject)
    {
        int startIndex = 0;
        int finalIndex = values.Count - 1;
        PropertyInfo property = typeof(T).GetProperty(propertyName);

        if (property is null)
            throw new ArgumentException(
                $"There is no \"{propertyName}\" property in type \"{nameof(T)}\""
            );

        object targetValue = property.GetValue(targetObject);

        while (startIndex <= finalIndex)
        {
            int middleIndex = (int)Math.Round((startIndex + finalIndex) / 2d);
            object currentValue = property.GetValue(values[middleIndex]);

            switch (GenericMethods.Compare(currentValue, targetValue))
            {
                case 0:
                    return middleIndex;

                case 1:
                    finalIndex = middleIndex - 1;
                    break;

                case -1:
                    startIndex = middleIndex + 1;
                    break;

                default:
                    throw new InvalidOperationException(
                        "The comparison function must return 0, 1 or -1."
                    );
            }
        }

        return -1;
    }
    #endregion

    #endregion

    #region GetSortedList
    /// <summary>
    /// Retrieves a sorted list from the original <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="list"></param>
    /// <param name="keySelector"></param>
    /// <returns></returns>
    public static IList<T> GetSortedList<T, TKey>(IEnumerable<T> list, Func<T, TKey> keySelector) =>
        list.OrderBy(keySelector).ToList();
    #endregion

    #region Sort
    /// <summary>
    /// Sorts the specified <see cref="IList{T}"/>/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="list"></param>
    /// <param name="keySelector"></param>
    public static void Sort<T, TKey>(ref IList<T> list, Func<T, TKey> keySelector) =>
        list = list.OrderBy(keySelector).ToList();
    #endregion

    #endregion

    #endregion
}
