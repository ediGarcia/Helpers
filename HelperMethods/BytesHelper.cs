using HelperMethods.Enums;

namespace HelperMethods;

public static class BytesHelper
{
    /// <summary>
    /// Gets the string representation of each file size unit.
    /// </summary>
    public static readonly IReadOnlyDictionary<FileSizeUnit, string> Units = new Dictionary<
        FileSizeUnit,
        string
    >
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
        { FileSizeUnit.Quetta, "QB" },
    };

    private const int MaxUnitIndex = (int)FileSizeUnit.Quetta;

    #region Public Methods

    #region ToBestUnitString
    /// <summary>
    /// Converts the given byte value to the best fitting file size unit and returns it as a formatted string.
    /// </summary>
    /// <param name="bytes">The number of bytes to convert.</param>
    /// <param name="decimalPlaces">Number of decimal places in the output (0-15).</param>
    /// <param name="useBinaryUnits">If true, uses 1024-based units; otherwise uses 1000-based units.</param>
    /// <returns>A formatted string representing the file size with appropriate unit.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when decimalPlaces is negative or exceeds 15.</exception>
    public static string ToBestUnitString(
        long bytes,
        int decimalPlaces = 0,
        bool useBinaryUnits = true
    )
    {
        if (decimalPlaces is < 0 or > 15)
            throw new ArgumentOutOfRangeException(
                nameof(decimalPlaces),
                "Decimal places must be between 0 and 15"
            );

        bool isNegative = bytes < 0;
        if (isNegative)
            bytes = -bytes;

        if (bytes == 0)
            return "0 B";

        double baseValue = useBinaryUnits ? 1024d : 1000d;

        int unitIndex = Math.Min(
            (int)Math.Floor(Math.Log(bytes) / Math.Log(baseValue)),
            MaxUnitIndex
        );

        FileSizeUnit bestUnit = (FileSizeUnit)unitIndex;

        double calculatedValue = bytes / Math.Pow(baseValue, unitIndex);

        return $"{(isNegative ? "-" : "")}{Math.Round(calculatedValue, decimalPlaces)} {Units[bestUnit]}";
    }
    #endregion

    #region ConvertUnit
    /// <summary>
    /// Converts a file size value from one unit to another.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="fromUnit">The source unit.</param>
    /// <param name="toUnit">The target unit.</param>
    /// <param name="useBinaryUnits">If true, uses 1024-based units; otherwise uses 1000-based units.</param>
    /// <returns>The converted value in the target unit.</returns>
    /// <exception cref="ArgumentException">Thrown when enum values are invalid.</exception>
    public static double ConvertUnits(
        double value,
        FileSizeUnit fromUnit,
        FileSizeUnit toUnit,
        bool useBinaryUnits = true
    )
    {
        if (!Enum.IsDefined(typeof(FileSizeUnit), fromUnit))
            throw new ArgumentException("Invalid source unit.", nameof(fromUnit));

        if (!Enum.IsDefined(typeof(FileSizeUnit), toUnit))
            throw new ArgumentException("Invalid target unit.", nameof(toUnit));

        if (fromUnit == toUnit)
            return value;

        double baseValue = useBinaryUnits ? 1024d : 1000d;
        int exponentDiff = (int)fromUnit - (int)toUnit;

        return value * Math.Pow(baseValue, exponentDiff);
    }
    #endregion

    #region ToString
    /// <summary>
    /// Converts a file size value to a string with the specified unit and number of decimal places.
    /// </summary>
    /// <param name="value">The numeric value.</param>
    /// <param name="unit">The file size unit.</param>
    /// <param name="decimalPlaces">Number of decimal places (0-15).</param>
    /// <returns>A formatted string with the value and unit.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when decimalPlaces is invalid.</exception>
    /// <exception cref="ArgumentException">Thrown when unit is invalid.</exception>
    public static string ToString(double value, FileSizeUnit unit, int decimalPlaces = 2)
    {
        if (decimalPlaces is < 0 or > 15)
            throw new ArgumentOutOfRangeException(
                nameof(decimalPlaces),
                "Decimal places must be between 0 and 15"
            );

        if (!Enum.IsDefined(typeof(FileSizeUnit), unit))
            throw new ArgumentException("Invalid unit", nameof(unit));

        return $"{Math.Round(value, decimalPlaces)} {Units[unit]}";
    }
    #endregion

    #endregion
}
