using HelperMethods.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

// ReSharper disable UnusedMember.Global

namespace HelperMethods;

public static class StringMethods
{
    public const string LineBreaks = "\n";
    public const string LowerCaseChars = "abcdefghijklmnopqrstuvwxyz";
    public const string NonSpaceChars = UpperCaseChars + LowerCaseChars + NumericChars + SpecialChars;
    public const string NumericChars = "0123456789";
    public const string SpaceChars = " \t";
    public const string SpecialChars = "!\"#$%&'()#+,-./:;<>=?@[]^_`´{}|~";
    public const string UpperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    #region Public Methods

    #region GenerateStringBuilder
    /// <summary>
    /// Generates a string builder with the selected start values.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static StringBuilder GenerateStringBuilder(params string[] values) =>
        new StringBuilder().Append(values);
    #endregion

    #region GenerateRandomString*

    #region GenerateRandomString(char[], int)
    /// <summary>
    /// Generates a random string with the selected characters and width.
    /// </summary>
    /// <param name="validChars"></param>
    /// <param name="width">The exact width of the resulting string. If the value is 0 (zero), an empty string will be returned.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string GenerateRandomString(char[] validChars, int width)
    {
        if (width < 0)
            throw new ArgumentOutOfRangeException(nameof(width), "The desired string width must be greater than or equal to zero.");

        if (width == 0)
            return String.Empty;

        char[] result = new char[width];

        for (int i = 0; i < width; i++)
            result[i] = validChars[NumberMethods.GetRandomInt(validChars.Length)];

        return new(result);
    }
    #endregion

    #region GenerateRandomString(int)
    /// <summary>
    /// Generates a random string with non-space characters and width.
    /// </summary>
    /// <param name="width"></param>
    /// <returns></returns>
    public static string GenerateRandomString(int width) =>
        GenerateRandomString(NonSpaceChars, width);
    #endregion

    #region GenerateRandomString(int, int)
    /// <summary>
    /// Generates a random string with non-space characters and width between [minWidth] and [maxWidth].
    /// </summary>
    /// <param name="minWidth"></param>
    /// <param name="maxWidth"></param>
    /// <returns></returns>
    public static string GenerateRandomString(int minWidth, int maxWidth) =>
        GenerateRandomString(NonSpaceChars, minWidth, maxWidth);
    #endregion

    #region GenerateRandomString(char[], int, int)
    /// <summary>
    /// Generates a random string with the selected characters and width between [minWidth] and [maxWidth].
    /// </summary>
    /// <param name="validChars"></param>
    /// <param name="minWidth"></param>
    /// <param name="maxWidth"></param>
    /// <returns></returns>
    public static string GenerateRandomString(char[] validChars, int minWidth, int maxWidth)
    {
        if (minWidth < 0 && maxWidth < 0)
            throw new ArgumentOutOfRangeException(nameof(maxWidth), "The desired string widths must be greater than or equal to zero.");

        minWidth = Math.Max(0, minWidth);
        maxWidth = Math.Max(0, maxWidth);

        return GenerateRandomString(validChars, NumberMethods.GetRandomInt(minWidth, maxWidth, true));
    }
    #endregion

    #region GenerateRandomString(string, int)
    /// <summary>
    /// Generates a random string with the selected characters and width.
    /// </summary>
    /// <param name="validChars"></param>
    /// <param name="width"></param>
    /// <returns>The exact width of the resulting string. If the value is 0 (zero), an empty string will be returned.</returns>
    public static string GenerateRandomString(string validChars, int width) =>
        GenerateRandomString(validChars.ToCharArray(), width);
    #endregion

    #region GenerateRandomString(string, int, int)
    /// <summary>
    /// Generates a random string with the selected characters and width between [minWidth] and [maxWidth].
    /// </summary>
    /// <param name="validChars"></param>
    /// <param name="minWidth"></param>
    /// <param name="maxWidth"></param>
    /// <returns></returns>
    public static string GenerateRandomString(string validChars, int minWidth, int maxWidth) =>
        GenerateRandomString(validChars.ToCharArray(), minWidth, maxWidth);
    #endregion

    #endregion

