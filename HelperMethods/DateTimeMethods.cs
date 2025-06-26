using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using HelperMethods.Classes;
using HelperMethods.Enums;

// ReSharper disable UnusedMember.Global

namespace HelperMethods;

/// <summary>
/// Date and time management methods.
/// </summary>
public static class DateTimeMethods
{
    #region Public Methods

    #region CheckHolilday*

    #region CheckHoliday(DateTime, BrazilianStates, string)
    /// <summary>
    /// Checks online if selected date is a holiday.
    /// </summary>
    /// <param name="date"></param>
    /// <param name="state"></param>
    /// <param name="city"></param>
    /// <returns></returns>
    /// <exception cref="WebException" />
    public static bool CheckHoliday(DateTime date, BrazilianState state, string city) =>
        CheckHoliday(RetrieveHolidays(date.Year, state, city), date);
    #endregion

    #region CheckHoliday(IEnumerable<CommemorativeDay>, DateTime, [bool])
    /// <summary>
    /// Checks whether the date is a holiday.
    /// </summary>
    /// <param name="dates"></param>
    /// <param name="date"></param>
    /// <param name="getOptional">Indicates whether optional holidays should be considered too.</param>
    /// <returns></returns>
    public static bool CheckHoliday(IEnumerable<CommemorativeDay> dates, DateTime date, bool getOptional = false) =>
        CheckHoliday(dates.ToList(), date, getOptional);
    #endregion

    #region CheckHoliday(List<CommemorativeDay>, DateTime, [bool])
    /// <summary>
    /// Checks whether the date is a holiday.
    /// </summary>
    /// <param name="dates"></param>
    /// <param name="day"></param>
    /// <param name="getOptional">Indicates whether optional holidays should be considered too.</param>
    /// <returns></returns>
    public static bool CheckHoliday(List<CommemorativeDay> dates, DateTime day, bool getOptional = false)
    {
        int index = dates.BinarySearch(new() { Date = day });
        return index >= 0 && dates[index].Type != CommemorativeDayType.RegularDay && (getOptional || dates[index].Type != CommemorativeDayType.Optional);
    }
    #endregion

    #endregion

    #region GetFirstDayOfMonth*

    #region GetFirstDayOfMonth(DateTime)
    /// <summary>
    /// Gets the selected date month's first day.
    /// </summary>
    /// <param name="baseDate"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
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

    #endregion

    #region GetFirstDayOfWeek*

    #region GetFirstDayOfWeek(DateTime)
    /// <summary>
    /// Gets the first day of the selected day week.
    /// </summary>
    /// <param name="baseDay"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
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

    #endregion

    #region GetHolidaysInRange
    /// <summary>
    /// Retrieve the holidays in a range.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="state"></param>
    /// <param name="city"></param>
    /// <param name="getOptional">Indicates whether optional holidays should be retrieved too.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException" />
    /// <exception cref="WebException" />
    // ReSharper disable once UnusedMember.Global
    public static IList<CommemorativeDay> GetHolidaysInRange(DateTime start, DateTime end, BrazilianState state, string city, bool getOptional = false)
    {
        if (end < start)
            throw new ArgumentException("The end date should be after the start date.");

        List<CommemorativeDay> holidays = null;
        List<CommemorativeDay> holidaysInRange = [];
        int currentYear = 0;

        while (start <= end)
        {
            if (currentYear != start.Year)
            {
                holidays = RetrieveHolidays(start.Year, state, city, getOptional).ToList();
                currentYear = start.Year;
            }

            int index = holidays.BinarySearch(new() { Date = start });

            if (index >= 0)
                holidaysInRange.Add(holidays[index]);

            start = start.AddDays(1);
        }

        return holidaysInRange;
    }
    #endregion

    #region GetLastDayOfMonth*

    #region GetLastDayOfMonth(DateTime)
    /// <summary>
    /// Gets the selected date month's last day.
    /// </summary>
    /// <param name="baseDay"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
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

    #endregion

    #region GetLastDayOfWeek*

