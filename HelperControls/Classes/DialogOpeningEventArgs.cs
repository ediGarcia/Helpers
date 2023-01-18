namespace HelperControls.Classes
{
    public class DialogOpeningEventArgs
    {
        #region Properties

        /// <summary>
        /// Cancels dialog opening.
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Type of the dialog.
        /// </summary>
        public DialogType DialogType { get; set; }

        /// <summary>
        /// Current selected file.
        /// </summary>
        public string CurrentSelectedFile { get; set; }

        #endregion

        public DialogOpeningEventArgs(string currentSelectedFile, DialogType dialogType)
        {
            CurrentSelectedFile = currentSelectedFile;
            DialogType = dialogType;
        }
    }
}
