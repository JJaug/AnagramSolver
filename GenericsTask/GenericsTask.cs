using System;

namespace GenericsTask
{
    public class GenericsTask<T> where T : struct
    {
        public T MapValueToEnum(string value)
        {
            if (!Enum.TryParse(value, out T result))
            {
                throw new Exception($"Value '{value}' is not part of Gender enum");
            }

            return result;
        }

    }
}
