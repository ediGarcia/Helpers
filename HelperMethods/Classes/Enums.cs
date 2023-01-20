using System;

namespace HelperMethods.Classes;

/// <summary>
/// Character classes.
/// </summary>
[Flags]
public enum CharacterClass
{
    Space = 1,
    Special = 2,
    UpperCase = 4,
    LowerCase = 8,
    Number = 16,
    LineBreak = 32,
    All = 64
}

/// <summary>
/// Types of celebrations.
/// </summary>
public enum CommemorativeDayType
{
    // ReSharper disable once UnusedMember.Global
    NationalHoliday = 1,
    // ReSharper disable once UnusedMember.Global
    StateHoliday = 2,
    // ReSharper disable once UnusedMember.Global
    CityHoliday = 3,
    Optional = 4,
    RegularDay = 9
}

/// <summary>
/// File system conflict action.
/// </summary>
public enum ConflictAction
{
    AppendNumber,
    AppendText,
    Break,
    Ignore,
    Overwrite
}

/// <summary>
/// File size units.
/// </summary>
public enum FileSizeUnit
{
    Byte,
    Kilo,
    Mega,
    // ReSharper disable once IdentifierTypo
    Giga,
    // ReSharper disable once IdentifierTypo
    Tera,
    // ReSharper disable once IdentifierTypo
    Peta,
    Exa,
    // ReSharper disable once IdentifierTypo
    Zetta,
    // ReSharper disable once IdentifierTypo
    Yotta
}

/// <summary>
/// File suffix types.
/// </summary>
public enum FileSuffixType
{
    Copy,
    Numeric
}

/// <summary>
/// Time field.
/// </summary>
public enum TimeField
{
    Day,
    Hour,
    Minute,
    Second,
    Millisecond,
    Ticks
}

/// <summary>
/// Indicates whether a URL is absolute, relative or invalid.
/// </summary>
public enum UrlType
{
    Absolute,
    Relative,
    Invalid
}