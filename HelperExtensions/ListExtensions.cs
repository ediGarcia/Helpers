using System.Collections;
using HelperMethods;

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
        Array.Copy(array, startIndex, newArray, 0, length.Value);

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
    public static int LastIndex<T>(this T[] array) => array.Length - 1;
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
    public static ICollection<T> Clone<T>(this ICollection<T> collection) => [.. collection];
    #endregion

    #region IsEmpty
    /// <summary>
    /// Indicates whether the current collection is empty.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static bool IsEmpty<T>(this ICollection<T> collection) => collection.Count == 0;
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

    #region IsNullOrEmpty
    /// <summary>
    /// Indicates whether the current collection is null or empty.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this ICollection<T> collection) =>
        collection == null || collection.Count == 0;
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
        List<T> itemsToRemove = collection.Where(predicate).ToList<T>();

        foreach (T item in itemsToRemove)
            collection.Remove(item);

        return itemsToRemove.Count;
    }
    #endregion

    #region RemoveMany
    /// <summary>
    /// Removes the first occurrence of each value from the <see cref="ICollection{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="values"></param>
    public static void RemoveMany<T>(this ICollection<T> collection, params T[] values) =>
        values.ForEach(_ => collection.Remove(_));
    #endregion

    #region TryRemoveFirst
    /// <summary>
    /// Removes the first element that matches the specified criteria.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool TryRemoveFirst<T>(
        this ICollection<T> collection,
        Func<T?, bool> predicate
    ) =>
        collection.FirstOrDefault(predicate) is { } itemToRemove && collection.Remove(itemToRemove);
    #endregion

    #endregion

    #region IDictionary<TKey, TValue>

    #region Clone
    /// <summary>
    /// Creates a copy of the current dictionary.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    public static Dictionary<TKey, TValue> Clone<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary
    )
        where TKey : notnull => new(dictionary);
    #endregion

    #region ForEach*

    #region ForEach(this IDictionary<TKey, TValue>, Action<TKey, TValue>, [int], [int?])
    /// <summary>
    /// Performs the specified action on each element of the dictionary.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the list.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void ForEach<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        Action<TKey, TValue> action,
        int startIndex = 0,
        int? length = null
    )
    {
        TKey[] keys = [.. dictionary.Keys];
        TValue[] values = [.. dictionary.Values];
        length ??= dictionary.Count - startIndex;

        for (int i = startIndex; i < startIndex + length; i++)
            action(keys[i], values[i]);
    }
    #endregion

    #region ForEach(this IDictionary<TKey, TValue>, Action<TKey, TValue, int>, [int], [int?])
    /// <summary>
    /// Performs the specified action on each element of the dictionary.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the list.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void ForEach<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        Action<TKey, TValue, int> action,
        int startIndex = 0,
        int? length = null
    )
    {
        TKey[] keys = [.. dictionary.Keys];
        TValue[] values = [.. dictionary.Values];
        length ??= dictionary.Count - startIndex;

        for (int i = startIndex; i < startIndex + length; i++)
            action(keys[i], values[i], i);
    }
    #endregion

    #endregion

    #region GetOrAdd

    #region GetOrAdd(this IDictionary<TKey, TValue>, TKey, [TValue])
    /// <summary>
    /// Gets the value associated with the specified key. If the key doesn't exist, it's created with the <see cref="defaultValue"/>.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static TValue GetOrAdd<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        TKey key,
        TValue defaultValue = default
    )
    {
        if (!dictionary.TryGetValue(key, out TValue value))
            dictionary.Add(key, value = defaultValue);

        return value;
    }
    #endregion

    #region GetOrAdd(this IDictionary<TKey, TValue>, TKey, Func<TValue>)
    /// <summary>
    /// Gets the value associated with the specified key. If the key doesn't exist, it's created with the value returned by the <see cref="valueFactory"/>.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="valueFactory"></param>
    /// <returns></returns>
    public static TValue GetOrAdd<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        TKey key,
        Func<TValue> valueFactory
    )
    {
        if (!dictionary.TryGetValue(key, out TValue value))
            dictionary.Add(key, value = valueFactory());

        return value;
    }
    #endregion

    #endregion

    #region ReverseForEach*

    #region ReverseForEach(this IDictionary<TKey, TValue>, Action<TKey, TValue>, [int?], [int?])
    /// <summary>
    /// Performs the specified action on each element of the dictionary in the inverse order.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the list.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void ReverseForEach<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        Action<TKey, TValue> action,
        int? startIndex = null,
        int? length = null
    )
    {
        TKey[] keys = [.. dictionary.Keys];
        TValue[] values = [.. dictionary.Values];

        startIndex ??= keys.Length - 1;
        length ??= startIndex + 1;

        for (int i = startIndex.Value; i > startIndex - length; i--)
            action?.Invoke(keys[i], values[i]);
    }
    #endregion

    #region ReverseForEach(this IDictionary<TKey, TValue>, Action<TKey, TValue, int>, [int?], [int?])
    /// <summary>
    /// Performs the specified action on each element of the dictionary in the inverse order.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the list.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void ReverseForEach<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        Action<TKey, TValue, int> action,
        int? startIndex = null,
        int? length = null
    )
    {
        TKey[] keys = [.. dictionary.Keys];
        TValue[] values = [.. dictionary.Values];

        startIndex ??= keys.LastIndex();
        length ??= startIndex + 1;

        for (int i = startIndex.Value; i > startIndex - length; i--)
            action?.Invoke(keys[i], values[i], i);
    }
    #endregion

    #endregion

    #region TrySetValue
    /// <summary>
    /// Tries to set the value for the specified key.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns>true, if the key exists and the value is property set; false otherwise.</returns>
    public static bool TrySetValue<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        TKey key,
        TValue value
    )
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] = value;
            return true;
        }

        return false;
    }
    #endregion

    #endregion

    #region IList

    #region Clone
    /// <summary>
    /// Creates a copy of the current list.
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static IList Clone(this IList list) => list.ToList<object>();
    #endregion

    #region GetItem
    /// <summary>
    /// Retrieves an item converted to the specified type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static T GetItem<T>(this IList list, int index) => (T)list[index];
    #endregion

    #region GetRandomItem
    /// <summary>
    /// Retrieves a random item from the current collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T GetRandomItem<T>(this IList list) =>
        (T)list[NumberHelper.GetRandomInt(list.Count)];
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
    public static void MoveDownAt(this IList list, int index) => list.MoveAt(index, index + 1);
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
    public static void MoveUpAt(this IList list, int index) => list.MoveAt(index, index - 1);

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
    public static void ReverseForEach<T>(
        this IList list,
        Action<T> action,
        int? startIndex = null,
        int? length = null
    )
    {
        startIndex ??= list.Count - 1;
        int lastIndex = length.HasValue ? startIndex.Value - length.Value : 0;

        for (int i = startIndex.Value; i >= lastIndex; i--)
            action((T)list[i]);
    }
    #endregion

    #region ReverseForEach(this IList, Action<T, int>, [int?], [int?])
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
    public static void ReverseForEach<T>(
        this IList list,
        Action<T, int> action,
        int? startIndex = null,
        int? length = null
    )
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
    public static bool All<T>(
        this IList<T> list,
        Func<T, bool> predicate,
        int startIndex,
        int? length = null
    )
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
    public static bool Any<T>(
        this IList<T> list,
        Func<T, bool> predicate,
        int startIndex,
        int? length = null
    )
    {
        int finalIndex = length.HasValue ? startIndex + length.Value : list.LastIndex();

        for (int i = startIndex; i < finalIndex; i++)
            if (predicate(list[i]))
                return true;

        return false;
    }
    #endregion

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
    public static T BinarySearch<T, TKey>(this IList<T> list, TKey value, Func<T, TKey> selector)
        where TKey : IComparable<TKey> => ListHelper.BinarySearch(list, value, selector);
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
    public static int BinarySearchIndex<T, TKey>(
        this IList<T> list,
        TKey value,
        Func<T, TKey> selector
    )
        where TKey : IComparable<TKey> => ListHelper.BinarySearchIndex(list, value, selector);
    #endregion

    #region Clone
    /// <summary>
    /// Creates a copy of the current collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IList<T> Clone<T>(this IList<T> iList) => [.. iList];
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
    public static void FillLeft<T>(this IList<T?> list, int quantity) =>
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
            throw new ArgumentOutOfRangeException(
                nameof(quantity),
                "The fill quantity must be greater than zero (0)."
            );

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
    public static void FillRight<T>(this IList<T?> list, int quantity) =>
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
            throw new ArgumentOutOfRangeException(
                nameof(quantity),
                "The fill quantity must be greater than zero (0)."
            );

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
    public static void ForEach<T>(
        this IList<T> list,
        Action<T> action,
        int startIndex,
        int? length = null
    )
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
    public static void ForEach<T>(
        this IList<T> list,
        Action<T, int> action,
        int startIndex = 0,
        int? length = null
    )
    {
        int finalIndex = length is null ? list.LastIndex() : startIndex + length.Value;

        for (int i = startIndex; i <= finalIndex; i++)
            action(list[i], i);
    }
    #endregion

    #endregion

    #region GetRandomItem
    /// <summary>
    /// Retrieves a random item from the current collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T GetRandomItem<T>(this IList<T> list) =>
        list[NumberHelper.GetRandomInt(list.Count)];
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

    #region Last
    /// <summary>
    /// Retrieves the last item of the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T Last<T>(this IList<T> list) => list[list.LastIndex()];
    #endregion

    #region LastIndex
    /// <summary>
    /// Gets the last index of the current list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static int LastIndex<T>(this IList<T> list) => list.Count - 1;
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

    #region Queue

    #region EnqueueMany
    /// <summary>
    /// Adds the specified values to the end of the <see cref="Queue{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queue"></param>
    /// <param name="values"></param>
    public static void EnqueueMany<T>(this Queue<T> queue, params T[] values) =>
        values.ForEach(queue.Enqueue);
    #endregion

    #region EnqueueRange
    /// <summary>
    /// Adds the specified values to the end of the <see cref="Queue{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queue"></param>
    /// <param name="values"></param>
    public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> values) =>
        values.ForEach(queue.Enqueue);
    #endregion

    #endregion

    #region Stack

    #region PushMany
    /// <summary>
    /// Inserts the specified values to the top of the <see cref="Stack{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stack"></param>
    /// <param name="values"></param>
    public static void PushMany<T>(this Stack<T> stack, params T[] values) =>
        values.ForEach(stack.Push);
    #endregion

    #region PushRange
    /// <summary>
    /// Inserts the specified values to the top of the <see cref="Stack{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stack"></param>
    /// <param name="values"></param>
    public static void PushRange<T>(this Stack<T> stack, IEnumerable<T> values) =>
        values.ForEach(stack.Push);
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
    public static bool Any<T>(this IEnumerable iEnumerable, Func<T, bool> predicate) =>
        iEnumerable is not null && iEnumerable.Cast<T>().Any(item => predicate(item));
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
    public static IEnumerable Cast<T1, T2>(
        this IEnumerable iEnumerable,
        Func<T1, T2> converterFunction
    ) => iEnumerable.Select(converterFunction);
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
    public static T First<T>(this IEnumerable iEnumerable, Func<T?, bool> predicate) =>
        iEnumerable.FirstOrDefault(predicate)
        ?? throw new InvalidOperationException("Sequence contains no matching element");
    #endregion

    #region FirstOrDefault
    /// <summary>
    /// Returns the first element in a sequence that satisfies a specified condition, or a default value if no such element is found.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="predicate"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static T FirstOrDefault<T>(
        this IEnumerable iEnumerable,
        Func<T, bool> predicate,
        T defaultValue = default
    )
    {
        foreach (T item in iEnumerable)
            if (predicate(item))
                return item;

        return defaultValue;
    }
    #endregion

    #region ForEach
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

    #region IsEmpty
    /// <summary>
    /// Indicates whether the current sequence is empty.
    /// </summary>
    /// <param name="iEnumerable"></param>
    /// <returns></returns>
    public static bool IsEmpty(this IEnumerable iEnumerable) =>
        iEnumerable != null && !iEnumerable.Any();
    #endregion

    #region IsNull
    /// <summary>
    /// Indicates whether the current sequence is null.
    /// </summary>
    /// <param name="iEnumerable"></param>
    /// <returns></returns>
    public static bool IsNull(this IEnumerable iEnumerable) => iEnumerable is null;
    #endregion

    #region IsNullOrEmpty
    /// <summary>
    /// Indicates whether the current sequence is null or empty.
    /// </summary>
    /// <param name="iEnumerable"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this IEnumerable iEnumerable) => iEnumerable?.Any() != true;
    #endregion

    #region Join*

    #region Join(this IEnumerable, char)
    /// <summary>
    /// Concatenates the members of a collection, using the specified separator between each member.
    /// </summary>
    /// <param name="iEnumerable"></param>
    /// <param name="separator"></param>
    /// <returns>A string that consists of the member of the value delimited by the separator char. If the collection has no members, the method returns <see cref="String.Empty"/>.</returns>
    public static string Join(this IEnumerable iEnumerable, char separator) =>
        String.Join($"{separator}", iEnumerable);
    #endregion

    #region Join(this IEnumerable, string)
    /// <summary>
    /// Concatenates the members of a collection, using the specified separator between each member.
    /// </summary>
    /// <param name="iEnumerable"></param>
    /// <param name="separator"></param>
    /// <returns>A string that consists of the member of the value delimited by the separator string. If the collection has no members, the method returns <see cref="String.Empty"/>.</returns>
    public static string Join(this IEnumerable iEnumerable, string separator) =>
        String.Join(separator, iEnumerable);
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
    public static IEnumerable<T2> Select<T1, T2>(
        this IEnumerable iEnumerable,
        Func<T1, T2> selector
    )
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
    public static IEnumerable<T2> Cast<T1, T2>(
        this IEnumerable<T1> iEnumerable,
        Func<T1, T2> converterFunction
    ) => iEnumerable.Select(converterFunction);
    #endregion

    #region Clone
    /// <summary>
    /// Creates a copy of the current <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <returns></returns>
    public static IEnumerable<T> Clone<T>(this IEnumerable<T> iEnumerable) => [.. iEnumerable];
    #endregion

    #region ContainsAll
    /// <summary>
    /// Indicates whether the <see cref="IEnumerable{T}"/> contains all the specified items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="items"></param>
    /// <returns>True, if the enumeration contains all the specified items. False, otherwise.</returns>
    public static bool ContainsAll<T>(this IEnumerable<T> iEnumerable, params T[] items)
    {
        if (items.Length == 0)
            return true;

        HashSet<T> sourceSet = iEnumerable as HashSet<T> ?? iEnumerable.ToHashSet();
        return items.All(sourceSet.Contains);
    }
    #endregion

    #region ContainsAny
    /// <summary>
    /// Indicates whether the <see cref="IEnumerable{T}"/> contains any the specified items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="items"></param>
    /// <returns>True, if the enumeration contains any of the specified items. False, otherwise.</returns>
    public static bool ContainsAny<T>(this IEnumerable<T> iEnumerable, params T[] items)
    {
        if (items.Length == 0)
            return true;

        HashSet<T> sourceSet = iEnumerable as HashSet<T> ?? iEnumerable.ToHashSet();
        return items.Any(sourceSet.Contains);
    }
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

    #endregion

    #region IndexOf
    /// <summary>
    /// Determines the index of the first item that matches the condition in the sequence.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static int IndexOf<T>(this IEnumerable<T> iEnumerable, Func<T, bool> predicate)
    {
        int index = 0;

        foreach (T item in iEnumerable)
        {
            if (predicate(item))
                return index;

            index++;
        }

        return -1;
    }
    #endregion

    #region None
    /// <summary>
    /// Determines whether no elements of a sequence satisfy a condition.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool None<T>(this IEnumerable<T> iEnumerable, Func<T, bool> predicate) =>
        iEnumerable.All(item => !predicate(item));
    #endregion

    #region ParallelAll
    /// <summary>
    /// Determines whether every the element of the sequence satisfies a condition. The process may run in parallel for each item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="predicate"></param>
    /// <returns>true if every the element in the source pass the test in the specified predicate; otherwise, false.</returns>
    public static bool ParallelAll<T>(this IEnumerable<T> iEnumerable, Func<T, bool> predicate) =>
        iEnumerable.AsParallel().All(predicate);
    #endregion

    #region ParallelAny
    /// <summary>
    /// Determines whether any element of the sequence satisfies a condition. The process may run in parallel for each item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="predicate"></param>
    /// <returns>true if any elements in the source pass the test in the specified predicate; otherwise, false.</returns>
    public static bool ParallelAny<T>(this IEnumerable<T> iEnumerable, Func<T, bool> predicate) =>
        iEnumerable.AsParallel().Any(predicate);
    #endregion

    #region ParallelCount
    /// <summary>
    /// Returns the count of elements in a sequence. The process may run in parallel for each item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="predicate"></param>
    /// <returns>The number of elements in the input sequence.</returns>
    public static int ParallelCount<T>(this IEnumerable<T> iEnumerable, Func<T, bool> predicate) =>
        iEnumerable.AsParallel().Count(predicate);
    #endregion

    #region ParallelForEach*

    #region ParallelForEach(this IEnumerable<T>, Action<T>)
    /// <summary>
    /// Executes a foreach (For Each in Visual Basic) operation in an <see cref="IEnumerable{T}"/> in which iterations may run in parallel.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="body"></param>
    /// /// <returns>A structure that contains information about which portion of the loop completed.</returns>
    public static ParallelLoopResult ParallelForEach<T>(
        this IEnumerable<T> iEnumerable,
        Action<T> body
    ) => Parallel.ForEach(iEnumerable, body);
    #endregion

    #region ParallelForEach(this IEnumerable<T>, Action<T, ParallelLoopState>)
    /// <summary>
    /// Executes a foreach (For Each in Visual Basic) operation in an <see cref="IEnumerable{T}"/> in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="body"></param>
    /// <returns>A structure that contains information about which portion of the loop completed.</returns>
    public static ParallelLoopResult ParallelForEach<T>(
        this IEnumerable<T> iEnumerable,
        Action<T, ParallelLoopState> body
    ) => Parallel.ForEach(iEnumerable, body);
    #endregion

    #endregion

    #region Sum<T>(this IEnumerable<T>, Func<T, TimeSpan>)
    /// <summary>
    /// Calculates the sum of <see cref="TimeSpan"/> values projected from elements in a sequence.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <param name="iEnumerable">The sequence of elements to aggregate.</param>
    /// <param name="selector">A function that projects each element of the sequence into a <see cref="TimeSpan"/> value.</param>
    /// <returns>The total <see cref="TimeSpan"/> resulting from summing the projected values.</returns>
    public static TimeSpan Sum<T>(this IEnumerable<T> iEnumerable, Func<T, TimeSpan> selector) =>
        iEnumerable.Aggregate(TimeSpan.Zero, (current, item) => current + selector(item));
    #endregion

    #region ToArray
    /// <summary>
    /// Creates a T array out of the specific type from the <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="conversionFunction"></param>
    /// <returns></returns>
    public static T2[] ToArray<T1, T2>(
        this IEnumerable<T1> iEnumerable,
        Func<T1, T2> conversionFunction
    ) => [.. iEnumerable.Select(conversionFunction)];
    #endregion

    #region ToList
    /// <summary>
    /// Creates a <see cref="List{T}"/> out of the specific type from the <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="iEnumerable"></param>
    /// <param name="conversionFunction"></param>
    /// <returns></returns>
    public static List<T2> ToList<T1, T2>(
        this IEnumerable<T1> iEnumerable,
        Func<T1, T2> conversionFunction
    ) => [.. iEnumerable.Select(conversionFunction)];
    #endregion

    #endregion

    #endregion
}
