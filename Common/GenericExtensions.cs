using System.Collections.Generic;

namespace System.Linq
{
    public static class GenericExtensions
    {
        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> items)
        {
            if (items == null)
                return true;

            return items.Any() == false;
        }
    }
}
