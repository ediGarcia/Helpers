using HelperMethods;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace HelperExtensions;

public static class StringExtensions
{
    #region Char

    #region IsDigit
    /// <inheritdoc cref="Char.IsDigit" />
    public static bool IsDigit(this char c) =>
        Char.IsDigit(c);
    #endregion

    #region IsLetter
    /// <inheritdoc cref="Char.IsLetter" />
    public static bool IsLetter(this char c) =>
        Char.IsLetter(c);
    #endregion

    #region IsLetterOrDigit
    /// <inheritdoc cref="Char.IsLetterOrDigit" />
    public static bool IsLetterOrDigit(this char c) =>
        Char.IsLetterOrDigit(c);
    #endregion

    #region IsLower
    /// <inheritdoc cref="Char.IsLower" />
    public static bool IsLower(this char c) =>
        Char.IsLower(c);
    #endregion

    #region IsUpper
    /// <inheritdoc cref="Char.IsUpper" />
    public static bool IsUpper(this char c) =>
        Char.IsUpper(c);
    #endregion

    #region ToLower
    /// <inheritdoc cref="Char.ToLower" />
    public static char ToLower(this char c) =>
        Char.ToLower(c);
    #endregion

    #region ToUpper
    /// <inheritdoc cref="Char.ToUpper" />
    public static char ToUpper(this char c) =>
        Char.ToUpper(c);
    #endregion

    #endregion

    #region String

    #region Append
    /// <summary>
    /// Appends the strings into the current one.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string Append(this string st, string value) =>
        String.Concat(st, value);
    #endregion

    #region AppendMany
    /// <summary>
    /// Appends the strings into the current one.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="separator"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static string AppendMany(this string st, string separator, params string[] values) =>
        String.Concat(st, String.Join(separator, values));
    #endregion

    #region AppendManyNew
    /// <summary>
    /// Appends each string that does not exist in the original one.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="separator"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static string AppendManyNew(this string st, string separator, params string[] values) =>
        st is null
            ? String.Join(separator, values)
            : String.Concat(st, String.Join(separator, values.Where(_ => !st.Contains(_))));
    #endregion

    #region AppendNew
    /// <summary>
    /// Appends the string if it does not exist in the original one.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string AppendNew(this string st, string value) =>
        st?.Contains(value) == true ? st : String.Concat(st, value);
    #endregion

    #region Contains

    #region Contains(this string, string, StringComparison)
    /// <summary>
    /// Returns a value indicating whether the specified string occurs within this string, using the specified comparison rules.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="value"></param>
    /// <param name="comparisonType"></param>
    /// <returns></returns>
    public static bool Contains(
        this string st,
        string value,
        StringComparison comparisonType = StringComparison.Ordinal
    ) => value is not null && st?.IndexOf(value, comparisonType) >= 0;
    #endregion

    #region Contains(this string, string, bool)
    /// <summary>
    /// Returns a value indicating whether the specified string occurs within this string, using the invariant culture.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="value"></param>
    /// <param name="ignoreCase"></param>
    /// <returns></returns>
    public static bool Contains(this string st, string value, bool ignoreCase) =>
        value is not null
        && st?.IndexOf(value, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0;
    #endregion

    #region Contains(this string, char, StringComparison)
    /// <summary>
    /// Returns a value indicating whether the specified string occurs within this string, using the specified comparison rules.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="value"></param>
    /// <param name="comparisonType"></param>
    /// <returns></returns>
    public static bool Contains(
        this string st,
        char value,
        StringComparison comparisonType = StringComparison.Ordinal
    ) => st?.IndexOf(value.ToString(), comparisonType) >= 0;
    #endregion

    #endregion

    #region ContainsAny*

    #region ContainsAny(this string, params string[])
    /// <summary>
    /// Returns a value indicating whether any of the specified substrings occur within this string.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool ContainsAny(this string st, params string[] values) =>
        values.Any(st.Contains);
    #endregion

    #region ContainsAny(this string, params char[])
    /// <summary>
    /// Returns a value indicating whether any of the specified substrings occur within this string.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool ContainsAny(this string st, params char[] values) =>
        values.Any(st.Contains);
    #endregion

    #endregion

    #region ContainsAll*

    #region ContainsAll(this string, params string[])
    /// <summary>
    /// Returns a value indicating whether all the specified substrings occur within this string.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool ContainsAll(this string st, params string[] values) =>
        values.All(st.Contains);
    #endregion

    #region ContainsAll(this string, params char[])
    /// <summary>
    /// Returns a value indicating whether all the specified substrings occur within this string.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool ContainsAll(this string st, params char[] values) =>
        values.All(st.Contains);
    #endregion

    #endregion

    #region ContainsChar
    /// <inheritdoc cref="String.Contains"/>
    public static bool ContainsChar(this string st, char value) =>
        st?.IndexOf(value) > 0;
    #endregion

    #region ContainsSpace
    /// <summary>
    /// Indicates whether the current string contains white space chars.
    /// </summary>
    /// <param name="st"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException">The string is null.</exception>
    public static bool ContainsSpace(this string st) =>
        st?.Any(Char.IsWhiteSpace) == true;
    #endregion

    #region ContainsString
    /// <inheritdoc cref="String.Contains"/>
    public static bool ContainsString(this string st, string value) =>
        st?.Contains(value) == true;
    #endregion

    #region EndsWith
    /// <summary>
    /// Determines whether the end of this string instance matches the specified string when compared using the invariant culture.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="value"></param>
    /// <param name="ignoreCase"></param>
    /// <returns></returns>
    public static bool EndsWith(this string st, string value, bool ignoreCase) =>
        !String.IsNullOrEmpty(value) && st?.EndsWith(value, ignoreCase, CultureInfo.InvariantCulture) == true;
    #endregion

    #region EndsWithAny
    /// <summary>
    /// Determines whether the end if this string instance matches any of the specified string.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool EndsWithAny(this string st, params string[] values) =>
        values.Any(st.EndsWith);
    #endregion

    #region EqualsAny
    /// <summary>
    /// Determines whether this instance and any another specified <see cref="T:System.String" /> object have the same value.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="comparison"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool EqualsAny(
        this string st,
        StringComparison comparison = StringComparison.Ordinal,
        params string[] values
    ) => values.Any(_ => st?.Equals(_, comparison) == true);
    #endregion

