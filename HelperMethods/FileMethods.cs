using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Xml.Serialization;
using HelperMethods.Enums;
using Microsoft.VisualBasic.FileIO;

// ReSharper disable UnusedMember.Global

namespace HelperMethods;

public static class FileMethods
{
    #region Public Methods

    #region Copy
    /// <summary>
    /// Copies the existing file to a new file.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="conflictAction"></param>
    /// <exception cref="IOException"></exception>
    public static void Copy(string source, string destination, FileNameConflictAction conflictAction = FileNameConflictAction.ThrowError)
    {
        if (ValidateConflictAction(destination, conflictAction))
            File.Copy(source, destination, true);
    }
    #endregion

    #region CopyToDirectory
    /// <summary>
    /// Copies an existing file into the specified directory.
    /// </summary>
    /// <param name="sourceFile"></param>
    /// <param name="destinationDirectory"></param>
    /// <param name="conflictAction"></param>
    public static void CopyToDirectory(string sourceFile, string destinationDirectory, FileNameConflictAction conflictAction = FileNameConflictAction.ThrowError) =>
        Copy(sourceFile, Path.Combine(destinationDirectory, Path.GetFileName(destinationDirectory)), conflictAction);
    #endregion

    #region CreateFile
    /// <summary>
    /// Create a file with the selected content.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    /// <param name="overwrite"></param>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="IOException" />
    /// <exception cref="PathTooLongException" />
    /// <exception cref="SecurityException" />
    /// <exception cref="UnauthorizedAccessException" />
    public static void CreateFile(string path, string content = "", bool overwrite = false)
    {
        if (!overwrite && Exists(path))
            throw new IOException($"\"{path}\" already exists.");

        File.WriteAllText(path, content ?? "");
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

    #region Delete
    /// <summary>
    /// Deletes a file, if it exists.
    /// </summary>
    /// <returns>true if the file exists, and it has been successfully deleted; false if file does not exist.</returns>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="IOException" />
    /// <exception cref="NotSupportedException" />
    /// <exception cref="PathTooLongException" />
    /// <exception cref="SecurityException" />
    /// <exception cref="UnauthorizedAccessException" />
    public static bool Delete(string path, bool recycle = false)
    {
        if (Exists(path))
        {
            FileSystem.DeleteFile(path, UIOption.OnlyErrorDialogs, recycle ? RecycleOption.SendToRecycleBin : RecycleOption.DeletePermanently);
            return true;
        }

        return false;
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

    #region Move
    /// <summary>
    /// Moves the specified file to a new location.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="conflictAction"></param>
    public static void Move(string source, string destination, FileNameConflictAction conflictAction = FileNameConflictAction.ThrowError)
    {
        if (ValidateConflictAction(destination, conflictAction))
            File.Move(source, destination);
    }
    #endregion

    #region MoveToDirectory
    /// <summary>
    /// Moves an existing file into the specified folder.
    /// </summary>
    /// <param name="sourceFile"></param>
    /// <param name="destinationDirectory"></param>
    /// <param name="conflictAction"></param>
    public static void MoveToDirectory(
        string sourceFile,
        string destinationDirectory,
        FileNameConflictAction conflictAction = FileNameConflictAction.ThrowError) =>
        Move(sourceFile, Path.Combine(destinationDirectory, Path.GetFileName(sourceFile)), conflictAction);
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
            XmlSerializer serializer = new(typeof(T));
            using StreamReader reader = new(path);
            return (T)serializer.Deserialize(reader);
        }
        catch
        {
            if (suppressException)
                return default;

            throw;
        }
    }
    #endregion

    #region RunOrOpen
    /// <summary>
    /// Runs or opens the specified file in its default application.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="runAsAdmin"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="FileNotFoundException" />
    public static Process RunOrOpen(string path, bool runAsAdmin = false) =>
        Process.Start(new ProcessStartInfo
        {
            FileName = path,
            Verb = runAsAdmin ? "runas" : "",
            UseShellExecute = true
        });
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
    public static void SaveDataToFile<T>(string path, T data, Classes.FileMode mode = Classes.FileMode.OpenOrCreate)
    {
        if (Exists(path))
        {
            if (mode is Classes.FileMode.CreateNew)
                throw new IOException($"The file \"{path}\" already exists.");
        }
        else if (mode is Classes.FileMode.Open)
            throw new FileNotFoundException($"The file \"{path}\" does not exist.");

        XmlSerializer serializer = new(typeof(T));
        using StreamWriter writer = new(path, mode == Classes.FileMode.Append);
        serializer.Serialize(writer, data);
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

    #region Private Methods

    #region ValidateConflictAction
    /// <summary>
    /// Validates the file name conflict action according to the destination file state.
    /// </summary>
    /// <param name="destinationPath"></param>
    /// <param name="conflictAction"></param>
    /// <returns></returns>
    /// <exception cref="IOException"></exception>
    private static bool ValidateConflictAction(string destinationPath, FileNameConflictAction conflictAction)
    {
        if (Exists(destinationPath))
            switch (conflictAction)
            {
                case FileNameConflictAction.ThrowError:
                    throw new IOException($"\"{destinationPath}\" already exists.");

                case FileNameConflictAction.Overwrite:
                    break;

                case FileNameConflictAction.Skip:
                    return false;
            }

        return true;
    }
    #endregion

    #endregion
}
