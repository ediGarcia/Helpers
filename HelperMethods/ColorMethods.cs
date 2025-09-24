using ColorUtils;
using System;
using System.Drawing;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

// ReSharper disable UnusedMember.Global

namespace HelperMethods;

/// <summary>
/// Color related methods.
/// </summary>
public static class ColorMethods
{
    #region Public Methods

    #region GenerateRandomPastelColor*

    #region GenerateRandomPastelBrush
    /// <summary>
    /// Returns a random pastel color.
    /// </summary>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static SolidColorBrush GenerateRandomPastelBrush() =>
        GetColorFromHsb((float)NumberMethods.GetRandomDouble(0, 360), 1, .9f).ToBrush();
    #endregion

    #region GenerateRandomPastelColor
    /// <summary>
    /// Returns a random pastel color.
    /// </summary>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static System.Drawing.Color GenerateRandomPastelColor() =>
        GetColorFromHsb((float)NumberMethods.GetRandomDouble(0, 360), 1, .9f);
    #endregion

    #region GenerateRandomPastelMediaColor
    /// <summary>
    /// Returns a random pastel color.
    /// </summary>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static Color GenerateRandomPastelMediaColor() =>
        GetColorFromHsb((float)NumberMethods.GetRandomDouble(0, 360), 1, .9f).ToMediaColor();
    #endregion

    #endregion

