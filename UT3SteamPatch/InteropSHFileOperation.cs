using System;
using System.Collections.Generic;
using System.Text;

namespace UT3SteamPatch
{
    using System.Runtime.InteropServices;
    using System.IO;

    public class InteropSHFileOperation
    {
        /// <summary>
        /// File Operation Function Type for SHFileOperation
        /// </summary>
        public enum FileOperationType : uint
        {
            /// <summary>
            /// Move the objects
            /// </summary>
            FO_MOVE = 0x0001,
            /// <summary>
            /// Copy the objects
            /// </summary>
            FO_COPY = 0x0002,
            /// <summary>
            /// Delete (or recycle) the objects
            /// </summary>
            FO_DELETE = 0x0003,
            /// <summary>
            /// Rename the object(s)
            /// </summary>
            FO_RENAME = 0x0004,
        }

        /// <summary>
        /// Possible flags for the SHFileOperation method.
        /// </summary>
        [Flags]
        public enum FileOperationFlags : ushort
        {
            /// <summary>
            /// Do not show a dialog during the process
            /// </summary>
            FOF_SILENT = 0x0004,
            /// <summary>
            /// Do not ask the user to confirm selection
            /// </summary>
            FOF_NOCONFIRMATION = 0x0010,
            /// <summary>
            /// Delete the file to the recycle bin.  (Required flag to send a file to the bin
            /// </summary>
            FOF_ALLOWUNDO = 0x0040,
            /// <summary>
            /// Do not show the names of the files or folders that are being recycled.
            /// </summary>
            FOF_SIMPLEPROGRESS = 0x0100,
            /// <summary>
            /// Surpress errors, if any occur during the process.
            /// </summary>
            FOF_NOERRORUI = 0x0400,
            /// <summary>
            /// dont copy NT file Security Attributes
            /// </summary>
            FOF_NOCOPYSECURITYATTRIBS = 0x0800,
            /// <summary>
            /// Warn if files are too big to fit in the recycle bin and will need
            /// to be deleted completely.
            /// </summary>
            FOF_WANTNUKEWARNING = 0x4000,
        }

        /// <summary>
        /// SHFILEOPSTRUCT for SHFileOperation from COM
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHFILEOPSTRUCT
        {

            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public FileOperationType wFunc;
            public string pFrom;
            public string pTo;
            public FileOperationFlags fFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);

        public static void CopyFile(string sourceFilePath, string destFilePath, bool confirmOverwrites)
        {
            CopyFile(sourceFilePath, destFilePath, confirmOverwrites, null);
        }

        public static void CopyFile(string sourceFilePath, string destFilePath, bool confirmOverwrites, IntPtr? hwnd)
        {
            // Check if file exists
            if (!File.Exists(sourceFilePath))
            {
                throw new IOException();
            }

            //InteropSHFileOperation fileOp = new InteropSHFileOperation();

            SHFILEOPSTRUCT fileOp = new SHFILEOPSTRUCT();
            fileOp.hwnd = hwnd ?? IntPtr.Zero;
            fileOp.wFunc = FileOperationType.FO_COPY;
            fileOp.pFrom = sourceFilePath + "\x0\x0";
            fileOp.pTo = destFilePath + "\x0\x0";

            if (!confirmOverwrites)
            {
                fileOp.fFlags = fileOp.fFlags | FileOperationFlags.FOF_ALLOWUNDO;
            }

            fileOp.fFlags |= FileOperationFlags.FOF_NOCOPYSECURITYATTRIBS;
            fileOp.fFlags |= FileOperationFlags.FOF_SIMPLEPROGRESS;

            if (SHFileOperation(ref fileOp) != 0)
            {
                throw new IOException();
            }
        }

        public static void CopyFolder(string sourceFolderPath, string destFolderPath, bool confirmOverwrites)
        {
            CopyFolder(sourceFolderPath, destFolderPath, confirmOverwrites, null);
        }

        public static void CopyFolder(string sourceFolderPath, string destFolderPath, bool confirmOverwrites, IntPtr? hwnd)
        {
            // Check if folder exists
            // to prevent SHFileOperation throwing errors on writing on a non-existant drive/folder
            if (!Directory.Exists(destFolderPath))
            {
                throw new IOException();
            }

            if (!Directory.Exists(sourceFolderPath))
            {
                throw new IOException();
            }

            SHFILEOPSTRUCT fileOp = new SHFILEOPSTRUCT();
            fileOp.wFunc = FileOperationType.FO_COPY;
            fileOp.pFrom = sourceFolderPath + "\x0\x0";
            fileOp.pTo = destFolderPath + "\x0\x0";

            if (!confirmOverwrites)
            {
                fileOp.fFlags = fileOp.fFlags | FileOperationFlags.FOF_NOCONFIRMATION;
            }

            fileOp.fFlags |= FileOperationFlags.FOF_NOCOPYSECURITYATTRIBS;
            fileOp.fFlags |= FileOperationFlags.FOF_SIMPLEPROGRESS;

            if (SHFileOperation(ref fileOp) != 0)
            {
                throw new IOException();
            }
        }
    }
}
