using System.Globalization;
using HelperClasses.Classes.Month;

namespace HelperMethods;

/// <summary>
/// Date and time management methods.
/// </summary>
public static class DateTimeHelper
{
    #region Public Methods

    #region GetFirstDayOfMonth*

    #region GetFirstDayOfMonth(DateTime)
    /// <summary>
    /// Gets the selected date month's first day.
    /// </summary>
    /// <param name="baseDate"></param>
    /// <returns></returns>
    public static DateTime GetFirstDayOfMonth(DateTime baseDate) =>
        GetFirstDayOfMonth(baseDate.Month, baseDate.Year);
    #endregion

    #region GetFirstDayOfMonth(int, [int])
    /// <summary>
    /// Gets the selected month's last day.
    /// </summary>
    /// <param name="month">The desired month, expressed as a number between 1 and 12.</param>
    /// <param name="year"></param>
    /// <returns></returns>
    public static DateTime GetFirstDayOfMonth(int? month = null, int? year = null) =>
        new(year ?? DateTime.Today.Year, month ?? DateTime.Today.Month, 1);
    #endregion

    #region GetFirstDayOfMonth(MonthYear)
    /// <summary>
    /// Gets the selected month's last day.
    /// </summary>
    /// <returns></returns>
    public static DateTime GetFirstDayOfMonth(MonthYear month) => month.ToDateTime();
    #endregion

    #endregion

    #region GetFirstDayOfWeek
    /// <summary>
    /// Gets the first day of the selected day week.
    /// </summary>
    /// <param name="baseDay"></param>
    /// <returns></returns>
    public static DateTime GetFirstDayOfWeek(DateTime? baseDay = null)
    {
        baseDay ??= DateTime.Today;

        try
        {
            return baseDay.Value.AddDays(-(int)baseDay.Value.DayOfWeek);
        }
        catch (ArgumentOutOfRangeException)
        {
            return DateTime.MinValue;
        }
    }
    #endregion

    #region GetLastDayOfMonth*

    #region GetLastDayOfMonth(DateTime)
    /// <summary>
    /// Gets the selected date month's last day.
    /// </summary>
    /// <param name="baseDay"></param>
    /// <returns></returns>
    public static DateTime GetLastDayOfMonth(DateTime baseDay) =>
        GetLastDayOfMonth(baseDay.Month, baseDay.Year);
    #endregion

    #region GetLastDayOfMonth(int, [int])
    /// <summary>
    /// Gets the selected month's last day for the selected year.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month">The desired month, expressed as a number between 1 and 12.</param>
    /// <returns></returns>
    public static DateTime GetLastDayOfMonth(int? month = null, int? year = null)
    {
        month ??= DateTime.Today.Month;
        year ??= DateTime.Today.Year;

        return month == 12 && year == 9999
            ? DateTime.MaxValue
            : GetFirstDayOfMonth(month, year).AddMonths(1).AddDays(-1);
    }
    #endregion

    #region GetLastDayOfMonth(MonthYear)
    /// <summary>
    /// Gets the selected month's last day for the selected year.
    /// </summary>
    /// <returns></returns>
    public static DateTime GetLastDayOfMonth(MonthYear month) =>
        month.IsSameMonth(DateTime.MaxValue)
            ? DateTime.MaxValue
            : GetFirstDayOfMonth(month).AddMonths(1).AddDays(-1);
    #endregion

    #endregion

    #region GetLastDayOfWeek
    /// <summary>
    /// Gets the last day of the selected day week.
    /// </summary>
    /// <param name="baseDay"></param>
    /// <returns></returns>
    public static DateTime GetLastDayOfWeek(DateTime? baseDay = null)
    {
        baseDay ??= DateTime.Today;

        try
        {
            return baseDay.Value.AddDays(6 - (int)baseDay.Value.DayOfWeek);
        }
        catch (ArgumentOutOfRangeException)
        {
            return DateTime.MaxValue;
        }
    }
    #endregion

    #region GetShortTimeString*

    #region GetShortTimeString(DateTime)
    /// <summary>
    /// Gets the time string in format HHH:mm.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string GetShortTimeString(DateTime dateTime) =>
        GetShortTimeString(dateTime.TimeOfDay);
    #endregion

