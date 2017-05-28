using System;
using System.Collections.Generic;

namespace AZCL.Collections
{
    /* C# Language Specification - Version 4.0 paragraph 8.8.4
     * ------------------------------------------------------------------------------
     * The order in which foreach traverses the elements of an array, is as follows:
     * For single-dimensional arrays elements are traversed in increasing index order,
     * starting with index 0 and ending with index Length – 1. For multi-dimensional
     * arrays, elements are traversed such that the indices of the rightmost dimension
     * are increased first, then the next left dimension, and so on to the left.
     */

    public partial struct ArrayR3<T> : IEquatable<ArrayR3<T>>, IEquatable<Array>, IEnumerable<T>//, ICollection<T> <-- TODO: for better Linq performance
    {
        /// <summary>
        /// A standard struct implementation of an IEnumerator&lt;<typeparamref name="T"/>&gt; for rank 3 arrays.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private int x, y, z, lenx, leny, lenz;
            private readonly T[,,] source;
            private T current;

            /// <summary>
            /// Creates an <see cref="Enumerator"/> from an array.
            /// </summary>
            /// <exception cref="ArgumentNullException">
            /// Thrown if <paramref name="array"/> is null.
            /// </exception>
            public static implicit operator Enumerator(T[,,] array)
            {
                return new Enumerator(array);
            }

            /* wip
            /// <summary>
            /// Creates an <see cref="Enumerator"/> from a <see cref="ReadOnlyArrayR3{T}"/>.
            /// </summary><remarks>
            /// This operator does not throw, even if the <see cref="ReadOnlyArrayR3{T}"/> argument was default initialized.
            /// </remarks>
            public static implicit operator Enumerator(ReadOnlyArrayR3<T> array)
            {
                return new Enumerator(array);
            }
            */

            /// <summary>
            /// Creates an <see cref="Enumerator"/> for the specified rank 3 array.
            /// </summary>
            public Enumerator(T[,,] array)
            {
                this.source = array;
                if (array == null || array.Length == 0) // <-- The Length check is important! (Because "... = new T[42,0,0];" is legal.)
                {
                    this.lenx = 0;
                    this.leny = 0;
                    this.lenz = 0;
                }
                else
                {
                    this.lenx = array.GetLength(0);
                    this.leny = array.GetLength(1);
                    this.lenz = array.GetLength(2);
                }
                this.x = 0;
                this.y = 0;
                this.z = 0;
                this.current = default(T);
            }

            /* wip
            /// <summary>
            /// Creates an <see cref="Enumerator"/> for the rank 2 array wrapped in the <see cref="ReadOnlyArrayR3{T}"/> argument.
            /// </summary>
            public Enumerator(ReadOnlyArrayR3<T> array) : this(array.Array)
            { }
            */

            // throws if startIndex is negative! (but there is no upper bound though!)
            internal Enumerator(T[,,] array, int startIndex) : this(array)
            {
                if (startIndex < 0)
                    throw new Exception(ERR.AZCL_INTERNAL_ERROR);

                ArrayHelper.CalculateIndexesUnbound(array, startIndex, out x, out y, out z);
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
                    if (unchecked((uint)(x - 1) >= (uint)lenx))
                        throw new InvalidOperationException(ERR.CURRENT_INVALID);
                    return current;
                }
            }

            void IDisposable.Dispose()
            { }

            /// <inheritdoc/>
            public bool MoveNext()
            {
                if (x >= lenx)
                {
                    current = default(T);
                    return false;
                }

                current = source[x, y, z];

                if (++z >= lenz)
                {
                    z = 0;
                    if (++y >= leny)
                    {
                        y = 0;
                        ++x;
                    }
                }

                return true;
            }

            /// <inheritdoc/>
            public void Reset()
            {
                x = 0;
                y = 0;
                z = 0;
                current = default(T);
            }
        }
    }
}
