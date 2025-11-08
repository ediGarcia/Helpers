namespace HelperMethods.Enums;

/// <summary>
/// Gets the directory action when trying to copy/move over an already existing file/folder.
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
/// Indicates whether a URL is absolute, relative or invalid.
/// </summary>
public enum UrlType
{
    Absolute,
    Relative,
    Invalid
}