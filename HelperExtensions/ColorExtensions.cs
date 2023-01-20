using System.Windows.Media;
// ReSharper disable UnusedMember.Global

namespace HelperExtensions;

public static class ColorExtensions
{
    #region Public Methods

    #region ToBrush (System.Windows.Media.Color)
    /// <summary>
    /// Returns the a <see cref="SolidColorBrush"/> representation of this <see cref="Color"/> .
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static SolidColorBrush ToBrush(this Color color) =>
        new(color);
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

    #endregion
}