using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AZCL.Collections
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class LinqExtensions // TODO: make public when deemed ready.
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
    }
}
