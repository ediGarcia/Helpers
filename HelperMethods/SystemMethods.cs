using HelperMethods.Enums;
using HelperMethods.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

// ReSharper disable UnusedMember.Global

namespace HelperMethods;

public static class SystemMethods
{
    public static string ApplicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static string UserDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

    #region Public Methods

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

    #region CopyToFolder*

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
    public static void CopyToFolder(
        string destinationFolder,
        IEnumerable<string> sourceEntries,
        ConflictAction conflictAction)
    {
        foreach (string sourceEntry in sourceEntries)
            CopyToFolder(sourceEntry, destinationFolder, conflictAction);
    }

    #endregion

    #endregion

    #region Delete
    /// <summary>
    /// Deletes files, folder and its contents.
    /// </summary>
    /// <param name="paths">Paths to be deleted.</param>
    public static void Delete(params string[] paths)
    {
        foreach (string path in paths)
            if (DirectoryMethods.IsDirectory(path))
                Directory.Delete(path, true);
            else
                File.Delete(path);
    }
    #endregion

    #region Exists
    /// <summary>
    /// Returns a value indicating whether the selected path exists.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool Exists(string path) =>
        FileMethods.Exists(path) || DirectoryMethods.Exists(path);
    #endregion

    #region ForceDelete
    /// <summary>
    /// Deletes files, folder and its contents even if they are read only.
    /// </summary>
    /// <param name="entries">Paths to be deleted.</param>
    public static void ForceDelete(params string[] entries)
    {
        foreach (string entry in entries)
            if (DirectoryMethods.IsDirectory(entry))
            {
                foreach (string fileSystemEntry in Directory.EnumerateFileSystemEntries(entry))
                    ForceDelete(fileSystemEntry);

                new DirectoryInfo(entry) { Attributes = FileAttributes.Normal }.Delete();
            }
            else
                new FileInfo(entry) { Attributes = FileAttributes.Normal }.Delete();
    }
    #endregion

    #region GetIcon
    /// <summary>
    /// Returns an icon for a given file.
    /// </summary>
    /// <param name="path">Pathname for file.</param>
    /// <param name="largeIcon">Indicates whether the method should return the large version of the icon</param>
    /// <param name="showLinkOverlay">Whether to include the link icon</param>
    public static ImageSource GetIcon(string path, bool largeIcon, bool showLinkOverlay = false)
    {
        // ReSharper disable CommentTypo
        WindowsHelper.ShFileInfo shFileInfo = new();
        uint flags = 0x100; //SHGFI_ICON;

        if (showLinkOverlay)
            flags += 0x8000; //SHGFI_LINKOVERLAY;

        flags += largeIcon ? 0U : 1U;

        WindowsHelper.SHGetFileInfo(path,
            0,
            ref shFileInfo,
            (uint)Marshal.SizeOf(shFileInfo),
            flags);

        Icon icon = (Icon)Icon.FromHandle(shFileInfo.hIcon).Clone(); // Copy (clone) the returned icon to a new object, thus allowing us to clean-up properly.
        WindowsHelper.DestroyIcon(shFileInfo.hIcon);

        return Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        // ReSharper restore CommentTypo
    }
    #endregion

    #region GetSize
    /// <summary>
    /// Retrieves the size of a path (if it is a directory, it will be calculated recursively).
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static long GetSize(string path) =>
        DirectoryMethods.IsDirectory(path)
            ? DirectoryMethods.GetDirectorySize(path)
            : FileMethods.IsFile(path)
                ? FileMethods.GetFileSize(path)
                : throw new FileNotFoundException($"Could not find a file or a directory named '{path}'.");
    #endregion

    #region GetProcesses
    /// <summary>
    /// Gets process which names matches the specified search pattern.
    /// </summary>
    /// <param name="searchPattern"></param>
    /// <returns></returns>
    public static Process[] GetProcesses(string searchPattern)
    {
        Regex regex = new(searchPattern.Replace(".", "\\."), RegexOptions.IgnoreCase);
        return Process.GetProcesses().Where(_ => regex.IsMatch(_.ProcessName)).ToArray();
    }
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
        && !Path.GetInvalidPathChars().Any(path.Contains)
            && (!onlyAbsolute || Path.IsPathRooted(path))
        && (!onlyExisting || Exists(path));

    #endregion

    #region KillProcesses*

    #region KillProcess(params Process[])
    /// <summary>
    /// Kill the specified processes.
    /// </summary>
    /// <param name="processes"></param>
    public static void KillProcesses(params Process[] processes)
    {
        foreach (Process process in processes)
            process.Kill();
    }
    #endregion

    #region KillProcesses(string)
    /// <summary> 
    /// Kill process found through the search pattern.
    /// </summary>
    /// <param name="searchPattern"></param>
    /// <returns>The number of processes killed.</returns>
    public static int KillProcesses(string searchPattern)
    {
        Process[] processes = GetProcesses(searchPattern);
        KillProcesses(processes);

        return processes.Length;
    }
    #endregion

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

    #region MoveToFolder*

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
    public static void MoveToFolder(string destinationFolder, IEnumerable<string> sourceEntries, ConflictAction conflictAction)
    {
        foreach (string sourceEntry in sourceEntries)
            MoveToFolder(sourceEntry, destinationFolder, conflictAction);
    }
    #endregion

    #endregion

