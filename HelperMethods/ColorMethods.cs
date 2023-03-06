using ColorUtils;
using System;
using System.Drawing;
using System.Windows.Media;
using Color = System.Drawing.Color;
// ReSharper disable UnusedMember.Global

namespace HelperMethods;

/// <summary>
/// Color related methods.
/// </summary>
public static class ColorMethods
{
    #region Public Methods

    #region GenerateRandomPastelColor

    #region GenerateRandomPastelBrush
    /// <summary>
    /// Returns a random pastel color.
    /// </summary>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static SolidColorBrush GenerateRandomPastelBrush() =>
        GetColorFromAhsb(NumberMethods.GetRandomFloat(360), 1, .9f).ToBrush();
    #endregion

    #region GenerateRandomPastelColor
    /// <summary>
    /// Returns a random pastel color.
    /// </summary>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static Color GenerateRandomPastelColor() =>
        GetColorFromAhsb(NumberMethods.GetRandomFloat(360), 1, .9f);
    #endregion

    #region GenerateRandomPastelMediaColor
    /// <summary>
    /// Returns a random pastel color.
    /// </summary>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static System.Windows.Media.Color GenerateRandomPastelMediaColor() =>
        GetColorFromAhsb(NumberMethods.GetRandomFloat(360), 1, .9f).ToMediaColor();
    #endregion

    #endregion

    #region GenerateRandomColor

    #region GenerateRandomBrush
    /// <summary>
    /// Returns a random color.
    /// </summary>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static SolidBrush GenerateRandomBrush() =>
        new(GenerateRandomColor());
    #endregion

    #region GenerateRandomColor
    /// <summary>
    /// Returns a random color.
    /// </summary>
    /// <returns></returns>
    public static Color GenerateRandomColor() =>
        Color.FromArgb(
            NumberMethods.GetRandomInt(255),
            NumberMethods.GetRandomInt(255),
            NumberMethods.GetRandomInt(255)
        );
    #endregion

    #region GenerateRandomMediaColor
    /// <summary>
    /// Generates a random color.
    /// </summary>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static System.Windows.Media.Color GenerateRandomMediaColor() =>
        System.Windows.Media.Color.FromArgb(
            1,
            (byte)NumberMethods.GetRandomInt(255, true),
            (byte)NumberMethods.GetRandomInt(255, true),
            (byte)NumberMethods.GetRandomInt(255, true)
        );
    #endregion

    #endregion

    #region GetBrushFromAhsb*

    #region GetBrushFromAhsb (int, float, float, float)
    /// <summary>
    /// Generates a color from HSB values.
    /// </summary>
    /// <param name="alpha"></param>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="brightness"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once IdentifierTypo
    public static SolidColorBrush GetBrushFromAhsb(int alpha, float hue, float saturation, float brightness) =>
        GetColorFromAhsb(alpha, hue, saturation, brightness).ToBrush();
    #endregion

    #region GetBrushFromAhsb (float, float, float)
    /// <summary>
    /// Generates a color from HSB values.
    /// </summary>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="brightness"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once IdentifierTypo
    public static SolidColorBrush GetBrushFromAhsb(float hue, float saturation, float brightness) =>
        GetBrushFromAhsb(255, hue, saturation, brightness);
    #endregion

    #endregion

    #region GetColorFromAhsb*

