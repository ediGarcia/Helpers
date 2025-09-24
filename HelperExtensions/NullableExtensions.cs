// ReSharper disable UnusedMember.Global

namespace HelperExtensions;

public static class NullableExtensions
{
    #region IsNull
    /// <summary>
    /// Gets or sets a value indicating whether the specified nullable object is null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns><see langword="true"/> if <paramref name="obj"/> is null; otherwise, <see langword="false"/>.</returns>
    public static bool IsNull<T>(this T? obj) where T : struct =>
        !obj.HasValue;
    #endregion
}