    #region FitsText
    /// <summary>
    /// Inserts ellipsis into a string to make it fit into the desired width.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="font"></param>
    /// <param name="desiredWidth"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    // ReSharper disable once UnusedMember.Global
    public static string FitsText(string text, Font font, int desiredWidth)
    {
        StringBuilder stringTest = new();

        //Calculates the maximum string width.
        using (Graphics graphics = new Control().CreateGraphics())
            do
                stringTest.Append("a");
            while (graphics.MeasureString(stringTest.ToString(), font, Point.Empty, StringFormat.GenericTypographic).Width < desiredWidth);

        return InsertEllipsisPrivate(text, stringTest.Length, false);
    }
    #endregion

    #region FormatXml
    /// <summary>
    /// Formats an XML string.
    /// </summary>
    /// <param name="xml" />
    /// <param name="indentChars" />
    /// <exception cref="OutOfMemoryException" />
    /// <exception cref="XmlException" />
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static string FormatXml(string xml, string indentChars = "\t")
    {
        using StreamReader reader = new(new MemoryStream());
        using (
            XmlWriter writer =
                XmlWriter.Create(
                       reader.BaseStream,
                       new()
                   {
                       Indent = true,
                       IndentChars = indentChars,
                       ConformanceLevel = ConformanceLevel.Document
                   }))
        {
            XmlDocument document = new();
            document.LoadXml(xml);
            document.WriteContentTo(writer);

            writer.Flush();
        }

        reader.BaseStream.Flush();
        reader.BaseStream.Position = 0;

        return reader.ReadToEnd();
    }
    #endregion

    #region InsertEllipsis
    /// <summary>
    /// Insert ellipsis in a string.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="maxWidth"></param>
    /// <exception cref="ArgumentOutOfRangeException" />
    /// <example>InsertEllipsis("Hello world.", 7) ==> "He...d."</example>
    /// <returns></returns>
    public static string InsertEllipsis(string text, int maxWidth) =>
        InsertEllipsisPrivate(text, maxWidth, false);
    #endregion

    #region InsertEllipsisAtTheEnd
    /// <summary>
    /// Inserts ellipsis at the end of a string.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="maxWidth"></param>
    /// <example>InsertEllipsisAtTheEnd("This is my really long string.", 20) ==> "This is my really..."</example>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    // ReSharper disable once UnusedMember.Global
    public static string InsertEllipsisAtTheEnd(string text, int maxWidth) =>
        InsertEllipsisPrivate(text, maxWidth, true);
    #endregion

    #region AreAllEmpty
    /// <summary>
    /// Indicates whether the sequence contains only empty strings.
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    public static bool AreAllEmpty(params string[] strings) =>
        strings?.All(_ => _ == String.Empty) == true;
    #endregion

    #region AreAllNull
    /// <summary>
    /// Indicates whether the sequence contains only null strings.
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    public static bool AreAllNull(params string[] strings) =>
        strings?.All(_ => _ is null) == true;
    #endregion

    #region AreAllNullOrEmpty
    /// <summary>
    /// Indicates whether the sequence contains only null or empty strings.
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    public static bool AreAllNullOrEmpty(params string[] strings) =>
        strings?.All(String.IsNullOrEmpty) == true;
    #endregion

    #region AreAllNullOrWhiteSpace
    /// <summary>
    /// Indicates whether the sequence contains only null, empty or white-space only strings.
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    public static bool AreAllNullOrWhiteSpace(params string[] strings) =>
        strings?.All(String.IsNullOrWhiteSpace) == true;
    #endregion

    #region IsAnyEmpty
    /// <summary>
    /// Indicates whether the sequence contains any empty strings.
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    public static bool IsAnyEmpty(params string[] strings) =>
        strings?.Any(_ => _ == String.Empty) == true;
    #endregion

    #region IsAnyNull
    /// <summary>
    /// Indicates whether the sequence contains any null strings.
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    public static bool IsAnyNull(params string[] strings) =>
        strings?.Any(_ => _ is null) == true;
    #endregion

    #region IsAnyNullOrEmpty
    /// <summary>
    /// Indicates whether the sequence contains any null or empty strings.
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    public static bool IsAnyNullOrEmpty(params string[] strings) =>
        strings?.Any(String.IsNullOrEmpty) == true;
    #endregion

    #region IsAnyNullOrWhiteSpace
    /// <summary>
    /// Indicates whether the sequence contains any null, empty or white-space only strings.
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    public static bool IsAnyNullOrWhiteSpace(params string[] strings) =>
        strings?.Any(String.IsNullOrWhiteSpace) == true;
    #endregion

