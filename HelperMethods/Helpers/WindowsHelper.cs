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

    #region Public Methods

    #region GetWindowsErrorMessage

    /// <summary>
    /// Throws a <see cref="SystemException"/> according to the specified error message.
    /// </summary>
    /// <param name="errorCode"></param>
    /// <param name="isFileOperation"></param>
    /// <exception cref="SystemException"></exception>
    public static void GetWindowsErrorMessage(int errorCode, bool isFileOperation)
    {
        if (errorCode == 0)
            return;

        // ReSharper disable StringLiteralTypo
        string errorMessage = errorCode switch
        {
            1 => "Incorrect function.",
            2 => "The system cannot find the file specified.",
            3 => "The system cannot find the path specified.",
            4 => "The system cannot open the file.",
            5 => "Access is denied.",
            6 => "The handle is invalid.",
            7 => "The storage control blocks were destroyed.",
            8 => "Not enough memory resources are available to process this command.",
            9 => "The storage control block address is invalid.",
            10 => "The environment is incorrect.",
            11 => "An attempt was made to load a program with an incorrect format.",
            12 => "The access code is invalid.",
            13 => "The data is invalid.",
            14 => "Not enough storage is available to complete this operation.",
            15 => "The system cannot find the drive specified.",
            16 => "The directory cannot be removed.",
            17 => "The system cannot move the file to a different disk drive.",
            18 => "There are no more files.",
            19 => "The media is write protected.",
            20 => "The system cannot find the device specified.",
            21 => "The device is not ready.",
            22 => "The device does not recognize the command.",
            23 => "Data error .",
            24 => "The program issued a command but the command length is incorrect.",
            25 => "The drive cannot locate a specific area or track on the disk.",
            26 => "The specified disk or diskette cannot be accessed.",
            27 => "The drive cannot find the sector requested.",
            28 => "The printer is out of paper.",
            29 => "The system cannot write to the specified device.",
            30 => "The system cannot read from the specified device.",
            31 => "A device attached to the system is not functioning.",
            32 => "The process cannot access the file because it is being used by another process.",
            33 => "The process cannot access the file because another process has locked a portion of the file.",
            34 => "The wrong diskette is in the drive. Insert %2  into drive %1.",
            36 => "Too many files opened for sharing.",
            38 => "Reached the end of the file.",
            39 => "The disk is full.",
            50 => "The request is not supported.",
            51 => "Windows cannot find the network path. Verify that the network path is correct and the destination computer is not busy or turned off. If Windows still cannot find the network path, contact your network administrator.",
            52 => "You were not connected because a duplicate name exists on the network. If joining a domain, go to System in Control Panel to change the computer name and try again. If joining a workgroup, choose another workgroup name.",
            53 => "The network path was not found.",
            54 => "The network is busy.",
            55 => "The specified network resource or device is no longer available.",
            56 => "The network BIOS command limit has been reached.",
            57 => "A network adapter hardware error occurred.",
            58 => "The specified server cannot perform the requested operation.",
            59 => "An unexpected network error occurred.",
            60 => "The remote adapter is not compatible.",
            61 => "The printer queue is full.",
            62 => "Space to store the file waiting to be printed is not available on the server.",
            63 => "Your file waiting to be printed was deleted.",
            64 => "The specified network name is no longer available.",
            65 => "Network access is denied.",
            66 => "The network resource type is not correct.",
            67 => "The network name cannot be found.",
            68 => "The name limit for the local computer network adapter card was exceeded.",
            69 => "The network BIOS session limit was exceeded.",
            70 => "The remote server has been paused or is in the process of being started.",
            71 => "No more connections can be made to this remote computer at this time because there are already as many connections as the computer can accept.",
            72 => "The specified printer or disk device has been paused.",
            80 => "The file exists.",
            82 => "The directory or file cannot be created.",
            83 => "Fail on INT 24.",
            84 => "Storage to process this request is not available.",
            85 => "The local device name is already in use.",
            86 => "The specified network password is not correct.",
            87 => "The parameter is incorrect.",
            88 => "A write fault occurred on the network.",
            89 => "The system cannot start another process at this time.",
            100 => "Cannot create another system semaphore.",
            101 => "The exclusive semaphore is owned by another process.",
            102 => "The semaphore is set and cannot be closed.",
            103 => "The semaphore cannot be set again.",
            104 => "Cannot request exclusive semaphores at interrupt time.",
            105 => "The previous ownership of this semaphore has ended.",
            106 => "Insert the diskette for drive %1.",
            107 => "The program stopped because an alternate diskette was not inserted.",
            108 => "The disk is in use or locked by another process.",
            109 => "The pipe has been ended.",
            110 => "The system cannot open the device or file specified.",
            111 => "The file name is too long.",
            112 => "There is not enough space on the disk.",
            113 => isFileOperation ? "The source and destination files are the same file." : "No more internal file identifiers available.",
            114 => isFileOperation ? "Multiple file paths were specified in the source buffer, but only one destination file path." : "The target internal file identifier is incorrect.",
            115 => isFileOperation ? "Rename operation was specified but the destination path is a different directory. Use the move operation instead." : "Unknown error.",
            116 => isFileOperation ? "The source is a root directory, which cannot be moved or renamed." : "Unknown error.",
            117 => isFileOperation ? "The operation was canceled by the user, or silently canceled if the appropriate flags were supplied to SHFileOperation." : "The IOCTL call made by the application program is not correct.",
            118 => isFileOperation ? "The destination is a subtree of the source." : "The verify-on-write switch parameter value is not correct.",
            119 => "The system does not support the command requested.",
            120 => isFileOperation ? "Security settings denied access to the source" : "This function is not supported on this system.",
            121 => isFileOperation ? "The source or destination path exceeded or would exceed MAX_PATH." : "The semaphore timeout period has expired.",
            122 => isFileOperation ? "The operation involved multiple destination paths, which can fail in the case of a move operation." : "The data area passed to a system call is too small.",
            123 => "The filename, directory name, or volume label syntax is incorrect.",
            124 => isFileOperation ? "The path in the source or destination or both was invalid." : "The system call level is not correct.",
            125 => isFileOperation ? "The source and destination have the same parent folder." : "The disk has no volume label.",
            126 => isFileOperation ? "The destination path is an existing file." : "The specified module could not be found.",
            127 => "The specified procedure could not be found.",
            128 => isFileOperation ? "The destination path is an existing folder." : "There are no child processes to wait for.",
            129 => isFileOperation ? "The name of the file exceeds MAX_PATH." : "The %1 application cannot be run in Win32 mode.",
            130 => isFileOperation ? "The destination is a read-only CD-ROM, possibly unformatted." : "Attempt to use a file handle to an open disk partition for an operation other than raw disk I/O.",
            131 => isFileOperation ? "The destination is a read-only DVD, possibly unformatted." : "An attempt was made to move the file pointer before the beginning of the file.",
            132 => isFileOperation ? "The destination is a writable CD-ROM, possibly unformatted." : "The file pointer cannot be set on the specified device or file.",
            133 => isFileOperation ? "The file involved in the operation is too large for the destination media or file system." : "A JOIN or SUBST command cannot be used for a drive that contains previously joined drives.",
            134 => isFileOperation ? "The source is a read-only CD-ROM, possibly unformatted." : "An attempt was made to use a JOIN or SUBST command on a drive that has already been joined.",
            135 => isFileOperation ? "The source is a read-only DVD, possibly unformatted." : "An attempt was made to use a JOIN or SUBST command on a drive that has already been substituted.",
            136 => isFileOperation ? "The source is a writable CD-ROM, possibly unformatted." : "The system tried to delete the JOIN of a drive that is not joined.",
            137 => "The system tried to delete the substitution of a drive that is not substituted.",
            138 => "The system tried to join a drive to a directory on a joined drive.",
            139 => "The system tried to substitute a drive to a directory on a substituted drive.",
            140 => "The system tried to join a drive to a directory on a substituted drive.",
            141 => "The system tried to SUBST a drive to a directory on a joined drive.",
            142 => "The system cannot perform a JOIN or SUBST at this time.",
            143 => "The system cannot join or substitute a drive to or for a directory on the same drive.",
            144 => "The directory is not a subdirectory of the root directory.",
            145 => "The directory is not empty.",
            146 => "The path specified is being used in a substitute.",
            147 => "Not enough resources are available to process this command.",
            148 => "The path specified cannot be used at this time.",
            149 => "An attempt was made to join or substitute a drive for which a directory on the drive is the target of a previous substitute.",
            150 => "System trace information was not specified in your CONFIG.SYS file, or tracing is disallowed.",
            151 => "The number of specified semaphore events for DosMuxSemWait is not correct.",
            152 => "DosMuxSemWait did not execute; too many semaphores are already set.",
            153 => "The DosMuxSemWait list is not correct.",
            154 => "The volume label you entered exceeds the label character limit of the target file system.",
            155 => "Cannot create another thread.",
            156 => "The recipient process has refused the signal.",
            157 => "The segment is already discarded and cannot be locked.",
            158 => "The segment is already unlocked.",
            159 => "The address for the thread ID is not correct.",
            160 => "One or more arguments are not correct.",
            161 => "The specified path is invalid.",
            162 => "A signal is already pending.",
            164 => "No more threads can be created in the system.",
            167 => "Unable to lock a region of a file.",
            170 => "The requested resource is in use.",
            171 => "Device's command support detection is in progress.",
            173 => "A lock request was not outstanding for the supplied cancel region.",
            174 => "The file system does not support atomic changes to the lock type.",
            180 => "The system detected a segment number that was not correct.",
            182 => "The operating system cannot run %1.",
            183 => isFileOperation ? "MAX_PATH was exceeded during the operation." : "Cannot create a file when that file already exists.",
            186 => "The flag passed is not correct.",
            187 => "The specified system semaphore name was not found.",
            188 => "The operating system cannot run %1.",
            189 => "The operating system cannot run %1.",
            190 => "The operating system cannot run %1.",
            191 => "Cannot run %1 in Win32 mode.",
            192 => "The operating system cannot run %1.",
            193 => "%1 is not a valid Win32 application.",
            194 => "The operating system cannot run %1.",
            195 => "The operating system cannot run %1.",
            196 => "The operating system cannot run this application program.",
            197 => "The operating system is not presently configured to run this application.",
            198 => "The operating system cannot run %1.",
            199 => "The operating system cannot run this application program.",
            200 => "The code segment cannot be greater than or equal to 64K.",
            201 => "The operating system cannot run %1.",
            202 => "The operating system cannot run %1.",
            203 => "The system could not find the environment option that was entered.",
            205 => "No process in the command subtree has a signal handler.",
            206 => "The filename or extension is too long.",
            207 => "The ring 2 stack is in use.",
            208 => "The global filename characters, * or ?, are entered incorrectly or too many global filename characters are specified.",
            209 => "The signal being posted is not correct.",
            210 => "The signal handler cannot be set.",
            212 => "The segment is locked and cannot be reallocated.",
            214 => "Too many dynamic-link modules are attached to this program or dynamic-link module.",
            215 => "Cannot nest calls to LoadModule.",
            216 => "This version of %1 is not compatible with the version of Windows you're running. Check your computer's system information and then contact the software publisher.",
            217 => "The image file %1 is signed, unable to modify.",
            218 => "The image file %1 is strong signed, unable to modify.",
            220 => "This file is checked out or locked for editing by another user.",
            221 => "The file must be checked out before saving changes.",
            222 => "The file type being saved or retrieved has been blocked.",
            223 => "The file size exceeds the limit allowed and cannot be saved.",
            224 => "Access Denied. Before opening files in this location, you must first add the web site to your trusted sites list, browse to the web site, and select the option to login automatically.",
            225 => "Operation did not complete successfully because the file contains a virus or potentially unwanted software.",
            226 => "This file contains a virus or potentially unwanted software and cannot be opened. Due to the nature of this virus or potentially unwanted software, the file has been removed from this location.",
            229 => "The pipe is local.",
            230 => "The pipe state is invalid.",
            231 => "All pipe instances are busy.",
            232 => "The pipe is being closed.",
            233 => "No process is on the other end of the pipe.",
            234 => "More data is available.",
            240 => "The session was canceled.",
            254 => "The specified extended attribute name was invalid.",
            255 => "The extended attributes are inconsistent.",
            258 => "The wait operation timed out.",
            259 => "No more data is available.",
            266 => "The copy functions cannot be used.",
            267 => "The directory name is invalid.",
            275 => "The extended attributes did not fit in the buffer.",
            276 => "The extended attribute file on the mounted file system is corrupt.",
            277 => "The extended attribute table file is full.",
            278 => "The specified extended attribute handle is invalid.",
            282 => "The mounted file system does not support extended attributes.",
            288 => "Attempt to release mutex not owned by caller.",
            298 => "Too many posts were made to a semaphore.",
            299 => "Only part of a ReadProcessMemory or WriteProcessMemory request was completed.",
            300 => "The oplock request is denied.",
            301 => "An invalid oplock acknowledgment was received by the system.",
            302 => "The volume is too fragmented to complete this operation.",
            303 => "The file cannot be opened because it is in the process of being deleted.",
            304 => "Short name settings may not be changed on this volume due to the global registry setting.",
            305 => "Short names are not enabled on this volume.",
            306 => "The security stream for the given volume is in an inconsistent state. Please run CHKDSK on the volume.",
            307 => "A requested file lock operation cannot be processed due to an invalid byte range.",
            308 => "The subsystem needed to support the image type is not present.",
            309 => "The specified file already has a notification GUID associated with it.",
            310 => "An invalid exception handler routine has been detected.",
            311 => "Duplicate privileges were specified for the token.",
            312 => "No ranges for the specified operation were able to be processed.",
            313 => "Operation is not allowed on a file system internal file.",
            314 => "The physical resources of this disk have been exhausted.",
            315 => "The token representing the data is invalid.",
            316 => "The device does not support the command feature.",
            317 => "The system cannot find message text for message number 0x%1 in the message file for %2.",
            318 => "The scope specified was not found.",
            319 => "The Central Access Policy specified is not defined on the target machine.",
            320 => "The Central Access Policy obtained from Active Directory is invalid.",
            321 => "The device is unreachable.",
            322 => "The target device has insufficient resources to complete the operation.",
            323 => "A data integrity checksum error occurred. Data in the file stream is corrupt.",
            324 => "An attempt was made to modify both a KERNEL and normal Extended Attribute  in the same operation.",
            326 => "Device does not support file-level TRIM.",
            327 => "The command specified a data offset that does not align to the device's granularity/alignment.",
            328 => "The command specified an invalid field in its parameter list.",
            329 => "An operation is currently in progress with the device.",
            330 => "An attempt was made to send down the command via an invalid path to the target device.",
            331 => "The command specified a number of descriptors that exceeded the maximum supported by the device.",
            332 => "Scrub is disabled on the specified file.",
            333 => "The storage device does not provide redundancy.",
            334 => "An operation is not supported on a resident file.",
            335 => "An operation is not supported on a compressed file.",
            336 => "An operation is not supported on a directory.",
            337 => "The specified copy of the requested data could not be read.",
            350 => "No action was taken as a system reboot is required.",
            351 => "The shutdown operation failed.",
            352 => "The restart operation failed.",
            353 => "The maximum number of sessions has been reached.",
            400 => "The thread is already in background processing mode.",
            401 => "The thread is not in background processing mode.",
            402 => "The process is already in background processing mode.",
            403 => "The process is not in background processing mode.",
            1026 => isFileOperation ? "An unknown error occurred. This is typically due to an invalid path in the source or destination. This error does not occur on Windows Vista and later." : "Unknown error",
            65536 => isFileOperation ? "An unspecified error occurred on the destination." : "Unknown error",
            65652 => isFileOperation ? "Destination is a root directory and cannot be renamed." : "Unknown error",
            _ => "Unknown error."
        };
        // ReSharper restore StringLiteralTypo

        throw new SystemException(errorMessage);
    }
    #endregion

    #endregion
}