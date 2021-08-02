using System;

namespace AnagramSolver.BusinessLogic.Exceptions
{
    [Serializable]
    public class WrongCommandException : Exception
    {
        public WrongCommandException()
        {

        }

        public WrongCommandException(string command)
            : base($"Invalid Command: {command}")
        {

        }

        public WrongCommandException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
