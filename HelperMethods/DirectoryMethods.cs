using HelperMethods.Enums;
using HelperMethods.Helpers;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SearchOption = System.IO.SearchOption;

namespace HelperMethods;

public static class DirectoryMethods
{
    public static string ApplicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static string TemporaryDirectory = Path.GetTempPath();
    public static string UserDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

    #region Public Methods

    #region Copy
    /// <summary>
    /// Copies a folder to a new location.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="conflictAction"></param>
    public static void Copy(
        string source,
        string destination,
        FileNameConflictAction conflictAction = FileNameConflictAction.ThrowError) =>
        Transfer(source, destination, conflictAction, true);
    #endregion

    #region ClearDirectory
    /// <summary>
    /// Deletes all the files and directories from a given path.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="innerDirectoryAction"></param>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="DirectoryNotFoundException" />
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException" />
    /// <exception cref="PathTooLongException" />
    /// <exception cref="SecurityException" />
    /// <exception cref="UnauthorizedAccessException" />
    public static void ClearDirectory(string path, InnerDirectoryAction innerDirectoryAction = InnerDirectoryAction.Delete)
    {
        foreach (string file in ListFiles(path))
            File.Delete(file);

        if (innerDirectoryAction == InnerDirectoryAction.Ignore)
            return;

        foreach (string directory in ListDirectories(path))
            if (innerDirectoryAction == InnerDirectoryAction.Clear)
                ClearDirectory(directory, InnerDirectoryAction.Clear);
            else
                Directory.Delete(directory, true);
    }
    #endregion

    #region Create
    /// <summary>
    /// Creates a new empty directory.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="conflictAction"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="IOException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="PathTooLongException"></exception>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public static void Create(string path, FileNameConflictAction conflictAction = FileNameConflictAction.Skip)
    {
        if (Exists(path))
            switch (conflictAction)
            {
                case FileNameConflictAction.ThrowError:
                    throw new IOException($"\"{path}\" already exists.");

                case FileNameConflictAction.Overwrite:
                    Delete(path);
                    break;

                case FileNameConflictAction.Skip:
                    return;
            }

        Directory.CreateDirectory(path);
    }
    #endregion

    #region CreateRandomDirectory
    /// <summary>
    /// Creates new folder with a random name.
    /// </summary>
    /// <param name="parentDirectory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="DirectoryNotFoundException" />
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException" />
    /// <exception cref="PathTooLongException" />
    /// <exception cref="UnauthorizedAccessException" />
    public static string CreateRandomDirectory(string parentDirectory)
    {
        string newDirectoryPath = GenerateRandomDirectoryPath(parentDirectory);

        while (SystemMethods.Exists(newDirectoryPath))
            GenerateRandomDirectoryPath(parentDirectory);

        Directory.CreateDirectory(newDirectoryPath);

        return newDirectoryPath;
    }
    #endregion

    #region CreateTemporaryDirectory
    /// <summary>
    /// Creates new temporary folder.
    /// </summary>
    /// <param name="directoryName">New directory name, if null a random name will be generated.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="DirectoryNotFoundException" />
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException" />
    /// <exception cref="PathTooLongException" />
    /// <exception cref="SecurityException" />
    /// <exception cref="UnauthorizedAccessException" />
    public static string CreateTemporaryDirectory(string directoryName = null)
    {
        string novaPasta = Path.Combine(Path.GetTempPath(), directoryName ?? GenerateRandomDirectoryPath());

        //Checks if the folder exists.
        while (Exists(novaPasta))
            novaPasta = Path.Combine(Path.GetTempPath(), directoryName ?? GenerateRandomDirectoryPath());

        Directory.CreateDirectory(novaPasta);
        return novaPasta;
    }
    #endregion

