using HelperMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
// ReSharper disable UnusedMember.Global

namespace HelperExtensions;

public static class ListExtensions
{
    #region Public methods

    #region T[]

    #region GetSubArray
    /// <summary>
    /// Retrieves a sub-array from this instance. The sub-array starts at a specified position and has a specified length.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static T[] GetSubArray<T>(this T[] array, int startIndex, int? length = null)
    {
        length ??= array.Length - startIndex;

        T[] newArray = new T[length.Value];

        for (int i = 0; i < length; i++)
            newArray[i] = array[startIndex + i];

        return newArray;
    }
    #endregion

    #region LastIndex
    /// <summary>
    /// Gets the last index of an array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
    public static int LastIndex<T>(this T[] array) =>
        array.Length - 1;
    #endregion

    #endregion

    #region ICollection<T>

    #region AddMany
    /// <summary>
    /// Adds multiple items to the collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="items"></param>
    public static void AddMany<T>(this ICollection<T> collection, params T[] items) =>
        items.ForEach(collection.Add);
    #endregion

    #region AddRange
    /// <summary>
    /// Adds the items into the current collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="items"></param>
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items) =>
        items.ForEach(collection.Add);
    #endregion

    #region Clone
    /// <summary>
    /// Creates a copy of the current collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static ICollection<T> Clone<T>(this ICollection<T> collection)
    {
        Collection<T> cloned = [];
        collection.ForEach(cloned.Add);

        return cloned;
    }
    #endregion

    #region IsInsideBounds
    /// <summary>
    /// Indicates whether the specified <see cref="index"/> is inside the bound of the current collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static bool IsInsideBounds<T>(this ICollection<T> collection, int index) =>
        !collection.IsNullOrEmpty() && index >= 0 && index < collection.Count;
    #endregion

    #region Remove
    /// <summary>
    /// Removes every element that matches the specified criteria.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="predicate"></param>
    /// <returns>The number of items removed.</returns>
    public static int Remove<T>(this ICollection<T> collection, Func<T, bool> predicate)
    {
        int itemsRemovedCount = 0;

        collection.ToArray().ForEach(_ =>
        {
            if (predicate(_))
            {
                collection.Remove(_);
                itemsRemovedCount++;
            }
        });

        return itemsRemovedCount;
    }
    #endregion

    #region RemoveFirst
    /// <summary>
    /// Removes the first element that matches the specified criteria.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool RemoveFirst<T>(this ICollection<T> collection, Func<T, bool> predicate) =>
        collection.FirstOrDefault(predicate) is { } itemToRemove && collection.Remove(itemToRemove);
    #endregion

    #endregion

    #region IDictionary<T1, T2>

    #region Clone
    /// <summary>
    /// Creates a copy of the current dictionary.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    public static Dictionary<T1, T2> Clone<T1, T2>(this IDictionary<T1, T2> dictionary)
    {
        Dictionary<T1, T2> cloned = new(dictionary.Count);
        dictionary.ForEach(cloned.Add);

        return cloned;
    }
    #endregion

    #region ConcurrentForEach*

    #region ConcurrentForEach(this IDictionary<T1, T2>, Action<T1, T2>, [int], [int?])
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
    public static void ConcurrentForEach<T1, T2>(this IDictionary<T1, T2> dictionary, Action<T1, T2> action,
        int startIndex = 0, int? length = null)
    {
        T1[] keys = [.. dictionary.Keys];
        T2[] values = [.. dictionary.Values];
        List<Task> tasks = new(keys.Length);
        int loopLimit = length.HasValue ? startIndex + length.Value : dictionary.Count;

        for (int i = startIndex; i < loopLimit; i++)
            tasks.Add(Task.Run(() => action(keys[i], values[i])));

        Task.WaitAll([.. tasks]);
    }
    #endregion

    #region ConcurrentForEach(this IDictionary<T1, T2>, Action<T1, T2, int>, [int], [int?])
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
    public static void ConcurrentForEach<T1, T2>(this IDictionary<T1, T2> dictionary, Action<T1, T2, int> action,
        int startIndex = 0, int? length = null)
    {
        T1[] keys = [.. dictionary.Keys];
        T2[] values = [.. dictionary.Values];
        List<Task> tasks = new(keys.Length);
        int loopLimit = length.HasValue ? startIndex + length.Value : dictionary.Count;

        for (int i = startIndex; i < loopLimit; i++)
            tasks.Add(Task.Run(() => action(keys[i], values[i], i)));

        Task.WaitAll([.. tasks]);
    }
    #endregion

    #endregion

    #region ConcurrentReverseForEach*

    #region ConcurrentReverseForEach<T1, T2>(this IDictionary<T1, T2>, Action<T1, T2>, [int?], [int?])
    /// <summary>
    /// Asynchronously performs the specified action on each element of the dictionary in the inverse order.
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
    public static void ConcurrentReverseForEach<T1, T2>(this IDictionary<T1, T2> dictionary, Action<T1, T2> action, int? startIndex = null, int? length = null)
    {
        T1[] keys = [.. dictionary.Keys];
        T2[] values = [.. dictionary.Values];
        List<Task> tasks = new(keys.Length);

        startIndex ??= keys.LastIndex();
        length ??= keys.Length;

        for (int i = startIndex.Value; i > startIndex - length; i--)
            tasks.Add(Task.Run(() => action(keys[i], values[i])));

        Task.WaitAll([.. tasks]);
    }
    #endregion

    #region ConcurrentReverseForEach<T1, T2>(this IDictionary<T1, T2>, Action<T1, T2, int>, [int?], [int?])
    /// <summary>
    /// Asynchronously performs the specified action on each element of the dictionary in the inverse order.
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
    public static void ConcurrentReverseForEach<T1, T2>(this IDictionary<T1, T2> dictionary, Action<T1, T2, int> action, int? startIndex = null, int? length = null)
    {
        T1[] keys = [.. dictionary.Keys];
        T2[] values = [.. dictionary.Values];
        List<Task> tasks = new(keys.Length);

        startIndex ??= keys.LastIndex();
        length ??= keys.Length;

        for (int i = startIndex.Value; i > startIndex - length; i--)
            tasks.Add(Task.Run(() => action(keys[i], values[i], i)));

        Task.WaitAll([.. tasks]);
    }
    #endregion

    #endregion

    #region ForEach*

    #region ForEach(this IDictionary<T1, T2>, Action<T1, T2>, [int], [int?])
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
    public static void ForEach<T1, T2>(this IDictionary<T1, T2> dictionary, Action<T1, T2> action,
        int startIndex = 0, int? length = null)
    {
        T1[] keys = [.. dictionary.Keys];
        T2[] values = [.. dictionary.Values];

        for (int i = startIndex; i < startIndex + (length ?? dictionary.Count); i++)
            action(keys[i], values[i]);
    }
    #endregion

    #region ForEach(this IDictionary<T1, T2>, Action<T1, T2, int>, [int], [int?])
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
        T1[] keys = [.. dictionary.Keys];
        T2[] values = [.. dictionary.Values];

        for (int i = startIndex; i < startIndex + (length ?? dictionary.Count); i++)
            action(keys[i], values[i], i);
    }
    #endregion

    #endregion

    #region ReverseForEach*

    #region ReverseForEach<T1, T2>(this IDictionary<T1, T2>, Action<T1, T2>, [int?], [int?])
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
    public static void ReverseForEach<T1, T2>(this IDictionary<T1, T2> dictionary, Action<T1, T2> action, int? startIndex = null, int? length = null)
    {
        T1[] keys = [.. dictionary.Keys];
        T2[] values = [.. dictionary.Values];

        startIndex ??= keys.Length - 1;
        length ??= keys.Length;

        for (int i = startIndex.Value; i > startIndex - length; i--)
            action?.Invoke(keys[i], values[i]);
    }
    #endregion

    #region InverseForEach<T1, T2>(this IDictionary<T1, T2>, Action<T1, T2, int>, [int?], [int?])
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
    public static void ReverseForEach<T1, T2>(this IDictionary<T1, T2> dictionary, Action<T1, T2, int> action, int? startIndex = null, int? length = null)
    {
        T1[] keys = [.. dictionary.Keys];
        T2[] values = [.. dictionary.Values];

        startIndex ??= keys.LastIndex();
        length ??= keys.Length;

        for (int i = startIndex.Value; i > startIndex - length; i--)
            action?.Invoke(keys[i], values[i], i);
    }
    #endregion

    #endregion

    #endregion

    #region IList

    #region Clone
    /// <summary>
    /// Creates a copy of the current list.
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static IList Clone(this IList list) =>
        list.ToList<object>();
    #endregion

    #region GetItem
    /// <summary>
    /// Retrieves an item converted to the specified type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static T GetItem<T>(this IList list, int index) =>
        (T)list[index];
    #endregion

    #region Move
    /// <summary>
    /// Moves the item at the specified source index to the specified destination index within the current <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="item"></param>
    /// <param name="destinationIndex"></param>
    /// <returns>true if the item is successfully moved inside the <see cref="IList{T}"/>; otherwise, false. This method returns false if the item is not found in the original <see cref="IList{T}"/> or the item is already at the destination index.</returns>
    public static bool Move<T>(this IList list, T item, int destinationIndex)
    {
        int currentItemIndex = list.IndexOf(item);

        if (currentItemIndex != -1 && currentItemIndex != destinationIndex)
        {
            list.Remove(item);
            list.Insert(destinationIndex, item);
            return true;
        }

        return false;
    }
    #endregion

    #region MoveAt
    /// <summary>
    /// Moves the item at the specified source index to the specified destination index within the current <see cref="IList"/>.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="currentIndex"></param>
    /// <param name="destinationIndex"></param>
    public static void MoveAt(this IList list, int currentIndex, int destinationIndex)
    {
        object item = list[currentIndex];

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
    /// <returns>true if the item is successfully moved inside the <see cref="IList{T}"/>; otherwise, false. This method returns false if the item is not found in the original <see cref="IList"/>.</returns>
    public static bool MoveDown<T>(this IList list, T item)
    {
        int index = list.IndexOf(item);
        list.MoveDownAt(index);
        return true;
    }
    #endregion

    #region MoveDownAt
    /// <summary>
    /// Moves the item at the specified source index one position down within the current <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="index"></param>
    public static void MoveDownAt(this IList list, int index) =>
        list.MoveAt(index, index + 1);
    #endregion

    #region MoveUp
    /// <summary>
    /// Moves the item one position up within the current <see cref="IList"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="item"></param>
    /// <returns>true if the item is successfully moved inside the <see cref="IList"/>; otherwise, false. This method returns false if the item is not found in the original <see cref="IList{T}"/>.</returns>
    public static bool MoveUp<T>(this IList list, T item)
    {
        int index = list.IndexOf(item);
        list.MoveUpAt(index);
        return true;
    }
    #endregion

    #region MoveUpAt
    /// <summary>
    /// Moves the item at the specified source index one position up within the current <see cref="IList"/>.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="index"></param>
    public static void MoveUpAt(this IList list, int index) =>
        list.MoveAt(index, index - 1);

    #endregion

    #region ReverseForEach*

    #region ReverseForEach(this IList, Action<T>, [int?], [int?])
    /// <summary>
    /// Performs the specified action on each element of the list in the reverse order.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the list.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void ReverseForEach<T>(this IList list, Action<T> action, int? startIndex = null, int? length = null)
    {
        startIndex ??= list.Count - 1;
        int lastIndex = length.HasValue ? startIndex.Value - length.Value : 0;

        for (int i = startIndex.Value; i >= lastIndex; i--)
            action((T)list[i]);
    }
    #endregion

    #region ReverseForEach(this IList<T>, Action<T, int>, [int?], [int?])
    /// <summary>
    /// Performs the specified action on each element of the list in the reverse order.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the list.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void ReverseForEach<T>(this IList list, Action<T, int> action, int? startIndex = null, int? length = null)
    {
        startIndex ??= list.Count - 1;
        int lastIndex = length.HasValue ? startIndex.Value - length.Value : 0;

        for (int i = startIndex.Value; i >= lastIndex; i--)
            action((T)list[i], i);
    }
    #endregion

    #endregion

    #endregion

    #region IList<T>

    #region All
    /// <summary>
    /// Determines whether all the elements of a sequence satisfy the specified condition.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="predicate"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static bool All<T>(this IList<T> list, Func<T, bool> predicate, int startIndex, int? length)
    {
        int finalIndex = length.HasValue ? startIndex + length.Value : list.LastIndex();

        for (int i = startIndex; i < finalIndex; i++)
            if (!predicate(list[i]))
                return false;

        return true;
    }
    #endregion

    #region Any
    /// <summary>
    /// Determines whether any of the elements of a sequence satisfies the specified condition.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="predicate"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static bool Any<T>(this IList<T> list, Func<T, bool> predicate, int startIndex, int? length)
    {
        int finalIndex = length.HasValue ? startIndex + length.Value : list.LastIndex();

        for (int i = startIndex; i < finalIndex; i++)
            if (predicate(list[i]))
                return true;

        return false;
    }
    #endregion

    #region BinarySearch*

    #region BinarySearch(this IList<T>, T)
    /// <summary>
    /// Performs a binary search and retrieves the index of the target item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="targetValue"></param>
    /// <returns>The index of the target item, if it exists within the specified collection. -1, otherwise.</returns>
    public static int BinarySearch<T>(this IList<T> list, T targetValue) where T : IComparable<T> =>
        list is List<T> listImplementation
            ? listImplementation.BinarySearch(targetValue)
            : ListMethods.BinarySearch(list, targetValue);

    #endregion

    #region BinarySearch(this IList<T>, T, Func<T, T, int>)
    /// <summary>
    /// Performs a binary search and retrieves the index of the target item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="targetValue"></param>
    /// <param name="comparisonFunction">A comparison function that should return: 0, if both values are equal;
    /// 1 if the first value is greater than the second; -1 if the first value is less than the second.</param>
    /// <returns>The index of the target item, if it exists within the specified collection. -1, otherwise.</returns>
    public static int BinarySearch<T>(this IList<T> list, T targetValue, Func<T, T, int> comparisonFunction) =>
        ListMethods.BinarySearch(list, targetValue, comparisonFunction);
    #endregion

    #endregion

    #region Clone
    /// <summary>
    /// Creates a copy of the current list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<T> Clone<T>(this IList<T> list)
    {
        List<T> cloned = new(list.Count);
        list.ForEach(cloned.Add);

        return cloned;
    }
    #endregion

    #region ConcurrentForEach*

    #region ConcurrentForEach(this IList<T>, Action<T>, int, [int?])
    /// <summary>
    /// Asynchronously performs the specified action on each element of the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the list.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void ConcurrentForEach<T>(this IList<T> list, Action<T> action, int startIndex, int? length = null)
    {
        List<Task> tasks = new(length ?? list.Count);
        int finalIndex = length is null ? list.LastIndex() : startIndex + length.Value;

        for (int i = startIndex; i <= finalIndex; i++)
            tasks.Add(Task.Run(() => action(list[i])));

        Task.WaitAll([.. tasks]);
    }
    #endregion

    #region ConcurrentForEach(this IList<T>, Action<T, int>, int, [int?])
    /// <summary>
    /// Asynchronously performs the specified action on each element of the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the list.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void ConcurrentForEach<T>(
        this IList<T> list,
        Action<T, int> action,
        int startIndex,
        int? length = null)
    {
        List<Task> tasks = new(length ?? list.Count);
        int finalIndex = length is null ? list.LastIndex() : startIndex + length.Value;

        for (int i = startIndex; i <= finalIndex; i++)
            tasks.Add(Task.Run(() => action(list[i], i)));

        tasks.ForEach(_ => _.Wait());
    }
    #endregion

    #endregion

    #region ConcurrentReverseForEach*

    #region ConcurrentReverseForEach(this IList<T>, Action<T>, [int?], [int?]
    /// <summary>
    /// Asynchronously performs the specified action on each element of the list in the reverse order.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the list.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void ConcurrentReverseForEach<T>(
        this IList<T> list,
        Action<T> action,
        int? startIndex = null,
        int? length = null)
    {
        startIndex ??= list.LastIndex();
        int lastIndex = length.HasValue ? startIndex.Value - length.Value : 0;
        List<Task> tasks = new(list.Count);

        for (int i = startIndex.Value; i >= lastIndex; i--)
            tasks.Add(Task.Run(() => action(list[i])));

        Task.WaitAll([.. tasks]);
    }
    #endregion

    #region ConcurrentReverseForEach(this IList<T>, Action<T, int>, [int?], [int?])
    /// <summary>
    /// Asynchronously performs the specified action on each element of the list in the reverse order.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the list.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void ConcurrentReverseForEach<T>(
        this IList<T> list,
        Action<T, int> action,
        int? startIndex = null,
        int? length = null)
    {
        startIndex ??= list.LastIndex();
        int lastIndex = length.HasValue ? startIndex.Value - length.Value : 0;
        List<Task> tasks = new(list.Count);

        for (int i = startIndex.Value; i >= lastIndex; i--)
            tasks.Add(Task.Run(() => action(list[i], i)));

        Task.WaitAll([.. tasks]);
    }
    #endregion

    #endregion

    #region FillLeft*

    #region FillLeft(this IList<T>, int)
    /// <summary>
    /// Inserts the specified quantity of the specified value to the beginning of the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="quantity"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void FillLeft<T>(this IList<T> list, int quantity) =>
        list.FillLeft(default, quantity);
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
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "The fill quantity must be greater than zero (0).");

        for (int i = 0; i < quantity; i++)
            list.Insert(0, value);
    }
    #endregion

    #endregion

    #region FillRight*

    #region FillRight(this IList<T>, int)
    /// <summary>
    /// Inserts the specified quantity of the default value to the beginning of the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="quantity"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void FillRight<T>(this IList<T> list, int quantity) =>
    list.FillRight(default, quantity);
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
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "The fill quantity must be greater than zero (0).");

        for (int i = 0; i < quantity; i++)
            list.Add(value);
    }
    #endregion

    #endregion

    #region ForEach*

    #region ForEach(this IList<T>, Action<T>, int, [int?])
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
    public static void ForEach<T>(this IList<T> list, Action<T> action, int startIndex, int? length = null)
    {
        int finalIndex = length is null ? list.LastIndex() : startIndex + length.Value;

        for (int i = startIndex; i <= finalIndex; i++)
            action(list[i]);
    }
    #endregion

    #region ForEach(this IList<T>, Action<T, int>, [int], [int?])
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
        int finalIndex = length is null ? list.LastIndex() : startIndex + length.Value;

        for (int i = startIndex; i <= finalIndex; i++)
            action(list[i], i);
    }
    #endregion

    #endregion

    #region GetSublist
    /// <summary>
    /// Retrieves a sublist from the original <see cref="List{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static IList<T> GetSublist<T>(this IList<T> list, int startIndex, int? length = null)
    {
        int finalIndex = length.HasValue ? startIndex + length.Value : list.LastIndex();
        List<T> newList = new(finalIndex - startIndex);

        for (int i = startIndex; i < finalIndex; i++)
            newList.Add(list[i]);

        return newList;
    }
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
        items.ReverseForEach<T>(_ => list.Insert(startIndex, _));
    #endregion

    #region Last
    /// <summary>
    /// Retrieves the last item of the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T Last<T>(IList<T> list) =>
        list[list.LastIndex()];
    #endregion

    #region LastIndex
    /// <summary>
    /// Gets the last index of the current list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static int LastIndex<T>(this IList<T> list) =>
        list.Count - 1;
    #endregion

    #region ToArray
    /// <summary>
    /// Retrieves an array from the original <see cref="List{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static T[] ToArray<T>(this IList<T> list, int startIndex, int? length)
    {
        int finalIndex = length.HasValue ? startIndex + length.Value : list.LastIndex();
        T[] newArray = new T[finalIndex - startIndex];
        int index = 0;

        for (int i = startIndex; i < finalIndex; i++)
            newArray[index++] = list[i];

        return newArray;
    }
    #endregion

    #endregion

    #region IEnumarable

    #region All
    /// <summary>
    /// Determines whether all the elements of a sequence satisfy the specified condition.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool All<T>(this IEnumerable iEnumerable, Func<T, bool> predicate)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (T item in iEnumerable)
            if (!predicate(item))
                return false;

        return true;
    }
    #endregion

    #region Any*

    #region Any(this IEnumerable)
    /// <summary>
    /// Determines whether the current <see cref="IEnumerable" /> is defined and contains any item.
    /// </summary>
    /// <param name="iEnumerable"></param>
    /// <returns></returns>
    public static bool Any(this IEnumerable iEnumerable) => 
        iEnumerable is not null && Enumerable.Any(iEnumerable.Cast<object>());

    #endregion

    #region Any(this IEnumerable, Func<T, bool>)
    /// <summary>
    /// Determines whether any of the elements of a sequence satisfies the specified condition.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool Any<T>(this IEnumerable iEnumerable, Func<T, bool> predicate)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (T item in iEnumerable)
            if (predicate(item))
                return true;

        return false;
    }
    #endregion

    #endregion

    #region Cast
    /// <summary>
    /// Casts the elements of an <see cref="IEnumerable"/> to a specific type.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="converterFunction"></param>
    /// <returns></returns>
    public static IEnumerable Cast<T1, T2>(this IEnumerable iEnumerable, Func<T1, T2> converterFunction) =>
        iEnumerable.Select(converterFunction);
    #endregion

    #region ConcurrentForEach
    /// <summary>
    /// Runs the specified <see cref="Action"/> for each item of the collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void ConcurrentForEach<T>(this IEnumerable iEnumerable, Action<T> action) =>
        Task.WaitAll([.. from T item in iEnumerable select Task.Run(() => action(item))]);
    #endregion

    #region First
    /// <summary>
    /// Returns the first element in a sequence that satisfies a specified condition.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static T First<T>(this IEnumerable iEnumerable, Func<T, bool> predicate) =>
        iEnumerable.FirstOrDefault(predicate) ?? throw new InvalidOperationException("Sequence contains no matching element");
    #endregion

    #region FirstOrDefault
    /// <summary>
    /// Returns the first element in a sequence that satisfies a specified condition, or a default value if no such element is found.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static T FirstOrDefault<T>(this IEnumerable iEnumerable, Func<T, bool> predicate)
    {
        foreach (T item in iEnumerable)
            if (predicate(item))
                return item;

        return default;
    }
    #endregion

    #region ForEach*

    #region ForEach<T>
    /// <summary>
    /// Runs the specified <see cref="Action"/> for each item of the <see cref="IEnumerable"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void ForEach<T>(this IEnumerable iEnumerable, Action<T> action)
    {
        foreach (T item in iEnumerable)
            action(item);
    }
    #endregion

    #endregion

    #region Select
    /// <summary>
    /// Projects each element of a sequence into a new form.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static IEnumerable<T2> Select<T1, T2>(this IEnumerable iEnumerable, Func<T1, T2> selector)
    {
        List<T2> items = [];
        iEnumerable.ForEach<T1>(_ => items.Add(selector(_)));
        return items;
    }
    #endregion

    #region ToList
    /// <summary>
    /// Create a <see cref="List{T}"/> out of the specific type from the <see cref="IEnumerable"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <returns></returns>
    public static List<T> ToList<T>(this IEnumerable iEnumerable)
    {
        List<T> result = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (T item in iEnumerable)
            result.Add(item);

        return result;
    }
    #endregion

    #region Where
    /// <summary>
    /// Filters a sequence of values based on a predicate.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IEnumerable Where<T>(this IEnumerable iEnumerable, Func<T, bool> predicate)
    {
        List<T> items = [];

        iEnumerable.ForEach<T>(_ =>
        {
            if (predicate(_))
                items.Add(_);
        });

        return items;
    }
    #endregion

    #endregion

    #region IEnumerable<T>

    #region Cast
    /// <summary>
    /// Casts the elements of an <see cref="IEnumerable{T}"/> to a specific type.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="converterFunction"></param>
    /// <returns></returns>
    public static IEnumerable<T2> Cast<T1, T2>(this IEnumerable<T1> iEnumerable, Func<T1, T2> converterFunction) =>
        iEnumerable.Select(converterFunction);
    #endregion

    #region ConcurrentForEach*

    #region ConcurrentForEach(this IEnumerable<T>, Action<T>)
    /// <summary>
    /// Runs the specified <see cref="Action"/> for each item of the collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void ConcurrentForEach<T>(this IEnumerable<T> iEnumerable, Action<T> action) =>
        Task.WaitAll([.. iEnumerable.Select(item => Task.Run(() => action(item)))]);
    #endregion

    #region ConcurrentForEach(this IEnumerable<T>, Action<T, int>)
    /// <summary>
    /// Runs the specified <see cref="Action"/> for each item of the collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void ConcurrentForEach<T>(this IEnumerable<T> iEnumerable, Action<T, int> action) =>
        Task.WaitAll([.. iEnumerable.Select((item, index) => Task.Run(() => action(item, index)))]);
    #endregion

    #endregion

    #region ContainsAll
    /// <summary>
    /// Indicates whether the <see cref="IEnumerable{T}"/> contains all the specified items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="items"></param>
    /// <returns>True, if the enumeration contains all the specified items. False, otherwise.</returns>
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

    #region ForEach*

    #region ForEach(this IEnumerable<T>, Action<T>)
    /// <summary>
    /// Runs the specified <see cref="Action"/> for each item of the collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void ForEach<T>(this IEnumerable<T> iEnumerable, Action<T> action)
    {
        foreach (T item in iEnumerable)
            action(item);
    }
    #endregion

    #region ForEach(this IEnumerable<T>, Action<T, int>)
    /// <summary>
    /// Runs the specified <see cref="Action"/> for each item of the collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void ForEach<T>(this IEnumerable<T> iEnumerable, Action<T, int> action)
    {
        int index = 0;

        foreach (T item in iEnumerable)
            action(item, index++);
    }
    #endregion

    #region Select
    /// <summary>
    /// Projects each element of a sequence into a new form.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static IEnumerable<T2> Select<T1, T2>(this IEnumerable<T1> iEnumerable, Func<T1, int, T2> selector)
    {
        List<T2> items = [];
        iEnumerable.ForEach((item, index) => items.Add(selector(item, index)));
        return items;
    }
    #endregion

    #endregion

    #region IndexOf
    /// <summary>
    /// Searches for the specified object and returns the zero-based index of the first occurence within the entire <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static int IndexOf<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
    {
        int index = 0;

        foreach (T item in collection)
        {
            if (predicate(item))
                return index;

            index++;
        }

        return -1;
    }
    #endregion

    #region IsEmpty
    /// <summary>
    /// Indicates whether the current <see cref="IEnumerable{T}"/> is empty.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static bool IsEmpty<T>(this IEnumerable<T> collection) =>
        !collection.Any();
    #endregion

    #region IsNull
    /// <summary>
    /// Indicates whether the current <see cref="IEnumerable{T}"/> is null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static bool IsNull<T>(this IEnumerable<T> collection) =>
        collection is null;
    #endregion

    #region IsNullOrEmpty
    /// <summary>
    /// Indicates whether the <see cref="IEnumerable{T}"/> is null or empty.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> iEnumerable) =>
        iEnumerable?.Any() != true;
    #endregion

    #region ToArray
    /// <summary>
    /// Creates a T array out of the specific type from the <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static T2[] ToArray<T1, T2>(this IEnumerable<T1> iEnumerable, Func<T1, T2> func) =>
        [.. iEnumerable.Select(func)];
    #endregion

    #region ToList 
    /// <summary>
    /// Creates a <see cref="List{T}"/> out of the specific type from the <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static List<T2> ToList<T1, T2>(this IEnumerable<T1> iEnumerable, Func<T1, T2> func) =>
        iEnumerable.Select(func).ToList();
    #endregion

    #region Sum
    /// <summary>
    /// Computes the sequence of <see cref="TimeSpan"/> values that are obtained by invoking a transform function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static TimeSpan Sum<T>(this IEnumerable<T> source, Func<T, TimeSpan> selector)
    {
        TimeSpan result = TimeSpan.Zero;
        source.ForEach(_ => result += selector(_));

        return result;
    }
    #endregion

    #endregion

    #endregion
}