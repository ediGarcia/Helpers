using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
    /// <exception cref="MissingMethodException" />
    /// <remarks>The copied class type (also its properties and fields types, if deep cloning) cannot be abstract and must have a parameterless constructor, otherwise a <see cref="MissingMethodException" /> will be thrown.</remarks>
    public static T Clone<T>(this T obj)
    {
        if (obj is null)
            return default;

        BinaryFormatter serializer = new();
        using MemoryStream stream = new();
        
        serializer.Serialize(stream, obj);
        stream.Flush();
        stream.Position = 0;

        return (T)serializer.Deserialize(stream);
    }
    #endregion

    #endregion
}