    #region Delete
    /// <summary>
    /// Deletes a directory, if it exists.
    /// </summary>
    /// <returns>true if the directory exists, and it has been successfully deleted; false if directory does not exist.</returns>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="IOException" />
    /// <exception cref="NotSupportedException" />
    /// <exception cref="OperationCanceledException"></exception>
    /// <exception cref="PathTooLongException" />
    /// <exception cref="SecurityException"></exception>
    /// <exception cref="UnauthorizedAccessException" />
    public static bool Delete(string path, bool recycle = false)
    {
        if (Exists(path))
        {
            FileSystem.DeleteDirectory(path, UIOption.OnlyErrorDialogs, recycle ? RecycleOption.SendToRecycleBin : RecycleOption.DeletePermanently);
            return true;
        }

        return false;
    }
    #endregion

    #region Exists
    /// <inheritdoc cref="Directory.Exists"/>
    public static bool Exists(string path) =>
        Directory.Exists(path);
    #endregion

    #region GenerateRandomDirectoryPath
    /// <summary>
    /// Generates a random directory path.
    /// </summary>
    /// <param name="parentFolder">Optional parent folder. If null, the system user folder will be used.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="PlatformNotSupportedException"></exception>
    public static string GenerateRandomDirectoryPath(string parentFolder = null)
    {
        parentFolder ??= Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string nomePasta = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
        return Path.Combine(parentFolder, nomePasta);
    }
    #endregion

    #region GetDefaultDirectoryIcon
    /// <summary>
    /// Retrieves the system's default directory icon.
    /// </summary>
    /// <param name="largeIcon"></param>
    /// <param name="showLinkOverlay"></param>
    /// <returns></returns>
    public static ImageSource GetDefaultDirectoryIcon(bool largeIcon, bool showLinkOverlay = false)
    {
        // ReSharper disable CommentTypo
        WindowsHelper.ShStockIconInfo info = new();
        info.cbSize = (uint)Marshal.SizeOf(info);

        uint flags = 0x100 | (largeIcon ? 0U : 1U);

        if (showLinkOverlay)
            flags += 0x8000; //SHGFI_LINKOVERLAY;

        WindowsHelper.SHGetStockIconInfo(0x3/*SHSIID_FOLDER*/, flags, ref info);

        Icon icon = (Icon)Icon.FromHandle(info.hIcon).Clone(); // Get a copy that doesn't use the original handle.
        WindowsHelper.DestroyIcon(info.hIcon); // Clean up native icon to prevent resource leak.

        return Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        // ReSharper restore CommentTypo
    }
    #endregion