    #region GetLastDayOfWeek(DateTime)
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
    // ReSharper disable once UnusedMember.Global
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
    public static string GetTimeString(DateTime dateTime) =>
        GetTimeString(dateTime.TimeOfDay);
    #endregion

    #region GetTimeString(TimeSpan)
    /// <summary>
    /// Gets the time string in format HHH:mm:ss.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public static string GetTimeString(TimeSpan timeSpan) =>
        GetTimeString(timeSpan.TotalSeconds);
    #endregion

    #region GetTimeString(double)
    /// <summary>
    /// Gets the time string in format HHH:mm:ss.
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static string GetTimeString(double seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        return $@"{(timeSpan < TimeSpan.Zero ? "-" : "")}{Math.Floor(Math.Abs(timeSpan.TotalHours)):00}:{timeSpan:\:mm\:ss}";
    }
    #endregion

    #region GetTimeString(double time, TimeField timeField)
    /// <summary>
    /// Gets the TimeSpan string in format HHH:mm:ss.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="timeField"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static string GetTimeString(double time, TimeField timeField) =>
        timeField switch
        {
            TimeField.Day => GetTimeString(TimeSpan.FromDays(time)),
            TimeField.Hour => GetTimeString(TimeSpan.FromHours(time)),
            TimeField.Minute => GetTimeString(TimeSpan.FromMinutes(time)),
            TimeField.Second => GetTimeString(TimeSpan.FromSeconds(time)),
            TimeField.Millisecond => GetTimeString(TimeSpan.FromMilliseconds(time)),
            TimeField.Ticks => GetTimeString(TimeSpan.FromTicks((long)time)),
            _ => throw new ArgumentOutOfRangeException(time.ToString(CultureInfo.InvariantCulture))
        };
    #endregion

    #endregion

    #region Max*

    #region Max(params DateTime[])
    /// <summary>
    /// Returns the largest DateTime value.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static DateTime Max(params DateTime[] values) =>
        values is null || values.Length == 0 ? DateTime.MaxValue : values.Max();
    #endregion

    #region Max(params TimeSpan[])
    /// <summary>
    /// Returns the largest TimeSpan value.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
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
    // ReSharper disable once UnusedMember.Global
    public static DateTime Min(params DateTime[] values) =>
        values is null || values.Length == 0 ? DateTime.MinValue : values.Min();
    #endregion

    #region Min(TimeSpan, TimeSpan)
    /// <summary>
    /// Returns the smallest TimeSpan value.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
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
    public static DateTime Parse(string value, IFormatProvider provider = null, DateTimeStyles styles = DateTimeStyles.None) =>
        DateTime.Parse(value, provider ?? CultureInfo.CurrentCulture, styles);
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
    public static DateTime ParseExact(string value, string format, IFormatProvider provider = null, DateTimeStyles styles = DateTimeStyles.None) =>
        DateTime.ParseExact(value, format, provider ?? CultureInfo.CurrentCulture, styles);
    #endregion

    #region RemoveSeconds*

