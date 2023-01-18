using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
// ReSharper disable CommentTypo

namespace HelperControls.Controls
{
    public delegate void PageChangedEventHandler(object sender, PageCollectionChangedEventArgs e);
    public delegate void SelectedPageChangedEventHandler(object sender, PageCollectionSelectionChangedEventArgs e);

    public class PageCollectionChangedEventArgs : EventArgs
    {
        public Page Page { get; set; }
    }
    public class PageCollectionSelectionChangedEventArgs : EventArgs
    {
        public Page ActualPage { get; set; }

        public Page PreviousPage { get; set; }
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [Designer("System.Windows.Forms.Design.TabPageDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [ToolboxItem(false)]
    [DesignTimeVisible(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    public class Page : Panel
    {
        #region Itens Ocultos

        #region Eventos
        /// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.AutoSize" /> property changes.</summary>
        [Category("CatPropertyChanged")]
        [Description("ControlOnAutoSizeChangedDescr")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler AutoSizeChanged
        {
            add => base.AutoSizeChanged += value;
            remove => base.AutoSizeChanged -= value;
        }

        /// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.Dock" /> property changes.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler DockChanged
        {
            add => base.DockChanged += value;
            remove => base.DockChanged -= value;
        }

        /// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.Enabled" /> property changes.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler EnabledChanged
        {
            add => base.EnabledChanged += value;
            remove => base.EnabledChanged -= value;
        }

        /// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.Location" /> property changes.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler LocationChanged
        {
            add => base.LocationChanged += value;
            remove => base.LocationChanged -= value;
        }

        /// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.TabIndex" /> property changes.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler TabIndexChanged
        {
            add => base.TabIndexChanged += value;
            remove => base.TabIndexChanged -= value;
        }

        /// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.TabStop" /> property changes.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler TabStopChanged
        {
            add => base.TabStopChanged += value;
            remove => base.TabStopChanged -= value;
        }

        /// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabControl.Text" /> property changes.</summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event EventHandler TextChanged
        {
            add => base.TextChanged += value;
            remove => base.TextChanged -= value;
        }

        /// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.Visible" /> property changes.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler VisibleChanged
        {
            add => base.VisibleChanged += value;
            remove => base.VisibleChanged -= value;
        }
        #endregion

        #region Propriedades
        /// <inheritdoc />
        /// <summary>This property is not meaningful for this control.</summary>
        /// <returns>The control grows as much as necessary to fit its contents but does not shrink smaller than the value of its size property</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [Localizable(false)]
        public override AutoSizeMode AutoSizeMode => AutoSizeMode.GrowOnly;

        /// <inheritdoc />
        /// <summary>This property is not meaningful for this control.</summary>
        /// <returns>The default value is <see langword="false" />.</returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoSize => base.AutoSize;

        /// <inheritdoc />
        /// <summary>This member is not meaningful for this control.</summary>
        /// <returns>An <see cref="T:System.Windows.Forms.AnchorStyles" /> value.</returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override AnchorStyles Anchor => base.Anchor;

        /// <inheritdoc />
        /// <summary>This member is not meaningful for this control.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.DockStyle" /> value.</returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override DockStyle Dock => base.Dock;

        /// <summary>This member is not meaningful for this control.</summary>
        /// <returns>The default is <see langword="true" />.</returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool Enabled => base.Enabled;

        /// <summary>This property is not meaningful for this control.</summary>
        /// <returns>The x and y coordinates which specifies the location of the object.</returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Point Location => base.Location;

        /// <inheritdoc />
        /// <summary>This property is not meaningful for this control.</summary>
        /// <returns>The upper limit of the size of the object.</returns>
        [DefaultValue(typeof(Size), "0, 0")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Size MaximumSize => base.MaximumSize;

        /// <inheritdoc />
        /// <summary>This property is not meaningful for this control.</summary>
        /// <returns>The lower limit of the size of the object.</returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Size MinimumSize => base.MinimumSize;

        /// <summary>This property is not meaningful for this control.</summary>
        /// <returns>The size of a rectangular area into which the control can fit.</returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Size PreferredSize => base.PreferredSize;

        /// <summary>This property is not meaningful for this control.</summary>
        /// <returns>The tab order of the control.</returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new int TabIndex => base.TabIndex;

        /// <summary>This member is not meaningful for this control.</summary>
        /// <returns>The default is <see langword="true" />.</returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool TabStop => base.TabStop;

        /// <inheritdoc />
        /// <summary>Gets or sets the text to display on the tab.</summary>
        /// <returns>The text to display on the tab.</returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Text => base.Text;

        /// <summary>This member is not meaningful for this control.</summary>
        /// <returns>The default is <see langword="true" />.</returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool Visible => base.Visible;
        #endregion

        #endregion

        #region Propriedades

        /// <summary>
        /// Indice esta é a página selecionada.
        /// </summary>
        [Category("Behavior"), Description("Sets whether a page is the selected page of the control.")]
        public bool IsSelected { get; internal set; }

        /// <summary>
        /// Gets of sets the name of the control.
        /// </summary>
        [Browsable(true)]
        [Category("Design"), Description("Gets of sets the name of the control.")]
        public new string Name { get; set; }
        #endregion

        public Page()
        {
            SetStyle(ControlStyles.CacheText, true);
            // ReSharper disable once VirtualMemberCallInConstructor
            Dock = DockStyle.Fill;
        }

        #region Métodos Públicos

        #region GetPageOfComponent
        /// <summary>Retrieves the page that contains the specified object.</summary>
        /// <param name="comp">The object to look for. </param>
        /// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> that contains the specified object, or <see langword="null" /> if the object cannot be found.</returns>
        public static Page GetPageOfComponent(object comp)
        {
            Control control = comp as Control;

            if (comp is null)
                return null;

            while (control != null && !(control is Page))
                control = control.Parent;

            return (Page)control;
        }
        #endregion

        #endregion

        #region Métodos Privados

        #region SetBoundsCore
        /// <inheritdoc />
        /// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.SetBoundsCore(System.Int32,System.Int32,System.Int32,System.Int32,System.Windows.Forms.BoundsSpecified)" />.</summary>
        /// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
        /// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
        /// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
        /// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
        /// <param name="specified">A bitwise combination of <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            Control parentInternal = Parent;
            if (parentInternal is TabControl && parentInternal.IsHandleCreated)
            {
                Rectangle displayRectangle = parentInternal.DisplayRectangle;
                base.SetBoundsCore(displayRectangle.X, displayRectangle.Y, displayRectangle.Width, displayRectangle.Height, specified == BoundsSpecified.None ? BoundsSpecified.None : BoundsSpecified.All);
            }
            else
                base.SetBoundsCore(x, y, width, height, specified);
        }
        #endregion

        #endregion
    }

    public class PageCollection : IList
    {
        public virtual Page this[int index]
        {
            get => _owner.PagesList[index];
            set => _owner.PagesList[index] = value;
        }

        #region Propriedades

        #region Obrigatórias
        object IList.this[int index]
        {
            get => this[index];
            set => this[index] = value as Page ?? throw new ArgumentException(nameof(value));
        }

        public IEnumerator GetEnumerator() => _owner.PagesList.GetEnumerator();

        public bool IsFixedSize => true;

        public bool IsReadOnly => false;

        public bool IsSynchronized => false;

        public object SyncRoot => this;
        #endregion

        /// <inheritdoc />
        /// <summary>
        /// Número de páginas no controle.
        /// </summary>
        public int Count => _owner.PagesList.Count;
        #endregion

        private readonly PageControl _owner; //Page control that owns this collection.

        public PageCollection(PageControl owner) =>
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));

        #region Métodos

        #region Obrigatórios

        public int Add(object value)
        {
            Add(ConverterParaPage(value));
            return 1;
        }

        public bool Contains(object value) => Contains(ConverterParaPage(value));

        public void CopyTo(Array array, int index)
        {
            if (Count > 0)
                Array.Copy(_owner.PagesList.ToArray(), 0, array, index, Count);
        }

        public int IndexOf(object value) => IndexOf(ConverterParaPage(value));

        public void Insert(int index, object value) => Insert(index, ConverterParaPage(value));

        public void Remove(object value) => Remove(ConverterParaPage(value));

        public void RemoveAt(int index) => RemoveAtIndex(index);
        #endregion

        public void Add(Page page) => _owner.Add(page);

        public void Clear() => _owner.Clear();

        public bool Contains(Page page) => _owner.Contains(page);

        public int IndexOf(Page page) => _owner.IndexOf(page);

        public void Insert(int index, Page page) => _owner.Insert(page, index);

        public void Remove(Page page) => _owner.Remove(page);

        public Page RemoveAtIndex(int index) => _owner.RemoveAt(index);

        #endregion

        #region Métodos Privados

        #region VerificarTipo
        /// <summary>
        /// Verifica se o objeto é do tipo Page.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private Page ConverterParaPage(object value)
        {
            if (!(value is Page page))
                throw new ArgumentException(nameof(value));

            return page;
        }
        #endregion

        #endregion
    }
}
