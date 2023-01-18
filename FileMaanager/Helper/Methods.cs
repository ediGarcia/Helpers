using HelperMethods;

namespace FileManager.Helper
{
    public static class Methods
    {
        #region GetFileSize
        /// <summary>
        /// Retrieves the file/folder size in bytes.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static long GetFileSize(string path) =>
            BytesMethods.GetFileSize(path);
        #endregion
    }
}
