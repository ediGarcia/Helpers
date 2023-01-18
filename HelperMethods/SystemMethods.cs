using HelperExtensions;
using HelperMethods.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
// ReSharper disable UnusedMember.Global

namespace HelperMethods;

public static class SystemMethods
{
    #region Enums

    /// <summary>
    /// Possible flags for the SHFileOperation method.
    /// </summary>
    [Flags]
    private enum FileOperationFlags : ushort
    {
        /// <summary>
        /// Do not show a dialog during the process
        /// </summary>
        FofSilent = 0x0004,
        /// <summary>
        /// Do not ask the user to confirm selection
        /// </summary>
        FofNoConfirmation = 0x0010,
        /// <summary>
        /// Delete the file to the recycle bin.  (Required flag to send a file to the bin
        /// </summary>
        FofAllowUndo = 0x0040,
        /// <summary>
        /// Do not show the names of the files or folders that are being recycled.
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        FofSimpleProgress = 0x0100,
        /// <summary>
        /// Suppress errors, if any occur during the process.
        /// </summary>
        FofNoErrorUi = 0x0400,
        /// <summary>
        /// Warn if files are too big to fit in the recycle bin and will need
        /// to be deleted completely.
        /// </summary>
        FofWantNukeWarning = 0x4000
    }

    /// <summary>
    /// File Operation Function Type for SHFileOperation
    /// </summary>
    private enum FileOperationType : uint
    {
        /// <summary>
        /// Move the objects
        /// </summary>
        FoMove = 0x0001,
        /// <summary>
        /// Copy the objects
        /// </summary>
        FoCopy = 0x0002,
        /// <summary>
        /// Delete (or recycle) the objects
        /// </summary>
        FoDelete = 0x0003,
        /// <summary>
        /// Rename the object(s)
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        FoRename = 0x0004
    }

    /// <summary>
    /// ShFileOpStruct for SHFileOperation from COM
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct ShFileOpStruct
    {
        // ReSharper disable once IdentifierTypo
        private readonly IntPtr _hwnd;
        [MarshalAs(UnmanagedType.U4)] public FileOperationType WFunc;
        public string PFrom;
        public string PTo;
        public FileOperationFlags FFlags;
        [MarshalAs(UnmanagedType.Bool)] private readonly bool _fAnyOperationsAborted;
        private readonly IntPtr _hNameMappings;
        private readonly string _lpszProgressTitle;
    }

    #endregion

    #region Imports

