using System;

namespace HelperExtensions;

// ReSharper disable once UnusedMember.Global
public static class TypeExtensions
{
    #region Public Methods

    #region GetTypeCode
    /// <summary>
    /// Returns the <see cref="TypeCode"/> for the current <see cref="Type"/>.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static TypeCode GetTypeCode(this Type type) =>
        Type.GetTypeCode(type);
    #endregion

    #region IsFloatingPoint

    /// <summary>
    /// Indicates whether the current <see cref="Type"/> represents a floating point numeric value.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsFloatingPoint(this Type type) =>
        type.GetTypeCode() is TypeCode.Single or TypeCode.Decimal or TypeCode.Double;
    #endregion

    #region IsInteger

    /// <summary>
    /// Indicates whether the current <see cref="Type"/> represents a integer numeric value.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsInteger(this Type type) =>
        type.GetTypeCode() is TypeCode.SByte or TypeCode.Byte or TypeCode.Int16 or TypeCode.Int32 or TypeCode.Int64
            or TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64;
    #endregion

    #region IsNumeric
    /// <summary>
    /// Indicates whether the current <see cref="Type"/> corresponds to a numeric value.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsNumeric(this Type type) =>
        type.IsInteger() || type.IsFloatingPoint();
    #endregion

    #endregion
}