    #region GetShortTimeString(TimeSpan)
    /// <summary>
    /// Gets the time string in format HHH:mm.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public static string GetShortTimeString(TimeSpan timeSpan) =>
        GetShortTimeString(timeSpan.TotalSeconds);
    #endregion

    #region GetShortTimeString(double)
    /// <summary>
    /// Gets the time string in format HHH:mm.
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static string GetShortTimeString(double seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        return $"{(timeSpan < TimeSpan.Zero ? "-" : "")}{Math.Floor(Math.Abs(timeSpan.TotalHours)):00}:{Math.Abs(timeSpan.Minutes):00}";
    }
    #endregion

    #endregion

    #region GetTimeString*

    #region GetTimeString(DateTime)
    /// <summary>
    /// Gets the time string in format HHH:mm:ss.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string GetTimeString(DateTime dateTime) => GetTimeString(dateTime.TimeOfDay);
    #endregion

    #region GetTimeString(TimeSpan)
    /// <summary>
    /// Gets the time string in format HHH:mm:ss.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public static string GetTimeString(TimeSpan timeSpan) =>
        $@"{(timeSpan < TimeSpan.Zero ? "-" : "")}{Math.Floor(Math.Abs(timeSpan.TotalHours)):00}:{timeSpan:\:mm\:ss}";
    #endregion

    #endregion

    #region IsSameDate
    /// <summary>
    /// Indicates whether all provided dates are the same date (ignoring time).
    /// </summary>
    /// <param name="dateTimes"></param>
    /// <returns></returns>
    public static bool IsSameDate(params DateTime[] dateTimes)
    {
        if (dateTimes.Length == 0)
            return false;

        DateTime first = dateTimes[0];
        return dateTimes.All(d => d.Date == first.Date);
    }
    #endregion

    #region IsSameMonth
    /// <summary>
    /// Indicates whether all provided dates are in the same month and year.
    /// </summary>
    /// <param name="dateTimes"></param>
    /// <returns></returns>
    public static bool IsSameMonth(params DateTime[] dateTimes)
    {
        if (dateTimes.Length == 0)
            return false;

        DateTime firstDateTime = dateTimes[0];
        return dateTimes.All(_ => _.Month == firstDateTime.Month && _.Year == firstDateTime.Year);
    }
    #endregion

    #region Max*

    #region Max(params DateTime[])
    /// <summary>
    /// Returns the largest DateTime value.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static DateTime Max(params DateTime[] values) =>
        values is null || values.Length == 0 ? DateTime.MaxValue : values.Max();
    #endregion

    #region Max(params TimeSpan[])
    /// <summary>
    /// Returns the largest TimeSpan value.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static TimeSpan Max(params TimeSpan[] values) =>
        values is null || values.Length == 0 ? TimeSpan.MaxValue : values.Max();
    #endregion

    #endregion

    #region Min*

    #region Min(params DateTime[])
    /// <summary>
    /// Returns the smallest DateTime value.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static DateTime Min(params DateTime[] values) =>
        values is null || values.Length == 0 ? DateTime.MinValue : values.Min();
    #endregion

    #region Min(TimeSpan, TimeSpan)
    /// <summary>
    /// Returns the smallest TimeSpan value.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static TimeSpan Min(params TimeSpan[] values) =>
        values is null || values.Length == 0 ? TimeSpan.MinValue : values.Min();
    #endregion

    #endregion

    #region Parse
    /// <summary>
    /// Converts the string representation of a date and time to its <see cref="DateTime"/> equivalent by using culture-specific format information and formatting style.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="provider"></param>
    /// <param name="styles"></param>
    /// <returns></returns>
    public static DateTime Parse(
        string value,
        IFormatProvider provider = null,
        DateTimeStyles styles = DateTimeStyles.None
    ) => DateTime.Parse(value, provider ?? CultureInfo.CurrentCulture, styles);
    #endregion

    #region ParseExact
    /// <summary>
    /// Converts the string representation of a date and time to its <see cref="DateTime"/> equivalent by using culture-specific format information and formatting style.
    /// The format of the string representation must match the specified format exactly or an exception is thrown.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <param name="styles"></param>
    /// <returns></returns>
    public static DateTime ParseExact(
        string value,
        string format,
        IFormatProvider provider = null,
        DateTimeStyles styles = DateTimeStyles.None
    ) => DateTime.ParseExact(value, format, provider ?? CultureInfo.CurrentCulture, styles);
    #endregion

