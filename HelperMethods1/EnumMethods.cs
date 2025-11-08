using System;

namespace HelperMethods;

public static class EnumMethods
{
    #region AddFlag*

    #region AddFlag(T, T)
    /// <summary>
    /// Returns a value that contains the source value plus the specified flag.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="flag"></param>
    /// <returns></returns>
    public static T AddFlag<T>(T source, T flag) where T : Enum =>
        (T)(object)((int)(source as object) | (int)(flag as object));
    #endregion

    #region AddFlag(Enum, Enum)
    /// <summary>
    /// Returns a value that contains the source value plus the specified flag.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="flag"></param>
    /// <returns></returns>
    public static Enum AddFlag(Enum source, Enum flag) =>
        AddFlag<Enum>(source, flag);
    #endregion

    #endregion

    #region RemoveFlag*

    #region RemoveFlag(T, T)
    /// <summary>
    /// Returns a value that contains the source value without the specified flag.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="flag"></param>
    /// <returns></returns>
    public static T RemoveFlag<T>(T source, T flag) where T : Enum =>
        (T)(object)((int)(source as object) & ~(int)(flag as object));
    #endregion

    #region RemoveFlag(Enum, Enum)
    /// <summary>
    /// Returns a value that contains the source value without the specified flag.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="flag"></param>
    /// <returns></returns>
    public static Enum RemoveFlag(Enum source, Enum flag) =>
        RemoveFlag<Enum>(source, flag);
    #endregion

    #endregion

    #region ToEnum*

    #region ToEnum(int)
    /// <summary>
    /// Converts the specified value to an enumeration member.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static T ToEnum<T>(object value) where T : Enum =>
        (T)Enum.ToObject(typeof(T), value);
    #endregion

    #region ToEnum(Type, int)
    /// <summary>
    /// Converts the specified value to an enumeration member.
    /// </summary>
    /// <param name="enumType"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Enum ToEnum(Type enumType, object value) =>
        Enum.ToObject(enumType, value) as Enum;
    #endregion

    #endregion
}
