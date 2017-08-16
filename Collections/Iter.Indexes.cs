using System;
//using System.Collections.Generic;

namespace AZCL.Collections // Only supporting zero-bound arrays!
{
    public static partial class Iter
    {
        internal struct IndexesEnumerable //:IEnumerable<int[]>
        {
            private readonly Array array;

            public IndexesEnumerable(Array array)
            {
                AZAssert.NotNullInternal(array, nameof(array));
                this.array = array;
            }

            public Array Array => array;

            public IndexesIterator GetEnumerator()
                => new IndexesIterator(array);
        }

        internal struct IndexesReverseEnumerable //:IEnumerable<int[]>
        {
            private readonly Array array;

            public IndexesReverseEnumerable(Array array)
            {
                AZAssert.NotNullInternal(array, nameof(array));
                this.array = array;
            }

            public Array Array => array;

            public IndexesReverseIterator GetEnumerator()
                => new IndexesReverseIterator(array);
        }

        internal struct IndexesIterator //: IEnumerator<int[]>
        {
            private int[] indexes;
            private readonly int[] lengths;
            
            internal IndexesIterator(Array array)
            {
                AZAssert.NotNullInternal(array, nameof(array));
                
                indexes = null;
                lengths = array.Length == 0 ? null : ArrayKit.GetLengths_Internal(array);
            }

            // ! Value is UNDEFINED before the first move, after a reset, as well as after a move has failed !
            // (It can be either a valid index, or null, or an empty array!)
            public int[] Current
                => indexes;

            // ! Once any Move, Increment, or Decrement method has failed, Current becomes UNDEFINED !
            // !  ALL subsequent calls to those methods will continue to fail until Reset is called  !
            public bool MoveNext()
            {
                if (indexes == null)
                {
                    if (lengths == null)
                        return false;

                    indexes = new int[lengths.Length];
                    return true;
                }

                return TryIncrement();
            }

            // ! Once any Move, Increment, or Decrement method has failed, Current becomes UNDEFINED !
            // !  ALL subsequent calls to those methods will continue to fail until Reset is called  !
            public bool MovePrev()
            {
                if (indexes == null)
                {
                    if (lengths == null)
                        return false;

                    indexes = CreateReverseIndexes(lengths);
                    return true;
                }

                return TryDecrement();
            }

            //     - MoveTo is like a combined Reset and Move that tries to move to a specified index -
            // ! A failed MoveTo is the same as any other failed Move ---> Current property becomes UNDEFINED !
            public bool MoveTo(int index)
            {
                if (lengths == null)
                    return false;

                if (indexes == null)
                    indexes = new int[lengths.Length];

                if (indexes.Length == 0)
                    return false;

                if (index == 0)
                    return true;

                for (int dim = indexes.Length - 1; dim >= 0; --dim)
                {
                    int len = lengths[dim]; // we know 'len' is non-zero (see cctor + null check above)
                    int tmp = index / len;
                    indexes[dim] = index - tmp * len;
                    if (tmp == 0)
                        return true;
                    else
                        index = tmp;
                }
                
                indexes = Empty<int>.Array; // <-- Ensures calling again will continue to return false !
                return false;
                /* (The overflow would be the product of all lengths times the remaining value in index) */
            }

            // Calculates the one-dimensional enumeration index for the current indexes.
            // Returns -1 if the current indexes are invalid/undefined.
            public int GetIndex()
            {
                if (indexes == null || indexes.Length == 0)
                    return -1;

                int sum = 0, factor = 1;
                for (int dim = indexes.Length - 1; dim >= 0; --dim)
                {
                    sum += indexes[dim] * factor;
                    factor *= lengths[dim];
                }

                return sum;
            }

            // true if there are no valid indexes at all (i.e. the source had zero elements or this iterator was default initialized)
            public bool IsEmpty
                => lengths == null;

