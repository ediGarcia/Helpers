using HelperMethods.Enums;
using HelperMethods.Helpers;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HelperMethods;

public static class DirectoryMethods
{
    #region Public Methods

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
        foreach (string file in ListFiles(path, false))
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

    #region CreateDirectory
    /// <summary>
    /// Creates the selected directory.
    /// </summary>
    /// <param name="path">New directory path.</param>
    /// <param name="conflictAction">Indicates the conflict action taken when the destination path already exists.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="DirectoryNotFoundException" />
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException" />
    /// <exception cref="PathTooLongException" />
    /// <exception cref="UnauthorizedAccessException" />
    public static string CreateDirectory(string path, ConflictAction conflictAction = ConflictAction.Break)
    {
        if ((path = PathMethods.SolvePathConflict(path, conflictAction, true)) is null)
            return null;

        Directory.CreateDirectory(path);
        return path;
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
    public static string[] ListDirectories(string dirPath, bool searchSubFolders = true, string searchPattern = "*.*") =>
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
    public static string[] ListFiles(string dirPath, bool searchSubfolders = true, string searchPattern = "*.*") =>
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
    public static string[] ListFilesAndDirs(string dirPath, bool searchSubFolders = true, string searchPattern = "*.*") =>
        Directory.GetFileSystemEntries(dirPath, searchPattern, searchSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
    #endregion

    #endregion
}
