using System;
using System.Reflection;

namespace HelperExtensions;

public static class GenericExtensions
{
    #region Public Methods

    #region Clone
    /// <summary>
    /// Creates a new instance of the specified type with the same values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="deepClone">Indicates whether the inner properties and fields should have their values cloned too, instead of just the reference.</param>
    /// <returns>A clone of the current object.</returns>
    /// <exception cref="MissingMethodException" />
    /// <remarks>The copied class type (also its properties and fields types, if deep cloning) cannot be abstract and must have a parameterless constructor, otherwise a <see cref="MissingMethodException" /> will be thrown.</remarks>
    public static T Clone<T>(this T obj, bool deepClone = false)
    {
        T newItem = Activator.CreateInstance<T>();
        obj.CopyTo(newItem, deepClone);
        return newItem;
    }
    #endregion

    #region CopyFrom
    /// <summary>
    /// Copies the values of the specified instance to the current instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="source"></param>
    /// <param name="deepCopy">Indicates whether the inner properties and fields should have their values copied too, instead of just the reference.</param>
    /// <exception cref="MissingMethodException" />
    /// <remarks>The copied class type (also its properties and fields types, if deep copying) cannot be abstract and must have a parameterless constructor, otherwise a <see cref="MissingMethodException" /> will be thrown.</remarks>
    public static void CopyFrom<T>(this T obj, T source, bool deepCopy = false) =>
        CopyValues(source, obj, deepCopy);
    #endregion

    #region CopyTo
    /// <summary>
    /// Copies the values of the current instance to the specified instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="destination"></param>
    /// <param name="deepCopy">Indicates whether the inner properties and fields should have their values copied too, instead of just the reference.</param>
    /// <exception cref="MissingMethodException" />
    /// <remarks>The copied class type (also its properties and fields types, if deep copying) cannot be abstract and must have a parameterless constructor, otherwise a <see cref="MissingMethodException" /> will be thrown.</remarks>
    public static void CopyTo<T>(this T obj, T destination, bool deepCopy = false) =>
        CopyValues(obj, destination, deepCopy);
    #endregion

    #endregion

    #region Private Methods

    #region CopyValuesPrivate
    /// <summary>
    /// Copies the properties' and fields' values from the source to the destination.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="deepCopy"></param>
    private static void CopyValues<T>(T source, T destination, bool deepCopy)
    {
        typeof(T).GetFields().ForEach(_ =>
        {
            if (_.IsStatic || _.IsInitOnly)
                return;

            object sourceFieldValue = _.GetValue(source);

            if (sourceFieldValue is not null && deepCopy && _.FieldType.GetTypeInfo().IsClass)
                CopyValues(sourceFieldValue, _.GetValue(destination), true);
            else
                _.SetValue(destination, sourceFieldValue);
        });

        typeof(T).GetProperties().ForEach(_ =>
        {
            if (!_.CanRead || !_.CanWrite)
                return;

            object sourcePropValue = _.GetValue(source);

            if (sourcePropValue is not null && deepCopy && _.PropertyType.GetTypeInfo().IsClass)
                CopyValues(sourcePropValue, _.GetValue(destination), true);
            else
                _.SetValue(destination, sourcePropValue);
        });
    }
    #endregion

    #endregion
}
