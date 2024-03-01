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
    public const string SpecialChars = "!\"#$%&'()#+,-./:;<>=?@[]^_`´{}|~";
    public const string SpaceChars = " \t";
    public const string UpperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string LowerCaseChars = "abcdefghijklmnopqrstuvwxyz";
    public const string NumericChars = "0123456789";
    public const string LineBreaks = "\n";

    #region Public Methods

    #region GetFirstNotEmpty
    /// <summary>
    /// Retrieves the first not empty string within the sequence.
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    public static string GetFirstNotEmpty(params string[] strings) =>
        strings?.FirstOrDefault(_ => _ != String.Empty);
    #endregion

    #region GetFirstNotNull
    /// <summary>
    /// Retrieves the first not null string within the sequence.
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    public static string GetFirstNotNull(params string[] strings) =>
        strings?.FirstOrDefault(_ => _ is not null);
    #endregion

    #region GetFirstNotNullOrEmpty
    /// <summary>
    /// Retrieves the first not null or empty string within the sequence.
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    public static string GetFirstNotNullOrEmpty(params string[] strings) =>
        strings?.FirstOrDefault(_ => !String.IsNullOrEmpty(_));
    #endregion

    #region GetFirstNotNullOrWhiteSpace
    /// <summary>
    /// Retrieves the first not null, empty or white-space only string within the sequence.
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    public static string GetFirstNotNullOrWhiteSpace(params string[] strings) =>
        strings?.FirstOrDefault(_ => !String.IsNullOrWhiteSpace(_));
    #endregion

    #region GenerateStringBuilder
    /// <summary>
    /// Generates a string builder with the selected start values.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static StringBuilder GenerateStringBuilder(params string[] values) =>
        new StringBuilder().Append(values);
    #endregion

    #region GetCurrentStringOrDefault
    /// <summary>
    /// Returns the default string if the original is null or has only white space characters.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="defaultText"></param>
    /// <param name="ignoreWhiteSpace">Allows white space chars-only string to be returned.</param>
    // ReSharper disable once UnusedMember.Global
    public static string GetCurrentStringOrDefault(string text, string defaultText, bool ignoreWhiteSpace = false) =>
        ignoreWhiteSpace && String.IsNullOrWhiteSpace(text) || String.IsNullOrEmpty(text) ? defaultText : text;
    #endregion

    #region GetRandomString*

    #region GetRandomString(int, params char[])
    /// <summary>
    /// Generates a random string using the selected chars.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="validChars"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">Desired width smaller than zero.</exception>
    public static string GetRandomString(int width, params char[] validChars) =>
        width < 0
            ? throw new ArgumentOutOfRangeException(nameof(width), "The desired string width must be greater than or equal to zero.")
            : validChars.Length == 0
                ? throw new ArgumentOutOfRangeException(nameof(width), "No valid chars found.")
                : GetRandomStringPrivate(width, validChars);
    #endregion

    #region GetRandomString(int, params string[])
    /// <summary>
    /// Generates a random string using the selected chars.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="validChars"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">Desired width smaller than zero.</exception>
    public static string GetRandomString(int width, params string[] validChars) =>
        GetRandomString(width, String.Join("", validChars).ToCharArray());
    #endregion

    #region GetRandomString(int, int, params char[])
    /// <summary>
    /// Generates a random string using the selected chars.
    /// </summary>
    /// <param name="minWidth"></param>
    /// <param name="maxWidth"></param>
    /// <param name="validChars"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">Desired width smaller than zero.</exception>
    // ReSharper disable once UnusedMember.Global
    public static string GetRandomString(int minWidth, int maxWidth, params char[] validChars)
    {
        ValidateWidth(minWidth, maxWidth);
        return GetRandomString(NumberMethods.GetRandomInt(minWidth, maxWidth, true), validChars);
    }

    #endregion

    #region GetRandomString(int, int, params string[])
    /// <summary>
    /// Generates a random string using the selected chars.
    /// </summary>
    /// <param name="minWidth"></param>
    /// <param name="maxWidth"></param>
    /// <param name="validChars"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">Desired width smaller than zero.</exception>
    // ReSharper disable once UnusedMember.Global
    public static string GetRandomString(int minWidth, int maxWidth, params string[] validChars)
    {
        ValidateWidth(minWidth, maxWidth);
        return GetRandomString(NumberMethods.GetRandomInt(minWidth, maxWidth, true), validChars);
    }

    #endregion

    #region GetRandomString(int, [CharacterClass])
    /// <summary>
    /// Generates a random string.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="classes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">Desired width smaller than zero.</exception>
    public static string GetRandomString(int width, CharacterClass classes = CharacterClass.All) =>
        width < 0
            ? throw new ArgumentOutOfRangeException(nameof(width), "The desired string width must be greater than or equal to zero.")
            : GetRandomString(width, GenerateValidCharsString(classes));
    #endregion

    #region GetRandomString(int, int, [CharacterClass])
    /// <summary>
    /// Generates a random string.
    /// </summary>
    /// <param name="maxWidth"></param>
    /// <param name="minWidth"></param>
    /// <param name="classes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"><see cref="minWidth"/> greater than <see cref="maxWidth"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><see cref="minWidth"/> or <see cref="maxWidth"/> smaller than zero.</exception>
    // ReSharper disable once UnusedMember.Global
    public static string GetRandomString(int minWidth, int maxWidth, CharacterClass classes = CharacterClass.All)
    {
        ValidateWidth(minWidth, maxWidth);
        return GetRandomString(NumberMethods.GetRandomInt(minWidth, maxWidth, true), classes);
    }
    #endregion

    #region GetRandomStrings(int, int, params char[])
    /// <summary>
    /// Generates random strings using the valid characters.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="stringCount"></param>
    /// <param name="validChars"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static string[] GetRandomStrings(int width, int stringCount, params char[] validChars)
    {
        List<string> generatedStrings = [];

        for (int i = 0; i < stringCount; i++)
            generatedStrings.Add(GetRandomStringPrivate(width, validChars));

        return generatedStrings.ToArray();
    }
    #endregion

    #region GetRandomStrings(int, int, params string[])
    /// <summary>
    /// Generates random strings using the valid characters.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="stringCount"></param>
    /// <param name="validChars"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static string[] GetRandomStrings(int width, int stringCount, params string[] validChars) =>
        GetRandomStrings(width, stringCount, String.Join("", validChars).ToCharArray());
    #endregion

    #region GetRandomStrings(int, int, int, params char[])
    /// <summary>
    /// Generates random strings using the valid characters.
    /// </summary>
    /// <param name="minWidth"></param>
    /// <param name="maxWidth"></param>
    /// <param name="stringCount"></param>
    /// <param name="validChars"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static string[] GetRandomStrings(int minWidth, int maxWidth, int stringCount, params char[] validChars) =>
        GetRandomStrings(NumberMethods.GetRandomInt(minWidth, maxWidth, true), stringCount, validChars);
    #endregion

    #region GetRandomStrings(int, int, int, params string[])
    /// <summary>
    /// Generates random strings using the valid characters.
    /// </summary>
    /// <param name="minWidth"></param>
    /// <param name="maxWidth"></param>
    /// <param name="stringCount"></param>
    /// <param name="validChars"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static string[] GetRandomStrings(int minWidth, int maxWidth, int stringCount, params string[] validChars) =>
        GetRandomStrings(minWidth, maxWidth, stringCount, String.Join("", validChars).ToCharArray());
    #endregion

    #region GetRandomStrings(int, int, [CharacterClass])
    /// <summary>
    /// Generates random strings.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="stringCount"></param>
    /// <param name="classes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    // ReSharper disable once UnusedMember.Global
    public static string[] GetRandomStrings(int width, int stringCount, CharacterClass classes = CharacterClass.All) =>
        GetRandomStrings(width, stringCount, GenerateValidCharsString(classes));
    #endregion

    #region GetRandomStrings(int, int, int, [CharacterClass])
    /// <summary>
    /// Generates random strings.
    /// </summary>
    /// <param name="minWidth"></param>
    /// <param name="maxWidth"></param>
    /// <param name="stringCount"></param>
    /// <param name="classes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    // ReSharper disable once UnusedMember.Global
    public static string[] GetRandomStrings(int minWidth, int maxWidth, int stringCount, CharacterClass classes = CharacterClass.All) =>
        GetRandomStrings(minWidth, maxWidth, stringCount, GenerateValidCharsString(classes));
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

    #region ValidateEmail

    /// <summary>
    /// Checks if the selected string is a valid email.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
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

    #region GetRandomString
    /// <summary>
    /// Generates a random string using the selected chars.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="validChars"></param>
    /// <returns></returns>
    private static string GetRandomStringPrivate(int width, char[] validChars)
    {
        StringBuilder text = new();

        for (int i = 0; i < width; i++)
            text.Append(validChars[NumberMethods.GetRandomInt(validChars.Length)]);

        return text.ToString();
    }
    #endregion

    #region GenerateValidCharsString
    /// <summary>
    /// Generates the valid chars string.
    /// </summary>
    /// <param name="classes"></param>
    /// <returns></returns>
    private static string GenerateValidCharsString(CharacterClass classes)
    {
        StringBuilder characters = new(96); //Max possible size.

        if (classes.HasFlag(CharacterClass.All))
            characters.Append(SpecialChars)
                .Append(SpaceChars)
                .Append(UpperCaseChars)
                .Append(LowerCaseChars)
                .Append(NumericChars)
                .Append(LineBreaks);
        else
        {
            if (classes.HasFlag(CharacterClass.Space))
                characters.Append(SpaceChars);

            if (classes.HasFlag(CharacterClass.Special))
                characters.Append(SpecialChars);

            if (classes.HasFlag(CharacterClass.UpperCase))
                characters.Append(UpperCaseChars);

            if (classes.HasFlag(CharacterClass.LowerCase))
                characters.Append(LowerCaseChars);

            if (classes.HasFlag(CharacterClass.Number))
                characters.Append(NumericChars);

            if (classes.HasFlag(CharacterClass.LineBreak))
                characters.Append(LineBreaks);
        }

        return characters.ToString();
    }
    #endregion

    #region ValidateWidth
    /// <summary>
    /// Validates the string width boundaries.
    /// </summary>
    /// <param name="minWidth"></param>
    /// <param name="maxWidth"></param>
    /// <exception cref="ArgumentOutOfRangeException">Any of the width boundaries smaller than zero.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Minimum width greater than the maximum width.</exception>
    private static void ValidateWidth(int minWidth, int maxWidth)
    {
        if (maxWidth < 0)
            throw new ArgumentOutOfRangeException(nameof(maxWidth), "The maximum string width must be greater than or equal to zero.");

        if (minWidth < 0)
            throw new ArgumentOutOfRangeException(nameof(minWidth), "The minimum string width must be greater than or equal to zero.");

        if (minWidth > maxWidth)
            throw new ArgumentOutOfRangeException(nameof(minWidth), "The minimum string width must be smaller than the maximum width.");
    }
    #endregion

    #endregion
}