    #region IsNumber
    /// <summary>
    /// Indicates whether the string contains the text representation of a number.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static bool IsNumber(string text) =>
        Double.TryParse(text, out double _);
    #endregion

    #region MakeFirstCharUpperCase
    /// <summary>
    /// Returns the string with the upper case first character.
    /// </summary>
    /// <param name="text"></param>
    /// <example>MakeFirstCharUpperCase("hello World!") ==> "Hello World!"</example>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static string MakeFirstCharUpperCase(string text) =>
        String.IsNullOrWhiteSpace(text) ? null : Char.ToUpper(text.First()) + text.Substring(1);
    #endregion

    #region MakeFirstCharWordUpperCase
    /// <summary>
    /// Make each word first char upper case.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string MakeFirstCharWordUpperCase(string text)
    {
        StringBuilder newText = new();
        bool isSeparator = true;

        foreach (char c in text)
        {
            newText.Append(isSeparator ? Char.ToUpper(c) : c);
            isSeparator = !Char.IsLetter(c);
        }

        return newText.ToString();
    }
    #endregion

    #region RemoveAccentuation
    /// <summary>
    /// Removes accentuation from the text.
    /// </summary>
    /// <param name="text"></param>
    /// ReSharper disable CommentTypo
    /// <example>RemoveAccentuation("Águas de São Paulo") ==> "Aguas de Sao Paulo"</example>
    /// ReSharper restore CommentTypo
    /// <returns></returns>
    public static string RemoveAccentuation(string text) =>
        String.IsNullOrWhiteSpace(text) //Checks the text.
            ? null
            : String.Join("",
                text.Normalize(NormalizationForm.FormD).Where(_ => CharUnicodeInfo.GetUnicodeCategory(_) != UnicodeCategory.NonSpacingMark));
    #endregion

    #region RemoveNewLine
    /// <summary>
    /// Removes new lines from a string.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="replacementText"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static string RemoveNewLine(string text, string replacementText = " ") =>
        text.Replace(Environment.NewLine, replacementText)
            .Replace("\n", replacementText)
            .Replace("\r", replacementText)
            .Replace("\v", replacementText);
    #endregion

    #region Replace
    /// <summary>
    /// Replaces all occurrences of a set of strings with a new value.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="oldValues"></param>
    /// <param name="newValue"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string Replace(this string st, IEnumerable<string> oldValues, string newValue)
    {
        if (oldValues == null)
            throw new ArgumentNullException(nameof(oldValues));

        foreach (string oldValue in oldValues)
            st = st.Replace(oldValue, newValue);

        return st;
    }
    #endregion

    #region ValidateEmail
    /// <summary>
    /// Checks if the selected string is a valid email.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool ValidateEmail(string email) =>
        email is not null && Regex.IsMatch(email,
            @"\A(?:[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?)\Z");
    #endregion

    #region ValidateUrl
    /// <summary>
    /// Checks URL type.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static UrlType ValidateUrl(string url) =>
        Uri.IsWellFormedUriString(url, UriKind.Absolute) ? UrlType.Absolute :
        Uri.IsWellFormedUriString(url, UriKind.Relative) ? UrlType.Relative : UrlType.Invalid;
    #endregion

    #endregion

    #region Private Methods

    #region InsertEllipsisPrivate
    /// <summary>
    /// Inserts ellipsis in a string.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="maxWidth"></param>
    /// <param name="isAtTheEnd"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    private static string InsertEllipsisPrivate(string text, int maxWidth, bool isAtTheEnd)
    {
        if (maxWidth < 3)
            throw new ArgumentOutOfRangeException(nameof(maxWidth), maxWidth, "The maximum width must be greater or equal to 3.");
        
        if (String.IsNullOrEmpty(text) || text.Length <= maxWidth)
            return text;

        //Inserts ellipsis at the end.
        if (isAtTheEnd)
            return text.Substring(0, maxWidth - 3) + "...";

        //Inserts ellipsis at the middle.
        maxWidth -= 3;
        int previousWidth = (int)Math.Ceiling(maxWidth / 2.0);
        int posteriorWidth = (int)Math.Floor(maxWidth / 2.0);

        return $"{text.Substring(0, previousWidth)}...{text.Substring(text.Length - posteriorWidth)}";
    }
    #endregion

    #endregion
}