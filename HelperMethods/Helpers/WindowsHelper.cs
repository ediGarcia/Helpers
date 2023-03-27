using System;
using System.Runtime.InteropServices;
// ReSharper disable MemberCanBePrivate.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable UnusedMember.Global

namespace HelperMethods.Helpers;

internal static class WindowsHelper
{
    #region Enums

    /// <summary>
    /// Possible flags for the SHFileOperation method.
    /// </summary>
    [Flags]
    public enum FileOperationFlags : ushort
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
    public enum FileOperationType : uint
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

    #endregion

    #region Structs

    [StructLayout(LayoutKind.Sequential)]
    public struct ShFileInfo
    {
        public IntPtr hIcon;
        public int iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    }

    /// <summary>
    /// ShFileOpStruct for SHFileOperation from COM
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct ShFileOpStruct
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

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct ShStockIconInfo
    {
        public uint cbSize;
        public IntPtr hIcon;
        public int iSysIconIndex;
        public int iIcon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szPath;
    }

    #endregion

    #region DLL Imports

    /// <summary>
    /// Provides access to function required to delete handle. This method is used internally
    /// and is not required to be called separately.
    /// </summary>
    /// <param name="hIcon">Pointer to icon handle.</param>
    /// <returns>N/A</returns>
    [DllImport("User32.dll")]
    public static extern int DestroyIcon(IntPtr hIcon);

    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    public static extern int SHFileOperation(ref ShFileOpStruct fileOp);

    [DllImport("Shell32.dll")]
    public static extern IntPtr SHGetFileInfo(
        string pszPath,
        uint dwFileAttributes,
        ref ShFileInfo shFileInfo,
        uint cbFileInfo,
        uint uFlags
    );

    [DllImport("shell32.dll")]
    public static extern int SHGetStockIconInfo(uint siId, uint uFlags, ref ShStockIconInfo stockIconInfo);

    #endregion
}