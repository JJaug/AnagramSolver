using System;
using System.ComponentModel;

namespace GenericsTask
{
    public class GenericsTask<T> where T : struct
    {
        public T MapValueToEnum(string value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (!Enum.TryParse<T>(value, out T result))
            {
                throw new Exception($"Value '{value}' is not part of Gender enum");
            }

            return result;
        }

    }
}