            // ! Once any Move, Increment, or Decrement method has failed, Current becomes UNDEFINED !
            // !  ALL subsequent calls to those methods will continue to fail until Reset is called  !
            internal bool TryIncrement()
            {
                for (int i = 0; i < indexes.Length; ++i)
                {
                    if (++indexes[i] < lengths[i]) return true;
                    else indexes[i] = 0; // <-- Once we go past the last index, all indexes will be reset to zero ! (effectively like an overflow - making the increment circular)
                }

                indexes = Empty<int>.Array; // <-- Ensures calling again will continue to return false !
                return false;
            }

            // ! Once any Move, Increment, or Decrement method has failed, Current becomes UNDEFINED !
            // !  ALL subsequent calls to those methods will continue to fail until Reset is called  !
            internal bool TryDecrement()
            {
                for (int i = 0; i < indexes.Length; ++i)
                {
                    if (--indexes[i] >= 0) return true;
                    else indexes[i] = lengths[i] - 1; // <-- Once we go past the first index, all indexes will be reset to upper bound ! (effectively like an overflow - making the decrement circular)
                }
                
                indexes = Empty<int>.Array; // <-- Ensures calling again will continue to return false !
                return false;
            }

            internal void Reset()
            {
                indexes = null;
            }

            private static int[] CreateReverseIndexes(int[] lengths)
            {
                AZAssert.NotNullInternal(lengths, nameof(lengths));

                var indexes = new int[lengths.Length];
                for (int i = 0; i < indexes.Length; ++i)
                    indexes[i] = lengths[i] - 1;
                return indexes;
            }
        }

        // TODO: port over improvements from the non-reversed iterator (above) (...also for more code-comments: see above)
        internal struct IndexesReverseIterator //: IEnumerator<int[]>
        {
            private int[] indexes;
            private readonly int[] lengths;

            internal IndexesReverseIterator(Array array)
            {
                AZAssert.NotNullInternal(array, nameof(array));

                indexes = null;
                lengths = array.Length == 0 ? null : ArrayKit.GetLengths_Internal(array);
            }

            public int[] Current
                => indexes;

            /// <summary>
            /// Moves backwards! (Decrements)
            /// </summary>
            public bool MoveNext()
            {
                if (indexes == null)
                {
                    if (lengths == null)
                        return false;

                    indexes = CreateReverseIndexes(lengths);
                    return true;
                }

                return TryDecrement();
            }

            /// <summary>
            /// Moves forwards! (Increments)
            /// </summary>
            public bool MovePrev()
            {
                if (indexes == null)
                {
                    if (lengths == null)
                        return false;

                    indexes = new int[lengths.Length];
                    return true;
                }

                return TryIncrement();
            }

            public bool IsEmpty
                => lengths == null;

            internal bool TryIncrement()
            {
                for (int i = 0; i < indexes.Length; ++i)
                {
                    if (++indexes[i] < lengths[i]) return true;
                    else indexes[i] = 0; // <-- Once we go past the last index, all indexes will be zero ! (effectively like an overflow - making the increment circular)
                }

                indexes = Empty<int>.Array; // <-- Ensures calling again will continue to return false !
                return false;
            }

            internal bool TryDecrement()
            {
                for (int i = 0; i < indexes.Length; ++i)
                {
                    if (--indexes[i] >= 0) return true;
                    else indexes[i] = lengths[i] - 1; // <-- Once we go past the first index, all indexes will be their respective upper bound ! (effectively like an overflow - making the decrement circular)
                }

                indexes = Empty<int>.Array; // <-- Ensures calling again will continue to return false !
                return false;
            }

            internal void Reset()
            {
                indexes = null;
            }

            private static int[] CreateReverseIndexes(int[] lengths)
            {
                AZAssert.NotNullInternal(lengths, nameof(lengths));

                var indexes = new int[lengths.Length];
                for (int i = 0; i < indexes.Length; ++i)
                    indexes[i] = lengths[i] - 1;
                return indexes;
            }
        }
    }
}
