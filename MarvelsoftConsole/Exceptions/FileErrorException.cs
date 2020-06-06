using System;

namespace MarvelsoftConsole.Exceptions
{
    [Serializable]
    public class FileErrorException : Exception
    {
        public FileErrorException() { }

        public FileErrorException(string message)
            : base(message) { }
        public FileErrorException(string message, Exception inner) : base(message, inner) { }
    }
}