    [DllImport("shell32.dll", EntryPoint = "ExtractIconA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    private static extern IntPtr ExtractIcon(int hInst, string lpszExeFileName, int nIconIndex);

    // ReSharper disable once StringLiteralTypo
    [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
    // ReSharper disable once IdentifierTypo
    private static extern uint AssocQueryString(int flags, int str, string pszAssoc, string pszExtra, [Out] StringBuilder pszOut, ref uint pcchOut);

    [DllImport("Shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    private static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);

    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    private static extern int SHFileOperation(ref ShFileOpStruct fileOp);

    // ReSharper disable once StringLiteralTypo
    [DllImport("shlwapi.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false, ThrowOnUnmappableChar = true)]
    private static extern int SHLoadIndirectString(string pszSource, StringBuilder pszOutBuf, int cchOutBuf, IntPtr ppvReserved);

    #endregion

    #region Public Methods

    #region AppendCustomPathSuffix
    /// <summary>
    /// Appends a custom suffix to the selected path if it already exists.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="customSuffix"></param>
    /// <param name="force">Indicates whether the suffix should be appended even if the path does not exist.</param>
    /// <returns></returns>
    public static string AppendCustomPathSuffix(string path, string customSuffix, bool force = false)
    {
        if (force && !CheckPathExists(path))
            return AppendPathSuffix(path, customSuffix);

        string suffix = "";
        string newPath;

        while (CheckPathExists(newPath = AppendPathSuffix(path, suffix)))
            suffix += customSuffix;

        return newPath;
    }
    #endregion

    #region AppendPathSuffix
    /// <summary>
    /// Appends a suffix to the selected path if it already exists.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="suffixType"></param>
    /// <param name="force">Indicates whether the suffix should be appended even if the path does not exist.</param>
    /// <returns></returns>
    public static string AppendPathSuffix(string path, FileSuffixType suffixType, bool force = false)
    {
        if (force && !CheckPathExists(path))
            return AppendPathSuffix(path, suffixType == FileSuffixType.Copy ? " - Copy" : " (1)");

        int fileCount = 0;
        string suffix = "";
        string newPath;

        while (CheckPathExists(newPath = AppendPathSuffix(path, suffix)))
            switch (suffixType)
            {
                case FileSuffixType.Copy:
                    suffix += " - Copy";
                    break;

                case FileSuffixType.Numeric:
                    suffix = $" ({++fileCount})";
                    break;
            }

        return newPath;
    }
    #endregion

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

    #region CheckFileExists
    /// <summary>
    /// Determines whether the specified file exists.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool CheckFileExists(string path) =>
        File.Exists(path);
    #endregion

    #region CheckDirectoryExists
    /// <summary>
    /// Determines whether the specified directory exists.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool CheckDirectoryExists(string path) =>
        Directory.Exists(path);
    #endregion

    #region CheckPathExists
    /// <summary>
    /// Returns a value indicating whether the selected path exists.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool CheckPathExists(string path) =>
        CheckFileExists(path) || CheckDirectoryExists(path);
    #endregion

    #region Copy
    /// <summary>
    /// Copies a file or folder and its contents to the destination path.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="conflictAction"></param>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="DirectoryNotFoundException" />
    /// <exception cref="IOException" />
    /// <exception cref="NotSupportedException" />
    /// <exception cref="PathTooLongException" />
    /// <exception cref="UnauthorizedAccessException" />
    /// <returns>Returns the destination path.</returns>
    public static string Copy(string source, string destination, ConflictAction conflictAction) =>
        ChangeFileDirectoryLocation(source, destination, conflictAction, true);
    #endregion

    #region CopyOS
    /// <summary>
    /// Copies files and folders and its contents using the system's UI.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    public static void CopyOs(string source, string destination) =>
        CallSystemFileOperation(source, destination, FileOperationType.FoCopy, 0);
    #endregion

    #region CopyToFolder(string, string, ConflictAction)
    /// <summary>
    /// Copies the selected file/folder and its contents into the destination folder.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destinationFolder"></param>
    /// <param name="conflictAction"></param>
    /// <returns></returns>
    public static string CopyToFolder(string source, string destinationFolder, ConflictAction conflictAction) =>
        Copy(source, Path.Combine(destinationFolder, Path.GetFileName(source)), conflictAction);
    #endregion

    #region CopyToFolder(string, ConflictAction, params string[])
    /// <summary>
    /// Copies the selected files/folders and their contents into the destination folder.
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="conflictAction"></param>
    /// <param name="sourceEntries"></param>
    public static void CopyToFolder(string destination, ConflictAction conflictAction, params string[] sourceEntries) =>
        CopyToFolder(destination, sourceEntries, conflictAction);
    #endregion

    #region CopyToFolder(string, IEnumerable<string>, ConflictAction)
    /// <summary>
    /// Copies the selected files/folders and their contents into the destination folder.
    /// </summary>
    /// <param name="destinationFolder"></param>
    /// <param name="sourceEntries"></param>
    /// <param name="conflictAction"></param>
    public static void CopyToFolder(string destinationFolder, IEnumerable<string> sourceEntries, ConflictAction conflictAction) =>
        sourceEntries.ForEach(_ => CopyToFolder(_, destinationFolder, conflictAction));
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
        if ((path = ManageConflictAction(path, true, conflictAction, true)) is null)
            return null;

        Directory.CreateDirectory(path);
        return path;
    }
    #endregion

    #region CreateEmptyFile
    /// <summary>
    /// Creates an empty file.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="conflictAction">Indicates the conflict action taken when the selected path already exists.</param>
    /// <exception cref="IOException" />
    public static string CreateEmptyFile(string path, ConflictAction conflictAction) =>
        CreateFile(path, null, conflictAction);
    #endregion

    #region CreateFile
    /// <summary>
    /// Create a file with the selected content and returns the new file's path.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    /// <param name="conflictAction">Indicates the conflict action taken when the selected path already exists.</param>
    /// <exception cref="IOException" />
    public static string CreateFile(string path, string content, ConflictAction conflictAction)
    {
        if ((path = ManageConflictAction(path, false, conflictAction, false)) is null)
            return null;

        File.WriteAllText(path, content ?? "");
        return path;
    }
    #endregion

    #region CreateProcess(string, [bool])
    /// <summary>
    /// Creates a process to run a operational system command.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="isAdmin"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException" />
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ObjectDisposedException" />
    /// <exception cref="FileNotFoundException" />
    /// <exception cref="System.ComponentModel.Win32Exception" />
    public static Process CreateProcess(string path, bool isAdmin = false) =>
        new() { StartInfo = new(path) { WorkingDirectory = Path.GetDirectoryName(path), Verb = isAdmin ? "runas" : "" } };
    #endregion

    #region CreateProcess(string, string, [bool])
    /// <summary>
    /// Creates a process to run a operational system command.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="arguments"></param>
    /// <param name="isAdmin"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException" />
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ObjectDisposedException" />
    /// <exception cref="FileNotFoundException" />
    /// <exception cref="System.ComponentModel.Win32Exception" />
    public static Process CreateProcess(string command, string arguments, bool isAdmin = false) =>
        new() { StartInfo = new(command, arguments) { WorkingDirectory = Path.GetDirectoryName(command), Verb = isAdmin ? "runas" : "" } };
    #endregion

    #region CreateProcessAt(string, string, [bool])
    /// <summary>
    /// Creates a process to run a operational system command at the specified location.
    /// </summary>
    /// <param name="workingFolder"></param>
    /// <param name="path"></param>
    /// <param name="isAdmin"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException" />
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ObjectDisposedException" />
    /// <exception cref="FileNotFoundException" />
    /// <exception cref="System.ComponentModel.Win32Exception" />
    public static Process CreateProcessAt(string workingFolder, string path, bool isAdmin = false) =>
        new() { StartInfo = new(path) { WorkingDirectory = workingFolder, Verb = isAdmin ? "runas" : "" } };
    #endregion

    #region CreateProcessAt(string, string, string, [bool])

    /// <summary>
    /// Creates a process to run a operational system command at the specified location.
    /// </summary>
    /// <param name="workingFolder"></param>
    /// <param name="command"></param>
    /// <param name="arguments"></param>
    /// <param name="isAdmin"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException" />
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ObjectDisposedException" />
    /// <exception cref="FileNotFoundException" />
    /// <exception cref="System.ComponentModel.Win32Exception" />
    public static Process CreateProcessAt(string workingFolder, string command, string arguments, bool isAdmin = false) =>
        new()
        {
            StartInfo = new(command, arguments)
            {
                WorkingDirectory = workingFolder,
                Verb = isAdmin ? "runas" : ""
            }
        };

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

        while (CheckPathExists(newDirectoryPath))
            GenerateRandomDirectoryPath(parentDirectory);

        Directory.CreateDirectory(newDirectoryPath);

        return newDirectoryPath;
    }
    #endregion

    #region CreateRandomFile
    /// <summary>
    /// Create a random name empty file in the selected folder.
    /// </summary>
    /// <param name="parentFolderPath"></param>
    /// <returns></returns>
    public static string CreateRandomFile(string parentFolderPath)
    {
        string newFilePath = GenerateRandomFilePath(parentFolderPath);

        while (CheckPathExists(newFilePath))
            GenerateRandomFilePath(parentFolderPath);

        File.Create(newFilePath).Dispose();

        return newFilePath;
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
    /// <exception cref="System.Security.SecurityException" />
    /// <exception cref="UnauthorizedAccessException" />
    public static string CreateTemporaryDirectory(string directoryName = null)
    {
        string novaPasta = Path.Combine(Path.GetTempPath(), directoryName ?? GenerateRandomDirectoryPath());

        //Checks if the folder exists.
        while (Directory.Exists(novaPasta))
            novaPasta = Path.Combine(Path.GetTempPath(), directoryName ?? GenerateRandomDirectoryPath());

        Directory.CreateDirectory(novaPasta);
        return novaPasta;
    }
    #endregion

    #region Delete
    /// <summary>
    /// Deletes files, folder and its contents.
    /// </summary>
    /// <param name="paths">Paths to be deleted.</param>
    public static void Delete(params string[] paths) =>
        paths.ForEach(path =>
        {
            if (IsDirectory(path))
                Directory.Delete(path, true);
            else
                File.Delete(path);
        });
    #endregion

    #region DeleteOS
    /// <summary>
    /// Deletes files and folders and its contents using the system's UI.
    /// </summary>
    /// <param name="path"></param>
    public static void DeleteOs(string path)
    {
        if (!CheckPathExists(path))
            throw new IOException($"Could not find file {path}.");

        CallSystemFileOperation(path, null, FileOperationType.FoDelete, 0);
    }
    #endregion

    #region ExtractIcon(string, [bool], [Icon])
    /// <summary>
    /// Extracts an icon from a binary file.
    /// </summary>
    /// <param name="binaryFilePath"></param>
    /// <param name="getLargeVersion"></param>
    /// <param name="defaultIcon">Default icon, returned when no icon is found in file.</param>
    /// <returns></returns>
    public static Icon ExtractIcon(string binaryFilePath, bool getLargeVersion = true, Icon defaultIcon = null) =>
        ExtractIcon(binaryFilePath, getLargeVersion, 0, defaultIcon);
    #endregion

    #region ExtractIcon(string, [bool], [int], [Icon])
    /// <summary>
    /// Extracts an icon from a binary file.
    /// </summary>
    /// <param name="binaryFilePath"></param>
    /// <param name="iconIndex">Icon index inside the binary file.</param>
    /// <param name="getLargeVersion"></param>
    /// <param name="defaultIcon">Default icon, returned when no icon is found in file.</param>
    /// <returns></returns>
    public static Icon ExtractIcon(string binaryFilePath, bool getLargeVersion = true, int iconIndex = 0, Icon defaultIcon = null)
    {
        ExtractIconEx(binaryFilePath, iconIndex, out IntPtr large, out IntPtr small, 1);

        try
        {
            return Icon.FromHandle(getLargeVersion ? large : small);
        }
        catch
        {
            return defaultIcon;
        }
    }
    #endregion

    #region ExtractIconExtension
    /// <summary>
    /// Retrieves the system's default icon for the selected extension.
    /// </summary>
    /// <param name="extension"></param>
    /// <param name="defaultIcon">The default value, if the extraction fails.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="System.Security.SecurityException" />
    /// <exception cref="UnauthorizedAccessException" />
    /// <exception cref="IOException" />
    public static Icon ExtractIconExtension(string extension, Icon defaultIcon = null)
    {
        //Valid extension check.
        if (String.IsNullOrWhiteSpace(extension))
            throw new NullReferenceException("The selected extension cannot be null, empty or white spaces.");

        extension = InsertExtensionDot(extension);

        RegistryKey rkRoot = Registry.ClassesRoot; //Access Windows' keys registry.

        //Tries retrieving the icon.
        if (rkRoot.GetSubKeyNames().FirstOrDefault(x => x == extension) is { } keyName &&
            rkRoot.OpenSubKey(keyName) is { } rkFileType &&
            rkFileType.GetValue("") is string defaultValue &&
            rkRoot.OpenSubKey(defaultValue + @"\DefaultIcon") is { } rkFileIcon &&
            rkFileIcon.GetValue("") is string data)
        {
            data = data.Replace("\"", "");

            string foundExtension;
            int iconIndex = 0;
            int commaPosition = data.IndexOf(",", StringComparison.Ordinal);

            if (commaPosition > 0)
            {
                foundExtension = data.Substring(0, commaPosition);
                Int32.TryParse(data.Substring(commaPosition + 1), out iconIndex);
            }
            else
                foundExtension = data;

            try
            {
                return Icon.FromHandle(ExtractIcon(0, foundExtension, iconIndex));
            }
            catch
            {
                return defaultIcon; //Error extracting icon.
            }
        }

        return defaultIcon; //No icon found.
    }
    #endregion

    #region ExtractIconFile
    /// <summary>
    /// Extracts the file icon.
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="getBigVersion">Indicates whether the method should return the icon's big version.</param>
    /// <param name="defaultIcon">The default value, if the extraction fails.</param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static Icon ExtractIconFile(string filePath, bool getBigVersion = true, Icon defaultIcon = null)
    {
        Icon icon = null;
        string iconPath = RetrieveIconPath(Path.GetExtension(filePath));

        if (!String.IsNullOrEmpty(iconPath))
        {
            if (iconPath.StartsWith("@{"))
            {
                StringBuilder outBuff = new(1024);
                SHLoadIndirectString(iconPath, outBuff, outBuff.Capacity, IntPtr.Zero);

                icon = Icon.FromHandle(new Bitmap(outBuff.ToString()).GetHicon());
            }
            else
            {
                int iconIndex = 0;

                if (iconPath.Contains(","))
                {
                    iconIndex = Int32.Parse(iconPath.Substring(iconPath.IndexOf(",", StringComparison.Ordinal) + 1));
                    iconPath = iconPath.Substring(0, iconPath.IndexOf(",", StringComparison.Ordinal));
                }

                icon = ExtractIcon(iconPath, getBigVersion, iconIndex);
            }
        }

        icon ??= ExtractIconExtension(Path.GetExtension(filePath));

        return icon ?? defaultIcon;
    }
    #endregion

    #region ForceDelete
    /// <summary>
    /// Deletes files, folder and its contents even if they are read only.
    /// </summary>
    /// <param name="entries">Paths to be deleted.</param>
    public static void ForceDelete(params string[] entries) =>
        entries.ForEach(entry =>
        {
            if (IsDirectory(entry))
            {
                Directory.EnumerateFileSystemEntries(entry).ForEach(__ => ForceDelete(__)); //Deletes each folder entry.
                new DirectoryInfo(entry) { Attributes = FileAttributes.Normal }.Delete();
            }
            else
                new FileInfo(entry) { Attributes = FileAttributes.Normal }.Delete();
        });
    #endregion

    #region GenerateRandomFilePath
    /// <summary>
    /// Generates a random file path.
    /// </summary>
    /// <param name="rootFolderPath">Parent folder path.</param>
    /// <returns></returns>
    public static string GenerateRandomFilePath(string rootFolderPath)
    {
        string fileName = Path.GetRandomFileName();
        return String.IsNullOrWhiteSpace(rootFolderPath) ? fileName : Path.Combine(rootFolderPath, fileName);
    }
    #endregion

    #region GenerateRandomDirectoryPath
    /// <summary>
    /// Generates a random directory path.
    /// </summary>
    /// <param name="parentFolder">.</param>
    /// <returns></returns>
    public static string GenerateRandomDirectoryPath(string parentFolder = null)
    {
        string nomePasta = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
        return String.IsNullOrWhiteSpace(parentFolder) ? nomePasta : Path.Combine(parentFolder, nomePasta);
    }
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
            return CheckPathExists(pattern) ? new[] { pattern } : new string[0];

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
                    return new string[0];

                if (i == parts.Length - 1) //If this is the end of the path (a file name).
                    return Directory.GetFileSystemEntries(combined, parts[i], SearchOption.TopDirectoryOnly);

                IEnumerable<string> directories = Directory.EnumerateDirectories(combined, parts[i], SearchOption.TopDirectoryOnly);
                return directories.SelectMany(dir => GetAllMatchingPaths(Path.Combine(dir, String.Join(separator, parts.Skip(i + 1)))));
            }

        return new string[0];
    }
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
    /// Retrieves the parent directory name for the specified path.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetParentDirectory(string path) =>
        Path.GetDirectoryName(path.TrimEnd('/', '\\'));
    #endregion

    #region GetSizeFromPath
    /// <summary>
    /// Retrieves the size of a path (if it is a directory, it will be calculated recursively),
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static long GetSizeFromPath(string path) =>
        IsDirectory(path)
            ? GetDirectorySize(path)
            : IsFile(path)
                ? GetFileSize(path)
                : throw new FileNotFoundException($"Could not find a file or a directory named '{path}'.");
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

    #region IsDirectory
    /// <summary>
    /// Determines whether the selected path belongs to a directory.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool IsDirectory(string path) =>
        CheckDirectoryExists(path) && File.GetAttributes(path).HasFlag(FileAttributes.Directory);
    #endregion

    #region IsFile
    /// <summary>
    /// Determines whether the selected path belongs to a file.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool IsFile(string path) =>
        CheckFileExists(path) && !IsDirectory(path);
    #endregion

    #region IsValidLocalPath
    /// <summary>
    /// Indicates whether the selected string represents a valid local path.
    /// </summary>
    /// <param name="path">Path to validate.</param>
    /// <param name="onlyAbsolute">Indicates whether only absolute paths should be accepted.</param>
    /// <param name="onlyExisting">Indicates whether only existing paths should be accepted.</param>
    /// <returns></returns>
    public static bool IsValidLocalPath(string path, bool onlyAbsolute = false, bool onlyExisting = false) =>
        !String.IsNullOrWhiteSpace(path)
        && !path.ContainsAny(Path.GetInvalidPathChars())
        && (!onlyAbsolute || Path.IsPathRooted(path))
        && (!onlyExisting || CheckPathExists(path));
    #endregion

    #region ListDirectories
    /// <summary>
    /// Lists selected directory sub-directories.
    /// </summary>
    /// <param name="dirPath"></param>
    /// <param name="searchSubFolders"></param>
    /// <param name="searchPattern"></param>
    /// <returns></returns>
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
    public static string[] ListFilesAndDirs(string dirPath, bool searchSubFolders = true, string searchPattern = "*.*") =>
        Directory.GetFileSystemEntries(dirPath, searchPattern, searchSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
    #endregion

    #region Move
    /// <summary>
    /// Moves a file or folder and its contents to the destination path.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="conflictAction"></param>
    /// <exception cref="ArgumentException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="DirectoryNotFoundException" />
    /// <exception cref="IOException" />
    /// <exception cref="NotSupportedException" />
    /// <exception cref="PathTooLongException" />
    /// <exception cref="UnauthorizedAccessException" />
    /// <exception cref="UnauthorizedAccessException" />
    /// <returns>Returns the destination path.</returns>
    public static string Move(string source, string destination, ConflictAction conflictAction) =>
        ChangeFileDirectoryLocation(source, destination, conflictAction, false);
    #endregion

    #region MoveOS
    /// <summary>
    /// Moves files or folder and its contents using the system's UI.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    public static void MoveOs(string source, string destination) =>
        CallSystemFileOperation(source, destination, FileOperationType.FoMove, 0);
    #endregion

    #region MoveToFolder(string, string, ConflictAction)
    /// <summary>
    /// Copies the selected file/folder and its contents into the destination folder.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destinationFolder"></param>
    /// <param name="conflictAction"></param>
    /// <returns></returns>
    public static string MoveToFolder(string source, string destinationFolder, ConflictAction conflictAction) =>
        Move(source, Path.Combine(destinationFolder, Path.GetFileName(source)), conflictAction);

    #endregion

    #region MoveToFolder(string, ConflictAction, params string[])
    /// <summary>
    /// Moves the selected files/folders and their contents into the destination folder.
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="conflictAction"></param>
    /// <param name="sourceEntries"></param>
    public static void MoveToFolder(string destination, ConflictAction conflictAction, params string[] sourceEntries) =>
        MoveToFolder(destination, sourceEntries, conflictAction);
    #endregion

    #region MoveToFolder(string, IEnumerable<string>, ConflictAction)
    /// <summary>
    /// Copies the selected files/folders and their contents into the destination folder.
    /// </summary>
    /// <param name="destinationFolder"></param>
    /// <param name="sourceEntries"></param>
    /// <param name="conflictAction"></param>
    public static void MoveToFolder(string destinationFolder, IEnumerable<string> sourceEntries, ConflictAction conflictAction) =>
        sourceEntries.ForEach(_ => MoveToFolder(_, destinationFolder, conflictAction));
    #endregion

    #region ReadFileLines
    /// <summary>
    /// Returns the lines of a file's content.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string[] ReadFileLines(string path)
    {
        List<string> lines = new();

        //Opens the file in read-only mode without any kind of block.
        using StreamReader reader = new(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        while (!reader.EndOfStream)
            lines.Add(reader.ReadLine());

        return lines.ToArray();
    }
    #endregion

    #region RetrieveDataFromFile
    /// <summary>
    /// Retrieves data from an existing file.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public static T RetrieveDataFromFile<T>(string path)
    {
        using FileStream stream = new(path, FileMode.Open);
        return (T)new BinaryFormatter().Deserialize(stream);
    }
    #endregion

    #region Run
    /// <summary>
    /// Runs a command through operational system.
    /// </summary>
    /// <param name="workingFolder"></param>
    /// <param name="path"></param>
    /// <param name="arguments"></param>
    /// <param name="isAdmin"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException" />
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ObjectDisposedException" />
    /// <exception cref="FileNotFoundException" />
    /// <exception cref="System.ComponentModel.Win32Exception" />
    public static Process Run(string path, string arguments = null, string workingFolder = null, bool isAdmin = false) =>
        Process.Start(new ProcessStartInfo(path, arguments)
        {
            WorkingDirectory = workingFolder ?? Path.GetDirectoryName(path),
            UseShellExecute = true,
            Verb = isAdmin ? "runas" : ""
        });
    #endregion

    #region SaveDataToFile
    /// <summary>
    /// Saves the data into the selected file (if it exists, will be overwritten, otherwise, will be created).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="data"></param>
    public static void SaveDataToFile<T>(string path, T data)
    {
        using FileStream stream = new(path, FileMode.OpenOrCreate);
        new BinaryFormatter().Serialize(stream, data);
    }
    #endregion

    #region SendToRecycleBin
    /// <summary>
    /// Send file or directory to the Recycle Bin.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="suppressErrors">Indicates whether errors should be suppressed.</param>
    /// <param name="suppressBigFileWarning">Indicates whether big files should be permanently deleted without the user confirmation.</param>
    public static void SendToRecycleBin(string path, bool suppressErrors = true, bool suppressBigFileWarning = true)
    {
        if (!CheckPathExists(path))
            throw new IOException($"Could not find file {path}.");

        FileOperationFlags flags = FileOperationFlags.FofAllowUndo | FileOperationFlags.FofNoConfirmation;

        if (suppressErrors)
            flags |= FileOperationFlags.FofNoErrorUi;

        flags |= suppressBigFileWarning ? FileOperationFlags.FofSilent : FileOperationFlags.FofWantNukeWarning;

        CallSystemFileOperation(path, null, FileOperationType.FoDelete, flags);
    }
    #endregion

    #endregion

    #region Private Methods

    #region AppendPathSuffix
    /// <summary>
    /// Appends the selected suffix to the file/directory name.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="suffix"></param>
    /// <returns></returns>
    private static string AppendPathSuffix(string path, string suffix) =>
        Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + suffix + Path.GetExtension(path));
    #endregion

    #region CallSystemFileOperation
    /// <summary>
    /// Calls operational system's file operation
    /// </summary>
    /// <param name="pathTo">Destination path for copy and move commands.</param>
    /// <param name="fileOperationType"></param>
    /// <param name="flags"></param>
    /// <param name="pathFrom"></param>
    private static void CallSystemFileOperation(string pathFrom, string pathTo, FileOperationType fileOperationType, FileOperationFlags flags)
    {
        ShFileOpStruct fs = new()
        {
            WFunc = fileOperationType,
            PFrom = pathFrom + '\0' + '\0',
            FFlags = flags
        };

        if (fileOperationType is FileOperationType.FoCopy or FileOperationType.FoMove)
            fs.PTo = pathTo;

        SHFileOperation(ref fs);
    }
    #endregion

    #region ChangeFileFolderLocation
    /// <summary>
    /// Changes a file or folder location in the file system.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="conflictAction"></param>
    /// <param name="keepOriginal">Indicates whether the original file should be kept.</param>
    /// <returns></returns>
    private static string ChangeFileDirectoryLocation(string source, string destination, ConflictAction conflictAction, bool keepOriginal)
    {
        if (IsDirectory(source))
        {
            Func<string, string, ConflictAction, string> sendToFolder =
                keepOriginal ? CopyToFolder : MoveToFolder;

            if ((destination = ManageConflictAction(destination, true, conflictAction, true)) is null)
                return null;

            Directory.CreateDirectory(destination); //Creates the folder, if it doesn't exist.
            Directory.GetFiles(source, "*", SearchOption.TopDirectoryOnly).ForEach(_ => sendToFolder(_, destination, conflictAction)); //Copies the directory files.
            Directory.GetDirectories(source, "*", SearchOption.TopDirectoryOnly).ForEach(_ => sendToFolder(_, destination, conflictAction)); //Copies the inner directories.

            if (!keepOriginal)
                Delete(source);
        }
        else
        {
            if ((destination = ManageConflictAction(destination, false, conflictAction, !keepOriginal)) is null)
                return null;

            Directory.CreateDirectory(Path.GetDirectoryName(destination)); //Creates the destination folder, if it doesn't exist.

            if (keepOriginal)
                File.Copy(source, destination, true);
            else
                File.Move(source, destination);
        }

        return destination;
    }
    #endregion

    #region GetDirectorySize
    /// <summary>
    /// Retrieves the size of the specified directory based on the sizes of its files and subdirectories.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static long GetDirectorySize(string path)
    {
        DirectoryInfo dirInfo = new(path);

        return dirInfo.GetFiles().Sum(_ => _.Length) +
               dirInfo.GetDirectories().Sum(_ => GetDirectorySize(_.FullName));
    }
    #endregion

    #region GetFileSize
    /// <summary>
    /// Retrieves the size of the specified file.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static long GetFileSize(string path) =>
        new FileInfo(path).Length;
    #endregion

    #region ManageConflictAction

    /// <summary>
    /// Manages the conflict action.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="isFolder">Indicates whether the selected path is meant to point to a folder.</param>
    /// <param name="conflictAction"></param>
    /// <param name="deleteOnOverwrite">Indicates whether the existing file should be deleted when the Overwrite action is chosen.</param>
    /// <returns></returns>
    public static string ManageConflictAction(string path, bool isFolder, ConflictAction conflictAction, bool deleteOnOverwrite)
    {
        if (isFolder && Directory.Exists(path) || !isFolder && File.Exists(path))
            switch (conflictAction)
            {
                case ConflictAction.AppendNumber:
                    path = AppendPathSuffix(path, FileSuffixType.Numeric);
                    break;

                case ConflictAction.AppendText:
                    path = AppendPathSuffix(path, FileSuffixType.Copy);
                    break;

                case ConflictAction.Break:
                    throw new IOException($"The {(isFolder ? "folder" : "file")} {path} already exists.");

                case ConflictAction.Ignore:
                    path = null;
                    break;

                case ConflictAction.Overwrite:
                    if (deleteOnOverwrite)
                        Delete(path);
                    break;
            }

        return path;
    }
    #endregion

    #region RetrieveIconPath
    /// <summary>
    /// Retrieves the default icon path for the selected extension.
    /// </summary>
    /// <param name="extension"></param>
    /// <returns></returns>
    private static string RetrieveIconPath(string extension)
    {
        const int sOk = 0;
        const int sFalse = 1;

        uint length = 0;

        if (AssocQueryString(0, 15, extension, null, null, ref length) != sFalse)
            return null;

        StringBuilder sb = new((int)length);

        return AssocQueryString(0, 15, extension, null, sb, ref length) != sOk ? null : sb.ToString();
    }
    #endregion

    #endregion
}