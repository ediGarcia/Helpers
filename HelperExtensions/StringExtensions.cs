using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using HelperMethods;
// ReSharper disable UnusedMember.Global

namespace HelperExtensions;

public static class StringExtensions
{
    #region Char

    #region IsDigit
    /// <inheritdoc cref="Char.IsDigit(char)" />
    public static bool IsDigit(this char c) => Char.IsDigit(c);
    #endregion

    #region IsLetter
    /// <inheritdoc cref="Char.IsLetter(char)" />
    public static bool IsLetter(this char c) => Char.IsLetter(c);
    #endregion

    #region IsLetterOrDigit
    /// <inheritdoc cref="Char.IsLetterOrDigit(char)" />
    public static bool IsLetterOrDigit(this char c) => Char.IsLetterOrDigit(c);
    #endregion

    #region IsLower
    /// <inheritdoc cref="Char.IsLower(char)" />
    public static bool IsLower(this char c) => Char.IsLower(c);
    #endregion

    #region IsUpper
    /// <inheritdoc cref="Char.IsUpper(char)" />
    public static bool IsUpper(this char c) => Char.IsUpper(c);
    #endregion

    #region ToLower
    /// <inheritdoc cref="Char.ToLower(char)" />
    public static char ToLower(this char c) => Char.ToLower(c);
    #endregion

    #region ToUpper
    /// <inheritdoc cref="Char.ToUpper(char)" />
    public static char ToUpper(this char c) => Char.ToUpper(c);
    #endregion

    #endregion

    extension(string st)
    {
        #region Append
        /// <summary>
        /// Appends the strings into the current one.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Append(string value) =>
            String.Concat(st, value);
        #endregion

        #region AppendMany
        /// <summary>
        /// Appends the strings into the current one.
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public string AppendMany(string separator, params string[] values) =>
            String.Concat(st, String.Join(separator, values));
        #endregion

        #region AppendManyNew
        /// <summary>
        /// Appends each string that does not exist in the original one.
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public string AppendManyNew(string separator, params string[] values) =>
            st is null
                ? String.Join(separator, values)
                : String.Concat(st, String.Join(separator, values.Where(_ => !st.Contains(_))));
        #endregion

        #region AppendNew
        /// <summary>
        /// Appends the string if it does not exist in the original one.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string AppendNew(string value) =>
            st?.Contains(value) == true ? st : String.Concat(st, value);
        #endregion

        #region Contains*

        #region Contains(string?, [StringComparison])
        /// <summary>
        /// Returns a value indicating whether the specified string occurs within this string, using the specified comparison rules.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public bool Contains(string? value, StringComparison comparisonType = StringComparison.Ordinal) =>
            value is not null && st.Contains(value, comparisonType);
        #endregion

        #region Contains(string, bool)
        /// <summary>
        /// Returns a value indicating whether the specified string occurs within this string, using the invariant culture.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public bool Contains(string value, bool ignoreCase) =>
            value is not null
            && st?.IndexOf(
                value,
                ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal
            ) >= 0;
        #endregion

        #region Contains(char, [StringComparison]
        /// <summary>
        /// Returns a value indicating whether the specified string occurs within this string, using the specified comparison rules.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public bool Contains(char value, StringComparison comparisonType = StringComparison.Ordinal) =>
            st.Contains(value.ToString(), comparisonType);
        #endregion

        #endregion

        #region ContainsAny*

        #region ContainsAny(params string[])
        /// <summary>
        /// Returns a value indicating whether any of the specified substrings occur within this string.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool ContainsAny(params string[] values) =>
            values.Any(st.Contains);
        #endregion

        #region ContainsAny(params char[])
        /// <summary>
        /// Returns a value indicating whether any of the specified substrings occur within this string.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool ContainsAny(params char[] values) =>
            values.Any(st.Contains);
        #endregion

        #endregion

        #region ContainsAll*

        #region ContainsAll(params string[])
        /// <summary>
        /// Returns a value indicating whether all the specified substrings occur within this string.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool ContainsAll(params string[] values) =>
            values.All(st.Contains);
        #endregion

        #region ContainsAll(params char[])
        /// <summary>
        /// Returns a value indicating whether all the specified substrings occur within this string.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool ContainsAll(params char[] values) =>
            values.All(st.Contains);
        #endregion

        #endregion

        #region ContainsChar
        /// <inheritdoc cref="String.Contains(char)"/>
        public bool ContainsChar(char value) =>
            st.IndexOf(value) > 0;
        #endregion

        #region ContainsSpace
        /// <summary>
        /// Indicates whether the current string contains white space chars.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">The string is null.</exception>
        public bool ContainsSpace() =>
            st.Any(Char.IsWhiteSpace);
        #endregion

        #region ContainsString
        /// <inheritdoc cref="String.Contains(char)"/>
        public bool ContainsString(string value) =>
            st.Contains(value);
        #endregion

        #region EndsWith*

