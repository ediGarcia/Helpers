using System;

namespace HelperControls.Classes
{
    public class FileErrorEventArgs : EventArgs
    {
        public Exception Exception { get; }

        public FileErrorEventArgs(Exception exception) =>
            Exception = exception;
    }
}