using System;

namespace HelperMethods.Enums;

/// <summary>
/// Brazilian states.
/// </summary>
public enum BrazilianState
{
    // ReSharper disable IdentifierTypo
    Acre,
    Alagoas,
    Amapa,
    Amazonas,
    Bahia,
    Ceara,
    DistritoFederal,
    EspiritoSanto,
    Goias,
    Maranhao,
    MatoGrosso,
    MatoGrossoDoSul,
    MinasGerais,
    Para,
    Paraiba,
    Parana,
    Pernambuco,
    Piaui,
    RioDeJaneiro,
    RioGrandeDoNorte,
    RioGrandeDoSul,
    Rondonia,
    Roraima,
    SantaCatarina,
    SaoPaulo,
    Sergipe,
    Tocantins
    // ReSharper restore IdentifierTypo
}

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
/// Gets the directory action when trying to copy/move over an already existing file.
/// </summary>
public enum FileNameConflictAction
{
    ThrowError,
    Overwrite,
    Skip
}

/// <summary>
/// File size units.
/// </summary>
public enum FileSizeUnit
{
    // ReSharper disable IdentifierTypo
    Byte,
    Kilo,
    Mega,
    Giga,
    Tera,
    Peta,
    Exa,
    Zetta,
    Yotta,
    Ronna,
    Quetta
    // ReSharper restore IdentifierTypo
}

/// <summary>
/// The action to be taking regarding the inner directories.
/// </summary>
public enum InnerDirectoryAction
{
    Clear,
    Delete,
    Ignore
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