    #region FillLeft
    /// <summary>
    /// Insert the specified string value at the beginning of the string the selected number of times.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="count"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string FillLeft(this string st, int count, string value)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), "The count must be equal or greater than 0 (zero).");

        return new StringBuilder().Insert(0, value, count).Append(st).ToString();
    }
    #endregion

    #region FillRight
    /// <summary>
    /// Appends the specified string value the selected number of times.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="count"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string FillRight(this string st, int count, string value)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(
                nameof(count),
                "The count must be equal or greater than 0 (zero)."
            );

        return new StringBuilder(st).Insert(st.Length, value, count).ToString();
    }
    #endregion

    #region GetMatches
    /// <summary>
    /// Retrieves the regex matches for the specified string.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static string[] GetMatches(this string st, string pattern) =>
        Regex.Matches(st, pattern).Select(m => m.Value).ToArray();
    #endregion

    #region GetValueOrDefault
    /// <summary>
    /// Returns the default value if the current string is null.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static string GetValueOrDefault(this string st, string defaultValue = null) =>
        st ?? defaultValue;
    #endregion

    #region IsContainedBy
    /// <summary>
    /// Indicates whether the current string is contained by the specified value.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="value"></param>
    /// <param name="comparisonType"></param>
    /// <returns></returns>
    public static bool IsContainedBy(
        this string st,
        string value,
        StringComparison comparisonType = StringComparison.Ordinal
    ) => value?.Contains(st, comparisonType) == true;
    #endregion

    #region IsContainedByAll
    /// <summary>
    /// Indicates whether the current string is contained by all the specified values.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="comparisonType"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool IsContainedByAll(
        this string st,
        StringComparison comparisonType = StringComparison.Ordinal,
        params string[] values
    ) => values.All(_ => _.Contains(st, comparisonType));
    #endregion

    #region IsContainedByAny
    /// <summary>
    /// Indicates whether the current string is contained by any of the specified values.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="comparisonType"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool IsContainedByAny(
        this string st,
        StringComparison comparisonType = StringComparison.Ordinal,
        params string[] values
    ) => values.Any(_ => _.Contains(st, comparisonType));
    #endregion

    #region IsEmpty
    /// <summary>
    /// Indicates whether the current string is empty (String.Empty).
    /// </summary>
    /// <param name="st"></param>
    /// <returns></returns>
    public static bool IsEmpty(this string st) =>
        st == "";
    #endregion

    #region IsEqualTo
    /// <summary>
    /// Indicates whether the specified string equals the current one.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="value"></param>
    /// <param name="comparisonType"></param>
    /// <returns></returns>
    public static bool IsEqualTo(
        this string str,
        string value,
        StringComparison comparisonType = StringComparison.Ordinal
    ) => StringMethods.AreAllNull(str, value) || String.Compare(str, value, comparisonType) == 0;
    #endregion

    #region IsNull
    /// <summary>
    /// Indicates whether the current string is null.
    /// </summary>
    /// <param name="st"></param>
    /// <returns></returns>
    public static bool IsNull(this string st) =>
        st is null;
    #endregion

    #region IsNullOrEmpty
    /// <summary>
    /// Indicates whether the current string is null or empty.
    /// </summary>
    /// <param name="st"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this string st) =>
        String.IsNullOrEmpty(st);
    #endregion

    #region IsNullOrWhiteSpace
    /// <summary>
    /// Indicates whether the current string is null, empty or contains only white-space characters.
    /// </summary>
    /// <param name="st"></param>
    /// <returns></returns>
    public static bool IsNullOrWhiteSpace(this string st) =>
        String.IsNullOrWhiteSpace(st);
    #endregion

    #region IsWhiteSpace
    /// <summary>
    /// Indicates whether the current string contains only white-space characters.
    /// </summary>
    /// <param name="st"></param>
    /// <returns></returns>
    public static bool IsWhiteSpace(this string st) =>
        st != null && String.IsNullOrWhiteSpace(st);
    #endregion

    #region Matches
    /// <summary>
    /// Indicates whether the specified regular expression finds a match in the current string.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static bool Matches(this string st, string pattern) =>
        Regex.IsMatch(st, pattern);
    #endregion

    #region Prepend
    /// <summary>
    /// Prepends the strings into the current one.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string Prepend(this string st, string value) =>
        String.Concat(value, st);
    #endregion

    #region PrependMany
    /// <summary>
    /// Prepends the strings into the current one.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static string PrependMany(this string st, params string[] values) =>
        String.Concat(String.Join(String.Empty, values), st);
    #endregion

    #region PrependManyNew
    /// <summary>
    /// Prepends each string that does not exist in the original one.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static string PrependManyNew(this string st, params string[] values) =>
        st is null
            ? String.Concat(values)
            : String.Concat(String.Concat(values.Where(_ => !st.Contains(_))), st);
    #endregion

    #region PrependNew
    /// <summary>
    /// Prepends the string if it does not exist in the original one.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string PrependNew(this string st, string value) =>
        // ReSharper disable once ArrangeRedundantParentheses
        st?.Contains(value) == true
            ? st
            : (value + st);
    #endregion

    #region Split*

    #region Split(this string, char, [StringSplitOptions])
    /// <summary>
    /// Splits a string into substrings based on the characters in an array. You can specify whether the substrings include empty array elements.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="separator">A character array that delimits the substrings in this string, an empty array that contains no delimiters, or <see langword="null" />.</param>
    /// <param name="options"><see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned.</param>
    /// <returns></returns>
    public static IReadOnlyList<string> Split(
        this string st,
        char separator,
        StringSplitOptions options = StringSplitOptions.None
    ) => st.Split([separator], options);
    #endregion

    #region Split(this string, string, [StringSplitOptions])
    /// <summary>
    /// Splits a string into substrings based on the strings in an array. You can specify whether the substrings include empty array elements.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="separator">A string array that delimits the substrings in this string, an empty array that contains no delimiters, or <see langword="null" />.</param>
    /// <param name="options"><see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned.</param>
    /// <returns></returns>
    public static IReadOnlyList<string> Split(
        this string st,
        string separator,
        StringSplitOptions options = StringSplitOptions.None
    ) => st.Split([separator], options);
    #endregion

    #region Split(this string, string[], [StringSplitOptions])
    /// <summary>
    /// Splits a string into substrings based on the strings in an array. You can specify whether the substrings include empty array elements.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="separators">A string array that delimits the substrings in this string, an empty array that contains no delimiters, or <see langword="null" />.</param>
    /// <param name="options"><see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned.</param>
    /// <returns></returns>
    public static IReadOnlyList<string> Split(this string st, string[] separators, StringSplitOptions options) =>
        st.Split(separators, options);
    #endregion

    #region Split(this string, params string[])
    /// <summary>
    /// Splits a string into substrings based on the strings in an array. You can specify whether the substrings include empty array elements.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="separators">A string array that delimits the substrings in this string, an empty array that contains no delimiters, or <see langword="null" />.</param>
    /// <returns></returns>
    public static IReadOnlyList<string> Split(this string st, params string[] separators) =>
        st.Split(separators, StringSplitOptions.None);
    #endregion

    #endregion

    #region StartsWith
    /// <summary>
    /// Determines whether the beginning of this string instance matches the specified string when compared using the invariant culture.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="value"></param>
    /// <param name="ignoreCase"></param>
    /// <returns></returns>
    public static bool StartsWith(this string st, string value, bool ignoreCase) =>
        !String.IsNullOrEmpty(value) && st?.StartsWith(value, ignoreCase, CultureInfo.InvariantCulture) is true;
    #endregion

    #region StartsWithAny
    /// <summary>
    /// Determines whether the beginning if this string instance matches any of the specified string.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool StartsWithAny(this string st, params string[] values) =>
        values?.Any(st.StartsWith) == true;
    #endregion

    #region ToDouble
    /// <summary>
    /// Converts the string to double.
    /// </summary>
    /// <param name="st"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="FormatException"></exception>
    /// <exception cref="OverflowException"></exception>
    public static double ToDouble(this string st) =>
        Double.Parse(st);
    #endregion

    #region ToInt
    /// <summary>
    /// Converts the string to int.
    /// </summary>
    /// <param name="st"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="FormatException"></exception>
    /// <exception cref="OverflowException"></exception>
    public static int ToInt(this string st) =>
        Int32.Parse(st);
    #endregion

    #endregion

    #region StringBuilder

    #region Append
    /// <summary>
    /// Appends multiple strings into the string builder.
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static StringBuilder Append(this StringBuilder sb, params object[] values)
    {
        values.ForEach(item => sb.Append(item));
        return sb;
    }
    #endregion

    #region AppendLine
    /// <summary>
    /// Appends multiple strings into the string builder then adds a line break.
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static StringBuilder AppendLine(this StringBuilder sb, params object[] values)
    {
        values.ForEach(_ => sb.Append(_));
        return sb.AppendLine();
    }
    #endregion

    #region AppendLines*

    #region AppendLines(this StringBuilder, params object[])
    /// <summary>
    /// Appends multiple strings into the string builder then adds a line break.
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static StringBuilder AppendLines(this StringBuilder sb, params object[] values)
    {
        values.ForEach(_ => sb.AppendLine(_));
        return sb;
    }
    #endregion

    #region AppendLines(this StringBuilder, IEnumerable<T>)
    /// <summary>
    /// Appends multiple strings into the string builder then adds a line break.
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static StringBuilder AppendLines<T>(this StringBuilder sb, IEnumerable<T> values)
    {
        values.ForEach(_ => sb.AppendLine(_));
        return sb;
    }
    #endregion

    #endregion

    #region AppendIfNotNull
    /// <summary>
    /// Appends the specified string value if it's not null.
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="value"></param>
    /// <param name="prefix"></param>
    /// <param name="suffix"></param>
    /// <returns></returns>
    public static StringBuilder AppendIfNotNull(
        this StringBuilder sb,
        object value,
        object prefix = null,
        object suffix = null
    )
    {
        if (value is not null)
            sb.Append(prefix).Append(value).Append(suffix);

        return sb;
    }
    #endregion

    #region AppendIfNotNullOrWhiteSpace
    /// <summary>
    /// Appends the specified sting value if it's not null, empty or contains only white-space characters.
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="value"></param>
    /// <param name="prefix"></param>
    /// <param name="suffix"></param>
    /// <returns></returns>
    public static StringBuilder AppendIfNotNullOrWhiteSpace(
        this StringBuilder sb,
        object value,
        object prefix = null,
        object suffix = null
    )
    {
        string stringValue = value?.ToString();

        if (stringValue.IsNullOrWhiteSpace() == false)
            sb.Append(prefix).Append(stringValue).Append(suffix);

        return sb;
    }
    #endregion

    #region IsNull
    /// <summary>
    /// Indicates whether the current <see cref="StringBuilder"/> is null.
    /// </summary>
    /// <param name="st"></param>
    /// <returns></returns>
    public static bool IsNull(this StringBuilder st) =>
        st?.ToString() is null;
    #endregion

    #region IsNullOrEmpty
    /// <summary>
    /// Indicates whether the current <see cref="StringBuilder"/> is null or empty.
    /// </summary>
    /// <param name="st"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this StringBuilder st) =>
        st?.ToString().IsNullOrEmpty() == true;
    #endregion

    #region IsNullOrWhiteSpace
    /// <summary>
    /// Indicates whether the current <see cref="StringBuilder"/> is null, empty or contains only white-space characters.
    /// </summary>
    /// <param name="st"></param>
    /// <returns></returns>
    public static bool IsNullOrWhiteSpace(this StringBuilder st) =>
        st?.ToString().IsNullOrWhiteSpace() == true;
    #endregion

    #region IsWhiteSpace
    /// <summary>
    /// Indicates whether the current <see cref="StringBuilder"/> contains only white-space characters.
    /// </summary>
    /// <param name="st"></param>
    /// <returns></returns>
    public static bool IsWhiteSpace(this StringBuilder st) =>
        st.ToString().IsWhiteSpace();
    #endregion

    #endregion
}
