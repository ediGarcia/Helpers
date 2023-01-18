using System.Windows.Media;
// ReSharper disable UnusedMember.Global

namespace ColorUtils
{
    public static class ColorUtils
    {
        #region ToBrush (System.Drawing.Color)
        /// <summary>
        /// Returns the a <see cref="SolidColorBrush"/> representation of this <see cref="System.Drawing.Color"/> .
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static SolidColorBrush ToBrush(this System.Drawing.Color color) =>
            new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
        #endregion

        #region ToBrush (System.Windows.Media.Color)
        /// <summary>
        /// Returns the a <see cref="SolidColorBrush"/> representation of this <see cref="Color"/> .
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static SolidColorBrush ToBrush(this Color color) =>
            new SolidColorBrush(color);
        #endregion

        #region ToDrawingColor (System.Windows.Media.Color)
        /// <summary>
        /// Returns the a <see cref="System.Drawing.Color"/> representation of this <see cref="Color"/> .
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static System.Drawing.Color ToDrawingColor(this Color color) =>
            System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        #endregion

        #region ToDrawingColor (System.Windows.Media.SolidColorBrush)
        /// <summary>
        /// Returns the a <see cref="System.Drawing.Color"/> representation of this <see cref="SolidColorBrush"/> .
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static System.Drawing.Color ToDrawingColor(this SolidColorBrush color) =>
            System.Drawing.Color.FromArgb(color.Color.A, color.Color.R, color.Color.G, color.Color.B);
        #endregion

        #region ToMediaColor (System.Drawing.Color)
        /// <summary>
        /// Returns the a <see cref="Color"/> representation of this <see cref="System.Drawing.Color"/> .
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ToMediaColor(this System.Drawing.Color color) =>
            Color.FromArgb(color.A, color.R, color.G, color.B);
        #endregion

        #region ToMediaColor (System.Windows.Media.SolidColorBrush)
        /// <summary>
        /// Returns the a <see cref="Color"/> representation of this <see cref="SolidColorBrush"/> .
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ToMediaColor(this SolidColorBrush color) =>
            color.Color;
        #endregion
    }
}