    #region RemoveMilliseconds*

    #region RemoveMilliseconds(DateTime)
    /// <summary>
    /// Returns a new DateTime without the milliseconds and ticks.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime RemoveMilliseconds(DateTime dateTime) =>
        new(
            dateTime.Year,
            dateTime.Month,
            dateTime.Day,
            dateTime.Hour,
            dateTime.Minute,
            dateTime.Second
        );
    #endregion

    #region RemoveMilliseconds(TimeSpan)
    /// <summary>
    /// Returns a new TimeSpan without the milliseconds and ticks.
    /// </summary>
    /// <returns></returns>
    public static TimeSpan RemoveMilliseconds(TimeSpan timeSpan) =>
        new(timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    #endregion

    #endregion

    #region RemoveSeconds*

    #region RemoveSeconds(DateTime)
    /// <summary>
    /// Returns a new DateTime without the seconds, milliseconds and ticks.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime RemoveSeconds(DateTime dateTime) =>
        new(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
    #endregion

    #region RemoveSeconds(TimeSpan)
    /// <summary>
    /// Returns a new TimeSpan without the seconds, milliseconds and ticks.
    /// </summary>
    /// <returns></returns>
    public static TimeSpan RemoveSeconds(TimeSpan timeSpan) =>
        new(timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, 0);
    #endregion

    #endregion

    #region RoundToMinutes*

    #region RoundToMinutes(DateTime)
    /// <summary>
    /// Returns a new DateTime with the time rounded to the nearest minute.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime RoundToMinutes(DateTime dateTime) =>
        dateTime.Date.Add(RoundToMinutes(dateTime.TimeOfDay));
    #endregion

    #region RoundToMinutes(TimeSpan)
    /// <summary>
    /// Returns a new TimeSpan with the time rounded to the nearest minute.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public static TimeSpan RoundToMinutes(TimeSpan timeSpan) =>
        TimeSpan.FromMinutes(Math.Round(timeSpan.TotalMinutes));
    #endregion

    #endregion

    #region RoundToSeconds*

    #region RoundToSeconds(DateTime)
    /// <summary>
    /// Returns a new DateTime with the time rounded to the nearest second.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime RoundToSeconds(DateTime dateTime) =>
        dateTime.Date.Add(RoundToSeconds(dateTime.TimeOfDay));
    #endregion

    #region RoundToSeconds(TimeSpan)
    /// <summary>
    /// Returns a new TimeSpan with the time rounded to the nearest second.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public static TimeSpan RoundToSeconds(TimeSpan timeSpan) =>
        TimeSpan.FromSeconds(Math.Round(timeSpan.TotalSeconds));
    #endregion

    #endregion

    #region TryParse
    /// <summary>
    /// Converts the string representation of a date and time to its <see cref="DateTime"/> equivalent by using culture-specific format information and formatting style,
    /// and returns a value indicating whether the conversion succeeded.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <param name="provider"></param>
    /// <param name="styles"></param>
    /// <returns></returns>
    public static bool TryParse(
        string value,
        out DateTime result,
        IFormatProvider provider = null,
        DateTimeStyles styles = DateTimeStyles.None
    ) => DateTime.TryParse(value, provider ?? CultureInfo.CurrentCulture, styles, out result);
    #endregion

    #region TryParseExact
    /// <summary>
    /// Converts the string representation of a date and time to its <see cref="DateTime"/> equivalent by using culture-specific format information and formatting style.
    /// The format of the string representation must match the specified format exactly or an exception is thrown.
    /// The method returns a value indicating whether the conversion succeeded.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="format"></param>
    /// <param name="result"></param>
    /// <param name="provider"></param>
    /// <param name="styles"></param>
    /// <returns></returns>
    public static bool TryParseExact(
        string value,
        string format,
        out DateTime result,
        IFormatProvider provider = null,
        DateTimeStyles styles = DateTimeStyles.None
    ) =>
        DateTime.TryParseExact(
            value,
            format,
            provider ?? CultureInfo.CurrentCulture,
            styles,
            out result
        );
    #endregion

    #endregion
}
