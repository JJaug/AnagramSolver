using System;

namespace GenericsTask
{
    public class GenericsTask<T> where T : struct
    {
        public T MapValueToEnum(string value)
        {
            Type type = typeof(T);
            if (!Enum.TryParse<T>(value, out T result))
            {
                throw new Exception($"Value '{value}' is not part of {type.Name} enum");
            }

            return result;
        }
    }
}
