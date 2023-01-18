using System;
using System.ComponentModel;
using System.Windows.Forms;
// ReSharper disable CommentTypo
// ReSharper disable UnusedMember.Global

namespace HelperControls.Controls
{
    [DefaultEvent("LayoutChanged")]
    public partial class ImageLayoutDialog : UserControl
    {
        [Category("Action"), Description("Occurs when selected layout changes.")]
        public event EventHandler LayoutChanged;

        #region Propriedades

        /// <summary>
        /// Layout selecionado.
        /// </summary>
        [Category("Appearance"), DefaultValue(ImageLayout.Center), Description("Selected image layout.")]

        public new ImageLayout Layout
        {
            get
            {
                if (RdbNone.Checked)
                    return ImageLayout.None;

                if (RdbTile.Checked)
                    return ImageLayout.Tile;

                if (RdbCenter.Checked)
                    return ImageLayout.Center;

                return RdbStretch.Checked ? ImageLayout.Stretch : ImageLayout.Zoom;
            }

            set
            {
                switch (value)
                {
                    case ImageLayout.None:
                        RdbNone.Checked = true;
                        break;

                    case ImageLayout.Tile:
                        RdbTile.Checked = true;
                        break;

                    case ImageLayout.Center:
                        RdbCenter.Checked = true;
                        break;

                    case ImageLayout.Stretch:
                        RdbStretch.Checked = true;
                        break;

                    default:
                        RdbZoom.Checked = true;
                        break;
                }
            }
        }
        #endregion

        public ImageLayoutDialog() => InitializeComponent();

        #region Eventos

        #region RdbLayout_Click
        /// <summary>
        /// Altera o layout selecionado e dispara o evento.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RdbLayout_Click(object sender, EventArgs e) =>
            LayoutChanged?.Invoke(this, EventArgs.Empty);
        #endregion

        #endregion
    }
}