    #region RemoveSeconds(DateTime)
    /// <summary>
    /// Returns a new DateTime without the seconds, milliseconds and ticks.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static DateTime RemoveSeconds(DateTime dateTime) =>
        new(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
    #endregion

    #region RemoveSeconds(TimeSpan)
    /// <summary>
    /// Returns a new TimeSpan without the seconds, milliseconds and ticks.
    /// </summary>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
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
    // ReSharper disable once UnusedMember.Global
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
    // ReSharper disable once UnusedMember.Global
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

    #region RetrieveCommemorativeDays
    /// <summary>
    /// Retrieves the commemorative days for the selected year in the selected state.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="state"></param>
    /// <param name="city"></param>
    /// <returns></returns>
    /// <exception cref="WebException" />
    public static IEnumerable<CommemorativeDay> RetrieveCommemorativeDays(int year, BrazilianState state, string city)
    {
        List<CommemorativeDay> days = [];
        XmlDocument holidayXml = new();

        //Retrieves data from the API.
        using (WebClient web = new())
        {
            web.Encoding = Encoding.UTF8;
            holidayXml.LoadXml(web.DownloadString($"http://www.calendario.com.br/api/api_feriados.php?ano={year}&estado={GetState(state)}&cidade={StringMethods.RemoveAccentuation(city).Replace("'", "").Replace(" ", "_")}&token=ZWRkeS5nYXJjaWEwN0BnbWFpbC5jb20maGFzaD00MTU2MjUxNw=="));
        }

        if (holidayXml["events"] != null)
            days.AddRange(from XmlNode node in holidayXml["events"]?.ChildNodes
                where node.Name != "location"
                select new CommemorativeDay
                {
                    Date = DateTime.ParseExact(node["date"].FirstChild.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Name = node["name"].FirstChild.Value,
                    Description = node["description"].FirstChild?.Value,
                    Type = (CommemorativeDayType)Int32.Parse(node["type_code"].FirstChild.Value),
                    Link = node["link"].FirstChild?.Value
                });

        days.Sort();
        return days;
    }
    #endregion

    #region RetrieveHolidays
    /// <summary>
    /// Retrieves the holidays for the selected year in the selected state.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="state"></param>
    /// <param name="city"></param>
    /// <param name="getOptional">Indicates whether optional holidays should be retrieved too.</param>
    /// <returns></returns>
    public static IEnumerable<CommemorativeDay> RetrieveHolidays(int year, BrazilianState state, string city, bool getOptional = false) =>
        RetrieveCommemorativeDays(year, state, city).Where(_ => _.Type != CommemorativeDayType.RegularDay && (getOptional || _.Type != CommemorativeDayType.Optional));
    #endregion

    #region TryCheckHoliday
    /// <summary>
    /// Attempts to check if the selected date is a holiday.
    /// </summary>
    /// <param name="selectedDate"></param>
    /// <param name="state"></param>
    /// <param name="city"></param>
    /// <param name="isHoliday"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static bool TryCheckHoliday(DateTime selectedDate, BrazilianState state, string city, out bool isHoliday)
    {
        try
        {
            isHoliday = CheckHoliday(selectedDate, state, city);
            return true;
        }
        catch
        {
            isHoliday = false;
            return false;
        }
    }
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
    public static bool TryParse(string value, out DateTime result, IFormatProvider provider = null, DateTimeStyles styles = DateTimeStyles.None) =>
        DateTime.TryParse(value, provider ?? CultureInfo.CurrentCulture, styles, out result);
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
    public static bool TryParseExact(string value, string format, out DateTime result, IFormatProvider provider = null, DateTimeStyles styles = DateTimeStyles.None) =>
        DateTime.TryParseExact(value, format, provider ?? CultureInfo.CurrentCulture, styles, out result);
    #endregion

    #endregion

    #region Private Methods

    #region GetState
    private static string GetState(BrazilianState state) =>
        state switch
        {
            BrazilianState.Acre => "ac",
            BrazilianState.Alagoas => "al",
            BrazilianState.Amapa => "ap",
            BrazilianState.Amazonas => "am",
            BrazilianState.Bahia => "ba",
            BrazilianState.Ceara => "ce",
            BrazilianState.DistritoFederal => "df",
            BrazilianState.EspiritoSanto => "es",
            BrazilianState.Goias => "go",
            BrazilianState.Maranhao => "ma",
            BrazilianState.MatoGrosso => "mt",
            BrazilianState.MatoGrossoDoSul => "ms",
            BrazilianState.MinasGerais => "mg",
            BrazilianState.Para => "pa",
            BrazilianState.Paraiba => "pb",
            BrazilianState.Parana => "pr",
            BrazilianState.Pernambuco => "pe",
            BrazilianState.Piaui => "pi",
            BrazilianState.RioDeJaneiro => "rj",
            BrazilianState.RioGrandeDoNorte => "rn",
            BrazilianState.RioGrandeDoSul => "rs",
            BrazilianState.Rondonia => "rd",
            BrazilianState.Roraima => "rr",
            BrazilianState.SantaCatarina => "sc",
            BrazilianState.SaoPaulo => "sp",
            BrazilianState.Sergipe => "se",
            BrazilianState.Tocantins => "to",
            _ => "sp"
        };
    #endregion

    #endregion
}