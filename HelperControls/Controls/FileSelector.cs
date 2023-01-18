using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using HelperControls.Classes;
using HelperMethods;
// ReSharper disable CommentTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

namespace HelperControls.Controls
{
    [DefaultEvent("FileChanged")]
    public partial class FileSelector : UserControl
    {
        #region Events

        public delegate void OnFileSelectorErrorEventHandler(object sender, FileErrorEventArgs e);

        public delegate void OnDialogOpeningEventHandler(object sender, DialogOpeningEventArgs e);

        /// <summary>
        /// Occurs when a dialog box is closed.
        /// </summary>
        [Category("Behavior"), Description("Occurs when a dialog box is closed.")]
        public event EventHandler DialogClosed;

        /// <summary>
        /// Occurs when a dialog box is opened.
        /// </summary>
        [Category("Behavior"), Description("Occurs when a dialog box is opened.")]
        public event OnDialogOpeningEventHandler DialogOpening;

        /// <summary>
        /// Occurs when the selected file/folder changes.
        /// </summary>
        [Category("Action"), Description("Occurs when the selected file/folder changes.")]
        public event EventHandler FileChanged;

        /// <summary>
        /// Occurs when the selected file/folder is opened/executed.
        /// </summary>
        [Category("Action"), Description("Occurs when the selected file/folder is opened/executed.")]
        public event EventHandler FileExecuted;

        /// <summary>
        /// Occurs when the selected file/folder cannot opened/executed.
        /// </summary>
        [Category("Action"), Description("Occurs when the selected file/folder cannot opened/executed.")]
        public event OnFileSelectorErrorEventHandler FileError;

        #endregion

        #region Propriedades

        /// <summary>
        /// Allows the user to manually edit file/folder path.
        /// </summary>
        [Category("Appearance"), DefaultValue(true), Description("Allows the user to manualy edit file/folder path.")]
        public bool AllowEdit
        {
            get => _isAllowEdit;
            set => TxtFile.Visible = _isAllowEdit = value;
        }

        /// <summary>
        /// Indicates which dialog box should be shown to the user.
        /// </summary>
        [Category("Behavior"), DefaultValue(DialogType.OpenFile), Description("Indicates which dialog box should be shown to the user.")]
        public DialogType Dialog { get; set; }

        /// <summary>
        /// Gets and sets file dialog initial file name.
        /// </summary>
        [Category("Behavior"), Description("Gets and sets file dialog initial file name.")]
        public string DialogFileName
        {
            get => SfdFile.FileName;
            set => SfdFile.FileName = OfdFile.FileName = value;
        }

        /// <summary>
        /// Message that will be shown in the file/folder dialog.
        /// </summary>
        [Category("Behavior"), DefaultValue(null), Description("Message that will be shown in the file/folder dialog.")]
        public string DialogMessage
        {
            get => OfdFile.Title;
            set => OfdFile.Title = SfdFile.Title = FbdFile.Description = value;
        }

        /// <summary>
        /// The files filters to display in the open file and save file dialog boxes. For example: \"C# files|*.cs|All files|*.*\".
        /// </summary>
        [Category("Behavior"), Description("The files filters to display in the open file and save file dialog boxes. For example: \"C# files|*.cs|All files|*.*\".")]
        public string Filter
        {
            get => OfdFile.Filter;
            set => OfdFile.Filter = SfdFile.Filter = value;
        }

        /// <summary>
        /// When this property is true, every executable files is run as admin.
        /// </summary>
        [Category("Behavior"), DefaultValue(false), Description("When this property is true, every executable files is run as admin.")]
        public bool IsRunAsAdmin { get; set; }

        /// <summary>
        /// Indicates whether the file selector should show the file/folder selection button.
        /// </summary>
        [Category("Appearance"), DefaultValue(true), Description("Indicates whether the file selector should show the file/folder selection button.")]
        public bool IsShowButton
        {
            get => _isButtonVisible;
            set
            {
                if (BtnFile.Visible && !value)
                    TxtFile.Width = LnkFile.Width += 36; //Extende os campos com o nome do arquivo.

                else if (!BtnFile.Visible && value)
                    TxtFile.Width = LnkFile.Width -= 36; //Encolhe os campos com o nome do arquivo.

                BtnFile.Visible = _isButtonVisible = value;
            }
        }

