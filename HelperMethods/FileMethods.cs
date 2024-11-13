using HelperMethods.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Xml.Serialization;

namespace HelperMethods;

public static class FileMethods
{
    #region Public Methods

    #region CreateFile
    /// <summary>
    /// Create a file with the selected content and returns the new file's path.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    /// <param name="conflictAction">Indicates the conflict action taken when the selected path already exists.</param>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="IOException" />
    /// <exception cref="PathTooLongException" />
    /// <exception cref="SecurityException" />
    /// <exception cref="UnauthorizedAccessException" />
    public static string CreateFile(string path, string content = "", ConflictAction conflictAction = ConflictAction.Break)
    {
        if (PathMethods.SolvePathConflict(path, conflictAction, false) is { } actualPath)
        {
            File.WriteAllText(actualPath, content ?? "");
            return actualPath;
        }

        return null;
    }
    #endregion

    #region CreateRandomFile
    /// <summary>
    /// Create a random name empty file in the selected folder.
    /// </summary>
    /// <param name="parentFolderPath"></param>
    /// <param name="content"></param>
    /// <param name="extension">The desired extension of the file. If not extension is provided, a random one is created.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException" />
    /// <exception cref="FileNotFoundException" />
    /// <exception cref="IOException" />
    /// <exception cref="PathTooLongException" />
    /// <exception cref="SecurityException" />
    /// <exception cref="UnauthorizedAccessException" />
    public static string CreateRandomFile(string parentFolderPath, string content = "", string extension = null)
    {
        string newFilePath = GenerateRandomFilePath(parentFolderPath, extension, true);
        File.WriteAllText(newFilePath, content ?? "");
        return newFilePath;
    }
    #endregion

    #region GenerateRandomFilePath
    /// <summary>
    /// Generates a random file path.
    /// </summary>
    /// <param name="rootFolderPath">Parent folder path.</param>
    /// <param name="extension">The desired extension of the file. If not extension is provided, a random one is created.</param>
    /// <param name="checkNotExisting">Indicates that the generated path should not exist in the current system.</param>
    /// <returns></returns>
    public static string GenerateRandomFilePath(string rootFolderPath, string extension = null, bool checkNotExisting = false)
    {
        string fileName;

        do
        {
            fileName = Path.GetRandomFileName();

            if (extension is not null)
                fileName = fileName.Replace(".", "") + PathMethods.InsertExtensionDot(extension);

            fileName = String.IsNullOrWhiteSpace(rootFolderPath) ? fileName : Path.Combine(rootFolderPath, fileName);
        } while (checkNotExisting && Exists(fileName));

        return fileName;
    }
    #endregion

    #region Exists
    /// <summary>
    /// Determines whether the specified file exists.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool Exists(string path) =>
        File.Exists(path);
    #endregion

    #region GetFileSize
    /// <summary>
    /// Retrieves the size of the specified file.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="FileNotFoundException" />
    /// <exception cref="IOException" />
    /// <exception cref="PathTooLongException" />
    /// <exception cref="SecurityException" />
    public static long GetFileSize(string path) =>
        new FileInfo(path).Length;
    #endregion

    #region IsFile
    /// <summary>
    /// Determines whether the selected path belongs to a file.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool IsFile(string path) =>
        Exists(path) && !DirectoryMethods.IsDirectory(path);
    #endregion

    #region OpenFile
    /// <summary>
    /// Opens the specified file.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="runAsAdmin"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="FileNotFoundException" />
    public static Process OpenFile(string path, bool runAsAdmin = false) =>
        Process.Start(new ProcessStartInfo
        {
            FileName = path,
            Verb = runAsAdmin ? "runas" : "",
            UseShellExecute = true
        });
    #endregion

    #region ReadFileLines
    /// <summary>
    /// Returns the lines of a file's content.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="FileNotFoundException" />
    /// <exception cref="IOException" />
    /// <exception cref="PathTooLongException" />
    /// <exception cref="SecurityException" />
    /// <exception cref="UnauthorizedAccessException" />
    public static string[] ReadFileLines(string path)
    {
        List<string> lines = [];

        //Opens the file in read-only mode without any kind of block.
        using StreamReader reader = new(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        while (!reader.EndOfStream)
            lines.Add(reader.ReadLine());

        return [.. lines];
    }
    #endregion

    #region ReadXml
    /// <summary>
    /// Reads the XML data from the specified file and converts it to the specified type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="FileNotFoundException" />
    /// <exception cref="IOException" />
    /// <exception cref="PathTooLongException" />
    /// <exception cref="SecurityException" />
    /// <exception cref="UnauthorizedAccessException" />
    public static T ReadXml<T>(string path)
    {
        using FileStream stream = new(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        return (T)new XmlSerializer(typeof(T)).Deserialize(stream);
    }
    #endregion

    #region RetrieveDataFromFile
    /// <summary>
    /// Retrieves data from an existing file.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="suppressException">Indicates whether the method should suppress any exception and return null.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="FileNotFoundException" />
    /// <exception cref="IOException" />
    /// <exception cref="PathTooLongException" />
    /// <exception cref="SerializationException" />
    /// <exception cref="SecurityException" />
    public static T RetrieveDataFromFile<T>(string path, bool suppressException = false)
    {
        try
        {
            using FileStream stream = new(path, FileMode.Open);
            return (T)new BinaryFormatter().Deserialize(stream);
        }
        catch
        {
            if (suppressException)
                return default;

            throw;
        }
    }
    #endregion

    #region RunFile
    /// <summary>
    /// Opens a file in the current operational system.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="arguments"></param>
    /// <param name="workingFolder"></param>
    /// <param name="runAsAdmin"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="FileNotFoundException" />
    public static Process RunFile(string path, string arguments = null, string workingFolder = null, bool runAsAdmin = false) =>
        SystemMethods.Run("explorer", $"\"{path}\" {arguments}", workingFolder, runAsAdmin, true);
    #endregion

    #region SaveDataToFile
    /// <summary>
    /// Saves the data into the selected file.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="data"></param>
    /// <param name="mode"></param>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="FileNotFoundException" />
    /// <exception cref="IOException" />
    /// <exception cref="PathTooLongException" />
    /// <exception cref="SerializationException" />
    /// <exception cref="SecurityException" />
    public static void SaveDataToFile<T>(string path, T data, FileMode mode = FileMode.OpenOrCreate)
    {
        using FileStream stream = new(path, mode);
        new BinaryFormatter().Serialize(stream, data);
        stream.Flush();
    }
    #endregion

    #region WriteXml
    /// <summary>
    /// Writes the specified data in XML format into the specified file.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="data"></param>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="FileNotFoundException" />
    /// <exception cref="IOException" />
    /// <exception cref="PathTooLongException" />
    /// <exception cref="SecurityException" />
    /// <exception cref="UnauthorizedAccessException" />
    public static void WriteXml<T>(string path, T data)
    {
        using FileStream stream = new(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
        new XmlSerializer(typeof(T)).Serialize(stream, data);
    }
    #endregion

    #endregion
}
