using HelperMethods.Enums;
using HelperMethods.Helpers;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CredentialManagement;

// ReSharper disable UnusedMember.Global

namespace HelperMethods;

public static class SystemMethods
{
    public static string ApplicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static string UserDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

    #region Public Methods

    #region Copy
    /// <summary>
    /// Copies the specified file or folder and its contents to the destination path.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="conflictAction"></param>
    /// <exception cref="IOException"></exception>
    public static void Copy(
        string source,
        string destination,
        FileNameConflictAction conflictAction = FileNameConflictAction.ThrowError)
    {
        if (FileMethods.Exists(source))
            FileMethods.Copy(source, destination, conflictAction);

        else if (DirectoryMethods.Exists(source))
            DirectoryMethods.Copy(source, destination, conflictAction);

        else
            throw new IOException($"\"{source}\" not found.");
    }
    #endregion

    #region Delete*

    #region Delete(params string[])
    /// <summary>
    /// Deletes files, folder and its contents.
    /// </summary>
    /// <param name="paths">Paths to be deleted.</param>
    public static void Delete(params string[] paths)
    {
        foreach (string path in paths)
            if (DirectoryMethods.IsDirectory(path))
                DirectoryMethods.Delete(path);
            else
                FileMethods.Delete(path);
    }
    #endregion

    #region Delete(string, [bool])
    /// <summary>
    /// Deletes the specified file or folder.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="recycle">Indicates whether the specified file or folder should be sent to the Recycle Bin, instead of permanently deleted.</param>
    /// <returns>true if the file or folder exists and is successfully deleted; false if the file or folder does not exist.</returns>
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
        if (FileMethods.Exists(path))
            FileMethods.Delete(path, recycle);
        else if (DirectoryMethods.Exists(path))
            DirectoryMethods.Delete(path, recycle);
        else
            return false;

        return true;
    }
    #endregion

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
        return [.. Process.GetProcesses().Where(_ => regex.IsMatch(_.ProcessName))];
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

    #region KillProcesses(params Process[])
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

    #region KillProcesses(params string[])
    /// <summary> 
    /// Kill process found through the search patterns.
    /// </summary>
    /// <param name="searchPatterns"></param>
    /// <returns>The number of processes killed.</returns>
    public static int KillProcesses(params string[] searchPatterns)
    {
        int processCount = 0;

        foreach (string searchPattern in searchPatterns)
        {
            Process[] processes = GetProcesses(searchPattern);
            KillProcesses(processes);

            processCount += processes.Length;
        }

        return processCount;
    }
    #endregion

    #endregion

    #region Move
    /// <summary>
    /// Moves the specified file or folder and its contents to the destination path.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="conflictAction"></param>
    /// <exception cref="IOException"></exception>
    public static void Move(
        string source,
        string destination,
        FileNameConflictAction conflictAction = FileNameConflictAction.ThrowError)
    {
        if (FileMethods.Exists(source))
            FileMethods.Move(source, destination, conflictAction);

        else if (DirectoryMethods.Exists(source))
            DirectoryMethods.Move(source, destination, conflictAction);

        else
            throw new IOException($"\"{source}\" not found.");
    }
    #endregion

    #region RemoveCredential
    /// <summary>
    /// Removes an existing credential entry from the system.
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    /// <exception cref="SystemException"></exception>
    public static bool RemoveCredential(string identifier) => 
        new Credential { Target = identifier }.Delete();
    #endregion

    #region RetrieveCredential
    /// <summary>
    /// Retrieves an existing credential entry from the system.
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    /// <exception cref="SystemException"></exception>
    public static Credential RetrieveCredential(string identifier)
    {
        Credential credential = new() { Target = identifier };
        return !credential.Load() ? null : credential;
    }
    #endregion

    #region Run
    /// <summary>
    /// Runs a command through the operational system.
    /// </summary>
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
    public static Process Run(
        string command,
        string arguments = "",
        bool runAsAdmin = false,
        bool hideConsoleWindow = false) =>
        Process.Start(new ProcessStartInfo(command, arguments)
        {
            CreateNoWindow = hideConsoleWindow,
            UseShellExecute = true,
            Verb = runAsAdmin ? "runas" : ""
        });
    #endregion

    #region StoreEnterpriseCredential
    /// <summary>
    /// Stores a new enterprise credential entry do the system.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    /// <exception cref="SystemException"></exception>
    public static void StoreEnterpriseCredential(string identifier, string userName, string password) =>
        StoreCredential(identifier, userName, password, PersistanceType.Enterprise);
    #endregion

    #region StoreLocalCredential
    /// <summary>
    /// Stores a new local credential entry do the system.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    /// <exception cref="SystemException"></exception>
    public static void StoreLocalCredential(string identifier, string userName, string password) =>
        StoreCredential(identifier, userName, password, PersistanceType.LocalComputer);
    #endregion

    #region StoreSessionCredential
    /// <summary>
    /// Stores a new session credential entry do the system.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    /// <exception cref="SystemException"></exception>
    public static void StoreSessionCredential(string identifier, string userName, string password) =>
        StoreCredential(identifier, userName, password, PersistanceType.Session);
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

    #region StoreCredential
    /// <summary>
    /// Stores a new credential entry do the system.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="persistanceType"></param>
    /// <returns></returns>
    /// <exception cref="SystemException"></exception>
    private static void StoreCredential(string identifier, string userName, string password, PersistanceType persistanceType)
    {
        Credential credential = new()
        {
            Target = identifier,
            Username = userName,
            Password = password,
            PersistanceType = persistanceType
        };

        if (!credential.Save())
            throw new SystemException($"The credential \"{identifier}\" could not be saved. Check if there is already a credential entry with the same id and if you have the required privileges.");
    }
    #endregion

    #endregion
}