    #region GetColorFromAhsb (int, float, float, float)
    /// <summary>
    /// Generates a color from HSB values.
    /// </summary>
    /// <param name="alpha"></param>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="brightness"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    // ReSharper disable once IdentifierTypo
    public static Color GetColorFromAhsb(int alpha, float hue, float saturation, float brightness)
    {
        if (alpha is < 0 or > 255)
            throw new ArgumentOutOfRangeException(nameof(alpha), alpha, "The alpha value must be between 0 e 255.");
        if (hue is < 0f or > 360f)
            throw new ArgumentOutOfRangeException(nameof(hue), hue, "The hue value must be between 0 e 360.");
        if (saturation is < 0f or > 1f)
            throw new ArgumentOutOfRangeException(nameof(saturation), saturation, "The saturation value must be between 0 e 1.");
        if (brightness is < 0f or > 1f)
            throw new ArgumentOutOfRangeException(nameof(brightness), brightness, "The brightness value must be between 0 e 1.");

        if (saturation == 0f)
        {
            int grayValue = (int)brightness * 255;
            return Color.FromArgb(alpha, grayValue, grayValue, grayValue);
        }

        float fMax, fMin;

        if (brightness > 0.5)
        {
            fMax = brightness - brightness * saturation + saturation;
            fMin = brightness + brightness * saturation - saturation;
        }
        else
        {
            fMax = brightness + brightness * saturation;
            fMin = brightness - brightness * saturation;
        }

        int iSextant = (int)Math.Floor(hue / 60f);

        if (hue >= 300f)
            hue -= 360f;

        hue = hue / 60f - 2f * (float)Math.Floor((iSextant + 1f) % 6f / 2f);

        float fMid = iSextant % 2 == 0 ? (hue * (fMax - fMin) + fMin) : (fMin - hue * (fMax - fMin));

        int iMax = Convert.ToInt32(fMax * 255);
        int iMid = Convert.ToInt32(fMid * 255);
        int iMin = Convert.ToInt32(fMin * 255);

        return iSextant switch
        {
            1 => Color.FromArgb(alpha, iMid, iMax, iMin),
            2 => Color.FromArgb(alpha, iMin, iMax, iMid),
            3 => Color.FromArgb(alpha, iMin, iMid, iMax),
            4 => Color.FromArgb(alpha, iMid, iMin, iMax),
            5 => Color.FromArgb(alpha, iMax, iMin, iMid),
            _ => Color.FromArgb(alpha, iMax, iMid, iMin)
        };
    }
    #endregion

    #region GetColorFromAhsb (float, float, float)
    /// <summary>
    /// Generates a color from HSB values.
    /// </summary>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="brightness"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    // ReSharper disable once IdentifierTypo
    public static Color GetColorFromAhsb(float hue, float saturation, float brightness) =>
        GetColorFromAhsb(255, hue, saturation, brightness);
    #endregion

    #endregion

    #region GetMediaColorFromAhsb*

    #region GetMediaColorFromAhsb (int, float, float, float)
    /// <summary>
    /// Generates a color from HSB values.
    /// </summary>
    /// <param name="alpha"></param>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="brightness"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once IdentifierTypo
    public static System.Windows.Media.Color GetMediaColorFromAhsb(int alpha, float hue, float saturation, float brightness) =>
        GetColorFromAhsb(alpha, hue, saturation, brightness).ToMediaColor();
    #endregion

    #region GetMediaColorFromAhsb (float, float, float)
    /// <summary>
    /// Generates a color from HSB values.
    /// </summary>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="brightness"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once IdentifierTypo
    public static System.Windows.Media.Color GetMediaColorFromAhsb(float hue, float saturation, float brightness) =>
        GetMediaColorFromAhsb(255, hue, saturation, brightness);
    #endregion 

    #endregion

    #region NeedsWhitefForeground*

    #region NeedsWhiteForeground (Brush)
    /// <summary>
    /// Indicates whether the current color is dark enough to need white foreground.
    /// </summary>
    /// <param name="brush"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static bool NeedsWhiteForeground(SolidBrush brush) =>
        NeedsWhiteForeground(brush.Color);
    #endregion

    #region NeedsWhiteForeground (System.Drawing.Color)
    /// <summary>
    /// Indicates whether the current color is dark enough to need white foreground.
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static bool NeedsWhiteForeground(Color color) =>
        (color.R * 299 + color.G * 587 + color.B * 144) / 1000 <= 123;
    #endregion

    #region NeedsWhiteForeground (System.Windows.Media.Color)
    /// <summary>
    /// Indicates whether the current color is dark enough to need white foreground.
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static bool NeedsWhiteForeground(System.Windows.Media.Color color) =>
        (color.R * 299 + color.G * 587 + color.B * 144) / 1000 <= 123;
    #endregion

    #endregion

    #endregion
}