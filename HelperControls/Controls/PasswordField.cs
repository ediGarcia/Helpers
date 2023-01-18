using System;
using System.ComponentModel;
using System.Windows.Forms;
using HelperControls.Properties;
// ReSharper disable CommentTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

namespace HelperControls.Controls
{
    public partial class PasswordField : UserControl
    {
        [Category("Property Changed"), Description("Event raised when the when the value of the Password property is changed on control.")]
        public event EventHandler PasswordChanged;

        [Category("Property Changed"), Description("Event raised when the when the value of the IsToggle property is changed on control.")]
        public event EventHandler ToggleChanged;

        #region Propriedades

        /// <summary>
        /// Indica se o botão deve travar a visualização da senha.
        /// </summary>
        [Category("Behavior"), DefaultValue(true), Description("Indicares wheter the button should lock password visualization.")]
        public bool IsToggle
        {
            get => _isToggle;
            set
            {
                bool isAlteracao = _isToggle != value;

                _isToggle = value;

                if (!_isToggle)
                    OcultarSenha();

                if (isAlteracao)
                    ToggleChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// O texto associado ao controle.
        /// </summary>
        [Category("Appearance"), Description("The text associated with the control.")]

        public string Password
        {
            get => _password;
            set
            {
                TxtPassword.Text = _password = value;
                PasswordChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion

        private const char PasswordChar = '●';
        private const char OpenChar = (char)0;
        private bool _isToggle = true;
        private string _password = String.Empty;

        public PasswordField() => InitializeComponent();

        #region Eventos

        #region BtnShow_Click
        /// <summary>
        /// Exibe/oculta a senha.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnShow_Click(object sender, EventArgs e)
        {
            if (IsToggle)
            {
                if (TxtPassword.PasswordChar == PasswordChar)
                    ExibirSenha();
                else
                    OcultarSenha();
            }
        }
        #endregion

        #region BtnShow_MouseDown
        /// <summary>
        /// Exibe a senha ao pressionar com o mouse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnShow_MouseDown(object sender, MouseEventArgs e)
        {
            if (!IsToggle)
                ExibirSenha();
        }
        #endregion

        #region BtnShow_MouseUp
        /// <summary>
        /// Oculta a senha ao soltar o mouse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnShow_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsToggle)
                OcultarSenha();
        }
        #endregion

        #region TxtPassword_TextChanged
        /// <summary>
        /// Evento disparado quando a senha é alterada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            _password = TxtPassword.Text;
            PasswordChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #endregion

        #region Métodos

        #region ExibirSenha
        /// <summary>
        /// Exibe a senha aberta.
        /// </summary>
        private void ExibirSenha()
        {
            TxtPassword.PasswordChar = OpenChar;
            BtnShow.Image = Resources.HidePassword;
        }
        #endregion

        #region OcultarSenha
        /// <summary>
        /// Oculta a senha.
        /// </summary>
        private void OcultarSenha()
        {
            TxtPassword.PasswordChar = PasswordChar;
            BtnShow.Image = Resources.ShowPassword;
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
