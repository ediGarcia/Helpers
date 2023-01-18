using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HelperControls.Controls
{
    public partial class ProgressBar : UserControl
    {
        #region Properties

        /// <summary>
        /// The upper bound of the range this ProgressBar is working with.
        /// </summary>
        [Category("Behavior"), DefaultValue(100), Description("The lower upper of the range this ProgressBar is working with.")]
        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                UpdateProgressBar();
            }
        }

        /// <summary>
        /// The lower bound of the range this ProgressBar is working with.
        /// </summary>
        [Category("Behavior"), DefaultValue(0), Description("The lower bound of the range this ProgressBar is working with.")]
        public int Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                UpdateProgressBar();
            }
        }

        /// <summary>
        /// Gets and sets the fore color of the text displayed above the progress bar.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ControlLightLight"), Description("Gets and sets the fore color of the text displayed above the progress bar.")]
        public Color OverProgressForeColor
        {
            get => LblMessageOver.ForeColor;
            set => LblMessageOver.ForeColor = value;
        }

        /// <summary>
        /// The background color of the progress bar.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "LimeGreen"), Description("The background color of the progress bar.")]
        public Color ProgressBarColor
        {
            get => PnlProgress.BackColor;
            set => PnlProgress.BackColor = value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Text associated with the control.
        /// </summary>
        [Category("Appearance"), DefaultValue(""), Browsable(true), Description("Text associated with the control.")]
        public override string Text
        {
            get => LblMessageOver.Text;
            set => LblMessageOver.Text = LblMessageUnder.Text = value;
        }

        /// <summary>
        /// Indicates whether the ProgressBar should show a marquee animation.
        /// </summary>
        [Category("Behavior"), DefaultValue(false), Description("Indicates whether the ProgressBar should show a marquee animation.")]
        public bool UseMarqueeAnimation
        {
            get => _marqueeTimer.Enabled;
            set
            {
                _marqueeTimer.Enabled = value;

                if (!_marqueeTimer.Enabled)
                    PnlProgress.Left = 0;
            }
        }

        /// <summary>
        /// Gets and sets the fore color of the text displayed below the progress bar.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ControlText"), Description("Gets and sets the fore color of the text displayed below the progress bar.")]
        public Color UnderProgressForeColor
        {
            get => LblMessageUnder.ForeColor;
            set => LblMessageUnder.ForeColor = value;
        }

        /// <summary>
        /// The current value of the ProgressBar, in range specified by the Minimum and Maximum properties.
        /// </summary>
        [Category("Behavior"), DefaultValue(0), Description("The current value of the ProgressBar, in range specified by the Minimum and Maximum properties.")]
        public int Value
        {
            get => _value;
            set
            {
                _value = value;

                if (_value > Maximum || _value < Minimum)
                    throw new ArgumentOutOfRangeException($"The value must be between Maximum ({Maximum}) and Minimum ({Minimum}).");

                UpdateProgressBar();
            }
        }

        #endregion

        private int _maximum = 100;
        private int _minimum;
        private readonly Timer _marqueeTimer = new Timer { Enabled = false, Interval = 300 };
        private int _value;

        public ProgressBar()
        {
            InitializeComponent();
            _marqueeTimer.Tick += MarqueeTimer_Tick;
        }

        #region Events

        #region MarqueeTimer_Tick
        private void MarqueeTimer_Tick(object sender, EventArgs e) =>
            PnlProgress.Left = (PnlProgress.Left + 1) % Width;
        #endregion

        #region ProgressBar_SizeChanged
        private void ProgressBar_SizeChanged(object sender, EventArgs e)
        {
            LblMessageOver.Width = Width;
            UpdateProgressBar();
        }
        #endregion

        #endregion

        #region Private methods

        #region UpdateProgressBar
        private void UpdateProgressBar() =>
            PnlProgress.Width = Math.Min((int)Math.Truncate(Width * (double)(Value - Minimum) / (Maximum - Minimum)), Width);
        #endregion

        #endregion
    }
}
