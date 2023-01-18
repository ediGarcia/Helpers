using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
// ReSharper disable UnusedMember.Global

namespace HelperControls.Controls
{
    [DefaultEvent("TextChanged")]
    public partial class PlaceholderTextBox : UserControl
    {
        #region Events

        /// <summary>
        /// Event raised when the value of the Text property is changed on Control.
        /// </summary>
        [Category("PropertyChanged"), Description("Event raised when the value of the Text property is changed on Control.")]
        public new event EventHandler TextChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates if return characters are accepted as input for multiline edit controls.
        /// </summary>
        [Category("Behavior"), DefaultValue(false), Description("Indicates if return characters are accepted as input for multiline edit controls.")]
        // ReSharper disable once UnusedMember.Global
        public bool AcceptsReturn
        {
            get => TxtTextBox.AcceptsReturn;
            set => TxtTextBox.AcceptsReturn = value;
        }

        /// <summary>
        /// Indicates if tab characters are accepted as input for multiline edit controls.
        /// </summary>
        [Category("Behavior"), DefaultValue(false), Description("Indicates if tab characters are accepted as input for multiline edit controls.")]
        // ReSharper disable once UnusedMember.Global
        public bool AcceptsTab
        {
            get => TxtTextBox.AcceptsTab;
            set => TxtTextBox.AcceptsTab = value;
        }

        /// <summary>
        /// The StringCollection to use when the AutoCompleteSource property is set to CustomSource.
        /// </summary>
        [Description("The StringCollection to use when the AutoCompleteSource property is set to CustomSource.")]
        // ReSharper disable once UnusedMember.Global
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get => TxtTextBox.AutoCompleteCustomSource;
            set => TxtTextBox.AutoCompleteCustomSource = value;
        }

        /// <summary>
        /// Indicates the text completion behavior of the text box.
        /// </summary>
        [DefaultValue(typeof(AutoCompleteMode), "None"), Description("Indicates the text completion behavior of the text box.")]
        // ReSharper disable once UnusedMember.Global
        public AutoCompleteMode AutoCompleteMode
        {
            get => TxtTextBox.AutoCompleteMode;
            set => TxtTextBox.AutoCompleteMode = value;
        }

        /// <summary>
        /// The auto complete source, which can be one of the values of the AutoCompleteSource enumeration.
        /// </summary>
        [DefaultValue(typeof(AutoCompleteSource), "None"), Description("The auto complete source, which can be one of the values of the AutoCompleteSource enumeration.")]
        // ReSharper disable once UnusedMember.Global
        public AutoCompleteSource AutoCompleteSource
        {
            get => TxtTextBox.AutoCompleteSource;
            set => TxtTextBox.AutoCompleteSource = value;
        }

        /// <summary>
        /// Indicates whether the edit control should have a border.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(BorderStyle), "FixedSingle"), Description("The background color of the component.")]
        public new BorderStyle BorderStyle
        {
            get => base.BorderStyle;
            set => base.BorderStyle = value;
        }

