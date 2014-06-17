using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

using Microsoft.VisualBasic.FileIO;

namespace UT3SteamPatch
{
    
    public partial class Main : Form
    {
#region Definitions
#region Definitions - Strings
        public const string FILE_EULA = "EULA.rtf";
        public const string FOLDER_PATCH = "Patch";

        public const string CHECKFOLDER_ENGINE = "Engine";
        public const string CHECKFOLDER_GAME = "UTGame";

        public const string FILE_INSTALL_UT3 = "ut3.exe";
        public const string FOLDER_INSTALL_BINARIES = "Binaries";


        public const string MESSAGE_TITLE = "Patch UT3 (Steam)";

        public const string STRING_NO_EULA = "No EULA file. \n\nPease, create the Rich Text Format file saved as '{0}'.";
        public const string STRING_BAD_EULA = "Bad EULA file found. \n\nPease, create the Rich Text Format file saved as '{0}'.";

        public const string STRING_NO_STEAM = "Could not find a Steam installation of UT3. Using the retail game.";

        public const string STRING_PATCH_FAILED = "Failed to apply patch!";
        public const string STRING_PATCH_NO = "The patch was not applied!";
        public const string STRING_PATCH_SUCCESS = "UT3 was successfully patched!";

        public const string STRING_BROWSE = "Please browse to where UT3 is installed";
        public const string STRING_APPLY = "Would you like to apply the patch to {0}'{1}'?";
        public const string STRING_NOT_INSTALLED = "Unreal Tournament 3 does not seem to be installed at '{0}'";

        public const string STRING_READONLY_ERROR = "The following file is readonly:{0}{1}{2}Verify if the file is accessible.";
        public const string STRING_ACCESS_ERROR = "The following file is in use:{0}{1}{2}Verify that the file is not opened and retry\nor abort the patching process.";

#endregion

#region Definitions - Workflow variables

        private bool UserClick;
        private bool UserManual;

#endregion

#region Definitions - Registry
        public struct SReg
        {
            public string path;
            public string key;

        }

