using System;
using HelperMethods;

namespace HelperExtensions;

// ReSharper disable once UnusedMember.Global
public static class DateTimeExtensions
{
    #region Public Methods

    #region DateTime

    #region IsWeekday
    /// <summary>
    /// Returns true if the current date is a weekday.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static bool IsWeekday(this DateTime dateTime) =>
        dateTime.DayOfWeek != DayOfWeek.Saturday && dateTime.DayOfWeek != DayOfWeek.Sunday;
    #endregion

    #region IsWeekend
    /// <summary>
    /// Returns true if the current date is a weekend.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static bool IsWeekend(this DateTime dateTime) =>
        dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
    #endregion

    #region ToBrazilianShortDateString
    /// <summary>
    /// Returns a date string in format "dd/MM/yyyy".
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static string ToBrazilianShortDateString(this DateTime dateTime) =>
        dateTime.ToString("dd/MM/yyyy");
    #endregion

    #region ToShortTimeString
    /// <summary>
    /// Returns a string in the format HH:mm.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static string ToShortTimeString(this DateTime dateTime) =>
        dateTime.TimeOfDay.ToShortTimeString();
    #endregion

    #region ToTimeString
    /// <summary>
    /// Gets the DateTime string in format HHH:mm:ss.
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static string ToTimeString(this DateTime time) =>
        time.TimeOfDay.ToTimeString();
    #endregion

    #endregion

    #region TimeSpan

    #region ToShortTimeString
    /// <summary>
    /// Returns a string in the format HH:mm.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public static string ToShortTimeString(this TimeSpan timeSpan) =>
        DateTimeMethods.GetShortTimeString(timeSpan.TotalSeconds);
    #endregion

    #region ToTimeString
    /// <summary>
    /// Gets the TimeSpan string in format HHH:mm:ss.
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string ToTimeString(this TimeSpan time) =>
        $"{(time < TimeSpan.Zero ? "-" : "")}{Math.Floor(Math.Abs(time.TotalHours)):00}:{time:\\:mm\\:ss}";
    #endregion

    #endregion

    #endregion
}