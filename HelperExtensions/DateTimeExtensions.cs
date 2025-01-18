using HelperMethods;
using System;

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
        dateTime.DayOfWeek is not DayOfWeek.Saturday and not DayOfWeek.Sunday;
    #endregion

    #region IsWeekend
    /// <summary>
    /// Returns true if the current date is a weekend.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static bool IsWeekend(this DateTime dateTime) =>
        dateTime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
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

    #region AddDays
    /// <summary>
    /// Returns a new <see cref="TimeSpan"/> that adds the specified number of days to the value of this instance.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TimeSpan AddDays(this TimeSpan time, double value) =>
        time.Add(TimeSpan.FromDays(value));
    #endregion

    #region AddHours
    /// <summary>
    /// Returns a new <see cref="TimeSpan"/> that adds the specified number of hours to the value of this instance.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TimeSpan AddHours(this TimeSpan time, double value) =>
        time.Add(TimeSpan.FromHours(value));
    #endregion

    #region AddMilliseconds
    /// <summary>
    /// Returns a new <see cref="TimeSpan"/> that adds the specified number of milliseconds to the value of this instance.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TimeSpan AddMilliseconds(this TimeSpan time, double value) =>
        time.Add(TimeSpan.FromMilliseconds(value));
    #endregion

    #region AddMinutes
    /// <summary>
    /// Returns a new <see cref="TimeSpan"/> that adds the specified number of minutes to the value of this instance.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TimeSpan AddMinutes(this TimeSpan time, double value) =>
        time.Add(TimeSpan.FromMinutes(value));
    #endregion

    #region AddSeconds
    /// <summary>
    /// Returns a new <see cref="TimeSpan"/> that adds the specified number of seconds to the value of this instance.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TimeSpan AddSeconds(this TimeSpan time, double value) =>
        time.Add(TimeSpan.FromSeconds(value));
    #endregion

    #region AddTicks
    /// <summary>
    /// Returns a new <see cref="TimeSpan"/> that adds the specified number of ticks to the value of this instance.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TimeSpan AddTicks(this TimeSpan time, long value) =>
        time.Add(TimeSpan.FromTicks(value));
    #endregion

    #region RoundToDays
    /// <summary>
    /// Returns a new <see cref="TimeSpan"/> that rounds this instance value's days.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public static TimeSpan RoundToDays(this TimeSpan timeSpan) =>
        TimeSpan.FromDays(Math.Round(timeSpan.TotalDays));
    #endregion

    #region RoundToHours
    /// <summary>
    /// Returns a new <see cref="TimeSpan"/> that rounds this instance value's hours.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public static TimeSpan RoundToHours(this TimeSpan timeSpan) =>
        TimeSpan.FromHours(Math.Round(timeSpan.TotalHours));
    #endregion

    #region RoundToMilliseconds
    /// <summary>
    /// Returns a new <see cref="TimeSpan"/> that rounds this instance value's milliseconds.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public static TimeSpan RoundToMilliseconds(this TimeSpan timeSpan) =>
        TimeSpan.FromMilliseconds(Math.Round(timeSpan.TotalMilliseconds));
    #endregion
    
    #region RoundToMinutes
    /// <summary>
    /// Returns a new <see cref="TimeSpan"/> that rounds this instance value's minutes.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public static TimeSpan RoundToMinutes(this TimeSpan timeSpan) =>
        TimeSpan.FromMinutes(Math.Round(timeSpan.TotalMinutes));
    #endregion

    #region RoundToSeconds
    /// <summary>
    /// Returns a new <see cref="TimeSpan"/> that rounds this instance value's seconds.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public static TimeSpan RoundToSeconds(this TimeSpan timeSpan) =>
        TimeSpan.FromSeconds(Math.Round(timeSpan.TotalSeconds));
    #endregion

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
        $@"{(time < TimeSpan.Zero ? "-" : "")}{Math.Floor(Math.Abs(time.TotalHours)):00}:{time:\:mm\:ss}";
    #endregion

    #endregion

    #endregion
}