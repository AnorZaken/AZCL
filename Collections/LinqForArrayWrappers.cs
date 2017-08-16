using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AZCL.Collections
{
    /// <summary>
    /// Provides array wrapper optimized versions of certain Linq extensions. (See Remarks for a complete list.) 
    /// </summary><remarks>
    /// Optimized extensions are provided for all AZCL array wrappers:
    /// <list type="nobullet">
    ///     <item><see cref="Array2{T}"/></item>
    ///     <item><see cref="Array3{T}"/></item>
    ///     <item><see cref="ReadOnlyArray{T}"/></item>
    ///     <item><see cref="ReadOnlyArray2{T}"/></item>
    ///     <item><see cref="ReadOnlyArray3{T}"/></item>
    /// </list>
    /// The following extensions are provided:
    /// <list type="nobullet">
    ///     <item><see cref="O:AZCL.Collections.LinqForArrayWrappers.Cast">Cast</see>*</item>
    ///     <item><see cref="O:AZCL.Collections.LinqForArrayWrappers.Count">Count</see>**</item>
    ///     <item><see cref="O:AZCL.Collections.LinqForArrayWrappers.ElementAt">ElementAt</see></item>
    ///     <item><see cref="O:AZCL.Collections.LinqForArrayWrappers.ElementAtOrDefault">ElementAtOrDefault</see></item>
    ///     <item><see cref="O:AZCL.Collections.LinqForArrayWrappers.Last">Last</see></item>
    ///     <item><see cref="O:AZCL.Collections.LinqForArrayWrappers.LastOrDefault">LastOrDefault</see></item>
    ///     <item><see cref="O:AZCL.Collections.LinqForArrayWrappers.Reverse">Reverse</see></item>
    ///     <item><see cref="O:AZCL.Collections.LinqForArrayWrappers.Skip">Skip</see></item>
    ///     <item><see cref="O:AZCL.Collections.LinqForArrayWrappers.ToArray">ToArray</see></item>
    ///     <item><see cref="O:AZCL.Collections.LinqForArrayWrappers.ToList">ToList</see></item>
    /// </list>
    /// <i>*The Cast extensions are slightly different from their Linq equivalents in that they require two generic type parameters instead of just one.</i>
    /// <br/>
    /// <i>**Using this extension will generate an Obsolete warning saying that the <c>Length</c> property should be used instead.
    /// This extension is also marked as not EditorBrowsable to avoid showing up in intellisense.</i>
    /// <para/>
    /// Note that in most cases the returned <c>IEnumerable&lt;T&gt;</c> doesn't implement <c>ICollection</c>.
    /// (This might be added in a future version.)
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class LinqForArrayWrappers
    {
        /* Unfortunately these Cast extensions cannot exactly match the Linq equivalent. :( */

        /// <summary>
        /// Casts the elements of a wrapped array of type <typeparamref name="TSource"/> to
        /// an <c>IEnumerable&lt;&gt;</c> of type <typeparamref name="TResult"/>.
        /// </summary><remarks>
        /// <para>
        /// Cast always succeeds if the source is empty (i.e. backing array is absent or has zero elements).
        /// </para>
        /// <inheritdoc cref="LinqForMultiRankArrays.AsEnumerable{TSource}(TSource[,,,])" select="para[@id='deferredExec']"/>
        /// <inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks><returns>
        /// An <c>IEnumerable&lt;<typeparamref name="TResult"/>&gt;</c> that can be used to enumerate all elements of the source.
        /// </returns>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <typeparam name="TSource">The type of the elements in source.</typeparam>
        /// <param name="source">A wrapped array to cast to an <c>IEnumerable&lt;&gt;</c> of the specified type.</param>
        /// <exception cref="InvalidCastException">
        /// Thrown if an enumerated element in the source couldn't be cast to type <typeparamref name="TResult"/>.
        /// </exception>
        public static IEnumerable<TResult> Cast<TResult, TSource>(Array2<TSource> source)
        {
            var array = source.ArrayRaw;
            var typed = array as TResult[,];
            return typed == null ? array == null ? Empty<TResult>.IEnumerable : ArrayKit.CastIter<TResult>(array) : new Array2<TResult>(typed);
        }

        /// <summary>
        /// Casts the elements of a wrapped array of type <typeparamref name="TSource"/> to
        /// an <c>IEnumerable&lt;&gt;</c> of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <inheritdoc cref="Cast{TResult, TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <typeparam name="TSource">The type of the elements in source.</typeparam>
        /// <param name="source">A wrapped array to cast to an <c>IEnumerable&lt;&gt;</c> of the specified type.</param>
        /// <exception cref="InvalidCastException">
        /// Thrown if an enumerated element in the source couldn't be cast to type <typeparamref name="TResult"/>.
        /// </exception>
        public static IEnumerable<TResult> Cast<TResult, TSource>(Array3<TSource> source)
        {
            var array = source.ArrayRaw;
            var typed = array as TResult[,,];
            return typed == null ? array == null ? Empty<TResult>.IEnumerable : ArrayKit.CastIter<TResult>(array) : new Array3<TResult>(typed);
        }

        /// <summary>
        /// Casts the elements of a wrapped array of type <typeparamref name="TSource"/> to
        /// an <c>IEnumerable&lt;&gt;</c> of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <inheritdoc cref="Cast{TResult, TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <typeparam name="TSource">The type of the elements in source.</typeparam>
        /// <param name="source">A wrapped array to cast to an <c>IEnumerable&lt;&gt;</c> of the specified type.</param>
        /// <exception cref="InvalidCastException">
        /// Thrown if an enumerated element in the source couldn't be cast to type <typeparamref name="TResult"/>.
        /// </exception>
        public static IEnumerable<TResult> Cast<TResult, TSource>(ReadOnlyArray2<TSource> source)
        {
            var array = source.ArrayRaw;
            var typed = array as TResult[,];
            return typed == null ? array == null ? Empty<TResult>.IEnumerable : ArrayKit.CastIter<TResult>(array) : new ReadOnlyArray2<TResult>(typed);
        }

        /// <summary>
        /// Casts the elements of a wrapped array of type <typeparamref name="TSource"/> to
        /// an <c>IEnumerable&lt;&gt;</c> of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <inheritdoc cref="Cast{TResult, TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <typeparam name="TSource">The type of the elements in source.</typeparam>
        /// <param name="source">A wrapped array to cast to an <c>IEnumerable&lt;&gt;</c> of the specified type.</param>
        /// <exception cref="InvalidCastException">
        /// Thrown if an enumerated element in the source couldn't be cast to type <typeparamref name="TResult"/>.
        /// </exception>
        public static IEnumerable<TResult> Cast<TResult, TSource>(ReadOnlyArray3<TSource> source)
        {
            var array = source.ArrayRaw;
            var typed = array as TResult[,,];
            return typed == null ? array == null ? Empty<TResult>.IEnumerable : ArrayKit.CastIter<TResult>(array) : new ReadOnlyArray3<TResult>(typed);
        }

        /// <summary>
        /// Casts the elements of a wrapped array of type <typeparamref name="TSource"/> to
        /// an <c>IEnumerable&lt;&gt;</c> of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <inheritdoc cref="Cast{TResult, TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <typeparam name="TSource">The type of the elements in source.</typeparam>
        /// <param name="source">A wrapped array to cast to an <c>IEnumerable&lt;&gt;</c> of the specified type.</param>
        /// <exception cref="InvalidCastException">
        /// Thrown if an enumerated element in the source couldn't be cast to type <typeparamref name="TResult"/>.
        /// </exception>
        public static IEnumerable<TResult> Cast<TResult, TSource>(ReadOnlyArray<TSource> source)
            => source.ArrayRaw?.Cast<TResult>() ?? Empty<TResult>.IEnumerable;

        // ---

        /// <summary>
        /// Returns the length of the specified array. (Marked as Obsolete! Use the Length property instead!)
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <c>source</c> is null.
        /// </exception>
        [Obsolete("Count extension used on an array. Use Length property instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static int Count<TSource>(this Array2<TSource> source)
            => source.Length;

        /// <summary>
        /// Returns the length of the specified array. (Marked as Obsolete! Use the Length property instead!)
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <c>source</c> is null.
        /// </exception>
        [Obsolete("Count extension used on an array. Use Length property instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static int Count<TSource>(this Array3<TSource> source)
            => source.Length;

        /// <summary>
        /// Returns the length of the specified array. (Marked as Obsolete! Use the Length property instead!)
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <c>source</c> is null.
        /// </exception>
        [Obsolete("Count extension used on an array. Use Length property instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static int Count<TSource>(this ReadOnlyArray2<TSource> source)
            => source.Length;

        /// <summary>
        /// Returns the length of the specified array. (Marked as Obsolete! Use the Length property instead!)
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <c>source</c> is null.
        /// </exception>
        [Obsolete("Count extension used on an array. Use Length property instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static int Count<TSource>(this ReadOnlyArray3<TSource> source)
            => source.Length;

        /// <summary>
        /// Returns the length of the specified array. (Marked as Obsolete! Use the Length property instead!)
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <c>source</c> is null.
        /// </exception>
        [Obsolete("Count extension used on an array. Use Length property instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static int Count<TSource>(this ReadOnlyArray<TSource> source)
            => source.Length;

        // ---

        /// <summary>
        /// Returns the element at a specified enumeration index in a wrapped array.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array.
        /// </returns>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        public static TSource ElementAt<TSource>(this Array2<TSource> source, int index)
        {
            try { return source.GetValue1D(index); }
            catch (IndexOutOfRangeException e)
            { throw new ArgumentOutOfRangeException(paramName: nameof(index), message: e.Message); }
        }

        /// <summary>
        /// Returns the element at a specified enumeration index in a wrapped array.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array.
        /// </returns>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        public static TSource ElementAt<TSource>(this Array3<TSource> source, int index)
        {
            try { return source.GetValue1D(index); }
            catch (IndexOutOfRangeException e)
            { throw new ArgumentOutOfRangeException(paramName: nameof(index), message: e.Message); }
        }

        /// <summary>
        /// Returns the element at a specified enumeration index in a wrapped array.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array.
        /// </returns>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        public static TSource ElementAt<TSource>(this ReadOnlyArray2<TSource> source, int index)
        {
            try { return source.GetValue1D(index); }
            catch (IndexOutOfRangeException e)
            { throw new ArgumentOutOfRangeException(paramName: nameof(index), message: e.Message); }
        }

        /// <summary>
        /// Returns the element at a specified enumeration index in a wrapped array.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array.
        /// </returns>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        public static TSource ElementAt<TSource>(this ReadOnlyArray3<TSource> source, int index)
        {
            try { return source.GetValue1D(index); }
            catch (IndexOutOfRangeException e)
            { throw new ArgumentOutOfRangeException(paramName: nameof(index), message: e.Message); }
        }

        /// <summary>
        /// Returns the element at a specified enumeration index in a wrapped array.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array.
        /// </returns>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        public static TSource ElementAt<TSource>(this ReadOnlyArray<TSource> source, int index)
        {
            try { return source[index]; }
            catch (IndexOutOfRangeException e)
            { throw new ArgumentOutOfRangeException(paramName: nameof(index), message: e.Message); }
        }

        // ---

        /// <summary>
        /// Returns the element at a specified enumeration index in a wrapped array,
        /// or the default value for the arrays element type if the index is out of bounds.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array, if the specified index is within bounds;
        /// otherwise <c>default(<typeparamref name="TSource"/>)</c>.
        /// </returns>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        public static TSource ElementAtOrDefault<TSource>(this Array2<TSource> source, int index)
            => source.GetValueOrDefault(index);

        /// <summary>
        /// Returns the element at a specified enumeration index in a wrapped array,
        /// or the default value for the arrays element type if the index is out of bounds.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array, if the specified index is within bounds;
        /// otherwise <c>default(<typeparamref name="TSource"/>)</c>.
        /// </returns>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        public static TSource ElementAtOrDefault<TSource>(this Array3<TSource> source, int index)
            => source.GetValueOrDefault(index);

        /// <summary>
        /// Returns the element at a specified enumeration index in a wrapped array,
        /// or the default value for the arrays element type if the index is out of bounds.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array, if the specified index is within bounds;
        /// otherwise <c>default(<typeparamref name="TSource"/>)</c>.
        /// </returns>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        public static TSource ElementAtOrDefault<TSource>(this ReadOnlyArray2<TSource> source, int index)
            => source.GetValueOrDefault(index);

        /// <summary>
        /// Returns the element at a specified enumeration index in a wrapped array,
        /// or the default value for the arrays element type if the index is out of bounds.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array, if the specified index is within bounds;
        /// otherwise <c>default(<typeparamref name="TSource"/>)</c>.
        /// </returns>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        public static TSource ElementAtOrDefault<TSource>(this ReadOnlyArray3<TSource> source, int index)
            => source.GetValueOrDefault(index);

        /// <summary>
        /// Returns the element at a specified enumeration index in a wrapped array,
        /// or the default value for the arrays element type if the index is out of bounds.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array, if the specified index is within bounds;
        /// otherwise <c>default(<typeparamref name="TSource"/>)</c>.
        /// </returns>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        public static TSource ElementAtOrDefault<TSource>(this ReadOnlyArray<TSource> source, int index)
            => unchecked((uint)index < (uint)source.Length) ? source[index] : default(TSource);

        // ---

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <remarks><inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <param name="source">A wrapped array to return the last element of. (See Remarks)</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or absent.
        /// </exception>
        public static TSource Last<TSource>(this Array2<TSource> source)
            => ArrayKit.Last(source.ArrayRaw);

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <remarks><inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <param name="source">A wrapped array to return the last element of. (See Remarks)</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or absent.
        /// </exception>
        public static TSource Last<TSource>(this Array3<TSource> source)
            => ArrayKit.Last(source.ArrayRaw);

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <remarks><inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <param name="source">A wrapped array to return the last element of. (See Remarks)</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or absent.
        /// </exception>
        public static TSource Last<TSource>(this ReadOnlyArray2<TSource> source)
            => ArrayKit.Last(source.ArrayRaw);

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <remarks><inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <param name="source">A wrapped array to return the last element of. (See Remarks)</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or absent.
        /// </exception>
        public static TSource Last<TSource>(this ReadOnlyArray3<TSource> source)
            => ArrayKit.Last(source.ArrayRaw);

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <remarks><inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <param name="source">A wrapped array to return the last element of. (See Remarks)</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or absent.
        /// </exception>
        public static TSource Last<TSource>(this ReadOnlyArray<TSource> source)
        {
            int len = source.Length;
            if (len == 0)
                throw new InvalidOperationException(AZCL.ERR.SOURCE_EMPTY);
            return source.ArrayRaw[len - 1];
        }

        // ---

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <remarks><inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <returns>The last element in the sequence that passes the test in the specified predicate function. (See Remarks)</returns>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="predicate"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or absent, or no element satisfies the condition in <paramref name="predicate"/>.
        /// </exception>
        public static TSource Last<TSource>(this Array2<TSource> source, Func<TSource, bool> predicate)
            => ArrayKit.Reverse(source.ArrayRaw).First(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <remarks><inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <returns>The last element in the sequence that passes the test in the specified predicate function. (See Remarks)</returns>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="predicate"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or absent, or no element satisfies the condition in <paramref name="predicate"/>.
        /// </exception>
        public static TSource Last<TSource>(this Array3<TSource> source, Func<TSource, bool> predicate)
            => ArrayKit.Reverse(source.ArrayRaw).First(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <remarks><inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <returns>The last element in the sequence that passes the test in the specified predicate function. (See Remarks)</returns>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="predicate"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or absent, or no element satisfies the condition in <paramref name="predicate"/>.
        /// </exception>
        public static TSource Last<TSource>(this ReadOnlyArray2<TSource> source, Func<TSource, bool> predicate)
            => ArrayKit.Reverse(source.ArrayRaw).First(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <remarks><inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <returns>The last element in the sequence that passes the test in the specified predicate function. (See Remarks)</returns>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="predicate"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or absent, or no element satisfies the condition in <paramref name="predicate"/>.
        /// </exception>
        public static TSource Last<TSource>(this ReadOnlyArray3<TSource> source, Func<TSource, bool> predicate)
            => ArrayKit.Reverse(source.ArrayRaw).First(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <remarks><inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <returns>The last element in the sequence that passes the test in the specified predicate function. (See Remarks)</returns>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="predicate"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or absent, or no element satisfies the condition in <paramref name="predicate"/>.
        /// </exception>
        public static TSource Last<TSource>(this ReadOnlyArray<TSource> source, Func<TSource, bool> predicate)
            => Reverse(source).First(predicate);

        // ---

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <remarks><inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <returns>
        /// <c>default(<typeparamref name="TSource"/>)</c> if the array is empty or absent; otherwise, the last element in the array.
        /// </returns>
        /// <param name="source">A wrapped array to return the last element of. (See Remarks)</param>
        public static TSource LastOrDefault<TSource>(this Array2<TSource> source)
            => ArrayKit.LastOrDefault(source.ArrayRaw);

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <param name="source">A wrapped array to return the last element of. (See Remarks)</param>
        public static TSource LastOrDefault<TSource>(this Array3<TSource> source)
            => ArrayKit.LastOrDefault(source.ArrayRaw);

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <param name="source">A wrapped array to return the last element of. (See Remarks)</param>
        public static TSource LastOrDefault<TSource>(this ReadOnlyArray2<TSource> source)
            => ArrayKit.LastOrDefault(source.ArrayRaw);

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <param name="source">A wrapped array to return the last element of. (See Remarks)</param>
        public static TSource LastOrDefault<TSource>(this ReadOnlyArray3<TSource> source)
            => ArrayKit.LastOrDefault(source.ArrayRaw);

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <param name="source">A wrapped array to return the last element of. (See Remarks)</param>
        public static TSource LastOrDefault<TSource>(this ReadOnlyArray<TSource> source)
        {
            int len = source.Length;
            if (len == 0)
                return default(TSource);
            return source.ArrayRaw[len - 1];
        }

        // ---

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition, or a default value if no such element is found.
        /// </summary>
        /// <remarks><inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <returns>
        /// <c>default(<typeparamref name="TSource"/>)</c> if the array is empty or absent, or if no element passes the test in the
        /// <paramref name="predicate"/> function; otherwise, the last element in the array that passes that test. (See Remarks)
        /// </returns>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="predicate"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this Array2<TSource> source, Func<TSource, bool> predicate)
            => ArrayKit.Reverse(source.ArrayRaw).FirstOrDefault(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition, or a default value if no such element is found.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(Array2{TSource}, Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="predicate"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this Array3<TSource> source, Func<TSource, bool> predicate)
            => ArrayKit.Reverse(source.ArrayRaw).FirstOrDefault(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition, or a default value if no such element is found.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(Array2{TSource}, Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="predicate"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this ReadOnlyArray2<TSource> source, Func<TSource, bool> predicate)
            => ArrayKit.Reverse(source.ArrayRaw).FirstOrDefault(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition, or a default value if no such element is found.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(Array2{TSource}, Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="predicate"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this ReadOnlyArray3<TSource> source, Func<TSource, bool> predicate)
            => ArrayKit.Reverse(source.ArrayRaw).FirstOrDefault(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition, or a default value if no such element is found.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(Array2{TSource}, Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A wrapped array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="predicate"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this ReadOnlyArray<TSource> source, Func<TSource, bool> predicate)
            => Reverse(source).FirstOrDefault(predicate);

        // ---

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary><remarks>
        /// <inheritdoc cref="LinqForMultiRankArrays.AsEnumerable{TSource}(TSource[,,,])" select="para[@id='deferredExec']"/>
        /// <inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks><returns>
        /// A sequence whose elements correspond to those of the input sequence in reverse order.
        /// </returns>
        /// <param name="source">A sequence of values to reverse. (See the Remarks section for what this means.)</param>
        public static IEnumerable<TSource> Reverse<TSource>(this Array2<TSource> source)
            => ArrayKit.Reverse(source.ArrayRaw);

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary>
        /// <inheritdoc cref="Reverse{TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <param name="source">A sequence of values to reverse. (See the Remarks section for what this means.)</param>
        public static IEnumerable<TSource> Reverse<TSource>(this Array3<TSource> source)
            => ArrayKit.Reverse(source.ArrayRaw);

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary>
        /// <inheritdoc cref="Reverse{TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <param name="source">A sequence of values to reverse. (See the Remarks section for what this means.)</param>
        public static IEnumerable<TSource> Reverse<TSource>(this ReadOnlyArray2<TSource> source)
            => ArrayKit.Reverse(source.ArrayRaw);

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary>
        /// <inheritdoc cref="Reverse{TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <param name="source">A sequence of values to reverse. (See the Remarks section for what this means.)</param>
        public static IEnumerable<TSource> Reverse<TSource>(this ReadOnlyArray3<TSource> source)
            => ArrayKit.Reverse(source.ArrayRaw);

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary>
        /// <inheritdoc cref="Reverse{TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <param name="source">A sequence of values to reverse. (See the Remarks section for what this means.)</param>
        public static IEnumerable<TSource> Reverse<TSource>(this ReadOnlyArray<TSource> source)
        {
            var arr = source.Array;
            for (int i = arr.Length - 1; i >= 0; --i)
                yield return arr[i];
        }

        // ---

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary><remarks>
        /// <inheritdoc cref="LinqForMultiRankArrays.AsEnumerable{TSource}(TSource[,,,])" select="para[@id='deferredExec']"/>
        /// <inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks>
        /// <returns>
        /// An <c>IEnumerable&lt;T&gt;</c> that contains the elements that occur after the specified enumeration index in the <paramref name="source"/>. (See Remarks)
        /// </returns>
        /// <param name="source">A wrapped array to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements. (Negative values are clamped to zero.)</param>
        public static IEnumerable<TSource> Skip<TSource>(this Array2<TSource> source, int count)
            => ArrayKit.Skip(source.ArrayRaw, count);

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <inheritdoc cref="Skip{TSource}(Array2{TSource}, int)" select="remarks|returns"/>
        /// <param name="source">A wrapped array to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements. (Negative values are clamped to zero.)</param>
        public static IEnumerable<TSource> Skip<TSource>(this Array3<TSource> source, int count)
            => ArrayKit.Skip(source.ArrayRaw, count);

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <inheritdoc cref="Skip{TSource}(Array2{TSource}, int)" select="remarks|returns"/>
        /// <param name="source">A wrapped array to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements. (Negative values are clamped to zero.)</param>
        public static IEnumerable<TSource> Skip<TSource>(this ReadOnlyArray2<TSource> source, int count)
            => ArrayKit.Skip(source.ArrayRaw, count);

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <inheritdoc cref="Skip{TSource}(Array2{TSource}, int)" select="remarks|returns"/>
        /// <param name="source">A wrapped array to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements. (Negative values are clamped to zero.)</param>
        public static IEnumerable<TSource> Skip<TSource>(this ReadOnlyArray3<TSource> source, int count)
            => ArrayKit.Skip(source.ArrayRaw, count);

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <inheritdoc cref="Skip{TSource}(Array2{TSource}, int)" select="remarks|returns"/>
        /// <param name="source">A wrapped array to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements. (Negative values are clamped to zero.)</param>
        public static IEnumerable<TSource> Skip<TSource>(this ReadOnlyArray<TSource> source, int count)
            => source.Array.Skip(count);

        // ---

        /// <summary>
        /// Creates a single rank array from a wrapped multi-rank array.
        /// </summary>
        /// <inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// <returns>
        /// A single-rank array that contains the elements of the wrapped multi-rank <paramref name="source"/> array in enumeration order. (See Remarks)
        /// </returns>
        /// <param name="source">A wrapped multi-rank array to create a single-rank array from.</param>
        public static TSource[] ToArray<TSource>(this Array2<TSource> source)
            => ArrayKit.ToArray(source.ArrayRaw);

        /// <summary>
        /// Creates a single rank array from a wrapped multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToArray{TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <param name="source">A wrapped multi-rank array to create a single-rank array from.</param>
        public static TSource[] ToArray<TSource>(this Array3<TSource> source)
            => ArrayKit.ToArray(source.ArrayRaw);

        /// <summary>
        /// Creates a single rank array from a wrapped multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToArray{TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <param name="source">A wrapped multi-rank array to create a single-rank array from.</param>
        public static TSource[] ToArray<TSource>(this ReadOnlyArray2<TSource> source)
            => ArrayKit.ToArray(source.ArrayRaw);

        /// <summary>
        /// Creates a single rank array from a wrapped multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToArray{TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <param name="source">A wrapped multi-rank array to create a single-rank array from.</param>
        public static TSource[] ToArray<TSource>(this ReadOnlyArray3<TSource> source)
            => ArrayKit.ToArray(source.ArrayRaw);

        /// <summary>
        /// Creates a copy of a wrapped array.
        /// </summary><remarks>
        /// This is the same as calling <see cref="ReadOnlyArray{T}.CopyBacking()"/>.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static TSource[] ToArray<TSource>(this ReadOnlyArray<TSource> source)
            => source.CopyBacking();

        // ---

        /// <summary>
        /// Creates a <c>List&lt;T&gt;</c> from a wrapped array.
        /// </summary>
        /// <inheritdoc cref="Array2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// <returns>
        /// A <c>List&lt;T&gt;</c> that contains the elements of the wrapped <paramref name="source"/> array in enumeration order. (See Remarks)
        /// </returns>
        /// <param name="source">A wrapped array to create a List from.</param>
        public static List<TSource> ToList<TSource>(this Array2<TSource> source)
            => ArrayKit.ToList(source.ArrayRaw);

        /// <summary>
        /// Creates a <c>List&lt;T&gt;</c> from a wrapped array.
        /// </summary>
        /// <inheritdoc cref="ToList{TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <param name="source">A wrapped array to create a List from.</param>
        public static List<TSource> ToList<TSource>(this Array3<TSource> source)
            => ArrayKit.ToList(source.ArrayRaw);

        /// <summary>
        /// Creates a <c>List&lt;T&gt;</c> from a wrapped array.
        /// </summary>
        /// <inheritdoc cref="ToList{TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <param name="source">A wrapped array to create a List from.</param>
        public static List<TSource> ToList<TSource>(this ReadOnlyArray2<TSource> source)
            => ArrayKit.ToList(source.ArrayRaw);

        /// <summary>
        /// Creates a <c>List&lt;T&gt;</c> from a wrapped array.
        /// </summary>
        /// <inheritdoc cref="ToList{TSource}(Array2{TSource})" select="remarks|returns"/>
        /// <param name="source">A wrapped array to create a List from.</param>
        public static List<TSource> ToList<TSource>(this ReadOnlyArray3<TSource> source)
            => ArrayKit.ToList(source.ArrayRaw);

        /// <summary>
        /// Creates a <c>List&lt;T&gt;</c> from a wrapped array.
        /// </summary><returns>
        /// A <c>List&lt;T&gt;</c> that contains the elements of the wrapped <paramref name="source"/> array.
        /// </returns>
        /// <param name="source">A wrapped array to create a List from.</param>
        public static List<TSource> ToList<TSource>(this ReadOnlyArray<TSource> source)
            => new List<TSource>(source.Array);
    }
}
