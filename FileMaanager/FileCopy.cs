using HelperMethods;
using System.IO;

namespace FileManager
{
    public class FileCopy
    {
        #region Properties

        /// <summary>
        /// Destination path.
        /// </summary>
        public string Destination { get; }

        /// <summary>
        /// Indicates whether the source files should be kept.
        /// </summary>
        public bool KeepSourceFiles { get; set; } = true;

        /// <summary>
        /// Indicates whether any already existing files should be overwritten.
        /// </summary>
        public bool Overwrite { get; set; }

        /// <summary>
        /// Source path.
        /// </summary>
        public string Source { get; }

        #endregion

        public FileCopy(string source, string destination)
        {
            Destination = destination;
            Source = source;
        }

        public void Copy()
        {
            if (!File.Exists(Source) && !Directory.Exists(Source))
                throw new IOException($"The file/directory \"{Source}\" does not exist.");

            if (!Overwrite && SystemMethods.IsDirectory(Source) && Directory.Exists(Source))
                throw new IOException($"The directory \"{Source}\" already exists.");
        }
    }
}