        #region EndsWith(string, bool)
        /// <summary>
        /// Determines whether the end of this string instance matches the specified string when compared using the invariant culture.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public bool EndsWith(string value, bool ignoreCase) =>
            !String.IsNullOrEmpty(value) && st.EndsWith(value, ignoreCase, CultureInfo.InvariantCulture);
        #endregion

        #region EndsWith(string, [StringComparison])
        /// <summary>
        /// Determines whether the end of this string instance matches the specified string when compared using the specified comparison option.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public bool EndsWith(string value, StringComparison comparison = StringComparison.OrdinalIgnoreCase) =>
            st.EndsWith(value, comparison);
        #endregion

        #endregion

        #region EndsWithAny
        /// <summary>
        /// Determines whether the end if this string instance matches any of the specified string.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool EndsWithAny(params string[] values) =>
            values.Any(st.EndsWith);
        #endregion

        #region EqualsAny
        /// <summary>
        /// Determines whether this instance and any another specified <see cref="T:System.String" /> object have the same value.
        /// </summary>
        /// <param name="comparison"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool EqualsAny(StringComparison comparison = StringComparison.Ordinal, params string[] values) =>
            values.Any(_ => st.Equals(_, comparison));
        #endregion

        #region FillLeft
        /// <summary>
        /// Insert the specified string value at the beginning of the string the selected number of times.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string FillLeft(int count, string value)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(count),
                    "The count must be equal or greater than 0 (zero)."
                );