        static readonly SReg[] COLL_UT3Locations =
            new SReg[]{
                new SReg() { path = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\InstallShield_{FDBBAF14-5ED8-49B7-A5BE-1C35668B074D}", key = "InstallLocation" }, // German system
                new SReg() { path = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\InstallShield_{C019D439-E7F8-49EB-85FA-6D0C8CCBDA23}", key = "InstallLocation" },
                new SReg() { path = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\InstallShield_{A007C579-B78D-4FDE-A85A-16987A251E53}", key = "InstallLocation" },
                new SReg() { path = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\InstallShield_{69082C6E-1944-4EAD-B119-06DCBF492C3F}", key = "InstallLocation" },
                new SReg() { path = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\InstallShield_{BFA90209-7AFF-4DB6-8E4B-E57305751AD7}", key = "InstallLocation" },
                new SReg() { path = "HKEY_CURRENT_USER\\SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\InstallShield_{C019D439-E7F8-49EB-85FA-6D0C8CCBDA23}", key = "InstallLocation" },
                new SReg() { path = "HKEY_CURRENT_USER\\SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\InstallShield_{A007C579-B78D-4FDE-A85A-16987A251E53}", key = "InstallLocation" },
                new SReg() { path = "HKEY_CURRENT_USER\\SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\InstallShield_{FDBBAF14-5ED8-49B7-A5BE-1C35668B074D}", key = "InstallLocation" },
                new SReg() { path = "HKEY_CURRENT_USER\\SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\InstallShield_{69082C6E-1944-4EAD-B119-06DCBF492C3F}", key = "InstallLocation" },
                new SReg() { path = "HKEY_CURRENT_USER\\SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\InstallShield_{BFA90209-7AFF-4DB6-8E4B-E57305751AD7}", key = "InstallLocation" },
            };

        static readonly string[] COLL_SteamUT3AppIDs = new string[] 
        {
            "13210", // Black Edition - international
            "13218", // Black Edition - spanish
            "13219", // Black Edition - german
            "13221", // Black Edition - italian
            "13222", // Black Edition - french
        };

        static readonly SReg[] COLL_SteamUT3Locations = 
            new SReg[]{
                new SReg() { path = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Steam App {0}", key = "InstallLocation" },
                new SReg() { path = "HKEY_CURRENT_USER\\SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Steam App {0}", key = "InstallLocation" },
                new SReg() { path = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Steam App {0}", key = "InstallLocation" },
                new SReg() { path = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Steam App {0}", key = "InstallLocation" },
            };

        public bool GetUT3Exe(ref string path)
        {
            foreach (SReg s in COLL_UT3Locations)
            {
                try
                {
                    string regvalue = "";
                    regvalue = (string)Registry.GetValue(s.path, s.key, "");
                    if (regvalue != null && regvalue != "")
                    {
                        string ut3path = Path.Combine(regvalue, FOLDER_INSTALL_BINARIES);
                        string ut3file = Path.Combine(ut3path, FILE_INSTALL_UT3);

                        path = ut3file;
                        return true;
                    }
                }
                catch { };
            }

            return false;
        }

        public string GetUT3Path()
        {
            foreach (SReg s in COLL_UT3Locations)
            {
                try
                {
                    string regvalue = "";
                    regvalue = (string)Registry.GetValue(s.path, s.key, "");
                    if (regvalue != null && regvalue != "")
                    {
                        return regvalue;
                    }
                }
                catch { };
            }

            return string.Empty;
        }

        private string GetUT3SteamPath()
        {
            foreach (SReg s in COLL_SteamUT3Locations)
            {
                foreach (string appid in COLL_SteamUT3AppIDs)
                {
                    try
                    {
                        string regpath = String.Format(s.path, appid);
                        string regvalue = "";
                        regvalue = (string)Registry.GetValue(regpath, s.key, "");
                        if (regvalue != null && regvalue != "")
                        {
                            return regvalue;
                        }
                    }
                    catch { };
                }
            }

            return string.Empty;
        }
#endregion
#endregion

#region GUI

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            String path = System.IO.Path.Combine(Application.StartupPath, FILE_EULA);

            try
            {
                if (File.Exists(path))
                {
                    RichTextEULA.LoadFile(path);
                }
                else
                {
                    RichTextEULA.Text = String.Format(STRING_NO_EULA, FILE_EULA);
                }
            }
            catch (Exception)
            {
                RichTextEULA.Text = String.Format(STRING_BAD_EULA, FILE_EULA);
            }
        }

        private void ButtonDecline_Click(object sender, EventArgs e)
        {
            HideEULA(true);
            ShowNotPatched();
        }

        private void ButtonAccept_Click(object sender, EventArgs e)
        {
            HideEULA();

            string ut3nosteampath = GetUT3Path();
            string ut3path = GetUT3SteamPath();
            if (ut3path.Length == 0 && ut3nosteampath.Length > 0)
            {
                MessageBox.Show(this, STRING_NO_STEAM, MESSAGE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ut3path = ut3nosteampath;
            }

            if ((ut3path.Length > 0 && InstallTo(ut3path)) || (!UserManual && PatchErrorManual(true)))
            {
                MessageBox.Show(STRING_PATCH_SUCCESS, MESSAGE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ShowNotPatched();
            }

            Application.Exit();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!UserClick)
            {
                this.Visible = false;
                ShowNotPatched();
            }
        }

 #endregion

        private bool InstallTo(string ut3path)
        {
            if (!Directory.Exists(ut3path) || !Directory.Exists(Path.Combine(ut3path, CHECKFOLDER_ENGINE)) || !Directory.Exists(Path.Combine(ut3path, CHECKFOLDER_GAME)))
            {
                string msg = String.Format(STRING_NOT_INSTALLED, ut3path);
                MessageBox.Show(msg, MESSAGE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string msg = String.Format(STRING_APPLY, Environment.NewLine, ut3path);
                if (MessageBox.Show(msg, MESSAGE_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    String PatchPath = System.IO.Path.Combine(Application.StartupPath, FOLDER_PATCH);
                    if (Directory.Exists(PatchPath))
                    {
                        if (CheckCopyTo(ut3path, PatchPath) && CopyTo(ut3path, PatchPath))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        MessageBox.Show(STRING_PATCH_FAILED, MESSAGE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }

            if (!UserManual)
            {
                return PatchErrorManual();
            }

            return false;
        }

        private bool CheckCopyTo(string ut3path, string PatchPath)
        {
            return CheckCopyTo(ut3path, PatchPath, false);
        }

        private bool CheckCopyTo(string ut3path, string PatchPath, bool onlycheck)
        {
            string[] files = Directory.GetFiles(PatchPath, "*.*", System.IO.SearchOption.AllDirectories);
            foreach (string f in files)
            {
                string subpath = f.Substring(PatchPath.Length).TrimStart('\\');
                string newpath = Path.Combine(ut3path, subpath);
                if (File.Exists(newpath))
                {
                    bool locked = false;
                    bool isreadonly = false;
                    DialogResult result = DialogResult.Retry;
                    while (result == DialogResult.Retry)
                    {
                        isreadonly = IsFileReadOnly(newpath);
                        if (isreadonly)
                        {
                            string msg = String.Format(STRING_READONLY_ERROR, Environment.NewLine, newpath, Environment.NewLine + Environment.NewLine);
                            result = MessageBox.Show(msg, MESSAGE_TITLE, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                            if (result == DialogResult.Ignore)
                            {
                                try
                                {
                                    FileInfo fi = new FileInfo(newpath);
                                    if (fi != null)
                                    {
                                        fi.IsReadOnly = false;
                                    }
                                }
                                catch
                                {
                                }

                                isreadonly = false;
                            }
                        }
                        else
                        {
                            result = DialogResult.OK;
                        }
                    }

                    if (!isreadonly)
                    {
                        result = DialogResult.Retry;
                        while (result == DialogResult.Retry)
                        {
                            locked = IsFileLocked(newpath);
                            if (locked)
                            {
                                string msg = String.Format(STRING_ACCESS_ERROR, Environment.NewLine, newpath, Environment.NewLine + Environment.NewLine);
                                result = MessageBox.Show(msg, MESSAGE_TITLE, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                            }
                            else
                            {
                                result = DialogResult.OK;
                            }
                        }
                    }

                    if (locked || isreadonly)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        protected virtual bool IsFileReadOnly(string filepath)
        {
            FileStream stream = null;

            try
            {
                stream = File.Open(filepath, FileMode.Open);
                if (!stream.CanWrite)
                {
                    return true;
                }
            }
            catch (UnauthorizedAccessException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        protected virtual bool IsFileLocked(string filepath)
        {
            FileStream stream = null;

            try
            {
                stream = File.Open(filepath, FileMode.Open);
            }
            catch (UnauthorizedAccessException)
            {
                return true;
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        private bool CopyTo(string ut3path, string PatchPath)
        {
            foreach (string d in Directory.GetDirectories(PatchPath))
            {
                try
                {
                    InteropSHFileOperation.CopyFolder(d, ut3path, false, this.Handle);

                    foreach (string f in Directory.GetFiles(d, "*.*", System.IO.SearchOption.AllDirectories))
                    {
                        string subpath = f.Substring(PatchPath.Length).TrimStart('\\');
                        string newpath = Path.Combine(ut3path, subpath);
                        FileInfo fi = new FileInfo(newpath);
                        if (fi != null)
                        {
                            fi.Attributes = fi.Attributes & ~FileAttributes.ReadOnly;
                        }

                        
                        fi = new FileInfo(f);
                        if (fi != null)
                        {
                            fi.Attributes = fi.Attributes & ~FileAttributes.ReadOnly;
                        }
                    }

                    FileSystem.DeleteDirectory(d, DeleteDirectoryOption.DeleteAllContents);
                }
                catch (UnauthorizedAccessException) { }
                catch
                {
                    return false;
                };

            }

            foreach (string f in Directory.GetFiles(PatchPath))
            {
                try
                {
                    string subpath = f.Substring(PatchPath.Length).TrimStart('\\');
                    string newpath = Path.Combine(ut3path, subpath);
                    InteropSHFileOperation.CopyFile(f, newpath, false, this.Handle);

                    File.SetAttributes(f, FileAttributes.Normal);
                    FileSystem.DeleteFile(f);
                }
                catch (UnauthorizedAccessException) { }
                catch
                {
                    return false;
                };

            }

            return true;
        }

        private bool PatchErrorManual()
        {
            return PatchErrorManual(false);
        }

        private bool PatchErrorManual(bool skipmessage)
        {
            UserManual = true;
            if (!skipmessage) MessageBox.Show(STRING_PATCH_FAILED, MESSAGE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            DialogFolder.ShowNewFolderButton = false;
            DialogFolder.Description = STRING_BROWSE;

            if (DialogFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return InstallTo(DialogFolder.SelectedPath);
            }

            return false;
        }

        private void HideEULA()
        {
            HideEULA(false);
        }

        private void HideEULA(bool forceclose)
        {
            UserClick = true;
            if (forceclose)
            {
                this.Hide();
            }
            else
            {
                //this.Visible = false;
                this.Opacity = 0.0;
                this.ShowInTaskbar = false;
            }
        }

        private void ShowNotPatched()
        {
            MessageBox.Show(STRING_PATCH_NO, MESSAGE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Application.Exit();
        }
    }
}
