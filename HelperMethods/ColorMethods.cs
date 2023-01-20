using ColorUtils;
using HelpersClasses.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace HelperMethods;

/// <summary>
/// Color related methods.
/// </summary>
public static class ColorMethods
{
    #region Public Methods

    #region Dictionary<Color, string>
    private static readonly Dictionary<Color, string> PortugueseColors = new()
    {
        // ReSharper disable StringLiteralTypo
        { Color.FromArgb(0, 160, 220), "Azul Turquesa" },
        { Color.FromArgb(40, 200, 240), "Azul Turquesa Brilhante" },
        { Color.FromArgb(30, 150, 200), "Azul Brasilis" },
        { Color.FromArgb(40, 160, 220), "Azul Brasilis Brilhante" },
        { Color.FromArgb(32, 112, 160), "Azul Tóquio" },
        { Color.FromArgb(127, 255, 212), "Água-Marinha" },
        { Color.FromArgb(102, 205, 170), "Água-Marinha Média" },
        { Color.FromArgb(40, 130, 185), "Azul Celeste" },
        { Color.FromArgb(227, 38, 54), "Alizarina" },
        { Color.Yellow, "Amarelo" },
        { Color.FromArgb(200, 160, 40), "Amarelo Brasilis" },
        { Color.FromArgb(160, 140, 100), "Amarelo Creme" },
        { Color.LightYellow, "Amarelo Claro" },
        { Color.FromArgb(242, 173, 53), "Amarelo Escuro" },
        { Color.GreenYellow, "Amarelo Esverdeado" },
        { Color.FromArgb(250, 250, 210), "Amarelo Ouro Claro" },
        { Color.FromArgb(238, 173, 45), "Amarelo Queimado" },
        { Color.FromArgb(255, 191, 0), "Âmbar" },
        { Color.Plum, "Ameixa" },
        { Color.FromArgb(153, 102, 204), "Ametista" },
        { Color.BlanchedAlmond, "Amêndoa" },
        { Color.FromArgb(123, 160, 91), "Aspargo" },
        { Color.Blue, "Azul" },
        { Color.AliceBlue, "Azul Alice" },
        { Color.SlateBlue, "Azul Ardósia" },
        { Color.FromArgb(132, 112, 255), "Azul Ardósia Claro" },
        { Color.DarkSlateBlue, "Azul Ardósia Escuro" },
        { Color.MediumSlateBlue, "Azul Ardósia Médio" },
        { Color.FromArgb(184, 202, 212), "Azul Areado" },
        { Color.SteelBlue, "Azul Aço" },
        { Color.LightSteelBlue, "Azul Aço Claro" },
        { Color.CadetBlue, "Azul Cadete" },
        { Color.FromArgb(5, 79, 119), "Azul Camarada" },
        { Color.Azure, "Azul Celeste" },
        { Color.FromArgb(0, 127, 255), "Azul Celeste Brilhante" },
        { Color.LightBlue, "Azul Claro" },
        { Color.FromArgb(0, 71, 171), "Azul Cobalto" },
        { Color.SkyBlue, "Azul Céu" },
        { Color.LightSkyBlue, "Azul Céu Claro" },
        { Color.DeepSkyBlue, "Azul Céu Profundo" },
        { Color.DarkBlue, "Azul Escuro" },
        { Color.CornflowerBlue, "Azul Flor De Milho" },
        { Color.FromArgb(93, 138, 168), "Azul Força Aérea" },
        { Color.DodgerBlue, "Azul Furtivo" },
        { Color.FromArgb(166, 170, 62), "Azul Manteiga" },
        { Color.FromArgb(18, 10, 143), "Azul Marinho" },
        { Color.MidnightBlue, "Azul Meia-Noite" },
        { Color.MediumBlue, "Azul Médio" },
        { Color.FromArgb(8, 77, 110), "Azul Petróleo" },
        { Color.PowderBlue, "Azul Pólvora" },
        { Color.FromArgb(36, 142, 255), "Azul Taparuere" },
        { Color.RoyalBlue, "Azul Real" },
        { Color.BlueViolet, "Azul Violeta" },
        { Color.FromArgb(244, 196, 48), "Açafrão" },
        { Color.FromArgb(245, 245, 220), "Bege" },
        { Color.Maroon, "Bordô" },
        { Color.FromArgb(144, 0, 32), "Borgonha" },
        { Color.White, "Branco" },
        { Color.AntiqueWhite, "Branco Antigo" },
        { Color.GhostWhite, "Branco Fantasma" },
        { Color.FloralWhite, "Branco Floral" },
        { Color.WhiteSmoke, "Branco Fumaça" },
        { Color.NavajoWhite, "Branco Navajo" },
        { Color.FromArgb(65, 96, 20), "Brasil" },
        { Color.FromArgb(205, 127, 50), "Bronze" },
        { Color.FromArgb(139, 87, 66), "Caramelo" },
        { Color.Khaki, "Caqui" },
        { Color.Thistle, "Cardo" },
        { Color.Crimson, "Carmesim" },
        { Color.FromArgb(113, 47, 38), "Carmim" },
        { Color.FromArgb(160, 50, 80), "Carmim Clássico" },
        { Color.FromArgb(150, 0, 24), "Carmim Carnáceo" },
        { Color.FromArgb(138, 0, 0), "Castanho Avermelhado" },
        { Color.Tan, "Castanho Claro" },
        { Color.FromArgb(237, 145, 33), "Cenoura" },
        { Color.FromArgb(222, 49, 99), "Cereja" },
        { Color.FromArgb(244, 0, 161), "Cereja Hollywood" },
        { Color.Chocolate, "Chocolate" },
        { Color.Cyan, "Ciano" },
        { Color.LightCyan, "Ciano Claro" },
        { Color.DarkCyan, "Ciano Escuro" },
        { Color.Gray, "Cinza" },
        { Color.SlateGray, "Cinza Ardósia" },
        { Color.LightSlateGray, "Cinza Ardósia Claro" },
        { Color.DarkSlateGray, "Cinza Ardósia Escuro" },
        { Color.LightGray, "Cinza Claro" },
        { Color.DarkGray, "Cinza Escuro" },
        { Color.DimGray, "Cinza Fosco" },
        { Color.Gainsboro, "Cinza Médio" },
        { Color.FromArgb(184, 115, 51), "Cobre" },
        { Color.SeaShell, "Concha" },
        { Color.Coral, "Coral" },
        { Color.LightCoral, "Coral Claro" },
        { Color.FromArgb(240, 220, 130), "Couro" },
        { Color.FromArgb(255, 253, 208), "Creme" },
        { Color.Bisque, "Creme De Marisco" },
        { Color.MintCream, "Creme De Menta" },
        { Color.DarkKhaki, "Caqui Escuro" },
        { Color.Goldenrod, "Dourado" },
        { Color.DarkGoldenrod, "Dourado Escuro" },
        { Color.PaleGoldenrod, "Dourado Pálido" },
        { Color.FromArgb(33, 36, 31), "Ébano" },
        { Color.FromArgb(42, 19, 51), "Eminência" },
        { Color.FromArgb(255, 36, 0), "Escarlate" },
        { Color.FromArgb(80, 200, 120), "Esmeralda" },
        { Color.FromArgb(27, 84, 66), "Eucalipto" },
        { Color.FromArgb(71, 20, 54), "Fandango" },
        { Color.FromArgb(209, 146, 117), "Feldspato" },
        { Color.FromArgb(183, 65, 14), "Ferrugem" },
        { Color.FromArgb(64, 0, 43), "Flerte" },
        { Color.FromArgb(61, 43, 31), "Fuligem" },
        { Color.FromArgb(131, 29, 28), "Grená" },
        { Color.FromArgb(86, 86, 86), "Gainsboro" },
        { Color.FromArgb(90, 91, 98), "Glitter" },
        { Color.FromArgb(75, 36, 35), "Goiaba" },
        { Color.SeaGreen, "Herbal" },
        { Color.FromArgb(87, 45, 100), "Heliotrópio" },
        { Color.Indigo, "Índigo" },
        { Color.FromArgb(30, 32, 43), "Independência" },
        { Color.FromArgb(35, 31, 81), "Iris" },
        { Color.FromArgb(0, 168, 107), "Jade" },
        { Color.OrangeRed, "Jambo" },
        { Color.FromArgb(97, 87, 49), "Jasmine" },
        { Color.FromArgb(56, 90, 25), "Kiwi" },
        { Color.FromArgb(91, 62, 77), "Kobi" },
        { Color.FromArgb(42, 27, 14), "Kobicha" },
        { Color.Orange, "Laranja" },
        { Color.DarkOrange, "Laranja Escuro" },
        { Color.FromArgb(255, 184, 77), "Laranja Claro" },
        { Color.Lavender, "Lavanda" },
        { Color.LavenderBlush, "Lavanda Avermelhada" },
        { Color.FromArgb(200, 162, 200), "Lilás" },
        { Color.FromArgb(0, 255, 0), "Limão" },
        { Color.Lime, "Lima" },
        { Color.Linen, "Linho" },
        { Color.FromArgb(222, 184, 135), "Madeira" },
        { Color.Fuchsia, "Magenta" },
        { Color.DarkMagenta, "Magenta Escuro" },
        { Color.FromArgb(224, 176, 255), "Malva" },
        { Color.PapayaWhip, "Mamão Batido" },
        { Color.Honeydew, "Maná" },
        { Color.Ivory, "Marfim" },
        { Color.FromArgb(150, 75, 0), "Marrom" },
        { Color.SandyBrown, "Marrom Amarelado" },
        { Color.Brown, "Marrom Claro" },
        { Color.RosyBrown, "Marrom Rosado" },
        { Color.SaddleBrown, "Marrom Sela" },
        { Color.FromArgb(251, 236, 93), "Milho" },
        { Color.Cornsilk, "Milho Claro" },
        { Color.Moccasin, "Mocassim" },
        { Color.FromArgb(255, 219, 88), "Mostarda" },
        { Color.Navy, "Naval" },
        { Color.Snow, "Neve" },
        { Color.FromArgb(91, 100, 86), "Nyanza" },
        { Color.FromArgb(204, 119, 34), "Ocre" },
        { Color.Olive, "Oliva" },
        { Color.DarkOliveGreen, "Oliva Escura" },
        { Color.OliveDrab, "Oliva Parda" },
        { Color.Orchid, "Orquídea" },
        { Color.DarkOrchid, "Orquídea Escura" },
        { Color.MediumOrchid, "Orquídea Média" },
        { Color.Gold, "Ouro" },
        { Color.Peru, "Pardo" },
        { Color.FromArgb(204, 102, 0), "Pardo Escuro" },
        { Color.Silver, "Prata" },
        { Color.Black, "Preto" },
        { Color.PeachPuff, "Pêssego" },
        { Color.MediumPurple, "Púrpura Média" },
        { Color.FromArgb(17, 17, 17), "Quantum" },
        { Color.FromArgb(32, 28, 31), "Quartz" },
        { Color.OldLace, "Renda Antiga" },
        { Color.Pink, "Rosa" },
        { Color.FromArgb(255, 0, 127), "Rosa Brilhante" },
        { Color.FromArgb(252, 15, 192), "Rosa Choque" },
        { Color.FromArgb(240, 130, 210), "Rosa Amoroso" },
        { Color.LightPink, "Rosa Claro" },
        { Color.FromArgb(218, 105, 161), "Rosa Danação" },
        { Color.MistyRose, "Rosa Embaçado" },
        { Color.HotPink, "Rosa Forte" },
        { Color.DeepPink, "Rosa Profundo" },
        { Color.Purple, "Roxo" },
        { Color.FromArgb(200, 25, 180), "Roxo Brasilis" },
        { Color.FromArgb(109, 53, 26), "Rútilo" },
        { Color.FromArgb(250, 127, 114), "Salmão" },
        { Color.LightSalmon, "Salmão Claro" },
        { Color.DarkSalmon, "Salmão Escuro" },
        { Color.FromArgb(255, 130, 71), "Siena" },
        { Color.FromArgb(112, 87, 20), "Sépia" },
        { Color.FromArgb(95, 52, 0), "Tangerina" },
        { Color.FromArgb(226, 114, 91), "Terracota" },
        { Color.Firebrick, "Tijolo Refratário" },
        { Color.Tomato, "Tomate" },
        { Color.Transparent, "Sem Preenchimento" },
        { Color.Wheat, "Trigo" },
        { Color.FromArgb(255, 36, 1), "Triássico" },
        { Color.Turquoise, "Turquesa" },
        { Color.DarkTurquoise, "Turquesa Escura" },
        { Color.MediumTurquoise, "Turquesa Média" },
        { Color.PaleTurquoise, "Turquesa Pálida" },
        { Color.FromArgb(53, 47, 76), "Ube" },
        { Color.FromArgb(236, 35, 0), "Urucum" },
        { Color.Green, "Verde" },
        { Color.YellowGreen, "Verde Amarelado" },
        { Color.LightGreen, "Verde Claro" },
        { Color.DarkGreen, "Verde Escuro" },
        { Color.ForestGreen, "Verde Floresta" },
        { Color.FromArgb(204, 255, 51), "Verde Fluorescente" },
        { Color.LimeGreen, "Verde Lima" },
        { Color.LawnGreen, "Verde Grama" },
        { Color.LightSeaGreen, "Verde Mar Claro" },
        { Color.DarkSeaGreen, "Verde Mar Escuro" },
        { Color.MediumSeaGreen, "Verde Mar Médio" },
        { Color.FromArgb(120, 134, 107), "Verde Militar" },
        { Color.FromArgb(107, 142, 35), "Verde-Oliva" },
        { Color.Chartreuse, "Verde Paris" },
        { Color.SpringGreen, "Verde Primavera" },
        { Color.MediumSpringGreen, "Verde Primavera Médio" },
        { Color.PaleGreen, "Verde Pálido" },
        { Color.Teal, "Verde-Azulado" },
        { Color.Red, "Vermelho" },
        { Color.FromArgb(85, 0, 0), "Vermelho Enegrecido" },
        { Color.DarkRed, "Vermelho Escuro" },
        { Color.IndianRed, "Vermelho Indiano" },
        { Color.FromArgb(208, 32, 144), "Vermelho Violeta" },
        { Color.MediumVioletRed, "Vermelho Violeta Médio" },
        { Color.PaleVioletRed, "Vermelho Violeta Pálido" },
        { Color.Violet, "Violeta" },
        { Color.DarkViolet, "Violeta Escuro" },
        { Color.FromArgb(248, 203, 248), "Violeta Claro" },
        { Color.FromArgb(115, 134, 120), "Xanadu" },
        { Color.FromArgb(0, 8, 66), "Zaffre" }
        // ReSharper restore StringLiteralTypo
    };
    #endregion

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

