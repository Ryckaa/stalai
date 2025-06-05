using System;
using System.Windows.Forms;

namespace PlaceMint.Access
{
    using PlaceMint.Access.Properties;

    /// <summary>
    /// Controler for File Dialogs
    /// </summary>
    public static class FileDialogHelper
    {
        /// <summary>
        /// Save Dialog
        /// </summary>
        /// <param name="filename">Filename to suggest in save dialog.</param>
        /// <param name="newName">Filename that was saved to</param>
        /// <returns>DialogResult in case specific actions should be taken beyond given.</returns>
        public static DialogResult Save(string filename, out string newName)
        {
            return worker(true, filename, out newName);
        }

        /// <summary>
        /// Open Dialog
        /// </summary>
        /// <param name="filename">Filename to suggest in save dialog.</param>
        /// <param name="newName">Filename that was saved to</param>
        /// <returns>DialogResult in case specific actions should be taken beyond given.</returns>
        public static DialogResult Load(string filename, out string newName)
        {
            return worker(false, filename, out newName);
        }

        private static DialogResult worker(bool save, string filename, out string newName)
        {
            FileDialog dialog;
            if (save)
            {
                dialog = new SaveFileDialog();
                dialog.Title = Resources.saveDialogTitle;
            }
            else
            {
                dialog = new OpenFileDialog();
                dialog.Title = Resources.configLoadDialogTitle;
            }
            dialog.DefaultExt = Resources.FileDefaultExt;
            dialog.Filter = Resources.FileFilter;
            dialog.InitialDirectory = Environment.CurrentDirectory;
            dialog.RestoreDirectory = true;
            dialog.FileName = filename;

            DialogResult result = dialog.ShowDialog();
            newName = (result == DialogResult.OK) ? dialog.FileName : null;
            return result;
        }
    }
}