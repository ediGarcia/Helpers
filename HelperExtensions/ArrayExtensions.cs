using System;

// ReSharper disable UnusedMember.Global
namespace HelperExtensions;

// ReSharper disable once UnusedMember.Global
public static class ArrayExtensions
{
    #region Public methods

    #region ArrayForEach

    #region ArrayForEach<T>(this T[], Action<T>, [int], [int?])
    /// <summary>
    /// Performs the specified action on each element of the array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the array.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void ArrayForEach<T>(this T[] array, Action<T> action, int startIndex = 0, int? length = null)
    {
        length ??= array.Length - startIndex;

        for (int i = startIndex; i < startIndex + length; i++)
            action?.Invoke(array[i]);
    }
    #endregion

    #region ArrayForEach<T>(this T[], Action<T, int>, [int], [int?])
    /// <summary>
    /// Performs the specified action on each element of the array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the array.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="action" /> is <see langword="null" />.</exception>
    /// <exception cref="T:System.InvalidOperationException">An element in the collection has been modified.</exception>
    public static void ArrayForEach<T>(this T[] array, Action<T, int> action, int startIndex = 0, int? length = null)
    {
        length ??= array.Length - startIndex;

        for (int i = startIndex; i < startIndex + length; i++)
            action?.Invoke(array[i], i);
    }
    #endregion

    #endregion

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
    /// Returns the last index of the array.
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static int LastIndex(this Array array) =>
        array.Length - 1;
    #endregion

    #endregion
}