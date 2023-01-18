using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HelperMethods;
using HelpersClasses.Enums;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo

namespace HelperControls.Forms
{
    public partial class FrmColorPicker : Form
    {
        public event EventHandler OnCorSelecionada;

        #region Propriedades

        /// <summary>
        /// Cor selecionada da janela.
        /// </summary>
        public Color Cor { get; set; } = Color.Black;

        /// <summary>
        /// Indica se é possível selecionar a opção "Sem Preenchimento".
        /// </summary>
        public bool PermitirTransparencia
        {
            get => _permitirTransparencia;
            set
            {
                if (!value && _permitirTransparencia)
                    Height -= 22;
                else if (value && !_permitirTransparencia)
                    Height += 22;

                PnlAutomatico.Visible = _permitirTransparencia = value;
            }
        }

        #endregion

        private static readonly List<Color> CoresRecentes = new List<Color>(10); //Cores recentemente selecionadas.
        private static readonly Point PosicaoSelecionada = new Point(3, 3); //Centraliza a cor selecionada.
        private static readonly Point PosicaoNaoSelecionada = new Point(0, 0); //Reposiciona cor não selecionada.
        private static readonly Size TamanhoSelecionada = new Size(14, 14); //Diminui e destaca cor selecionada.
        private static readonly Size TamanhoNaoSelecionada = new Size(20, 20); //Expande cor não selecionada.

        private bool _dialogoAberto; //Indica que a janela de seleção avançada de cores está aberta.
        private bool _permitirTransparencia = true; //Indica se é possível selecionar a opção "Sem Preenchimento".

        /// <inheritdoc />
        /// <summary>
        /// Inicializa a janela com cor selecionada.
        /// </summary>
        public FrmColorPicker()
        {
            Panel panelCor;

            InitializeComponent();

            #region Preenche a listagem de cores recentes.
            //Verifica se há cores recentes.
            if (CoresRecentes.Count > 0)
            {
                Panel[] pnlCores =
                {
                    PnlFundo1,
                    PnlFundo2,
                    PnlFundo3,
                    PnlFundo4,
                    PnlFundo5,
                    PnlFundo6,
                    PnlFundo7,
                    PnlFundo8,
                    PnlFundo9,
                    PnlFundo10
                };

                for (int i = CoresRecentes.Count - 1, j = 0; i >= 0; i--, j++)
                {
                    Panel panelCorRecent = (Panel)pnlCores[j].Controls[0];

                    pnlCores[j].Visible = true;
                    panelCorRecent.BackColor = CoresRecentes[i];
                    TotCor.SetToolTip(panelCorRecent, ColorMethods.GetPortugueseColorName(CoresRecentes[i], ColorStringType.Nome));

                    DefinirBordaPanel(panelCorRecent);
                }
            }
            #endregion

            //Seleciona a cor atual.
            if (Cor == Color.Transparent)
                SelecionarPanelCor(PnlCorTransparente);
            else
                foreach (Control control in PnlForm.Controls)
                    if (control is Panel panel && panel != PnlCorTransparente && panel.Controls.Count == 1 && (panelCor = (Panel)panel.Controls[0]).BackColor == Cor)
                    {
                        SelecionarPanelCor(panelCor);
                        break;
                    }

            //Oculta cores recentes e não existir.
            if (CoresRecentes.Count == 0)
            {
                PnlAutomatico.Top = 210;
                Height = 276;
                LblCoresRecentes.Visible = false;
            }

            Location = PointToClient(MousePosition); //Define a posição se janela junto ao ponteiro do mouse.
        }

        #region Eventos

        #region FrmColorPicker_Deactivate
        /// <summary>
        /// Fecha a janela ao perder foco (se não houver diálogo aberto).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmColorPicker_Deactivate(object sender, EventArgs e)
        {
            if (_dialogoAberto)
                return;

            DialogResult = DialogResult.Cancel;
            Hide();
        }
        #endregion

        #region LblMaisCores_Click
        /// <summary>
        /// Abre a janela de seleção avançada de cores.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblMaisCores_Click(object sender, EventArgs e)
        {
            _dialogoAberto = true;
            CldCor.Color = Cor;
            Visible = false;

            if (CldCor.ShowDialog(this) == DialogResult.OK)
                SelecionarCor(CldCor.Color);
            else
                Visible = true;

            _dialogoAberto = false;
        }
        #endregion

        #region LblMaisCores_MouseEnter
        /// <summary>
        /// Destaca o item "Mais Cores..." quando o mouse está sobre ele.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblMaisCores_MouseEnter(object sender, EventArgs e) =>
            DestacarItem(LblMaisCores);
        #endregion

        #region LblMaisCores_MouseLeave
        /// <summary>
        /// Retorna o item "Mais Cores..." ao estado original ao retirar o mouse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblMaisCores_MouseLeave(object sender, EventArgs e) =>
            RetornarEstadoItem(LblMaisCores);
        #endregion

        #region PnlAutomatico_Click
        /// <summary>
        /// Seleciona cor transparente ao clicar em "Sem Preenchimento".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlAutomatico_Click(object sender, EventArgs e) =>
            SelecionarCor(Color.Transparent);
        #endregion