    #region Recycle
    /// <summary>
    /// Send file or directory to the Recycle Bin.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="suppressErrors">Indicates whether errors should be suppressed.</param>
    /// <param name="suppressBigFileWarning">Indicates whether big files should be permanently deleted without the user confirmation.</param>
    /// <exception cref="IOException">Invalid or not existing path.</exception>
    public static async Task Recycle(string path, bool suppressErrors = true, bool suppressBigFileWarning = true)
    {
        if (!Exists(path))
            throw new IOException($"Could not find file \"{path}\".");

        WindowsHelper.FileOperationFlags flags = WindowsHelper.FileOperationFlags.FofAllowUndo | WindowsHelper.FileOperationFlags.FofNoConfirmation;

        if (suppressErrors)
            flags |= WindowsHelper.FileOperationFlags.FofNoErrorUi;

        flags |= suppressBigFileWarning ? WindowsHelper.FileOperationFlags.FofSilent : WindowsHelper.FileOperationFlags.FofWantNukeWarning;

        await Task.Run(() => CallSystemFileOperation(path, null, WindowsHelper.FileOperationType.FoDelete, flags));
    }
    #endregion

    #region Run
    /// <summary>
    /// Runs a command through operational system.
    /// </summary>
    /// <param name="workingFolder"></param>
    /// <param name="command"></param>
    /// <param name="arguments"></param>
    /// <param name="runAsAdmin"></param>
    /// <param name="hideConsoleWindow"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException" />
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ObjectDisposedException" />
    /// <exception cref="FileNotFoundException" />
    /// <exception cref="System.ComponentModel.Win32Exception" />
    public static Process Run(string command, string arguments = null, string workingFolder = null, bool runAsAdmin = false, bool hideConsoleWindow = false) =>
        Process.Start(new ProcessStartInfo(command, arguments)
        {
            WorkingDirectory = workingFolder ?? Path.GetDirectoryName(command),
            UseShellExecute = true,
            Verb = runAsAdmin ? "runas" : "",
            CreateNoWindow = hideConsoleWindow
        });
    #endregion

    #region SystemCopy
    /// <summary>
    /// Copies files and folders and its contents using the system's UI.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <exception cref="IOException">Invalid or not existing path.</exception>
    public static async Task SystemCopy(string source, string destination)
    {
        if (!Exists(source))
            throw new IOException($"Could not find file \"{source}\".");

        int resultCode = await Task.Run(() => CallSystemFileOperation(source, destination, WindowsHelper.FileOperationType.FoCopy, 0));
        WindowsHelper.GetWindowsErrorMessage(resultCode, true);
    }
    #endregion

    #region SystemDelete
    /// <summary>
    /// Deletes files and folders and its contents using the system's UI.
    /// </summary>
    /// <param name="path"></param>
    /// <exception cref="IOException">Invalid or not existing path.</exception>
    public static async Task SystemDelete(string path)
    {
        if (!Exists(path))
            throw new IOException($"Could not find file \"{path}\".");

        int resultCode = await Task.Run(() => CallSystemFileOperation(path, null, WindowsHelper.FileOperationType.FoDelete, 0));
        WindowsHelper.GetWindowsErrorMessage(resultCode, true);
    }
    #endregion

    #region SystemMove
    /// <summary>
    /// Moves files or folder and its contents using the system's UI.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <exception cref="IOException">Invalid or not existing path.</exception>
    public static async Task SystemMove(string source, string destination)
    {
        if (!Exists(source))
            throw new IOException($"Could not find file \"{source}\".");

        int resultCode = await Task.Run(() => CallSystemFileOperation(source, destination, WindowsHelper.FileOperationType.FoMove, 0));
        WindowsHelper.GetWindowsErrorMessage(resultCode, true);
    }
    #endregion

    #endregion

    #region Private Methods

    #region CallSystemFileOperation
    /// <summary>
    /// Calls operational system's file operation
    /// </summary>
    /// <param name="pathTo">Destination path for copy and move commands.</param>
    /// <param name="fileOperationType"></param>
    /// <param name="flags"></param>
    /// <param name="pathFrom"></param>
    private static int CallSystemFileOperation(string pathFrom, string pathTo, WindowsHelper.FileOperationType fileOperationType, WindowsHelper.FileOperationFlags flags)
    {
        WindowsHelper.ShFileOpStruct fs = new()
        {
            WFunc = fileOperationType,
            PFrom = pathFrom + '\0' + '\0',
            FFlags = flags
        };

        if (fileOperationType is WindowsHelper.FileOperationType.FoCopy or WindowsHelper.FileOperationType.FoMove)
            fs.PTo = pathTo;

        return WindowsHelper.SHFileOperation(ref fs);
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
        if (DirectoryMethods.IsDirectory(source))
        {
            Func<string, string, ConflictAction, string> sendToFolder =
                keepOriginal ? CopyToFolder : MoveToFolder;

            if ((destination = PathMethods.SolvePathConflict(destination, conflictAction, true)) is null)
                return null;

            Directory.CreateDirectory(destination); //Creates the folder, if it doesn't exist.

            foreach (string file in Directory.GetFileSystemEntries(source, "*", SearchOption.TopDirectoryOnly)) 
                sendToFolder(file, destination, conflictAction);

            if (!keepOriginal)
                Delete(source);
        }
        else
        {
            if ((destination = PathMethods.SolvePathConflict(destination, conflictAction, !keepOriginal)) is null)
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

    #endregion
}