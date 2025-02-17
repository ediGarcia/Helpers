using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
// ReSharper disable UnusedMember.Global

namespace HelperMethods;

public static class PathMethods
{
    #region Public Methods

    #region CheckExtension
    /// <summary>
    /// Indicates whether the selected file has one of the selected extensions.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="extensions"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static bool CheckExtension(string fileName, params string[] extensions) =>
        extensions.Any(extension =>
            String.Equals(Path.GetExtension(fileName), InsertExtensionDot(extension), StringComparison.OrdinalIgnoreCase));
    #endregion

    #region GetAllMatchingPaths
    /// <summary>
    /// Returns the list of paths discovered from the selected pattern.
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetAllMatchingPaths(string pattern)
    {
        //If the pattern contains no wildcards.
        if (!pattern.Contains("*") && !pattern.Contains("?"))
            return SystemMethods.Exists(pattern) ? [pattern] : [];

        string separator = Path.DirectorySeparatorChar.ToString();
        string[] parts = pattern.Split(Path.DirectorySeparatorChar);

        //There must be an existing root path.
        if (parts[0].Contains('*') || parts[0].Contains('?'))
            throw new ArgumentException("The root path must not have a wildcard", nameof(parts));

        for (int i = 1; i < parts.Length; i++)
            //If this part of the path is a wildcard that needs expanding.
            if (parts[i].Contains('*') || parts[i].Contains('?'))
            {
                //Create an absolute path up to the current wildcard and check if it exists.
                string combined = Path.Combine(parts[0], String.Join(separator, parts.Take(i)));
                if (!Directory.Exists(combined))
                    return [];

                if (i == parts.Length - 1) //If this is the end of the path (a file name).
                    return Directory.GetFileSystemEntries(combined, parts[i], SearchOption.TopDirectoryOnly);

                IEnumerable<string> directories = Directory.EnumerateDirectories(combined, parts[i], SearchOption.TopDirectoryOnly);
                return directories.SelectMany(dir => GetAllMatchingPaths(Path.Combine(dir, String.Join(separator, parts.Skip(i + 1)))));
            }

        return [];
    }
    #endregion

    #region GetExtension
    /// <summary>
    /// Returns the extension of the specified path string.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetExtension(string path) =>
        Path.GetExtension(path);
    #endregion

    #region GetFileName
    /// <summary>
    /// Returns the file name of the specified path.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="removeExtension"></param>
    /// <returns></returns>
    public static string GetFileName(string path, bool removeExtension = false) =>
        removeExtension ? Path.GetFileNameWithoutExtension(path) : Path.GetFileName(path); 
    #endregion

    #region GetFullPath
    /// <summary>
    /// Returns the absolute path for the specified path string.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetFullPath(string path) =>
        Path.GetFullPath(path);
    #endregion

    #region GetParentDirectory
    /// <summary>
    /// Returns the directory information for the specified path string.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetParentDirectory(string path) =>
        Path.GetDirectoryName(path);
    #endregion

    #region InsertExtensionDot
    /// <summary>
    /// Inserts a dot (.) in the begging of the extension if necessary.
    /// </summary>
    /// <param name="extension"></param>
    /// <returns></returns>
    public static string InsertExtensionDot(string extension)
    {
        if (!extension.StartsWith("."))
            extension = "." + extension;

        return extension;
    }
    #endregion

    #endregion
}
