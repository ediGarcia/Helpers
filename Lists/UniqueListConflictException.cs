using System;

namespace Lists
{
    public class ConflictException : Exception
    {
        public ConflictException() { }

        public ConflictException(string message) : base(message) { }
    }
}
