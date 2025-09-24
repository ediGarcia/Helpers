using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable UnusedMember.Global

namespace HelperMethods;

public static class ListMethods
{
    #region Public Methods

    #region IList<T>

    #region BinarySearch
    /// <summary>
    /// Performs a binary search to find the item which contains the specified <see cref="value"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="list"></param>
    /// <param name="value"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static T BinarySearch<T, TKey>(IList<T> list, TKey value, Func<T, TKey> selector) where TKey : IComparable<TKey>
    {
        int index = BinarySearchIndex(list, value, selector);
        return index == -1 ? default : list[index];
    }
    #endregion

    #region BinarySearchIndex
    /// <summary>
    /// Performs a binary search to find the index of the item which contains the specified <see cref="value"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="list"></param>
    /// <param name="value"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static int BinarySearchIndex<T, TKey>(IList<T> list, TKey value, Func<T, TKey> selector) where TKey : IComparable<TKey>
    {
        int startIndex = 0;
        int finalIndex = list.Count - 1;

        while (startIndex <= finalIndex)
        {
            int middleIndex = (int)Math.Round((startIndex + finalIndex) / 2d);
            TKey item = selector(list[middleIndex]);

            switch (item.CompareTo(value))
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

    #region GetSortedList
    /// <summary>
    /// Retrieves a sorted list from the original <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="list"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static IList<T> GetSortedList<T, TKey>(IEnumerable<T> list, Func<T, TKey> selector) =>
        [.. list.OrderBy(selector)];
    #endregion

    #region Sort
    /// <summary>
    /// Sorts the specified <see cref="IList{T}"/>/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="list"></param>
    /// <param name="selector"></param>
    public static void Sort<T, TKey>(ref IList<T> list, Func<T, TKey> selector) =>
        list = [.. list.OrderBy(selector)];
    #endregion

    #endregion

    #endregion
}
