using System;
using System.Collections.Generic;

namespace AZCL.Collections
{
    public partial struct ReadOnlyArray<T> : IEquatable<ReadOnlyArray<T>>, IEquatable<T[]>, IEnumerable<T>
    {
        /// <summary>
        /// A simple standard struct implementation of an IEnumerator&lt;T&gt; for arrays.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private readonly T[] source;
            private T current;
            private int index, length;

            /// <summary>
            /// Creates an enumerator for the specified array.
            /// </summary>
            public Enumerator(T[] array)
            {
                this.source = array;
                this.length = array == null ? 0 : array.Length;
                this.index = 0;
                this.current = default(T);
            }

            /// <inheritdoc/>
            public T Current
            {
                get { return current; }
            }

            void IDisposable.Dispose()
            { }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    if (unchecked((uint)(index - 1) >= (uint)length))
                        throw new InvalidOperationException("Enumerators current position is before the first or past the last element in the collection.");
                    return current;
                }
            }

            /// <inheritdoc/>
            public bool MoveNext()
            {
                if (index >= length)
                {
                    current = default(T);
                    index = length + 1;
                    return false;
                }
                current = source[index];
                index += 1;
                return true;
            }

            /// <inheritdoc/>
            public void Reset()
            {
                index = 0;
                current = default(T);
            }

            /// <summary>
            /// Creates an <see cref="ReadOnlyArrayEnumerator{T}"/> from a <see cref="ReadOnlyArray{T}.Enumerator"/>.
            /// </summary><remarks>
            /// This cast retains the current position of the <paramref name="enumerator"/> argument (such as it is at cast time)!
            /// </remarks>
            public static implicit operator ReadOnlyArrayEnumerator<T>(Enumerator enumerator)
            {
                var arr = enumerator.source;
                return arr == null ? default(ReadOnlyArrayEnumerator<T>) : new ReadOnlyArrayEnumerator<T>(arr, 0, arr.Length - 1, enumerator.index - 1);
            }
        }
    }
}