    #region GetDirectorySize
    /// <summary>
    /// Retrieves the size of the specified directory based on the sizes of its files and subdirectories.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="DirectoryNotFoundException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="IOException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="PathTooLongException"></exception>
    /// <exception cref="SecurityException"></exception>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public static long GetDirectorySize(string path)
    {
        long size = 0;
        Parallel.ForEach(Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories), _ => size += FileMethods.GetFileSize(_));
        return size;
    }
    #endregion

    #region GetParentDirectory
    /// <summary>
    /// Retrieves the parent directory name for the specified path.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="PathTooLongException"></exception>
    public static string GetParentDirectory(string path) =>
        Path.GetDirectoryName(path.TrimEnd('/', '\\'));
    #endregion

    #region IsDirectory
    /// <summary>
    /// Determines whether the selected path belongs to a directory.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DirectoryNotFoundException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="IOException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="PathTooLongException"></exception>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public static bool IsDirectory(string path) =>
        Exists(path) && File.GetAttributes(path).HasFlag(FileAttributes.Directory);
    #endregion

    #region IsParentDirectory
    /// <summary>
    /// Indicates whether a given path is a child of a specified directory.
    /// </summary>
    /// <param name="childPath">The path to be tested.</param>
    /// <param name="directoryPath">The possible parent directory.</param>
    /// <returns></returns> 
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="PathTooLongException"></exception>
    /// <exception cref="SecurityException"></exception>
    public static bool IsParentDirectory(string childPath, string directoryPath) =>
        Path.GetFullPath(childPath).StartsWith(Path.GetFullPath(directoryPath) + "\\");
    #endregion

    #region ListDirectories
    /// <summary>
    /// Lists selected directory subdirectories.
    /// </summary>
    /// <param name="dirPath"></param>
    /// <param name="searchSubFolders"></param>
    /// <param name="searchPattern"></param>
    /// <returns></returns> 
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="DirectoryNotFoundException"></exception>
    /// <exception cref="IOException"></exception>
    /// <exception cref="PathTooLongException"></exception>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public static IReadOnlyList<string> ListDirectories(string dirPath, string searchPattern = "*.*", bool searchSubFolders = false) =>
        Directory.GetDirectories(dirPath, searchPattern, searchSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
    #endregion

    #region ListFiles
    /// <summary>
    /// Returns the selected path's inner files list.
    /// </summary>
    /// <param name="dirPath"></param>
    /// <param name="searchSubfolders"></param>
    /// <param name="searchPattern"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="DirectoryNotFoundException"></exception>
    /// <exception cref="IOException"></exception>
    /// <exception cref="PathTooLongException"></exception>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public static IReadOnlyList<string> ListFiles(string dirPath, string searchPattern = "*.*", bool searchSubfolders = false) =>
        Directory.GetFiles(dirPath, searchPattern, searchSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
    #endregion

    #region ListFilesAndDirs
    /// <summary>
    /// Returns the selected path's inner files and directories list.
    /// </summary>
    /// <param name="dirPath"></param>
    /// <param name="searchSubFolders"></param>
    /// <param name="searchPattern"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="DirectoryNotFoundException"></exception>
    /// <exception cref="IOException"></exception>
    /// <exception cref="PathTooLongException"></exception>
    /// <exception cref="SecurityException"></exception>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public static IReadOnlyList<string> ListFilesAndDirs(string dirPath, string searchPattern = "*.*", bool searchSubFolders = false) =>
        Directory.GetFileSystemEntries(dirPath, searchPattern, searchSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
    #endregion

    #region Move
    /// <summary>
    /// Moves a folder to a new location.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="conflictAction"></param>
    public static void Move(
        string source,
        string destination,
        FileNameConflictAction conflictAction = FileNameConflictAction.ThrowError) =>
        Transfer(source, destination, conflictAction, false);
    #endregion

    #endregion

    #region Private Methods

    #region Transfer
    /// <summary>
    /// Moves or copies a folder to a new location.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="conflictAction"></param>
    /// <param name="keepOriginal"></param>
    private static void Transfer(
        string source,
        string destination,
        FileNameConflictAction conflictAction,
        bool keepOriginal)
    {
        if (!keepOriginal && !Exists(destination))
        {
            Directory.Move(source, destination);
            return;
        }

        Action<string, string, FileNameConflictAction> transferMethod =
            keepOriginal ? FileMethods.CopyToDirectory : FileMethods.MoveToDirectory;

        try
        {
            CreateFolder(source, destination, transferMethod);

            foreach (string sourceDir in ListDirectories(source, searchSubFolders: true))
                CreateFolder(sourceDir, sourceDir.Replace(source, destination), transferMethod);

            if (!keepOriginal)
                Delete(source);
        }
        catch
        {
            Delete(destination);
            throw;
        }

        #region Local Methods

        #region CreateFolder
        // Creates a new folder, if needed, and copies or moves the contents of the original folder into the new one.
        void CreateFolder(string sourceDir, string destinationDir, Action<string, string, FileNameConflictAction> transferAction)
        {
            if (!Exists(destinationDir))
                Create(destinationDir);

            foreach (string sourceFile in ListFiles(sourceDir))
                transferAction(sourceFile, destinationDir, conflictAction);
        }
        #endregion

        #endregion
    }
    #endregion

    #endregion
}
