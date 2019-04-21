using System.Collections.Generic;

namespace Common
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Yield<T>(this T value)
        {
            yield return value;
        }
    }
}