        /// <inheritdoc />
        /// <summary>
        /// The background color of the component.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "Window"), Description("The background color of the component.")]
        public override Color BackColor
        {
            get => _backColor;
            set
            {
                _backColor = value;

                if (Enabled)
                    base.BackColor = TxtTextBox.BackColor = _backColor;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// The cursor that appears when the mouse moves over the control.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Cursor), "IBeam"), Description("The cursor that appears when the mouse moves over the control.")]
        public override Cursor Cursor
        {
            get => TxtTextBox.Cursor;
            set => base.Cursor = TxtTextBox.Cursor = value;
        }

        /// <summary>
        /// Indicates whether the control should clear the text on focus lost, if the text contains only white space characters.
        /// </summary>
        [Category("Behavior"), DefaultValue(true), Description("Indicates whether the control should clear the text on focus lost, if the text contains only white space characters.")]
        public bool IsClearTextOnWhiteSpace { get; set; } = true;

        /// <summary>
        /// Controls whether the text of the edit control can span more than one line.
        /// </summary>
        [Category("Behavior"), DefaultValue(false), Description("Controls whether the text of the edit control can span more than one line.")]
        public bool IsMultiline
        {
            get => TxtTextBox.Multiline;
            set
            {
                TxtTextBox.Multiline = value;

                if (!TxtTextBox.Multiline)
                    Height = MaxSingleLineHeight;
            }
        }

        /// <summary>
        /// Controls whether the text in the edit control can be changed or not.
        /// </summary>
        [Category("Behavior"), DefaultValue(false), Description("Controls whether the text in the edit control can be changed or not.")]
        public bool IsReadOnly
        {
            get => TxtTextBox.ReadOnly;
            set => TxtTextBox.ReadOnly = value;
        }

        /// <summary>
        /// Indicates if lines are automatically word-wrapped for multiline edit controls.
        /// </summary>
        [Category("Behavior"), DefaultValue(true), Description("Indicates if lines are automatically word-wrapped for multiline edit controls.")]
        public bool IsWordWrapping
        {
            get => TxtTextBox.WordWrap;
            set => TxtTextBox.WordWrap = value;
        }

        /// <summary>
        /// Indicates the character to display for password input for single line edit controls.
        /// </summary>
        [Category("Behavior"), DefaultValue((char)0), Description("Indicates the character to display for password input for single line edit controls.")]
        public char PasswordChar
        {
            get => TxtTextBox.PasswordChar;
            set => TxtTextBox.PasswordChar = value;
        }

        /// <summary>
        /// The font used to display the placeholder text in the control.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Font), "Microsoft Sans Serif, 8.25pt, style=Italic"), Description("The font used to display the placeholder text in the control.")]
        public Font PlaceholderFont
        {
            get => _placeholderFont;
            set
            {
                _placeholderFont = value;

                if (_isShowingPlaceholder)
                    TxtTextBox.Font = _placeholderFont;
            }
        }

        /// <summary>
        /// Foreground color of the placeholder text.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "GrayText"), Description("Foreground color of the placeholder text.")]
        public Color PlaceholderForeColor
        {
            get => _placeholderForeColor;
            set
            {
                _placeholderForeColor = value;

                if (_isShowingPlaceholder)
                    TxtTextBox.ForeColor = _placeholderForeColor;
            }
        }

        /// <summary>
        /// Placeholder text.
        /// </summary>
        [Category("Appearance"), DefaultValue("Your text here"), Description("Placeholder text.")]
        public string PlaceholderText
        {
            get => _placeholderText;
            set
            {
                _placeholderText = value;

                if (_isShowingPlaceholder)
                    TxtTextBox.Text = _placeholderText;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Text associated with the control.
        /// </summary>
        [Category("Appearance"), Description("Text associated with the control."), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get => _text;
            set
            {
                _text = TxtTextBox.Text = value;
                _isShowingPlaceholder = false;
                ManagePlaceHolder();

                TextChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion

        private const int MaxSingleLineHeight = 20;
        private Color _backColor = SystemColors.Window;
        private Font _placeholderFont;
        private Color _placeholderForeColor = SystemColors.GrayText;
        private string _placeholderText = "Your text here";
        private string _text;
        private bool _isShowingPlaceholder;

        public PlaceholderTextBox()
        {
            InitializeComponent();
            PlaceholderFont = new Font(Font, FontStyle.Italic);

            ManagePlaceHolder();
        }

        #region Events

        #region PlaceholderTextBox_EnabledChanged

        /// <summary>
        /// Changes back color on Enabled changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaceholderTextBox_EnabledChanged(object sender, EventArgs e) =>
            base.BackColor = TxtTextBox.BackColor = Enabled ? BackColor : SystemColors.Control;

        #endregion

        #region PlaceholderTextBox_Enter
        /// <summary>
        /// Sets focus on the text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaceholderTextBox_Enter(object sender, EventArgs e) =>
            TxtTextBox.Focus();
        #endregion

        #region PlaceholderTextBox_FontChanged
        /// <summary>
        /// Updates text color.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaceholderTextBox_FontChanged(object sender, EventArgs e)
        {
            if (!_isShowingPlaceholder)
                TxtTextBox.Font = Font;
        }
        #endregion

        #region PlaceholderTextBox_ForeColorChanged
        /// <summary>
        /// Updates text fore color.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaceholderTextBox_ForeColorChanged(object sender, EventArgs e)
        {
            if (!_isShowingPlaceholder)
                TxtTextBox.ForeColor = ForeColor;
        }
        #endregion

        #region SetBoundsCore
        /// <inheritdoc />
        /// <summary>
        /// Sets control's maximum height.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="specified"></param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified) =>
            base.SetBoundsCore(x, y, width, IsMultiline ? height : MaxSingleLineHeight, specified);
        #endregion

        #region TxtTextBox_Enter

        /// <summary>
        /// Hides placeholder text on focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTextBox_Enter(object sender, EventArgs e) =>
            ManagePlaceHolder();
        #endregion

        #region TxtTextBox_Leave
        /// <summary>
        /// Checks if the placeholder is needed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTextBox_Leave(object sender, EventArgs e) =>
            ManagePlaceHolder();
        #endregion

        #endregion

        #region Methods

        #region ManagePlaceHolder
        /// <summary>
        /// Shows/hides placeholder text.
        /// </summary>
        private void ManagePlaceHolder()
        {
            if (!TxtTextBox.Focused && (String.IsNullOrEmpty(TxtTextBox.Text) || String.IsNullOrWhiteSpace(TxtTextBox.Text) && IsClearTextOnWhiteSpace))
            {
                if (!_isShowingPlaceholder)
                {
                    TxtTextBox.Text = PlaceholderText;
                    TxtTextBox.Font = PlaceholderFont;
                    TxtTextBox.ForeColor = PlaceholderForeColor;

                    _isShowingPlaceholder = true;
                }
            }
            else
            {
                if (_isShowingPlaceholder)
                {
                    TxtTextBox.Text = String.Empty;
                    _isShowingPlaceholder = false;
                }

                TxtTextBox.Font = Font;
                TxtTextBox.ForeColor = ForeColor;
            }
        }
        #endregion

        #endregion
    }
}
