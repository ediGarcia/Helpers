using System.Windows.Media;
using HelperMethods;

// ReSharper disable UnusedMember.Global

namespace HelperExtensions;

public static class ColorExtensions
{
    #region Public Methods

    #region Color

    #region ToBrush
    /// <summary>
    /// Returns a <see cref="SolidColorBrush"/> representation of this <see cref="Color"/> .
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static SolidColorBrush ToBrush(this Color color) =>
        new(color);
    #endregion

    #endregion

    #region SolidColorBrush

    #region GetDarker
    /// <summary>
    /// Returns a new <see cref="SolidColorBrush"/> with a darker shade.
    /// </summary>
    /// <param name="brush"></param>
    /// <param name="percentage"></param>
    /// <returns></returns>
    public static SolidColorBrush GetDarker(this SolidColorBrush brush, double percentage = 10) =>
        ColorMethods.GetDarkerBrush(brush, percentage);
    #endregion

    #region GetLighter
    /// <summary>
    /// Returns a new <see cref="SolidColorBrush"/> with a lighter shade.
    /// </summary>
    /// <param name="brush"></param>
    /// <param name="percentage"></param>
    /// <returns></returns>
    public static SolidColorBrush GetLighter(this SolidColorBrush brush, double percentage = 10) =>
        ColorMethods.GetLighterBrush(brush, percentage);
    #endregion

    #region ToMediaColor
    /// <summary>
    /// Returns a <see cref="Color"/> representation of this <see cref="SolidColorBrush"/> .
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static Color ToMediaColor(this SolidColorBrush color) =>
        color.Color;
    #endregion

    #endregion

    #endregion
}