using System;

namespace AnagramSolver.BusinessLogic.Exceptions
{
    [Serializable]
    public class DataBaseCouldNotBeFilledException : Exception
    {
        public DataBaseCouldNotBeFilledException()
        {

        }

        public DataBaseCouldNotBeFilledException(string message)
            : base(message)
        {

        }

        public DataBaseCouldNotBeFilledException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
