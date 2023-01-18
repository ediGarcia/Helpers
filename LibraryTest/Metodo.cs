using System;
using System.Windows.Forms;
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace LibraryTest
{
    /// <inheritdoc />
    ///  <summary>
    /// Métodos exibidos no array.
    ///  </summary>
    public class Metodo : IComparable<Metodo>
    {
        /// <summary>
        /// Texto do método.
        /// </summary>
        public string Texto { get; set; }

        /// <summary>
        /// Aba do método.
        /// </summary>
        public TabPage Aba { get; set; }

        public Metodo(string texto, TabPage aba)
        {
            Texto = texto;
            Aba = aba;
        }

        public int CompareTo(Metodo metodo) => Texto.CompareTo(metodo.Texto);
    }
}
