using System;
// ReSharper disable UnusedMember.Global

namespace HelperExtensions;

public static class TypeExtensions
{
    #region Public Methods

    #region GetCode
    /// <summary>
    /// Returns the <see cref="TypeCode"/> for the current <see cref="Type"/>.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static TypeCode GetCode(this Type type) =>
        Type.GetTypeCode(type);
    #endregion

    #region IsBuiltIn
    /// <summary>
    /// Indicates whether the specified type is a built-in C# type.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsBuiltIn(this Type type) =>
        type.Namespace == "System";
    #endregion

    #region IsFloatingPoint
    /// <summary>
    /// Indicates whether the current <see cref="Type"/> represents a floating point numeric value.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsFloatingPoint(this Type type) =>
        type.GetCode() is TypeCode.Single or TypeCode.Decimal or TypeCode.Double;
    #endregion

    #region IsInteger
    /// <summary>
    /// Indicates whether the current <see cref="Type"/> represents a integer numeric value.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsInteger(this Type type) =>
        type.GetCode() is TypeCode.SByte or TypeCode.Byte or TypeCode.Int16 or TypeCode.Int32 or TypeCode.Int64
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