    #region GenerateRandomColor*

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
    public static System.Drawing.Color GenerateRandomColor() =>
        System.Drawing.Color.FromArgb(
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
    public static Color GenerateRandomMediaColor() =>
        Color.FromArgb(
            255,
            (byte)NumberMethods.GetRandomInt(0, 255, true),
            (byte)NumberMethods.GetRandomInt(0, 255, true),
            (byte)NumberMethods.GetRandomInt(0, 255, true)
        );
    #endregion

    #endregion

    #region GetBrushFromAhsb
    /// <summary>
    /// Generates a color from HSB values.
    /// </summary>
    /// <param name="alpha"></param>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="brightness"></param>
    /// <returns></returns>
    // ReSharper disable once IdentifierTypo
    public static SolidColorBrush GetBrushFromAhsb(byte alpha, float hue, float saturation, float brightness) =>
        GetColorFromAhsb(alpha, hue, saturation, brightness).ToBrush();
    #endregion

    #region GetBrushFromHsb
    /// <summary>
    /// Generates a color from HSB values.
    /// </summary>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="brightness"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    // ReSharper disable once IdentifierTypo
    public static SolidColorBrush GetBrushFromHsb(float hue, float saturation, float brightness) =>
        GetBrushFromAhsb(255, hue, saturation, brightness);
    #endregion

    #region GetBrushFromArgb
    /// <summary>
    /// Creates a new <see cref="SolidColorBrush"/> by using the specified sRGB alpha channel and color channel values.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="r"></param>
    /// <param name="g"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static SolidColorBrush GetBrushFromArgb(byte a, byte r, byte g, byte b) =>
        new(Color.FromArgb(a, r, g, b));
    #endregion

    #region GetBrushFromRgb
    /// <summary>
    /// Creates a new <see cref="SolidColorBrush"/> by using the specified sRGB color channel values.
    /// </summary>
    /// <param name="r"></param>
    /// <param name="g"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static SolidColorBrush GetBrushFromRgb(byte r, byte g, byte b) =>
        new(Color.FromRgb(r, g, b));
    #endregion

    #region GetColorFromAhsb
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
    public static System.Drawing.Color GetColorFromAhsb(byte alpha, float hue, float saturation, float brightness)
    {
        if (hue is < 0f or > 360f)
            throw new ArgumentOutOfRangeException(nameof(hue), hue, "The hue value must be between 0 e 360.");
        if (saturation is < 0f or > 1f)
            throw new ArgumentOutOfRangeException(nameof(saturation), saturation, "The saturation value must be between 0 e 1.");
        if (brightness is < 0f or > 1f)
            throw new ArgumentOutOfRangeException(nameof(brightness), brightness, "The brightness value must be between 0 e 1.");

        if (saturation == 0f)
        {
            int grayValue = (int)brightness * 255;
            return System.Drawing.Color.FromArgb(alpha, grayValue, grayValue, grayValue);
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

        // ReSharper disable ArrangeRedundantParentheses
        float fMid = iSextant % 2 == 0 ? (hue * (fMax - fMin) + fMin) : (fMin - hue * (fMax - fMin));
        // ReSharper restore ArrangeRedundantParentheses

        int iMax = Convert.ToInt32(fMax * 255);
        int iMid = Convert.ToInt32(fMid * 255);
        int iMin = Convert.ToInt32(fMin * 255);

        return iSextant switch
        {
            1 => System.Drawing.Color.FromArgb(alpha, iMid, iMax, iMin),
            2 => System.Drawing.Color.FromArgb(alpha, iMin, iMax, iMid),
            3 => System.Drawing.Color.FromArgb(alpha, iMin, iMid, iMax),
            4 => System.Drawing.Color.FromArgb(alpha, iMid, iMin, iMax),
            5 => System.Drawing.Color.FromArgb(alpha, iMax, iMin, iMid),
            _ => System.Drawing.Color.FromArgb(alpha, iMax, iMid, iMin)
        };
    }
    #endregion

    #region GetColorFromHsb
    /// <summary>
    /// Generates a color from HSB values.
    /// </summary>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="brightness"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    // ReSharper disable once IdentifierTypo
    public static System.Drawing.Color GetColorFromHsb(float hue, float saturation, float brightness) =>
        GetColorFromAhsb(255, hue, saturation, brightness);
    #endregion

    #region GetDarkerBrush
    /// <summary>
    /// Returns a darker shade of the specified color.
    /// </summary>
    /// <param name="brush"></param>
    /// <param name="percentage"></param>
    /// <returns></returns>
    public static SolidColorBrush GetDarkerBrush(SolidColorBrush brush, double percentage) =>
        MultiplyBrushByConstant(brush, 1 - percentage / 100);
    #endregion

    #region GetLighterBrush
    /// <summary>
    /// Returns a lighter shade of the specified color.
    /// </summary>
    /// <param name="brush"></param>
    /// <param name="percentage"></param>
    /// <returns></returns>
    public static SolidColorBrush GetLighterBrush(SolidColorBrush brush, double percentage) =>
        MultiplyBrushByConstant(brush, 1 + percentage / 100);
    #endregion

    #region GetMediaColorFromAhsb
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
    public static Color GetMediaColorFromAhsb(byte alpha, float hue, float saturation, float brightness) =>
        GetColorFromAhsb(alpha, hue, saturation, brightness).ToMediaColor();
    #endregion

    #region GetMediaColorFromHsb
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
    public static Color GetMediaColorFromHsb(float hue, float saturation, float brightness) =>
        GetMediaColorFromAhsb(255, hue, saturation, brightness);
    #endregion 

    #region NeedsWhiteForeground*

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
    public static bool NeedsWhiteForeground(System.Drawing.Color color) =>
        (color.R * 299 + color.G * 587 + color.B * 144) / 1000 <= 123;
    #endregion

    #region NeedsWhiteForeground (System.Windows.Media.Color)
    /// <summary>
    /// Indicates whether the current color is dark enough to need white foreground.
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static bool NeedsWhiteForeground(Color color) =>
        (color.R * 299 + color.G * 587 + color.B * 144) / 1000 <= 123;
    #endregion

    #endregion

    #endregion

    #region Private Methods

    #region MultiplyBrushByConstant
    /// <summary>
    /// Multiplies each part of the color by a constant value.
    /// </summary>
    /// <param name="brush"></param>
    /// <param name="constant"></param>
    /// <returns></returns>
    private static SolidColorBrush MultiplyBrushByConstant(SolidColorBrush brush, double constant)
    {
        Color color = brush.Color;

        if (color is { R: 0, G: 0, B: 0 })
        {
            if (constant < 1)
                return brush;

            color = Color.FromArgb(color.A, 1, 1, 1);
        }


        byte red = (byte)GenericMethods.Max(GenericMethods.Min(color.R * constant, 255), 0);
        byte green = (byte)GenericMethods.Max(GenericMethods.Min(color.G * constant, 255), 0);
        byte blue = (byte)GenericMethods.Max(GenericMethods.Min(color.B * constant, 255), 0);

        return new(Color.FromArgb(color.A, red, green, blue));
    }
    #endregion

    #endregion
}