        /// <summary>
        /// Indicates whether the folder dialog box shold show the New Folder button.
        /// </summary>
        [Category("Behavior"), DefaultValue(false), Description("Indicates whether the folder dialog box shold show the New Folder button.")]
        public bool IsShowNewFolder
        {
            get => FbdFile.ShowNewFolderButton;
            set => FbdFile.ShowNewFolderButton = value;
        }

        /// <summary>
        /// When this property is true, only the name of the file will be shown in the control.
        /// </summary>
        [Category("Appearance"), DefaultValue(false), Description("When this property is true, only the name of the file will be shown in the control.")]
        public bool IsShowOnlyFileName
        {
            get => _isShowOnlyFilename;
            set
            {
                _isShowOnlyFilename = value;

                //Atualiza o nome do arquivo.
                if (!String.IsNullOrWhiteSpace(SelectedPath))
                {
                    _lockFilePath = true;
                    TxtFile.Text = LnkFile.Text = _isShowOnlyFilename ? Path.GetFileName(SelectedPath) : SelectedPath;
                    _lockFilePath = false;
                }
            }
        }

        /// <summary>
        /// Default displayed message when no file is selected.
        /// </summary>
        [Category("Appearance"), DefaultValue("No file selected"), Description("Default displayed message when no file is selected.")]
        public string NoFileMessage
        {
            get => _noFileMessage;
            set
            {
                _noFileMessage = value;

                //Atualiza a mensagem, caso não haja arquivo selecionado.
                if (String.IsNullOrWhiteSpace(SelectedPath))
                {
                    _lockFilePath = true;
                    TxtFile.Text = LnkFile.Text = _noFileMessage;
                    _lockFilePath = false;
                }
            }
        }