    #region GetColorFromAhsb (int, float, float, float)

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

    #endregion

    #region GetColorFromAhsb (float, float, float)

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
        GetColorFromAhsb(255, hue, saturation, brightness).ToBrush();
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
        GetColorFromAhsb(255, hue, saturation, brightness).ToMediaColor();
    #endregion

    #endregion

    #region GetPortugueseColorName
    /// <summary>
    /// Gets the selected color name.
    /// </summary>
    /// <param name="color"></param>
    /// <param name="type"></param>
    /// <param name="alternativeType">Alternative name format, in case the color has no Portuguese name.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException" />
    public static string GetPortugueseColorName(Color color, ColorStringType type, ColorStringType alternativeType = ColorStringType.Rgb) =>
        alternativeType == ColorStringType.Nome
            ? throw new ArgumentException($"{nameof(alternativeType)} must be Hexadecimal or RGB.", nameof(alternativeType))
            : type switch
            {
                ColorStringType.Nome => PortugueseColors.ContainsKey(color) ? PortugueseColors[color] : color.IsNamedColor ? color.Name : GetPortugueseColorName(color, alternativeType),
                ColorStringType.Rgb => $"{color.R}, {color.G}, {color.B}",
                _ => $"#{color.R:X2}{color.G:X2}{color.B:X2}"
            };
    #endregion

    #region NeedsWhitefForeground

    #region NeedsWhitefForeground (Brush)
    /// <summary>
    /// Indicates whether the current color is dark enough to need white foreground.
    /// </summary>
    /// <param name="brush"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static bool NeedsWhiteForeground(SolidBrush brush) =>
        NeedsWhiteForeground(brush.Color);
    #endregion

    #region NeedsWhitefForeground (System.Drawing.Color)
    /// <summary>
    /// Indicates whether the current color is dark enough to need white foreground.
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static bool NeedsWhiteForeground(Color color) =>
        (color.R * 299 + color.G * 587 + color.B * 144) / 1000 <= 123;
    #endregion

    #region NeedsWhitefForeground (System.Windows.Media.Color)
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