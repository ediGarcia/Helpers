using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable UnusedMember.Global

namespace HelperExtensions;

public static class ListExtensions
{
    #region Public methods

    #region ICollection<T>

    #region AddRange
    /// <summary>
    /// Adds the items into the current <see cref="ICollection{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="items"></param>
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items) =>
        items.ForEach(collection.Add);
    #endregion

    #region Contains
    /// <summary>
    /// Determines whether <see cref="List{T}"/> contains all of the specified values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    public static bool Contains<T>(this ICollection<T> list, params T[] items) =>
        items.All(list.Contains);
    #endregion

    #region ContainsAny
    /// <summary>
    /// Determines whether <see cref="List{T}"/> contains any of the specified values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    public static bool ContainsAny<T>(this ICollection<T> list, params T[] items) =>
        items.Any(list.Contains);
    #endregion

    #region LastIndex
    /// <summary>
    /// Returns the last index of the list.
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static int LastIndex<T>(this ICollection<T> list) =>
        list.Count - 1;
    #endregion

    #region Remove
    /// <summary>
    /// Removes the first occurrence of a specific object from the <see cref="List{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="item"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static bool Remove<T>(this ICollection<T> list, T item, IEqualityComparer<T> comparer) =>
        list.FirstOrDefault(_ => comparer.Equals(item, _)) is { } itemToRemove && list.Remove(itemToRemove);
    #endregion

    #region RemoveAll
    /// <summary>
    /// Removes every occurrence of a specific object from the <see cref="List{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="item"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static bool RemoveAll<T>(this ICollection<T> list, T item, IEqualityComparer<T> comparer)
    {
        bool itemsRemoved = false;

        list.ForEach(_ =>
        {
            if (comparer.Equals(item, _))
            {
                list.Remove(_);
                itemsRemoved = true;
            }
        });

        return itemsRemoved;
    }
    #endregion

    #endregion

    #region IDictionary<T1, T2>

    #region ForEach
    /// <summary>
    /// Performs the specified action on each element of the dictionary.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the list.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void ForEach<T1, T2>(this IDictionary<T1, T2> dictionary, Action<T1, T2, int> action,
        int startIndex = 0, int? length = null)
    {
        T1[] keys = dictionary.Keys.ToArray();
        T2[] values = dictionary.Values.ToArray();

        for (int i = startIndex; i < startIndex + (length ?? dictionary.Count); i++)
            action?.Invoke(keys[i], values[i], i);
    }
    #endregion

    #region InverseForEach
    /// <summary>
    /// Performs the specified action on each element of the dictionary in the inverse order.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the list.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void InverseForEach<T1, T2>(this IDictionary<T1, T2> dictionary, Action<T1, T2, int> action, int? startIndex = null, int? length = null)
    {
        T1[] keys = dictionary.Keys.ToArray();
        T2[] values = dictionary.Values.ToArray();

        startIndex ??= keys.Length - 1;
        length ??= keys.Length;

        for (int i = startIndex.Value; i > startIndex - length; i--)
            action?.Invoke(keys[i], values[i], i);
    }
    #endregion

    #endregion

    #region IList<T>

    #region AddMany
    /// <summary>
    /// Adds multiple items to the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="items"></param>
    public static void AddMany<T>(this IList<T> list, params T[] items) =>
        items.ForEach(list.Add);
    #endregion

    #region FillLeft

    #region FillLeft(this IList<T>, int)
    /// <summary>
    /// Inserts the specified quantity of the default value to the beginning of the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="quantity"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void FillLeft<T>(this IList<T> list, int quantity)
    {
        if (quantity < 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "The fill quantity must be greater than zero (0).");

        for (int i = 0; i < quantity; i++)
            list.Insert(0, default);
    }
    #endregion

    #region FillLeft(this IList<T>, T, int)
    /// <summary>
    /// Inserts the specified quantity of the specified value to the beginning of the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="value"></param>
    /// <param name="quantity"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void FillLeft<T>(this IList<T> list, T value, int quantity)
    {
        if (quantity < 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "The fill quantity must be greater than zero (0).");

        for (int i = 0; i < quantity; i++)
            list.Insert(0, value);
    }
    #endregion

    #endregion

    #region FillRight

    #region FillRight(this IList<T>, int)
    /// <summary>
    /// Inserts the specified quantity of the default value to the beginning of the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="quantity"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void FillRight<T>(this IList<T> list, int quantity)
    {
        if (quantity < 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "The fill quantity must be greater than zero (0).");

        for (int i = 0; i < quantity; i++)
            list.Add(default);
    }
    #endregion

    #region FillRight(this IList<T>, T, int)
    /// <summary>
    /// Inserts the specified quantity of the specified value to the end of the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="value"></param>
    /// <param name="quantity"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void FillRight<T>(this IList<T> list, T value, int quantity)
    {
        if (quantity < 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "The fill quantity must be greater than zero (0).");

        for (int i = 0; i < quantity; i++)
            list.Add(value);
    }
    #endregion

    #endregion

    #region ForEach
    /// <summary>
    /// Performs the specified action on each element of the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the list.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void ForEach<T>(this IList<T> list, Action<T, int> action, int startIndex = 0, int? length = null)
    {
        for (int i = startIndex; i < startIndex + (length ?? list.Count); i++)
            action?.Invoke(list[i], i);
    }
    #endregion

    #region InverseForEach

    #region InverseForEach(this IList<T>, Action<T>, [int?], [int?])
    /// <summary>
    /// Performs the specified action on each element of the list in the inverse order.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the list.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void InverseForEach<T>(this IList<T> list, Action<T> action, int? startIndex = null, int? length = null)
    {
        startIndex ??= list.Count - 1;
        int lastIndex = startIndex.Value - (length ?? list.Count);

        for (int i = startIndex.Value; i > lastIndex; i--)
            action?.Invoke(list[i]);
    }
    #endregion

    #region InverseForEach(this IList<T>, Action<T, int>, [int?], [int?])
    /// <summary>
    /// Performs the specified action on each element of the list in the inverse order.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the list.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void InverseForEach<T>(this IList<T> list, Action<T, int> action, int? startIndex = null, int? length = null)
    {
        startIndex ??= list.Count - 1;
        int lastIndex = startIndex.Value - (length ?? list.Count);

        for (int i = startIndex.Value; i > lastIndex; i--)
            action?.Invoke(list[i], i);
    }
    #endregion

    #endregion

    #region InsertMany
    /// <summary>
    /// Insert multiple items to the list starting at the specified index.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="startIndex"></param>
    /// <param name="items"></param>
    public static void InsertMany<T>(this IList<T> list, int startIndex, params T[] items) =>
        items.InverseForEach(_ => list.Insert(startIndex, _));
    #endregion

    #region Move
    /// <summary>
    /// Moves the item at the specified source index to the specified destination index within the current <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="currentIndex"></param>
    /// <param name="destinationIndex"></param>
    public static void Move<T>(this IList<T> list, int currentIndex, int destinationIndex)
    {
        T item = list[currentIndex];

        list.RemoveAt(currentIndex);
        list.Insert(destinationIndex, item);
    }
    #endregion

    #region MoveDown
    /// <summary>
    /// Moves the item one position down within the current <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="item"></param>
    public static void MoveDown<T>(this IList<T> list, T item) =>
        list.MoveDownAt(list.IndexOf(item));
    #endregion

    #region MoveDownAt
    /// <summary>
    /// Moves the item at the specified source index one position down within the current <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="index"></param>
    public static void MoveDownAt<T>(this IList<T> list, int index)
    {
        if (index == list.LastIndex())
            return;

        list.Move(index, index + 1);
    }
    #endregion

    #region MoveUp
    /// <summary>
    /// Moves the item one position up within the current <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="item"></param>
    public static void MoveUp<T>(this IList<T> list, T item) =>
        list.MoveUpAt(list.IndexOf(item));
    #endregion

    #region MoveUpAt
    /// <summary>
    /// Moves the item at the specified source index one position up within the current <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="index"></param>
    public static void MoveUpAt<T>(this IList<T> list, int index)
    {
        if (index == 0)
            return;

        list.Move(index, index - 1);
    }
    #endregion

    #endregion

    #region IEnumerable<T>

    #region ContainsAll
    /// <summary>
    /// Indicates whether the <see cref="IEnumerable{T}"/> contains all the specified items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="items"></param>
    /// <returns>True, if the enumeration contains all of the specified items. False, otherwise.</returns>
    public static bool ContainsAll<T>(this IEnumerable<T> iEnumerable, params T[] items) =>
        items.All(iEnumerable.Contains);
    #endregion

    #region ContainsAny
    /// <summary>
    /// Indicates whether the <see cref="IEnumerable{T}"/> contains any the specified items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="items"></param>
    /// <returns>True, if the enumeration contains any of the specified items. False, otherwise.</returns>
    public static bool ContainsAny<T>(this IEnumerable<T> iEnumerable, params T[] items) =>
        items.Any(iEnumerable.Contains);
    #endregion

    #region ForEach(this IEnumerable<T>, Action<T>)

    /// <summary>
    /// Performs the specified action on each element of the array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the array.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void ForEach<T>(this IEnumerable<T> iEnumerable, Action<T> action)
    {
        foreach (T value in iEnumerable)
            action?.Invoke(value);
    }

    #endregion

    #region ForEach(this IEnumerable<T>, Action<T, int>)

    /// <summary>
    /// Performs the specified action on each element of the array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the array.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void ForEach<T>(this IEnumerable<T> iEnumerable, Action<T, int> action)
    {
        int index = 0;

        foreach (T value in iEnumerable)
            action?.Invoke(value, index++);
    }

    #endregion

    #endregion

    #endregion
}