using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    /// Removes the first element that matches the specified criteria.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool Remove<T>(this ICollection<T> list, Func<T, bool> predicate) =>
        list.FirstOrDefault(predicate) is { } itemToRemove && list.Remove(itemToRemove);
    #endregion

    #region RemoveAll
    /// <summary>
    /// Removes every element that matches the specified criteria.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="action"></param>
    /// <returns>The number of items removed.</returns>
    public static int RemoveAll<T>(this ICollection<T> list, Func<T, bool> action)
    {
        int itemsRemovedCount = 0;
        T[] originalItems = list.ToArray();

        originalItems.ForEach(_ =>
        {
            if (action(_))
            {
                list.Remove(_);
                itemsRemovedCount++;
            }
        });

        return itemsRemovedCount;
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
        int finalIndex = length is null ? list.LastIndex() : startIndex + length.Value;

        for (int i = startIndex; i < finalIndex; i++)
            action?.Invoke(list[i], i);
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
        items.ReverseForEach(_ => list.Insert(startIndex, _));
    #endregion

    #region Move
    /// <summary>
    /// Moves the item at the specified source index to the specified destination index within the current <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="item"></param>
    /// <param name="destinationIndex"></param>
    /// <returns>true if the item is successful moved inside the <see cref="IList{T}"/>; otherwise, false. This method returns false if the item is not found in the original <see cref="IList{T}"/>.</returns>
    public static bool Move<T>(this IList<T> list, T item, int destinationIndex)
    {
        if (list.Remove(item))
        {
            list.Insert(destinationIndex, item);
            return true;
        }

        return false;
    }
    #endregion

    #region MoveAt
    /// <summary>
    /// Moves the item at the specified source index to the specified destination index within the current <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="currentIndex"></param>
    /// <param name="destinationIndex"></param>
    public static void MoveAt<T>(this IList<T> list, int currentIndex, int destinationIndex)
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
    /// <returns>true if the item is successful moved inside the <see cref="IList{T}"/>; otherwise, false. This method returns false if the item is not found in the original <see cref="IList{T}"/>.</returns>
    public static bool MoveDown<T>(this IList<T> list, T item)
    {
        int index = list.IndexOf(item);

        if (index < 0)
            return false;

        list.MoveDownAt(index);
        return true;
    }
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

        list.MoveAt(index, index + 1);
    }
    #endregion

    #region MoveUp
    /// <summary>
    /// Moves the item one position up within the current <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="item"></param>
    /// <returns>true if the item is successful moved inside the <see cref="IList{T}"/>; otherwise, false. This method returns false if the item is not found in the original <see cref="IList{T}"/>.</returns>
    public static bool MoveUp<T>(this IList<T> list, T item)
    {
        int index = list.IndexOf(item);

        if (index < 0)
            return false;

        list.MoveUpAt(index);
        return true;
    }
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

        list.MoveAt(index, index - 1);
    }
    #endregion

    #region ReverseForEach

    #region ReverseForEach(this IList<T>, Action<T>, [int?], [int?])
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
    public static void ReverseForEach<T>(this IList<T> list, Action<T> action, int? startIndex = null, int? length = null)
    {
        startIndex ??= list.LastIndex();
        int lastIndex = length.HasValue ? startIndex.Value - length.Value : 0;

        for (int i = startIndex.Value; i > lastIndex; i--)
            action?.Invoke(list[i]);
    }
    #endregion

    #region ReverseForEach(this IList<T>, Action<T, int>, [int?], [int?])
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
    public static void ReverseForEach<T>(this IList<T> list, Action<T, int> action, int? startIndex = null, int? length = null)
    {
        startIndex ??= list.LastIndex();
        int lastIndex = length.HasValue ? startIndex.Value - length.Value : 0;

        for (int i = startIndex.Value; i > lastIndex; i--)
            action?.Invoke(list[i], i);
    }
    #endregion

    #endregion

    #endregion

    #region IEnumerable<T>

    #region Cast
    /// <summary>
    /// Casts the elements of an <see cref="IEnumerable{T}"/> to an specific type.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static IEnumerable<T2> Cast<T1, T2>(this IEnumerable<T1> iEnumerable, Func<T1, T2> func) =>
        iEnumerable.Select(func);
    #endregion

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

    #region ForEach

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
            action(value);
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
            action(value, index++);
    }

    #endregion

    #endregion

    #region ForEachAsync

    #region ForEachAsync<T>(this IEnumerable<T>, Action<T>)
    /// <summary>
    /// Asynchronously performs the specified action on each element of the array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="action"></param>
    /// <param name="callback">Action to be executed right after every execution has finished.</param>
    public static void ForEachAsync<T>(this IEnumerable<T> iEnumerable, Action<T> action, Action callback = null)
    {
        iEnumerable.Select(value => Task.Run(() => action(value))).ForEach(_ => _.Wait());
            callback?.Invoke();
    }
    #endregion

    #region ForEachAsync<T>(this IEnumerable<T>, Action<T, int>)
    /// <summary>
    /// Asynchronously performs the specified action on each element of the array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="action"></param>
    /// <param name="callback">Action to be executed right after every execution has finished.</param>
    public static void ForEachAsync<T>(this IEnumerable<T> iEnumerable, Action<T, int> action, Action callback = null)
    {
        int index = 0;
        iEnumerable.Select(value => Task.Run(() => action(value, index++))).ForEach(_ => _.Wait());
            callback?.Invoke();
        }
    #endregion

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
        iEnumerable.Select(func).ToArray();
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

    #endregion

    #endregion
}