using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using HelperControls.Classes;
using HelperMethods;
// ReSharper disable CommentTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedParameter.Local

namespace HelperControls.Controls
{
    public partial class DropFileSelector : UserControl
    {
        #region Events

        /// <summary>
        /// Occurs when a dialog box is closed.
        /// </summary>
        [Category("Behavior"), Description("Occurs when a dialog box is closed.")]
        public event EventHandler DialogClosed;

        /// <summary>
        /// Occurs when a dialog box is opened.
        /// </summary>
        [Category("Behavior"), Description("Occurs when a dialog box is opened.")]
        public event EventHandler<DialogOpeningEventArgs> DialogOpening;

        /// <summary>
        /// Occurs when the selected file/folder changes.
        /// </summary>
        [Category("Action"), Description("Occurs when the selected file/folder changes.")]
        public event EventHandler FileChanged;

        /*/// <summary>
        /// Occurs when the selected file/folder is opened/executed.
        /// </summary>
        [Category("Action"), Description("Occurs when the selected file/folder is opened/executed.")]
        public event EventHandler FileExecuted;

        /// <summary>
        /// Occurs when the selected file/folder cannot opened/executed.
        /// </summary>
        [Category("Action"), Description("Occurs when the selected file/folder cannot opened/executed.")]
        public event EventHandler<FileErrorEventArgs> FileError;*/

        #endregion

        #region Propriedades

        /// <summary>
        /// Gets and sets file dialog initial file name.
        /// </summary>
        [Category("Behavior"), Description("Gets and sets file dialog initial file name.")]
        public string DialogFileName
        {
            get => OfdFile.FileName;
            set => OfdFile.FileName = value;
        }

        /// <summary>
        /// Message that will be shown in the file/folder dialog.
        /// </summary>
        [Category("Behavior"), DefaultValue(null), Description("Message that will be shown in the file/folder dialog.")]
        public string DialogMessage
        {
            get => OfdFile.Title;
            set => OfdFile.Title = FbdFile.Description = value;
        }

        /// <summary>
        /// The files filters to display in the open file and save file dialog boxes. For example: \"C# files|*.cs|All files|*.*\".
        /// </summary>
        [Category("Behavior"), Description("The files filters to display in the open file and save file dialog boxes. For example: \"C# files|*.cs|All files|*.*\".")]
        public string Filter
        {
            get => OfdFile.Filter;
            set => OfdFile.Filter = value;
        }

        /// <summary>
        /// Indicates whether the control should accept folders instead of files.
        /// </summary>
        [Category("Behavior"), Description("Indicates whether the control should accept folders instead of files.")]
        public bool IsFolderSelection { get; set; }

        /// <summary>
        /// When this property is true, every executable files is run as admin.
        /// </summary>
        [Category("Behavior"), DefaultValue(false), Description("When this property is true, every executable files is run as admin.")]
        public bool IsRunAsAdmin { get; set; }

        /// <summary>
        /// Indicates whether the folder dialog box should show the New Folder button.
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
                    LblFile.Text = _isShowOnlyFilename ? Path.GetFileName(SelectedPath) : SelectedPath;
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
                    LblFile.Text = _noFileMessage;
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
            set => OfdFile.InitialDirectory = FbdFile.SelectedPath = value;
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
                        FbdFile.SelectedPath = OfdFile.InitialDirectory = value; //Define a pasta incial dos diálogos.
                        OfdFile.FileName = String.Empty; //Define o arquivo e pasta iniciais nos diálogos.
                    }
                    else
                    {
                        FbdFile.SelectedPath = OfdFile.InitialDirectory = Path.GetDirectoryName(value); //Define a pasta incial dos diálogos.
                        OfdFile.FileName = Path.GetFileName(value); //Define o arquivo e pasta iniciais nos diálogos.
                    }
                }

                OfdFile.InitialDirectory = FbdFile.SelectedPath; //Define a pasta inicial pela última pasta selecionada.

                _lockFilePath = true;

                //Escreve o caminho do arquivo selecionado ou a mensagem, caso não exista arquivo.
                if (String.IsNullOrWhiteSpace(_selectedPath))
                    LblFile.Text = NoFileMessage;
                else
                    WinFormsMethods.FitText(LblFile, IsShowOnlyFileName ? Path.GetFileName(_selectedPath) : _selectedPath);

                _lockFilePath = false;
                FileChanged?.Invoke(this, EventArgs.Empty); //Dispara o evento.
            }
        }

        #endregion

        private bool _isShowOnlyFilename;
        private string _noFileMessage = "No file selected";
        private string _selectedPath;

        private bool _lockFilePath; //When set to true, textBox changes won't affect the selected file.

        public DropFileSelector() => InitializeComponent();

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

        #region LblFile_TextChanged
        /// <summary>
        /// Sets selected file from the user's typed path.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblFile_TextChanged(object sender, EventArgs e)
        {
            if (!_lockFilePath)
                SelectedPath = LblFile.Text;
        }
        #endregion

        #endregion

        #region Methods

        #region OpenDialog
        /// <summary>
        /// Opens file dialog box according to the DialogType.
        /// </summary>
        private void OpenDialog()
        {
            DialogResult resultado;
            string caminho = IsFolderSelection ? OfdFile.FileName : FbdFile.SelectedPath; //Gets file name for the event.

            DialogOpeningEventArgs eventArgs = new DialogOpeningEventArgs(caminho, IsFolderSelection ? DialogType.Folder : DialogType.OpenFile);
            DialogOpening?.Invoke(this, eventArgs);

            //Cancels dialog opening.
            if (eventArgs.Cancel)
                return;

            if (IsFolderSelection)
            {
                resultado = FbdFile.ShowDialog(this);
                caminho = FbdFile.SelectedPath;
            }
            else
            {
                resultado = OfdFile.ShowDialog(this);
                caminho = OfdFile.FileName;
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
