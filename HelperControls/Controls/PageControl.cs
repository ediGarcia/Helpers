using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
// ReSharper disable CommentTypo

namespace HelperControls.Controls
{
    public partial class PageControl : UserControl
    {
        #region Eventos

        /// <summary>
        /// Disparado quando uma página é adicionada ao controle.
        /// </summary>
        [Category("Action"), Description("Occurs when a new page is added.")]
        public PageChangedEventHandler PageAdded;

        /// <summary>
        /// Disparado quando uma página é inserda no controle.
        /// </summary>
        [Category("Action"), Description("Occurs when a page is removed.")]
        public PageChangedEventHandler PageRemoved;

        /// <summary>
        /// Disparado quando uma página é inserda no controle.
        /// </summary>
        [Category("Action"), Description("Occurs when a page is removed.")]
        public SelectedPageChangedEventHandler SelectedPageChanged;

        #endregion

        #region Propriedades

        /// <summary>
        /// Página selecionada.
        /// </summary>
        [Category("Behavior"), DefaultValue(null), Description("Gets and sets actual selected page.")]
        public Page SelectedPage
        {
            get => _selectedPage;
            set
            {
                //Valor null inserido.
                if (value is null)
                {
                    if (PagesCount == 0)
                        _selectedPage = null;

                    return;
                }

                Page previousPage = _selectedPage;

                if (!PagesList.Contains(value))
                    throw new ArgumentOutOfRangeException(nameof(value), "Selected page does not exist in the control.");

                PagesList.ForEach(_ => _.IsSelected = _ == value);

                _selectedPage = value;
                SelectedPageChanged?.Invoke(this, new PageCollectionSelectionChangedEventArgs { ActualPage = value, PreviousPage = previousPage });
            }
        }

        /// <summary>
        /// Lista de páginas do controle.
        /// </summary>
        [Category("Behavior"), Description("The Pages in PageControl.")]
        public PageCollection Pages { get; set; }

        [Browsable(false)]
        public int PagesCount => PagesList.Count;
        #endregion

        private Page _selectedPage;

        internal List<Page> PagesList { get; } = new List<Page>();

        public PageControl()
        {
            InitializeComponent();
            Pages = new PageCollection(this);

            Add(new Page());
            Add(new Page());
        }

        #region Métodos

        /// <summary>
        /// Adds a <see cref="T:Helpers.Forms.Page" /> to the collection.
        /// </summary>
        public void Add(Page page)
        {
            if (page.Name is null)
            {
                int count = PagesCount + 1;
                string newName = $"Pages{count}";

                while (PagesList.Exists(x => x.Name == newName))
                {
                    count++;
                    newName = $"Pages{count}";
                }

                page.Name = newName;
            }

            PagesList.Add(page);
            Controls.Add(page);

            if (PagesCount == 1)
                SelectedPage = page;

            PageAdded?.Invoke(this, new PageCollectionChangedEventArgs { Page = page });
        }

        /// <summary>
        /// Removes all the tab pages from the collection.
        /// </summary>
        public void Clear()
        {
            PagesList.Clear();
            Controls.Clear();
        }

        /// <summary>
        /// Contains a collection of <see cref="T:System.Windows.Forms.Control" /> objects.
        /// </summary>
        /// <param name="page">The <see cref="T:Helpers.Forms.Page" /> to locate in the collection.</param>
        /// <returns></returns>
        public bool Contains(Page page) => PagesList.Contains(page);

        /// <summary>
        /// Gets the page at the specified index from the collection.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Page Get(int index) => PagesList[index];

        /// <summary>
        /// Returns the index of the specified tab page in the collection.
        /// </summary>
        /// <param name="page">The <see cref="T:Helpers.Forms.Page" /> to locate in the collection.</param>
        /// <returns></returns>
        public int IndexOf(Page page) => PagesList.IndexOf(page);

        /// <summary>
        /// Inserts an existing page into the collection at the specified index.
        /// </summary>
        /// <param name="page">The <see cref="T:Helpers.Forms.Page" /> to insert in the collection.</param>
        /// <param name="index">The zero-based index location where the page is inserted.</param>
        public void Insert(Page page, int index)
        {
            PagesList.Insert(index, page);
            Controls.Add(page);
            Controls.SetChildIndex(page, index);
        }

        /// <summary>
        /// Removes a <see cref="T:Helpers.Forms.Page" /> from the collection.
        /// </summary>
        /// <param name="page">The <see cref="T:Helpers.Forms.Page" /> to remove.</param>
        public void Remove(Page page)
        {
            PagesList.Remove(page);
            Controls.Remove(page);

            PageRemoved?.Invoke(this, new PageCollectionChangedEventArgs { Page = page });
        }

        /// <summary>
        /// Removes the page at the specified index from the collection.
        /// </summary>
        /// <param name="index">The zero-based index of the <see cref="T:Helpers.Forms.Page" /> to remove.</param>
        /// <returns></returns>
        public Page RemoveAt(int index)
        {
            Page removedPage = PagesList[index];

            SelectedPage = PagesCount == 1 ? null :
                index == PagesCount - 1 ? PagesList[index - 1] : PagesList[index + 1];

            PagesList.RemoveAt(index);
            Controls.RemoveAt(index);

            PageRemoved?.Invoke(this, new PageCollectionChangedEventArgs { Page = removedPage });
            return removedPage;
        }

        #endregion
    }
}
