using System;
using HelperMethods.Enums;

namespace HelperMethods.Classes;

public class CommemorativeDay : IComparable<CommemorativeDay>
{
    #region Properties

    /// <summary>
    /// Current date.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Commemorative day description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// More information about the date.
    /// </summary>
    public string Link { get; set; }

    /// <summary>
    /// Commemorative day name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Commemorative day type.
    /// </summary>
    public CommemorativeDayType Type { get; set; }

    #endregion

    #region Methods

    #region CompareTo
    public int CompareTo(CommemorativeDay other) =>
        Date < other.Date ? -1 : Date == other.Date ? 0 : 1;
    #endregion

    #region ToString
    public override string ToString() =>
        $"{Date.ToShortDateString()} - {Name}";
    #endregion

    #endregion
}