            return new StringBuilder().Insert(0, value, count).Append(st).ToString();
        }
        #endregion

        #region FillRight
        /// <summary>
        /// Appends the specified string value the selected number of times.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string FillRight(int count, string value)
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
        /// <param name="pattern"></param>
        /// <returns></returns>
        public IReadOnlyCollection<string> GetMatches(string pattern) =>
            [.. Regex.Matches(st, pattern).Select(m => m.Value)];
        #endregion

        #region GetValueOrDefault
        /// <summary>
        /// Returns the default value if the current string is null.
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string GetValueOrDefault(string defaultValue = null) =>
            st ?? defaultValue;
        #endregion

        #region IsContainedBy
        /// <summary>
        /// Indicates whether the current string is contained by the specified value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public bool IsContainedBy(string value,
            StringComparison comparisonType = StringComparison.Ordinal
        ) =>
            value.Contains(st, comparisonType);
        #endregion

        #region IsContainedByAll
        /// <summary>
        /// Indicates whether the current string is contained by all the specified values.
        /// </summary>
        /// <param name="comparisonType"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool IsContainedByAll(StringComparison comparisonType = StringComparison.Ordinal, params string[] values) =>
            values.All(_ => _.Contains(st, comparisonType));
        #endregion

        #region IsContainedByAny
        /// <summary>
        /// Indicates whether the current string is contained by any of the specified values.
        /// </summary>
        /// <param name="comparisonType"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool IsContainedByAny(StringComparison comparisonType = StringComparison.Ordinal, params string[] values) =>
            values.Any(_ => _.Contains(st, comparisonType));
        #endregion

        #region IsEmpty
        /// <summary>
        /// Indicates whether the current string is empty (String.Empty).
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty() =>
            st.Length == 0;
        #endregion

        #region IsEqualTo
        /// <summary>
        /// Indicates whether the specified string equals the current one.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public bool IsEqualTo(string value, StringComparison comparisonType = StringComparison.Ordinal) =>
            StringHelper.AreAllNull(st, value) || String.Compare(st, value, comparisonType) == 0;
        #endregion

        #region IsNumber
        /// <summary>
        /// Determines whether the string represents a valid number.
        /// </summary>
        /// <returns></returns>
        public bool IsNumber() =>
            Double.TryParse(st, out _);
        #endregion

        #region Matches
        /// <summary>
        /// Indicates whether the specified regular expression finds a match in the current string.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public bool Matches(string pattern) =>
            Regex.IsMatch(st, pattern);
        #endregion

        #region Prepend
        /// <summary>
        /// Prepends the strings into the current one.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Prepend(string value) =>
            String.Concat(value, st);
        #endregion

        #region PrependMany
        /// <summary>
        /// Prepends the strings into the current one.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public string PrependMany(params string[] values) =>
            String.Concat(String.Join(String.Empty, values), st);
        #endregion

        #region PrependManyNew
        /// <summary>
        /// Prepends each string that does not exist in the original one.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public string PrependManyNew(params string[] values) =>
            st is null
                ? String.Concat(values)
                : String.Concat(String.Concat(values.Where(_ => !st.Contains(_))), st);
        #endregion

        #region PrependNew
        /// <summary>
        /// Prepends the string if it does not exist in the original one.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string PrependNew(string value) =>
            // ReSharper disable once ArrangeRedundantParentheses
            st?.Contains(value) == true
                ? st
                : (value + st);
        #endregion

        #region Split*

        #region Split(char, [StringSplitOptions])
        /// <summary>
        /// Splits a string into substrings based on the characters in an array. You can specify whether the substrings include empty array elements.
        /// </summary>
        /// <param name="separator">A character array that delimits the substrings in this string, an empty array that contains no delimiters, or <see langword="null" />.</param>
        /// <param name="options"><see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned.</param>
        /// <returns></returns>
        public IReadOnlyCollection<string> Split(
            char separator,
            StringSplitOptions options = StringSplitOptions.None
        ) =>
            st.Split([separator], options);
        #endregion

        #region Split(this string, string, [StringSplitOptions])
        /// <summary>
        /// Splits a string into substrings based on the strings in an array. You can specify whether the substrings include empty array elements.
        /// </summary>
        /// <param name="separator">A string array that delimits the substrings in this string, an empty array that contains no delimiters, or <see langword="null" />.</param>
        /// <param name="options"><see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned.</param>
        /// <returns></returns>
        public IReadOnlyCollection<string> Split(
            string separator,
            StringSplitOptions options = StringSplitOptions.None
        ) =>
            st.Split([separator], options);
        #endregion

        #region Split(this string, string[], [StringSplitOptions])
        /// <summary>
        /// Splits a string into substrings based on the strings in an array. You can specify whether the substrings include empty array elements.
        /// </summary>
        /// <param name="separators">A string array that delimits the substrings in this string, an empty array that contains no delimiters, or <see langword="null" />.</param>
        /// <param name="options"><see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned.</param>
        /// <returns></returns>
        public IReadOnlyCollection<string> Split(string[] separators, StringSplitOptions options) =>
            st.Split(separators, options);
        #endregion

        #region Split(this string, params string[])
        /// <summary>
        /// Splits a string into substrings based on the strings in an array. You can specify whether the substrings include empty array elements.
        /// </summary>
        /// <param name="separators">A string array that delimits the substrings in this string, an empty array that contains no delimiters, or <see langword="null" />.</param>
        /// <returns></returns>
        public IReadOnlyCollection<string> Split(params string[] separators) =>
            st.Split(separators, StringSplitOptions.None);
        #endregion

        #endregion

        #region StartsWith*

        #region StartsWith(string, bool)
        /// <summary>
        /// Determines whether the beginning of this string instance matches the specified string when compared using the invariant culture.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public bool StartsWith(string value, bool ignoreCase) =>
            !String.IsNullOrEmpty(value)
            && st?.StartsWith(value, ignoreCase, CultureInfo.InvariantCulture) is true;
        #endregion

        #region StartsWith(string, [StringComparison])
        /// <summary>
        /// Determines whether the start of this string instance matches the specified string when compared using the specified comparison option.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public bool StartsWith(
            
            string value,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase
        ) => st.StartsWith(value, comparison);
        #endregion

        #endregion

        #region StartsWithAny
        /// <summary>
        /// Determines whether the beginning if this string instance matches any of the specified string.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool StartsWithAny( params string[] values) =>
            values?.Any(st.StartsWith) == true;
        #endregion

        #region ToDouble
        /// <summary>
        /// Converts the string to double.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public double ToDouble() => Double.Parse(st);
        #endregion

        #region ToInt
        /// <summary>
        /// Converts the string to int.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public int ToInt() => Int32.Parse(st);
        #endregion
    }

    extension(string? st)
    {
        #region IsNull
        /// <summary>
        /// Indicates whether the current string is null.
        /// </summary>
        /// <returns></returns>
        public bool IsNull() =>
            st is null;
        #endregion

        #region IsNullOrEmpty
        /// <summary>
        /// Indicates whether the current string is null or empty.
        /// </summary>
        /// <returns></returns>
        public bool IsNullOrEmpty() =>
            String.IsNullOrEmpty(st);
        #endregion

        #region IsNullOrWhiteSpace
        /// <summary>
        /// Indicates whether the current string is null, empty or contains only white-space characters.
        /// </summary>
        /// <returns></returns>
        public bool IsNullOrWhiteSpace() =>
            String.IsNullOrWhiteSpace(st);
        #endregion

        #region IsWhiteSpace
        /// <summary>
        /// Indicates whether the current string contains only white-space characters.
        /// </summary>
        /// <returns></returns>
        public bool IsWhiteSpace() =>
            st != null && String.IsNullOrWhiteSpace(st);
        #endregion
    }

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

        if (!stringValue.IsNullOrWhiteSpace())
            sb.Append(prefix).Append(stringValue).Append(suffix);

        return sb;
    }
    #endregion

    #region IsEmpty
    /// <summary>
    /// Indicates whether the current <see cref="StringBuilder"/> is empty.
    /// </summary>
    /// <param name="sb"></param>
    /// <returns></returns>
    public static bool IsEmpty(this StringBuilder sb) => sb.Length == 0;
    #endregion

    #region IsNull
    /// <summary>
    /// Indicates whether the current <see cref="StringBuilder"/> is null.
    /// </summary>
    /// <param name="st"></param>
    /// <returns></returns>
    public static bool IsNull(this StringBuilder st) => st?.ToString() is null;
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
    public static bool IsWhiteSpace(this StringBuilder st) => st.ToString().IsWhiteSpace();
    #endregion

    #endregion
}
