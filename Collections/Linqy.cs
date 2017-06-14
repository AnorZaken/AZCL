using System;
using System.Collections.Generic;
using System.Linq;

namespace AZCL.Collections
{
    internal static class Linqy // TODO: make public when deemed ready.
    {
        public static IEnumerable<int> FindIndexes<TEnumerable, TElement>(this TEnumerable enumerable, Func<TElement, bool> predicate) where TEnumerable : IEnumerable<TElement>
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            int i = 0;
            foreach (var T in enumerable)
            {
                if (predicate(T))
                    yield return i;
                ++i;
            }
        }

        public static IEnumerable<T> Skip<T>(this ArrayR2<T> array, int count)
        {
            var enumerator = new ArrayR2<T>.Enumerator(array.Array, count < 0 ? 0 : count);
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

        public static IEnumerable<T> Skip<T>(this ReadOnlyArrayR2<T> array, int count)
        {
            var enumerator = new ArrayR2<T>.Enumerator(array.Array, count < 0 ? 0 : count);
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

        public static IEnumerable<T> Skip<T>(this ArrayR3<T> array, int count)
        {
            var enumerator = new ArrayR3<T>.Enumerator(array.Array, count < 0 ? 0 : count);
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

        public static IEnumerable<T> Skip<T>(this ReadOnlyArrayR3<T> array, int count)
        {
            var enumerator = new ArrayR3<T>.Enumerator(array.Array, count < 0 ? 0 : count);
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }
    }
}