        /// <summary>
        /// The initial directory for the dialog box.
        /// </summary>
        [Category("Behavior"), DefaultValue(@"C:\"), Description("The initial directory for the dialog box.")]
        public string RootDirectory
        {
            get => OfdFile.InitialDirectory;
            set => OfdFile.InitialDirectory = SfdFile.InitialDirectory = FbdFile.SelectedPath = value;
        }

        /// <summary>
        /// File/folder selected.
        /// </summary>
        [Category("Appearance"), Description("File/folder selected.")]
        public string SelectedPath
        {
            get => _selectedPath;
            set
            {
                _selectedPath = value;

                bool? isDirectory = null;

                //Verifica se o caminho passado trata-se de uma pasta.
                if (!String.IsNullOrEmpty(value))
                    try
                    {
                        isDirectory = SystemMethods.IsDirectory(value);
                    }
                    catch
                    {
                        try
                        {
                            isDirectory = Directory.Exists(value);
                        }
                        catch
                        {
                            // Ignored.
                        }
                    }

                if (isDirectory.HasValue) //Verifica se foi possível acessar o arquivo.
                {
                    //Define o caminho correto na caixa de diálogo.
                    if (isDirectory.Value)
                    {
                        FbdFile.SelectedPath = OfdFile.InitialDirectory = SfdFile.InitialDirectory = value; //Define a pasta incial dos diálogos.
                        OfdFile.FileName = SfdFile.FileName = String.Empty; //Define o arquivo e pasta iniciais nos diálogos.
                    }
                    else
                    {
                        FbdFile.SelectedPath = OfdFile.InitialDirectory = SfdFile.InitialDirectory = Path.GetDirectoryName(value); //Define a pasta incial dos diálogos.
                        OfdFile.FileName = SfdFile.FileName = Path.GetFileName(value); //Define o arquivo e pasta iniciais nos diálogos.
                    }
                }

                OfdFile.InitialDirectory = SfdFile.InitialDirectory = FbdFile.SelectedPath; //Define a pasta inicial pela última pasta selecionada.

                _lockFilePath = true;

                //Escreve o caminho do arquivo selecionado ou a mensagem, caso não exista arquivo.
                if (String.IsNullOrWhiteSpace(_selectedPath))
                    TxtFile.Text = LnkFile.Text = NoFileMessage;
                else
                {
                    TxtFile.Text = IsShowOnlyFileName ? Path.GetFileName(_selectedPath) : _selectedPath;
                    WinFormsMethods.FitText(LnkFile, TxtFile.Text);
                }

                _lockFilePath = false;

                TotFile.SetToolTip(LnkFile, String.IsNullOrWhiteSpace(_selectedPath) || !IsShowButton ? "Click to select a file" : _selectedPath + Environment.NewLine + "Click to open."); //Exibe o tooltip.
                FileChanged?.Invoke(this, EventArgs.Empty); //Dispara o evento.
            }
        }

        #endregion

        private bool _isAllowEdit = true;
        private bool _isButtonVisible = true;
        private bool _isShowOnlyFilename;
        private string _noFileMessage = "No file selected";
        private string _selectedPath;

        private bool _lockFilePath; //When set to true, textBox changes won't affect the selected file.

        public FileSelector()
        {
            InitializeComponent();

            //Torna o controle selecionável.
            SetStyle(ControlStyles.Selectable, true);
            TabStop = true;
        }

        #region Events

        #region BtnFile_Click
        /// <summary>
        /// Opens file dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFile_Click(object sender, EventArgs e) =>
            OpenDialog();
        #endregion

        #region LnkFile_Resize
        /// <summary>
        /// Show elipsis when necessary.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LnkFile_Resize(object sender, EventArgs e) =>
            WinFormsMethods.FitText(LnkFile, (IsShowOnlyFileName ? Path.GetFileName(SelectedPath) : SelectedPath) ?? NoFileMessage);
        #endregion

        #region LnkFile_LinkClicked
        /// <summary>
        /// Opens the selected file or the file dialog when the link is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LnkFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (IsShowButton && !String.IsNullOrWhiteSpace(SelectedPath))
            {
                FileExecuted?.Invoke(this, EventArgs.Empty);

                try
                {
                    SystemMethods.Run(SelectedPath, isAdmin: IsRunAsAdmin);
                }
                catch (Exception ex)
                {
                    if (FileError == null)
                        WinFormsMethods.ShowErrorMessage(this, "File/folder could not be open. To change this error behavior, edit the FileError event.\n\nSystem's message:\n\n" + ex.Message);
                    else
                        FileError(this, new FileErrorEventArgs(ex));
                }
            }
            else
                OpenDialog();
        }
        #endregion

        #region TxtFile_TextChanged
        /// <summary>
        /// Sets selected file from the user's typed path.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtFile_TextChanged(object sender, EventArgs e)
        {
            if (!_lockFilePath)
                SelectedPath = TxtFile.Text;
        }

        #endregion

        #endregion

        #region Methods

        #region ClearFile
        /// <summary>
        /// Removes selected file/folder.
        /// </summary>
        public void ClearFile() =>
            SelectedPath = null;
        #endregion

        #region OpenDialog
        /// <summary>
        /// Opens file dialog box according to the DialogType.
        /// </summary>
        public void OpenDialog()
        {
            DialogResult resultado;
            string caminho;

            //Gets file name for the event.
            switch (Dialog)
            {
                case DialogType.OpenFile:
                    caminho = FbdFile.SelectedPath;
                    break;

                case DialogType.SaveFile:
                    caminho = OfdFile.FileName;
                    break;

                default:
                    caminho = SfdFile.FileName;
                    break;
            }

            DialogOpeningEventArgs eventArgs = new DialogOpeningEventArgs(caminho, Dialog);
            DialogOpening?.Invoke(this, eventArgs);

            //Cancels dialog opening.
            if (eventArgs.Cancel)
                return;

            switch (Dialog)
            {
                case DialogType.Folder:
                    resultado = FbdFile.ShowDialog(this);
                    caminho = FbdFile.SelectedPath;
                    break;

                case DialogType.OpenFile:
                    resultado = OfdFile.ShowDialog(this);
                    caminho = OfdFile.FileName;
                    break;

                default:
                    resultado = SfdFile.ShowDialog(this);
                    caminho = SfdFile.FileName;
                    break;
            }

            if (resultado == DialogResult.OK)
                SelectedPath = caminho;

            DialogClosed?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region SetBoundsCore
        /// <inheritdoc />
        /// <summary>
        /// Define a altura máxima do controle.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="specified"></param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified) =>
            base.SetBoundsCore(x, y, width, 20, specified);
        #endregion

        #endregion
    }
}
