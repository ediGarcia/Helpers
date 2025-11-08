using System.Globalization;
using System.Text;

namespace HelperClasses.Classes.Month;

public readonly struct MonthYear : IComparable, IComparable<MonthYear>, IEquatable<MonthYear>, IFormattable
{
    #region Properties

    /// <summary>
    /// Gets the number of days in the month.
    /// </summary>
    public int DaysCount { get; }

    /// <summary>
    /// Gets a value indicating whether the year is a leap year.
    /// </summary>
    public bool IsLeapYear { get; }

    /// <summary>
    /// Gets the month component (1-12).
    /// </summary>
    public int Month { get; }

    /// <summary>
    /// Gets the year component.
    /// </summary>
    public int Year { get; }

    #endregion

    #region Static Properties

    public static MonthYear Today => new(DateTime.Today.Month, DateTime.Today.Year);

    #endregion

    public MonthYear(int month, int year)
    {
        if (month is < 1 or > 12)
            throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");

        DaysCount = DateTime.DaysInMonth(year, month);
        IsLeapYear = DateTime.IsLeapYear(year);
        Month = month;
        Year = year;
    }

    public MonthYear(int month) : this(month, DateTime.Today.Year)
    {
    }

    public MonthYear() : this(DateTime.Today.Month, DateTime.Today.Year)
    {
    }

    #region Public Methods

    #region AddMonths
    /// <summary>
    /// Returns a new <see cref="MonthYear"/> instance with the specified number of months added.
    /// </summary>
    /// <param name="months"></param>
    /// <returns></returns>
    public MonthYear AddMonths(int months)
    {
        int newMonth = Month + months;
        int newYear = Year;

        if (newMonth < 1)
        {
            newMonth = Math.Abs(newMonth);

            newYear = Year - newMonth / 12 - 1;
            newMonth = 12 - newMonth % 12;
        }
        else if (newMonth > 12)
        {
            newYear = Year + (newMonth - 1) / 12;
            newMonth = (newMonth - 1) % 12 + 1;
        }

        return new(newMonth, newYear);
    }
    #endregion

    #region AddYears
    /// <summary>
    /// Returns a new <see cref="MonthYear"/> instance with the specified number of years added.
    /// </summary>
    /// <param name="years"></param>
    /// <returns></returns>
    public MonthYear AddYears(int years) =>
        new(Month, Year + years);
    #endregion

    #region CompareTo*

    #region CompareTo(MonthYear)
    /// <inheritdoc />
    public int CompareTo(MonthYear other)
    {
        int yearComparison = Year.CompareTo(other.Year);
        return yearComparison != 0 ? yearComparison : Month.CompareTo(other.Month);
    }
    #endregion

    #region CompareTo(object?)
    /// <inheritdoc />
    public int CompareTo(object? obj) =>
        obj switch
        {
            MonthYear month => CompareTo(month),
            DateTime dateTime => CompareTo(FromDateTime(dateTime)),
            _ => throw new ArgumentException($"Object is not a {nameof(MonthYear)}", nameof(obj))
        };

    #endregion

    #endregion

    #region Equals*

    #region Equals(MonthYear)
    /// <inheritdoc />
    public bool Equals(MonthYear other) =>
        CompareTo(other) == 0;
    #endregion

    #region Equals(object?)
    /// <inheritdoc />
    public override bool Equals(object? obj) =>
        obj switch
        {
            MonthYear month => CompareTo(month) == 0,
            DateTime dateTime => CompareTo(dateTime) == 0,
            _ => false
        };
    #endregion

    #endregion

    #region GetHashCode
    /// <inheritdoc />
    public override int GetHashCode() =>
        HashCode.Combine(Month, Year);
    #endregion

    #region IsSameMonth
    /// <summary>
    /// Determines whether the specified <see cref="DateTime"/> falls within the same month and year as the current instance.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public bool IsSameMonth(DateTime dateTime) =>
        Month == dateTime.Month && Year == dateTime.Year;
    #endregion

    #region ToDateTime
    /// <summary>
    /// Returns a <see cref="DateTime"/> representation of the <see cref="MonthYear"/> instance.
    /// </summary>
    /// <param name="day"></param>
    /// <returns></returns>
    public DateTime ToDateTime(int day = 1) =>
        new(Year, Month, day);
    #endregion

    #region ToString*

    #region ToString()
    /// <inheritdoc />
    public override string ToString() =>
        $"{Month:D2}/{Year}";
    #endregion

    #region ToString(string?, [IFormatProvider?])
    public string ToString(string? format, IFormatProvider? provider = null)
    {
        if (String.IsNullOrEmpty(format))
            return ToString();

        StringBuilder result = new();
        string token = String.Empty;
        bool isMonth = false;
        bool isYear = false;

        foreach (char c in format)
        {
            switch (c)
            {
                case 'm':
                case 'M':
                {
                    if (!isMonth || token.Length == 4)
                    {
                        result.Append(ConvertToken(token, provider));
                        token = String.Empty;
                    }

                    isMonth = true;
                    isYear = false;
                    break;
                }

                case 'y':
                case 'Y':
                {
                    if (!isYear || token.Length == 4)
                    {
                        result.Append(ConvertToken(token, provider));
                        token = String.Empty;
                    }

                    isMonth = false;
                    isYear = true;
                    break;
                }

                default:
                {
                    if (isMonth || isYear)
                    {
                        result.Append(ConvertToken(token, provider));
                        token = String.Empty;
                    }

                    isMonth = false;
                    isYear = false;
                    break;
                }
            }

            token += c;
        }

        return result.Append(ConvertToken(token, provider)).ToString();
    }
    #endregion

    #region ToString(MonthFormat, YearFormat, string [separator], [IFormatProvider?])
    /// <summary>
    /// Returns a string representation of the <see cref="MonthYear"/> instance based on specified month and year formats.
    /// </summary>
    /// <param name="monthFormat"></param>
    /// <param name="yearFormat"></param>
    /// <param name="separator"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public string ToString(MonthFormat monthFormat, YearFormat yearFormat, string separator = "/", IFormatProvider? provider = null) =>
        ConvertMonthToString(monthFormat, provider) + separator + ConvertYearToString(yearFormat);
    #endregion

    #endregion

    #endregion

    #region Public Static Methods

    #region ==
    /// <summary>
    /// Returns a value indicating whether two <see cref="MonthYear"/> instances are equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(MonthYear left, MonthYear right) =>
        left.CompareTo(right) == 0;
    #endregion

    #region !=
    /// <summary>
    /// Returns a value indicating whether two <see cref="MonthYear"/> instances are not equal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(MonthYear left, MonthYear right) =>
        left.CompareTo(right) != 0;
    #endregion

    #region <*

    #region <(MonthYear, MonthYear)
    /// <summary>
    /// Determines whether one <see cref="MonthYear"/> instance is less than another.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator <(MonthYear left, MonthYear right) =>
        left.CompareTo(right) < 0;
    #endregion

    #region <(MonthYear, DateTime)
    /// <summary>
    /// Determines whether one <see cref="MonthYear"/> represents a previous month when compared to a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator <(MonthYear left, DateTime right) =>
        left.CompareTo(right) < 0;
    #endregion

    #endregion

    #region >*

    #region >(MonthYear, MonthYear)
    /// <summary>
    /// Determines whether one <see cref="MonthYear"/> instance is greater than another.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator >(MonthYear left, MonthYear right) =>
        left.CompareTo(right) > 0;
    #endregion

    #region >(MonthYear, DateTime)
    /// <summary>
    /// Determines whether one <see cref="MonthYear"/> represents a later month when compared to a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator >(MonthYear left, DateTime right) =>
        left.CompareTo(right) > 0;
    #endregion

    #endregion

    #region <=*

    #region <=(MonthYear, MonthYear)
    /// <summary>
    /// Determines whether one <see cref="MonthYear"/> instance is less than or equal to another.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator <=(MonthYear left, MonthYear right) =>
        left.CompareTo(right) <= 0;
    #endregion

    #region <=(MonthYear, DateTime)
    /// <summary>
    /// Determines whether one <see cref="MonthYear"/> represents a month less than or equal to a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator <=(MonthYear left, DateTime right) =>
        left.CompareTo(right) <= 0;
    #endregion

    #endregion

    #region >=*

    #region >=(MonthYear, MonthYear)
    /// <summary>
    /// Determines whether one <see cref="MonthYear"/> instance is greater than or equal to another.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator >=(MonthYear left, MonthYear right) =>
        left.CompareTo(right) >= 0;
    #endregion

    #region >=(MonthYear, DateTime)
    /// <summary>
    /// Determines whether one <see cref="MonthYear"/> represents a month greater than or equal to a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator >=(MonthYear left, DateTime right) =>
        left.CompareTo(right) >= 0;
    #endregion

    #endregion

    #region -
    /// <summary>
    /// Subtracts one <see cref="MonthYear"/> instance from another and returns the difference as a <see
    /// cref="TimeSpan"/>.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns>A <see cref="TimeSpan"/> representing the difference between the two <see cref="MonthYear"/> instances. The
    /// result is positive if <paramref name="left"/> is later than <paramref name="right"/>; otherwise, it is negative.</returns>
    public static TimeSpan operator -(MonthYear left, MonthYear right) =>
        left.ToDateTime() - right.ToDateTime();
    #endregion

    #region FromDateTime
    /// <summary>
    /// Returns a <see cref="MonthYear"/> instance from a given <see cref="DateTime"/>.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static MonthYear FromDateTime(DateTime dateTime) =>
        new(dateTime.Month, dateTime.Year);
    #endregion

    #endregion

    #region Private Methods

    #region ConvertMonthToString
    /// <summary>
    /// Returns the month component as a string based on the specified <see cref="MonthFormat"/>.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private string ConvertMonthToString(MonthFormat format, IFormatProvider? provider)
    {
        DateTimeFormatInfo formatInfo = DateTimeFormatInfo.GetInstance(provider);

        return format switch
        {
            MonthFormat.Number => Month.ToString(provider),
            MonthFormat.LeadingZeroNumber => Month.ToString("D2", provider),
            MonthFormat.ShortName => formatInfo.GetAbbreviatedMonthName(Month),
            MonthFormat.FullName => formatInfo.GetMonthName(Month),
            _ => throw new ArgumentOutOfRangeException(nameof(format), "Invalid month format.")
        };
    }
    #endregion

    #region ConvertToken
    /// <summary>
    /// Returns the string representation of a format token.
    /// </summary>
    /// <param name="token"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    private string ConvertToken(string token, IFormatProvider? provider) =>
        token.ToUpper() switch
        {
            "MMMM" => ConvertMonthToString(MonthFormat.FullName, provider),
            "MMM" => ConvertMonthToString(MonthFormat.ShortName, provider),
            "MM" => ConvertMonthToString(MonthFormat.LeadingZeroNumber, provider),
            "M" => ConvertMonthToString(MonthFormat.Number, provider),
            "YYYY" => ConvertYearToString(YearFormat.Full),
            "YYY" => ConvertYearToString(YearFormat.TwoDigit) + "Y",
            "YY" => ConvertYearToString(YearFormat.TwoDigit),
            _ => token
        };
    #endregion

    #region ConvertYearToString
    /// <summary>
    /// Returns the year component as a string based on the specified <see cref="YearFormat"/>.
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private string ConvertYearToString(YearFormat format) =>
        format switch
        {
            YearFormat.TwoDigit => (Year % 100).ToString("D2"),
            YearFormat.Full => Year.ToString("0000"),
            _ => throw new ArgumentOutOfRangeException(nameof(format), "Invalid year format.")
        };
    #endregion

    #endregion
}
