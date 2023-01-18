using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HelperControls.Forms;
using HelperMethods;
using HelpersClasses.Enums;

// ReSharper disable CommentTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

namespace HelperControls.Controls
{
    [DefaultEvent("ColorChanged")]
    public partial class ColorSelector : UserControl
    {
        [Category("Action"), Description("Occurs when the selected color of the control changes.")]
        public event EventHandler ColorChanged;

        #region Propriedades

        //Cor selecionada.
        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Control"), Description("Cor selecionada.")]
        public Color Color
        {
            get => LblCor.BackColor;
            set
            {
                LblCor.BackColor = value;
                ExibirNomeCor();
            }
        }

        /// <summary>
        /// Tipo de string da cor selecionada.
        /// </summary>
        [Category("Behavior"), DefaultValue(ColorStringType.Nome), Description("Selected color string shown in control.")]
        public ColorStringType ColorStringType { get; set; } = ColorStringType.Nome;

        /// <summary>
        /// Indica se o texto da cor deve ser exibido.
        /// </summary>
        [Category("Behavior"), DefaultValue(true), Description("Indica se o controle deve exibir cor pelo nome ou pr seu código hexadecimal.")]
        public bool IsColorNameEnabled
        {
            get => _isExibirNomeCor;
            set
            {
                _isExibirNomeCor = value;
                ExibirNomeCor();
            }
        }

        /// <summary>
        /// Indica se é possível selecionar a opção "Transparente".
        /// </summary>
        [Category("Behavior"), DefaultValue(false), Description("Exibe a opção \"Sem Preenchimento\".")]
        public bool AllowTransparentFill { get; set; }

        #endregion

        private bool _isExibirNomeCor = true;
        public ColorSelector() => InitializeComponent();

        #region Eventos

        #region LblCor_Click
        /// <summary>
        /// Abre a janela de seleção de cor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblCor_Click(object sender, EventArgs e)
        {
            FrmColorPicker colorPicker = new FrmColorPicker { Cor = LblCor.BackColor, PermitirTransparencia = AllowTransparentFill };

            colorPicker.OnCorSelecionada += (ev, a) =>
            {
                Color = colorPicker.Cor;
                ColorChanged?.Invoke(this, EventArgs.Empty);
            };

            colorPicker.DefinirPosicaoPorControle(LblCor);
            colorPicker.Show();
        }
        #endregion

        #endregion

        #region Métodos

        #region ExibirNomeCor
        /// <summary>
        /// Exibe/oculta o nome da cor.
        /// </summary>
        private void ExibirNomeCor()
        {
            if (IsColorNameEnabled)
            {
                LblCor.Text = ColorMethods.GetPortugueseColorName(Color, ColorStringType);
                LblCor.ForeColor = ColorMethods.NeedsWhiteForeground(Color) ? SystemColors.ControlLightLight : SystemColors.ControlText;
            }
            else
                LblCor.Text = "";
        }
        #endregion

        #endregion
    }
}
