namespace HelperMethods;

public static class PathHelper
{
    #region Public Methods

    #region CheckExtension
    /// <summary>
    /// Indicates whether the selected file has one of the selected extensions.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="extensions"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global
    public static bool CheckExtension(string fileName, params string[] extensions) =>
        extensions.Any(extension =>
            String.Equals(
                Path.GetExtension(fileName),
                InsertExtensionDot(extension),
                StringComparison.OrdinalIgnoreCase
            )
        );
    #endregion

    #region Combine
    /// <summary>
    /// Combines multiple path strings into a single path.
    /// </summary>
    /// <param name="paths"></param>
    /// <returns></returns>
    public static string Combine(params string[] paths) => Path.Combine(paths);
    #endregion

    #region GetAllMatchingPaths
    /// <summary>
    /// Returns the list of paths discovered from the selected pattern.
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetAllMatchingPaths(string pattern)
    {
        //If the pattern contains no wildcards.
        if (!pattern.Contains("*") && !pattern.Contains("?"))
            return SystemHelper.Exists(pattern) ? [pattern] : [];

        string separator = Path.DirectorySeparatorChar.ToString();
        string[] parts = pattern.Split(Path.DirectorySeparatorChar);

        //There must be an existing root path.
        if (parts[0].Contains('*') || parts[0].Contains('?'))
            throw new ArgumentException("The root path must not have a wildcard", nameof(parts));

        for (int i = 1; i < parts.Length; i++)
            //If this part of the path is a wildcard that needs expanding.
            if (parts[i].Contains('*') || parts[i].Contains('?'))
            {
                //Create an absolute path up to the current wildcard and check if it exists.
                string combined = Path.Combine(parts[0], String.Join(separator, parts.Take(i)));
                if (!Directory.Exists(combined))
                    return [];

                if (i == parts.Length - 1) //If this is the end of the path (a file name).
                    return Directory.GetFileSystemEntries(
                        combined,
                        parts[i],
                        SearchOption.TopDirectoryOnly
                    );

                IEnumerable<string> directories = Directory.EnumerateDirectories(
                    combined,
                    parts[i],
                    SearchOption.TopDirectoryOnly
                );
                return directories.SelectMany(dir =>
                    GetAllMatchingPaths(
                        Path.Combine(dir, String.Join(separator, parts.Skip(i + 1)))
                    )
                );
            }

        return [];
    }
    #endregion

    #region GetExtension
    /// <summary>
    /// Returns the extension of the specified path string.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetExtension(string path) => Path.GetExtension(path);
    #endregion

    #region GetFileName
    /// <summary>
    /// Returns the file name of the specified path.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="removeExtension"></param>
    /// <returns></returns>
    public static string GetFileName(string path, bool removeExtension = false) =>
        removeExtension ? Path.GetFileNameWithoutExtension(path) : Path.GetFileName(path);
    #endregion

    #region GetFullPath
    /// <summary>
    /// Returns the absolute path for the specified path string.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetFullPath(string path) => Path.GetFullPath(path);
    #endregion

    #region GetParentDirectory
    /// <summary>
    /// Returns the directory information for the specified path string.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetParentDirectory(string path) => Path.GetDirectoryName(path);
    #endregion

    #region GetTempPath
    /// <summary>
    /// Retrieves the path of the temporary folder for the current user.
    /// </summary>
    /// <returns></returns>
    public static string GetTempPath() => DirectoryHelper.TemporaryDirectory;
    #endregion

    #region GetTempFileName
    /// <summary>
    /// Generates a unique temporary file name.
    /// </summary>
    /// <returns></returns>
    public static string GetTempFileName() => Path.GetTempFileName();
    #endregion

    #region GetUniqueRandomPath
    /// <summary>
    /// Generates a unique random path inside the selected parent folder.
    /// </summary>
    /// <param name="parentFolder"></param>
    /// <param name="extension"></param>
    /// <returns></returns>
    public static string GetUniqueRandomPath(string parentFolder, string extension = null)
    {
        if (extension is not null && !extension.StartsWith('.'))
            extension = '.' + extension;

        string newPath;

        do newPath = Path.Combine(parentFolder, Guid.NewGuid().ToString()) + extension;
        while (SystemHelper.Exists(newPath));

        return newPath;
    }
    #endregion

    #region GetUniquePath
    /// <summary>
    /// Generates a unique path by appending an index to the file name if the selected path already exists.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="indexPrefix"></param>
    /// <param name="indexSuffix"></param>
    /// <returns></returns>
    public static string GetUniquePath(
        string path,
        string indexPrefix = " (",
        string indexSuffix = ")"
    )
    {
        if (!SystemHelper.Exists(path))
            return path;

        string parentPath = DirectoryHelper.GetParentDirectory(path);
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
        string fileExtension = Path.GetExtension(path);
        string newPath;
        int index = 1;

        do newPath =
            $"{Path.Combine(parentPath, fileNameWithoutExtension)}{indexPrefix}{index++}{indexSuffix}{fileExtension}";
        while (SystemHelper.Exists(newPath));

        return newPath;
    }
    #endregion

    #region GetRoot
    /// <summary>
    /// Retrieves the root directory information of the specified path string.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetRoot(string path) => Path.GetPathRoot(path);
    #endregion

    #region InsertExtensionDot
    /// <summary>
    /// Inserts a dot (.) in the begging of the extension if necessary.
    /// </summary>
    /// <param name="extension"></param>
    /// <returns></returns>
    public static string InsertExtensionDot(string extension)
    {
        if (!extension.StartsWith("."))
            extension = "." + extension;

        return extension;
    }
    #endregion

    #endregion
}
