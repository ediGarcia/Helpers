using HelperMethods.Enums;
using System;
using System.Collections.Generic;

namespace HelperMethods;

public static class BytesMethods
{
    /// <summary>
    /// Gets the string representation of each file size unit.
    /// </summary>
    public static readonly IReadOnlyDictionary<FileSizeUnit, string> Units = new Dictionary<FileSizeUnit, string>
    {
        { FileSizeUnit.Byte, "B" },
        { FileSizeUnit.Kilo, "KB" },
        { FileSizeUnit.Mega, "MB" },
        { FileSizeUnit.Giga, "GB" },
        { FileSizeUnit.Tera, "TB" },
        { FileSizeUnit.Peta, "PB" },
        { FileSizeUnit.Exa, "EB" },
        { FileSizeUnit.Zetta, "ZB" },
        { FileSizeUnit.Yotta, "YB" },
        { FileSizeUnit.Ronna, "RB" },
        { FileSizeUnit.Quetta, "QB" }
    };

    private static readonly IReadOnlyDictionary<FileSizeUnit, double> BinaryMultipliers = new Dictionary<FileSizeUnit, double>
    {
        { FileSizeUnit.Byte, 1 },
        { FileSizeUnit.Kilo, 1024 },
        { FileSizeUnit.Mega, Math.Pow(1024, 2) },
        { FileSizeUnit.Giga, Math.Pow(1024, 3) },
        { FileSizeUnit.Tera, Math.Pow(1024, 4) },
        { FileSizeUnit.Peta, Math.Pow(1024, 5) },
        { FileSizeUnit.Exa, Math.Pow(1024, 6) },
        { FileSizeUnit.Zetta, Math.Pow(1024, 7) },
        { FileSizeUnit.Yotta, Math.Pow(1024, 8) },
        { FileSizeUnit.Ronna, Math.Pow(1024, 9) },
        { FileSizeUnit.Quetta, Math.Pow(1024, 10) }
    };

    private static readonly IReadOnlyDictionary<FileSizeUnit, double> DecimalMultipliers = new Dictionary<FileSizeUnit, double>
    {
        { FileSizeUnit.Byte, 1 },
        { FileSizeUnit.Kilo, 1000 },
        { FileSizeUnit.Mega, Math.Pow(1000, 2) },
        { FileSizeUnit.Giga, Math.Pow(1000, 3) },
        { FileSizeUnit.Tera, Math.Pow(1000, 4) },
        { FileSizeUnit.Peta, Math.Pow(1000, 5) },
        { FileSizeUnit.Exa, Math.Pow(1000, 6) },
        { FileSizeUnit.Zetta, Math.Pow(1000, 7) },
        { FileSizeUnit.Yotta, Math.Pow(1000, 8) },
        { FileSizeUnit.Ronna, Math.Pow(1000, 9) },
        { FileSizeUnit.Quetta, Math.Pow(1000, 10) }
    };

    #region Public Methods

    #region ConvertUnit
    /// <summary>
    /// Converts a file size value from one unit to another.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="fromUnit"></param>
    /// <param name="toUnit"></param>
    /// <param name="useBinaryUnits"></param>
    /// <returns></returns>
    public static double ConvertUnits(double value, FileSizeUnit fromUnit, FileSizeUnit toUnit,
        bool useBinaryUnits = true)
    {
        if (fromUnit == toUnit)
            return value;

        return useBinaryUnits
            ? value *(BinaryMultipliers[fromUnit] / BinaryMultipliers[toUnit])
            : value * (DecimalMultipliers[fromUnit] / DecimalMultipliers[toUnit]);
    }
    #endregion

    #region ToString
    /// <summary>
    /// Converts a file size value to a string with the specified unit and number of decimal places.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="unit"></param>
    /// <param name="decimalPlaces"></param>
    /// <returns></returns>
    public static string ToString(double value, FileSizeUnit unit, int decimalPlaces = 2) =>
        $"{Math.Round(value, decimalPlaces)} {Units[unit]}";
    #endregion

    #endregion
}