        #region PnlAutomatico_MouseEnter
        /// <summary>
        /// Destaca o item "Automático" ao passar o mouse sobre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlAutomatico_MouseEnter(object sender, EventArgs e) =>
            DestacarItem(PnlAutomatico);
        #endregion

        #region PnlAutomatico_MouseLeave
        /// <summary>
        /// Retorna o item "Automático" ao estado original quando o mouse sai.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlAutomatico_MouseLeave(object sender, EventArgs e) =>
            RetornarEstadoItem(PnlAutomatico);
        #endregion

        #region PnlCor_Click
        /// <summary>
        /// Seleciona uma cor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlCor_Click(object sender, EventArgs e)
        {
            Panel panelCor = (Panel)sender;

            if (panelCor.Controls.Count > 0)
                panelCor = (Panel)panelCor.Controls[0];

            SelecionarCor(panelCor == PnlCorTransparente ? Color.Transparent : panelCor.BackColor);
        }
        #endregion

        #region PnlCor_MouseEnter
        /// <summary>
        /// Destaca a cor abaixo do mouse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlCor_MouseEnter(object sender, EventArgs e)
        {
            Panel panelCor = (Panel)sender;
            SelecionarPanelCor(panelCor);

            if (panelCor == PnlCorTransparente)
                DestacarItem(PnlAutomatico);
        }
        #endregion

        #region PnlCor_MouseLeave
        /// <summary>
        /// Desseleciona a cor quando o mouse sai do quadro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlCor_MouseLeave(object sender, EventArgs e)
        {
            Panel panelCor = (Panel)sender;
            DesselecionarCor(panelCor);

            if (panelCor == PnlCorTransparente || panelCor == PnlAutomatico)
                RetornarEstadoItem(PnlAutomatico);
        }
        #endregion

        #endregion

        #region Métodos

        #region DefinirPosicaoPorControle
        /// <summary>
        /// Define a posição da janela de acordo com o controle passado por parâmetro.
        /// </summary>
        /// <param name="controle"></param>
        public void DefinirPosicaoPorControle(Control controle)
        {
            Point posicaoControle = controle.Parent.PointToScreen(controle.Location);
            Top = posicaoControle.Y + controle.Height;
            Left = posicaoControle.X;
        }
        #endregion

        #region DesselecionarCor
        /// <summary>
        /// Retorna a cor ao estado anterior.
        /// </summary>
        /// <param name="panelCor"></param>
        private void DesselecionarCor(Panel panelCor)
        {
            Color corSelecionada = panelCor == PnlCorTransparente ? Color.Transparent : panelCor.BackColor;

            if (corSelecionada == Cor || corSelecionada == Color.Transparent && panelCor == PnlCorTransparente)
                return;

            panelCor.Location = PosicaoNaoSelecionada;
            panelCor.Size = TamanhoNaoSelecionada;

            DefinirBordaPanel(panelCor);
        }
        #endregion

        #region DefinirBordaPanel
        /// <summary>
        /// Desenha borda ao redor de panels com coes muito claras.
        /// </summary>
        /// <param name="panelCor"></param>
        private void DefinirBordaPanel(Panel panelCor)
        {
            Color corSelecionada = panelCor == PnlCorTransparente ? Color.Transparent : panelCor.BackColor;

            if (Math.Sqrt(corSelecionada.R * corSelecionada.R * .241 + corSelecionada.G * corSelecionada.G * .691 + corSelecionada.B * corSelecionada.B * .068) >= 250)
                panelCor.BorderStyle = BorderStyle.FixedSingle;
        }
        #endregion

        #region DestacarItem
        /// <summary>
        /// Destaca item passado por parâmetro.
        /// </summary>
        /// <param name="item"></param>
        private static void DestacarItem(Control item) =>
            item.BackColor = Color.LightGray;
        #endregion

        #region RetornarEstadoItem
        /// <summary>
        /// Retorna item passado por parâmetro ao estado original.
        /// </summary>
        /// <param name="item"></param>
        private static void RetornarEstadoItem(Control item) =>
            item.BackColor = SystemColors.Window;
        #endregion

        #region SelecionarCor
        /// <summary>
        /// Seleciona uma cor e fecha a janela.
        /// </summary>
        /// <param name="cor"></param>
        private void SelecionarCor(Color cor)
        {
            Cor = cor;

            if (cor != Color.Transparent && !CoresRecentes.Contains(cor))
            {
                if (CoresRecentes.Count == 10)
                    CoresRecentes.RemoveAt(0);

                CoresRecentes.Add(cor);
            }

            DialogResult = DialogResult.OK;
            OnCorSelecionada?.Invoke(this, EventArgs.Empty);
            Hide();
        }
        #endregion

        #region SelecionarPanelCor
        /// <summary>
        /// Destaca a cor selecionada.
        /// </summary>
        /// <param name="panelCor"></param>
        private static void SelecionarPanelCor(Panel panelCor)
        {
            panelCor.Location = PosicaoSelecionada;
            panelCor.Size = TamanhoSelecionada;
            panelCor.BorderStyle = BorderStyle.None;
        }
        #endregion

        #endregion
    }
}
