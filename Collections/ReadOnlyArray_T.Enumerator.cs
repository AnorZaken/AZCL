using System;
using System.Collections.Generic;

namespace AZCL.Collections
{
    public partial struct ReadOnlyArray<T> : IEquatable<ReadOnlyArray<T>>, IEquatable<Array>, IEnumerable<T>
    {
        /// <summary>
        /// A simple standard struct implementation of an IEnumerator&lt;<typeparamref name="T"/>&gt; for arrays.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private int index, length;
            private readonly T[] source;
            private T current;

            /// <summary>
            /// Creates an <see cref="Enumerator"/> for the specified array.
            /// </summary>
            public Enumerator(T[] array)
            {
                this.source = array;
                this.length = array == null ? 0 : array.Length;
                this.index = 0;
                this.current = default(T);
            }

            /// <summary>
            /// Creates an <see cref="Enumerator"/> for the array wrapped in the <see cref="ReadOnlyArray{T}"/> argument.
            /// </summary>
            public Enumerator(ReadOnlyArray<T> array) : this(array.Array)
            { }

            // startIndex must non-negative, but there is no upper bound though!
            internal Enumerator(T[] array, int startIndex) : this(array)
            {
                AZAssert.GEQZeroInternal(startIndex, nameof(startIndex));
                index = startIndex;
            }

            /// <inheritdoc/>
            public T Current
            {
                get { return current; }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    if (unchecked((uint)(index - 1) >= (uint)length))
                        throw new InvalidOperationException(ERR.CURRENT_INVALID);
                    return current;
                }
            }

            void IDisposable.Dispose()
            { }

            /// <inheritdoc/>
            public bool MoveNext()
            {
                if (index >= length)
                {
                    current = default(T);
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
            /// Creates an <see cref="ArrayEnumeratorReadOnly{T}"/> from a <see cref="ReadOnlyArray{T}.Enumerator"/>.
            /// </summary><remarks>
            /// This cast retains the current position of the <paramref name="enumerator"/> argument (such as it is at cast time)!
            /// </remarks>
            public static implicit operator ArrayEnumeratorReadOnly<T>(Enumerator enumerator)
            {
                var arr = enumerator.source;
                return arr == null ? default(ArrayEnumeratorReadOnly<T>) : new ArrayEnumeratorReadOnly<T>(arr, 0, arr.Length - 1, enumerator.index - 1);
            }
        }
    }
}
