using System;
using System.Reflection;

// ReSharper disable UnusedMember.Global

namespace HelperExtensions;

public static class TypeExtensions
{
    #region Public Methods

    #region Type

    #region ContainsProperty
    /// <summary>
    /// Indicates whether the specified property exists in the current type.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="propertyName"></param>
    /// <param name="comparisonType"></param>
    /// <returns></returns>
    public static bool ContainsProperty(this Type type, string propertyName, StringComparison comparisonType = StringComparison.Ordinal) =>
        type.GetProperties().Any<PropertyInfo>(_ => propertyName.Equals(_.Name, comparisonType));
    #endregion

    #region GetCode
    /// <summary>
    /// Returns the <see cref="TypeCode"/> for the current <see cref="Type"/>.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static TypeCode GetCode(this Type type) =>
        Type.GetTypeCode(type);
    #endregion

    #region GetDefaultValue
    /// <summary>
    /// Returns the default value for a given type. If type is a value type, this method returns null.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static object GetDefaultValue(this Type type) =>
        type.IsValueType ? Activator.CreateInstance(type) : null;
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
    /// Indicates whether the current <see cref="Type"/> represents an integer numeric value.
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

    #region PropertyInfo

    #region GetValueOrDefault
    /// <summary>
    /// Returns the property value from the specified object or a default value if it cannot be retrieved.
    /// </summary>
    /// <param name="propertyInfo"></param>
    /// <param name="obj"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static object GetValueOrDefault(this PropertyInfo propertyInfo, object obj, object defaultValue = null)
    {
        try
        {
            object value = propertyInfo.GetValue(obj);
            return value ?? defaultValue;
        }
        catch
        {
            return defaultValue;
        }
    }
    #endregion

    #region TryGetValue
    /// <summary>
    /// Attempts to get the property value from the specified object.
    /// </summary>
    /// <param name="propertyInfo"></param>
    /// <param name="obj"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool TryGetValue(this PropertyInfo propertyInfo, object obj, out object value)
    {
        try
        {
            value = propertyInfo.GetValue(obj);
            return true;
        }
        catch
        {
            value = null;
            return false;
        }
    }
    #endregion

    #endregion

    #endregion
}