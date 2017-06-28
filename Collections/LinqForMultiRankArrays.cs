using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AZCL.Collections
{
    /// <summary>
    /// Provides a few alternative Linq extension equivalents that specifically target multi-rank arrays.
    /// </summary><remarks>
    /// The provided extensions are optimizations for cases where the standard Linq alternatives perform
    /// suboptimally* on multi-rank arrays - as well as special cases where existing Linq methods doesn't
    /// work at all and would perform poorly even if they did (because multi-rank arrays doesn't implement
    /// <c>IEnumerable&lt;T&gt;)</c>.<br/>
    /// Note that aside from performance the observable behavior should be in every way identical.
    /// <para/>
    /// The following extensions have been specialized:
    /// <list type="nobullet">
    ///     <item><see cref="O:AZCL.Collections.LinqForMultiRankArrays.AsEnumerable">AsEnumerable</see></item>
    ///     <item><see cref="O:AZCL.Collections.LinqForMultiRankArrays.Cast">Cast</see>*</item>
    ///     <item><see cref="O:AZCL.Collections.LinqForMultiRankArrays.Count">Count**</see></item>
    ///     <item><see cref="O:AZCL.Collections.LinqForMultiRankArrays.ElementAt">ElementAt</see></item>
    ///     <item><see cref="O:AZCL.Collections.LinqForMultiRankArrays.ElementAtOrDefault">ElementAtOrDefault</see></item>
    ///     <item><see cref="O:AZCL.Collections.LinqForMultiRankArrays.Last">Last</see></item>
    ///     <item><see cref="O:AZCL.Collections.LinqForMultiRankArrays.LastOrDefault">LastOrDefault</see></item>
    ///     <item><see cref="O:AZCL.Collections.LinqForMultiRankArrays.Reverse">Reverse</see></item>
    ///     <item><see cref="O:AZCL.Collections.LinqForMultiRankArrays.Skip">Skip</see></item>
    ///     <item><see cref="O:AZCL.Collections.LinqForMultiRankArrays.ToArray">ToArray</see></item>
    ///     <item><see cref="O:AZCL.Collections.LinqForMultiRankArrays.ToList">ToList</see></item>
    /// </list>
    /// <i>*Some of the Cast extensions are slightly different from their Linq equivalents in that they require two generic type parameters instead of just one.</i>
    /// <br/>
    /// <i>**Using this extension will generate an Obsolete warning saying that the <c>Length</c> property should be used instead.
    /// This extension is also marked as not EditorBrowsable to avoid showing up in intellisense.</i>
    /// <para/>
    /// Note that in most cases the returned <c>IEnumerable&lt;T&gt;</c> doesn't implement <c>ICollection</c>.
    /// (This might be added in a future version.)
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class LinqForMultiRankArrays
    {
        // for use in expressions (where the argument is called "source")
        private static T NullCheck<T>(T source)
        {
            if (source == null) new ArgumentNullException(nameof(source));
            return source;
        }

        /// <summary>
        /// Returns the specified multi-rank array typed as IEnumerable&lt;<typeparamref name="TSource"/>&gt;.
        /// </summary><remarks>
        /// <note type="note">
        /// For enumerating rank 2 and rank 3 arrays see the <see cref="O:AZCL.ArrayHelper.AsLinqable{T}"/> extensions.
        /// They return thin feature-rich array wrappers that implements <c>IEnumerable&lt;T&gt;</c>.
        /// </note>
        /// <para id="deferredExec">
        /// This method is implemented by using deferred execution. The immediate return value is an object that stores all
        /// the information that is required to perform the action. The query represented by this method is not executed
        /// until the object is enumerated either by calling its <c>GetEnumerator</c> method directly or by using <c>foreach</c>.
        /// </para>
        /// <inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// <para>
        /// Unfortunately multi-rank arrays in C# only implements IEnumerable and not IEnumerable&lt;<typeparamref name="TSource"/>&gt;.
        /// To solve this AZCL implements its own enumerators for multi-rank arrays (up to rank 10) and provides specialized wrappers (up to rank 3) for use with Linq and in foreach loops.
        /// <br/>
        /// Example: given an array <c>T[,,,] arr;</c> for some type <c>T</c> it can be used as <c>foreach(T elem in arr.AsEnumerable())</c>".
        /// </para>
        /// Note that the returned type doesn't implement <c>ICollection</c> nor <c>ICollection&lt;T&gt;</c> even though multi-rank arrays do implement <c>ICollection</c> (non-generic).
        /// </remarks><returns>
        /// The multi-rank array typed as IEnumerable&lt;T&gt;.
        /// </returns>
        /// <param name="source">The multi-rank array to type as IEnumerable&lt;<typeparamref name="TSource"/>&gt;</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <seealso cref="AZCL.Collections.LinqForMultiRankArrays.Cast{TResult}(Array)"/>
        public static IEnumerable<TSource> AsEnumerable<TSource>(this TSource[,,,] source) //4
        {
            foreach (var i in new Iter.IndexesEnumerable(NullCheck(source)))
                yield return source[i[0], i[1], i[2], i[3]];
        }

        /// <summary>
        /// Returns the specified multi-rank array typed as IEnumerable&lt;<typeparamref name="TSource"/>&gt;.
        /// </summary>
        /// <inheritdoc cref="AsEnumerable{T}(T[,,,])" select="remarks|returns|seealso"/>
        /// <param name="source">The multi-rank array to type as IEnumerable&lt;<typeparamref name="TSource"/>&gt;</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> AsEnumerable<TSource>(this TSource[,,,,] source) //5
        {
            foreach (var i in new Iter.IndexesEnumerable(NullCheck(source)))
                yield return source[i[0], i[1], i[2], i[3], i[4]];
        }

        /// <summary>
        /// Returns the specified multi-rank array typed as IEnumerable&lt;<typeparamref name="TSource"/>&gt;.
        /// </summary>
        /// <inheritdoc cref="AsEnumerable{T}(T[,,,])" select="remarks|returns|seealso"/>
        /// <param name="source">The multi-rank array to type as IEnumerable&lt;<typeparamref name="TSource"/>&gt;</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> AsEnumerable<TSource>(this TSource[,,,,,] source) //6
        {
            foreach (var i in new Iter.IndexesEnumerable(NullCheck(source)))
                yield return source[i[0], i[1], i[2], i[3], i[4], i[5]];
        }

        /// <summary>
        /// Returns the specified multi-rank array typed as IEnumerable&lt;<typeparamref name="TSource"/>&gt;.
        /// </summary>
        /// <inheritdoc cref="AsEnumerable{T}(T[,,,])" select="remarks|returns|seealso"/>
        /// <param name="source">The multi-rank array to type as IEnumerable&lt;<typeparamref name="TSource"/>&gt;</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> AsEnumerable<TSource>(this TSource[,,,,,,] source) //7
        {
            foreach (var i in new Iter.IndexesEnumerable(NullCheck(source)))
                yield return source[i[0], i[1], i[2], i[3], i[4], i[5], i[6]];
        }

        /// <summary>
        /// Returns the specified multi-rank array typed as IEnumerable&lt;<typeparamref name="TSource"/>&gt;.
        /// </summary>
        /// <inheritdoc cref="AsEnumerable{T}(T[,,,])" select="remarks|returns|seealso"/>
        /// <param name="source">The multi-rank array to type as IEnumerable&lt;<typeparamref name="TSource"/>&gt;</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> AsEnumerable<TSource>(this TSource[,,,,,,,] source) //8
        {
            foreach (var i in new Iter.IndexesEnumerable(NullCheck(source)))
                yield return source[i[0], i[1], i[2], i[3], i[4], i[5], i[6], i[7]];
        }

        /// <summary>
        /// Returns the specified multi-rank array typed as IEnumerable&lt;<typeparamref name="TSource"/>&gt;.
        /// </summary>
        /// <inheritdoc cref="AsEnumerable{T}(T[,,,])" select="remarks|returns|seealso"/>
        /// <param name="source">The multi-rank array to type as IEnumerable&lt;<typeparamref name="TSource"/>&gt;</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> AsEnumerable<TSource>(this TSource[,,,,,,,,] source) //9
        {
            foreach (var i in new Iter.IndexesEnumerable(NullCheck(source)))
                yield return source[i[0], i[1], i[2], i[3], i[4], i[5], i[6], i[7], i[8]];
        }

        /// <summary>
        /// Returns the specified multi-rank array typed as IEnumerable&lt;<typeparamref name="TSource"/>&gt;.
        /// </summary>
        /// <inheritdoc cref="AsEnumerable{T}(T[,,,])" select="remarks|returns|seealso"/>
        /// <param name="source">The multi-rank array to type as IEnumerable&lt;<typeparamref name="TSource"/>&gt;</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> AsEnumerable<TSource>(this TSource[,,,,,,,,,] source) //10
        {
            foreach (var i in new Iter.IndexesEnumerable(NullCheck(source)))
                yield return source[i[0], i[1], i[2], i[3], i[4], i[5], i[6], i[7], i[8], i[9]];
        }

        // ---

        /// <summary>
        /// Casts the elements of a multi-rank array of type <typeparamref name="TSource"/> to
        /// an <c>IEnumerable&lt;&gt;</c> of type <typeparamref name="TResult"/>.
        /// </summary><remarks>
        /// <para>
        /// Cast always succeeds if the source is empty (i.e. has zero elements).
        /// </para>
        /// <inheritdoc cref="AsEnumerable{TSource}(TSource[,,,])" select="para[@id='deferredExec']"/>
        /// <inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks><returns>
        /// An <c>IEnumerable&lt;<typeparamref name="TResult"/>&gt;</c> that can be used to enumerate all elements of the source.
        /// </returns>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <typeparam name="TSource">The type of the elements in source.</typeparam>
        /// <param name="source">A multi-rank array to cast to an <c>IEnumerable&lt;&gt;</c> of the specified type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown if an enumerated element in the source couldn't be cast to type <typeparamref name="TResult"/>.
        /// </exception>
        public static IEnumerable<TResult> Cast<TResult, TSource>(this TSource[,] source) //2
        {
            return (NullCheck(source) as TResult[,])?.AsLinqable() ?? ArrayHelper.CastIter<TResult>(source);
        }

        /// <summary>
        /// Casts the elements of a multi-rank array of type <typeparamref name="TSource"/> to
        /// an <c>IEnumerable&lt;&gt;</c> of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <inheritdoc cref="Cast{TResult, TSource}(TSource[,])" select="remarks|returns"/>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <typeparam name="TSource">The type of the elements in source.</typeparam>
        /// <param name="source">A multi-rank array to cast to an <c>IEnumerable&lt;&gt;</c> of the specified type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown if an enumerated element in the source couldn't be cast to type <typeparamref name="TResult"/>.
        /// </exception>
        public static IEnumerable<TResult> Cast<TResult, TSource>(this TSource[,,] source) //3
        {
            return (NullCheck(source) as TResult[,,])?.AsLinqable() ?? ArrayHelper.CastIter<TResult>(source);
        }

        /// <summary>
        /// Casts the elements of a multi-rank array of type <typeparamref name="TSource"/> to
        /// an <c>IEnumerable&lt;&gt;</c> of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <inheritdoc cref="Cast{TResult, TSource}(TSource[,])" select="remarks|returns"/>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <typeparam name="TSource">The type of the elements in source.</typeparam>
        /// <param name="source">A multi-rank array to cast to an <c>IEnumerable&lt;&gt;</c> of the specified type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown if an enumerated element in the source couldn't be cast to type <typeparamref name="TResult"/>.
        /// </exception>
        public static IEnumerable<TResult> Cast<TResult, TSource>(this TSource[,,,] source) //4
        {
            return (NullCheck(source) as TResult[,,,])?.AsEnumerable() ?? ArrayHelper.CastIter<TResult>(source);
        }

        /// <summary>
        /// Casts the elements of a multi-rank array of type <typeparamref name="TSource"/> to
        /// an <c>IEnumerable&lt;&gt;</c> of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <inheritdoc cref="Cast{TResult, TSource}(TSource[,])" select="remarks|returns"/>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <typeparam name="TSource">The type of the elements in source.</typeparam>
        /// <param name="source">A multi-rank array to cast to an <c>IEnumerable&lt;&gt;</c> of the specified type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown if an enumerated element in the source couldn't be cast to type <typeparamref name="TResult"/>.
        /// </exception>
        public static IEnumerable<TResult> Cast<TResult, TSource>(this TSource[,,,,] source) //5
        {
            return (NullCheck(source) as TResult[,,,,,])?.AsEnumerable() ?? ArrayHelper.CastIter<TResult>(source);
        }

        /// <summary>
        /// Casts the elements of a multi-rank array of type <typeparamref name="TSource"/> to
        /// an <c>IEnumerable&lt;&gt;</c> of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <inheritdoc cref="Cast{TResult, TSource}(TSource[,])" select="remarks|returns"/>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <typeparam name="TSource">The type of the elements in source.</typeparam>
        /// <param name="source">A multi-rank array to cast to an <c>IEnumerable&lt;&gt;</c> of the specified type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown if an enumerated element in the source couldn't be cast to type <typeparamref name="TResult"/>.
        /// </exception>
        public static IEnumerable<TResult> Cast<TResult, TSource>(this TSource[,,,,,] source) //6
        {
            return (NullCheck(source) as TResult[,,,,,])?.AsEnumerable() ?? ArrayHelper.CastIter<TResult>(source);
        }

        /// <summary>
        /// Casts the elements of a multi-rank array of type <typeparamref name="TSource"/> to
        /// an <c>IEnumerable&lt;&gt;</c> of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <inheritdoc cref="Cast{TResult, TSource}(TSource[,])" select="remarks|returns"/>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <typeparam name="TSource">The type of the elements in source.</typeparam>
        /// <param name="source">A multi-rank array to cast to an <c>IEnumerable&lt;&gt;</c> of the specified type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown if an enumerated element in the source couldn't be cast to type <typeparamref name="TResult"/>.
        /// </exception>
        public static IEnumerable<TResult> Cast<TResult, TSource>(this TSource[,,,,,,] source) //7
        {
            return (NullCheck(source) as TResult[,,,,,,])?.AsEnumerable() ?? ArrayHelper.CastIter<TResult>(source);
        }

        /// <summary>
        /// Casts the elements of a multi-rank array of type <typeparamref name="TSource"/> to
        /// an <c>IEnumerable&lt;&gt;</c> of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <inheritdoc cref="Cast{TResult, TSource}(TSource[,])" select="remarks|returns"/>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <typeparam name="TSource">The type of the elements in source.</typeparam>
        /// <param name="source">A multi-rank array to cast to an <c>IEnumerable&lt;&gt;</c> of the specified type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown if an enumerated element in the source couldn't be cast to type <typeparamref name="TResult"/>.
        /// </exception>
        public static IEnumerable<TResult> Cast<TResult, TSource>(this TSource[,,,,,,,] source) //8
        {
            return (NullCheck(source) as TResult[,,,,,,,])?.AsEnumerable() ?? ArrayHelper.CastIter<TResult>(source);
        }

        /// <summary>
        /// Casts the elements of a multi-rank array of type <typeparamref name="TSource"/> to
        /// an <c>IEnumerable&lt;&gt;</c> of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <inheritdoc cref="Cast{TResult, TSource}(TSource[,])" select="remarks|returns"/>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <typeparam name="TSource">The type of the elements in source.</typeparam>
        /// <param name="source">A multi-rank array to cast to an <c>IEnumerable&lt;&gt;</c> of the specified type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown if an enumerated element in the source couldn't be cast to type <typeparamref name="TResult"/>.
        /// </exception>
        public static IEnumerable<TResult> Cast<TResult, TSource>(this TSource[,,,,,,,,] source) //9
        {
            return (NullCheck(source) as TResult[,,,,,,,,])?.AsEnumerable() ?? ArrayHelper.CastIter<TResult>(source);
        }

        /// <summary>
        /// Casts the elements of a multi-rank array of type <typeparamref name="TSource"/> to
        /// an <c>IEnumerable&lt;&gt;</c> of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <inheritdoc cref="Cast{TResult, TSource}(TSource[,])" select="remarks|returns"/>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <typeparam name="TSource">The type of the elements in source.</typeparam>
        /// <param name="source">A multi-rank array to cast to an <c>IEnumerable&lt;&gt;</c> of the specified type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown if an enumerated element in the source couldn't be cast to type <typeparamref name="TResult"/>.
        /// </exception>
        public static IEnumerable<TResult> Cast<TResult, TSource>(this TSource[,,,,,,,,,] source) //10
        {
            return (NullCheck(source) as TResult[,,,,,,,,,])?.AsEnumerable() ?? ArrayHelper.CastIter<TResult>(source);
        }

        /// <summary>
        /// Casts the elements of an array (of any rank) to an <c>IEnumerable&lt;&gt;</c> of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <inheritdoc cref="Cast{TResult, TSource}(TSource[,])" select="remarks|returns"/>
        /// <typeparam name="TResult">The type to cast the elements of source to.</typeparam>
        /// <param name="source">A multi-rank array to cast to an <c>IEnumerable&lt;&gt;</c> of the specified type.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown if an enumerated element in the source couldn't be cast to type <typeparamref name="TResult"/>.
        /// </exception>
        public static IEnumerable<TResult> Cast<TResult>(this Array source) // ! Note that this will also "capture" rank 1 arrays !
        {
            // is it a strongly typed array? (GetElementType returns null if it isn't)
            // var tElem = NullCheck(source).GetType().GetElementType();

            switch (NullCheck(source).Rank)
            {
                case 1:
                    return (source as TResult[]) ?? ArrayHelper.CastIter<TResult>(source);
                case 2:
                    return (source as TResult[,])?.AsLinqable() ?? ArrayHelper.CastIter<TResult>(source);
                case 3:
                    return (source as TResult[,,])?.AsLinqable() ?? ArrayHelper.CastIter<TResult>(source);
                case 4:
                    return (source as TResult[,,,])?.AsEnumerable() ?? ArrayHelper.CastIter<TResult>(source);
                case 5:
                    return (source as TResult[,,,,])?.AsEnumerable() ?? ArrayHelper.CastIter<TResult>(source);
                case 6:
                    return (source as TResult[,,,,,])?.AsEnumerable() ?? ArrayHelper.CastIter<TResult>(source);
                case 7:
                    return (source as TResult[,,,,,,])?.AsEnumerable() ?? ArrayHelper.CastIter<TResult>(source);
                case 8:
                    return (source as TResult[,,,,,,,])?.AsEnumerable() ?? ArrayHelper.CastIter<TResult>(source);
                case 9:
                    return (source as TResult[,,,,,,,,])?.AsEnumerable() ?? ArrayHelper.CastIter<TResult>(source);
                case 10:
                    return (source as TResult[,,,,,,,,,])?.AsEnumerable() ?? ArrayHelper.CastIter<TResult>(source);
                default:
                    return ArrayHelper.CastIter<TResult>(source);
            }
        }

        /// <summary>
        /// Dummy cast to make sure no unnecessary casts are accidentally performed! (Marked as Obsolete!)
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <c>source</c> is null.
        /// </exception>
        [Obsolete("Redundant cast detected. Remove it unless what you actually wanted was AsEnumerable - if so then replace it with AsEnumerable.")]
        public static IEnumerable<TResult> Cast<TResult>(this IEnumerable<TResult> source)
            => NullCheck(source);

        // ---

        /// <summary>
        /// Returns the length of the specified array. (Marked as Obsolete! Use Array.Length property instead!)
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <c>source</c> is null.
        /// </exception>
        [Obsolete("Count extension used on an array - use Length property instead!")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static int Count(this Array source)
            => NullCheck(source).Length;

        // ---

        /// <summary>
        /// Returns the element at a specified enumeration index in a multi-rank array.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array.
        /// </returns>
        /// <param name="array">A multi-rank array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        public static TSource ElementAt<TSource>(this TSource[,] array, int index)
        {
            int x, y;
            ArrayHelper.CalculateIndexes(array, index, out x, out y); // <-- handles the exceptions
            return array[x, y];
        }

        /// <summary>
        /// Returns the element at a specified enumeration index in a multi-rank array.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array.
        /// </returns>
        /// <param name="array">A multi-rank array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        public static TSource ElementAt<TSource>(this TSource[,,] array, int index)
        {
            int x, y, z;
            ArrayHelper.CalculateIndexes(array, index, out x, out y, out z); // <-- handles the exceptions
            return array[x, y, z];
        }

        /// <summary>
        /// Returns the element at a specified enumeration index in a multi-rank array.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array.
        /// </returns>
        /// <param name="array">A multi-rank array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        public static TSource ElementAt<TSource>(this TSource[,,,] array, int index)
        {
            int x, y, z, w;
            ArrayHelper.CalculateIndexes(array, index, out x, out y, out z, out w); // <-- handles the exceptions
            return array[x, y, z, w];
        }

        /// <summary>
        /// Returns the element at a specified enumeration index in a multi-rank array.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array.
        /// </returns>
        /// <param name="array">A multi-rank array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        public static TSource ElementAt<TSource>(this TSource[,,,,] array, int index) //5
        {
            var i = ArrayHelper.CalculateIndexes(array, index); // <-- handles the exceptions
            return array[i[0], i[1], i[2], i[3], i[4]];
        }

        /// <summary>
        /// Returns the element at a specified enumeration index in a multi-rank array.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array.
        /// </returns>
        /// <param name="array">A multi-rank array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        public static TSource ElementAt<TSource>(this TSource[,,,,,] array, int index) //6
        {
            var i = ArrayHelper.CalculateIndexes(array, index); // <-- handles the exceptions
            return array[i[0], i[1], i[2], i[3], i[4], i[5]];
        }

        /// <summary>
        /// Returns the element at a specified enumeration index in a multi-rank array.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array.
        /// </returns>
        /// <param name="array">A multi-rank array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        public static TSource ElementAt<TSource>(this TSource[,,,,,,] array, int index) //7
        {
            var i = ArrayHelper.CalculateIndexes(array, index); // <-- handles the exceptions
            return array[i[0], i[1], i[2], i[3], i[4], i[5], i[6]];
        }

        /// <summary>
        /// Returns the element at a specified enumeration index in a multi-rank array.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array.
        /// </returns>
        /// <param name="array">A multi-rank array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        public static TSource ElementAt<TSource>(this TSource[,,,,,,,] array, int index) //8
        {
            var i = ArrayHelper.CalculateIndexes(array, index); // <-- handles the exceptions
            return array[i[0], i[1], i[2], i[3], i[4], i[5], i[6], i[7]];
        }

        /// <summary>
        /// Returns the element at a specified enumeration index in a multi-rank array.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array.
        /// </returns>
        /// <param name="array">A multi-rank array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        public static TSource ElementAt<TSource>(this TSource[,,,,,,,,] array, int index) //9
        {
            var i = ArrayHelper.CalculateIndexes(array, index); // <-- handles the exceptions
            return array[i[0], i[1], i[2], i[3], i[4], i[5], i[6], i[7], i[8]];
        }

        /// <summary>
        /// Returns the element at a specified enumeration index in a multi-rank array.
        /// </summary>
        /// <returns>
        /// The element at the specified position in the array.
        /// </returns>
        /// <param name="array">A multi-rank array to return an element from.</param>
        /// <param name="index">The zero-based enumeration index of the element to retrieve.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is less than zero or greater than or equal to the length of the specified array.
        /// </exception>
        public static TSource ElementAt<TSource>(this TSource[,,,,,,,,,] array, int index) //10
        {
            var i = ArrayHelper.CalculateIndexes(array, index); // <-- handles the exceptions
            return array[i[0], i[1], i[2], i[3], i[4], i[5], i[6], i[7], i[8], i[9]];
        }

        // ---

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <remarks><inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,] source) //2
            => ArrayHelper.Last(NullCheck(source));

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <remarks><inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,,] source) //3
            => ArrayHelper.Last(NullCheck(source));

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <remarks><inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,,,] source) //4
        {
            var i = ArrayHelper.GetUpperBounds(NullCheck(source));
            try { return source[i[0], i[1], i[2], i[3]]; }
            catch (IndexOutOfRangeException)
            { throw new InvalidOperationException(AZCL.ERR.SOURCE_EMPTY); }
        }

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <remarks><inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,,,,] source) //5
        {
            var i = ArrayHelper.GetUpperBounds(NullCheck(source));
            try { return source[i[0], i[1], i[2], i[3], i[4]]; }
            catch (IndexOutOfRangeException)
            { throw new InvalidOperationException(AZCL.ERR.SOURCE_EMPTY); }
        }

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <remarks><inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,,,,,] source) //6
        {
            var i = ArrayHelper.GetUpperBounds(NullCheck(source));
            try { return source[i[0], i[1], i[2], i[3], i[4], i[5]]; }
            catch (IndexOutOfRangeException)
            { throw new InvalidOperationException(AZCL.ERR.SOURCE_EMPTY); }
        }

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <remarks><inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,,,,,,] source) //7
        {
            var i = ArrayHelper.GetUpperBounds(NullCheck(source));
            try { return source[i[0], i[1], i[2], i[3], i[4], i[5], i[6]]; }
            catch (IndexOutOfRangeException)
            { throw new InvalidOperationException(AZCL.ERR.SOURCE_EMPTY); }
        }

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <remarks><inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,,,,,,,] source) //8
        {
            var i = ArrayHelper.GetUpperBounds(NullCheck(source));
            try { return source[i[0], i[1], i[2], i[3], i[4], i[5], i[6], i[7]]; }
            catch (IndexOutOfRangeException)
            { throw new InvalidOperationException(AZCL.ERR.SOURCE_EMPTY); }
        }

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <remarks><inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,,,,,,,,] source) //9
        {
            var i = ArrayHelper.GetUpperBounds(NullCheck(source));
            try { return source[i[0], i[1], i[2], i[3], i[4], i[5], i[6], i[7], i[8]]; }
            catch (IndexOutOfRangeException)
            { throw new InvalidOperationException(AZCL.ERR.SOURCE_EMPTY); }
        }

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <remarks><inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,,,,,,,,,] source) //10
        {
            var i = ArrayHelper.GetUpperBounds(NullCheck(source));
            try { return source[i[0], i[1], i[2], i[3], i[4], i[5], i[6], i[7], i[8], i[9]]; }
            catch (IndexOutOfRangeException)
            { throw new InvalidOperationException(AZCL.ERR.SOURCE_EMPTY); }
        }

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <remarks><inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <param name="source">An array (of any rank) to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty.
        /// </exception>
        public static object Last(this Array source) //N
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            try
            {
                if (source.Rank == 1)
                    return source.GetValue(source.Length - 1);
                else
                    return source.GetValue(ArrayHelper.GetUpperBounds(source));
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException(AZCL.ERR.SOURCE_EMPTY);
            }
        }

        // ---

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <remarks><inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <returns>The last* element in the sequence that passes the test in the specified predicate function. (*See Remarks)</returns>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or no element satisfies the condition in <paramref name="predicate"/>.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,] source, Func<TSource, bool> predicate) //2
            => ArrayHelper.Reverse(NullCheck(source)).First(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <inheritdoc cref="Last{TSource}(TSource[,], Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or no element satisfies the condition in <paramref name="predicate"/>.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,,] source, Func<TSource, bool> predicate) //3
            => ArrayHelper.Reverse(NullCheck(source)).First(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <inheritdoc cref="Last{TSource}(TSource[,], Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or no element satisfies the condition in <paramref name="predicate"/>.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,,,] source, Func<TSource, bool> predicate) //4
            => Reverse(source).First(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <inheritdoc cref="Last{TSource}(TSource[,], Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or no element satisfies the condition in <paramref name="predicate"/>.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,,,,] source, Func<TSource, bool> predicate) //5
            => Reverse(source).First(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <inheritdoc cref="Last{TSource}(TSource[,], Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or no element satisfies the condition in <paramref name="predicate"/>.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,,,,,] source, Func<TSource, bool> predicate) //6
            => Reverse(source).First(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <inheritdoc cref="Last{TSource}(TSource[,], Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or no element satisfies the condition in <paramref name="predicate"/>.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,,,,,,] source, Func<TSource, bool> predicate) //7
            => Reverse(source).First(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <inheritdoc cref="Last{TSource}(TSource[,], Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or no element satisfies the condition in <paramref name="predicate"/>.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,,,,,,,] source, Func<TSource, bool> predicate) //8
            => Reverse(source).First(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <inheritdoc cref="Last{TSource}(TSource[,], Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or no element satisfies the condition in <paramref name="predicate"/>.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,,,,,,,,] source, Func<TSource, bool> predicate) //9
            => Reverse(source).First(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <inheritdoc cref="Last{TSource}(TSource[,], Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the source array is empty or no element satisfies the condition in <paramref name="predicate"/>.
        /// </exception>
        public static TSource Last<TSource>(this TSource[,,,,,,,,,] source, Func<TSource, bool> predicate) //10
            => Reverse(source).First(predicate);

        // ---

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <remarks><inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <returns>
        /// <c>default(<typeparamref name="TSource"/>)</c> if the array is empty; otherwise, the last element in the array.
        /// </returns>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,] source) //2
            => ArrayHelper.LastOrDefault(NullCheck(source));

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,,] source) //3
            => ArrayHelper.LastOrDefault(NullCheck(source));

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,,,] source) //4
            => NullCheck(source).Length == 0 ? default(TSource) : source.Last();

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,,,,] source) //5
            => NullCheck(source).Length == 0 ? default(TSource) : source.Last();

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,,,,,] source) //6
            => NullCheck(source).Length == 0 ? default(TSource) : source.Last();

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,,,,,,] source) //7
            => NullCheck(source).Length == 0 ? default(TSource) : source.Last();

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,,,,,,,] source) //8
            => NullCheck(source).Length == 0 ? default(TSource) : source.Last();

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,,,,,,,,] source) //9
            => NullCheck(source).Length == 0 ? default(TSource) : source.Last();

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return the last* element of. (*See Remarks)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,,,,,,,,,] source) //10
            => NullCheck(source).Length == 0 ? default(TSource) : source.Last();

        // ---

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition, or a default value if no such element is found.
        /// </summary>
        /// <remarks><inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/></remarks>
        /// <returns>
        /// <c>default(<typeparamref name="TSource"/>)</c> if the array is empty or if no element passes the test in the
        /// <paramref name="predicate"/> function; otherwise, the last* element in the array that passes that test. (*See Remarks)
        /// </returns>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,] source, Func<TSource, bool> predicate) //2
            => ArrayHelper.Reverse(NullCheck(source)).FirstOrDefault(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition, or a default value if no such element is found.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(TSource[,], Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,,] source, Func<TSource, bool> predicate) //3
            => ArrayHelper.Reverse(NullCheck(source)).FirstOrDefault(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition, or a default value if no such element is found.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(TSource[,], Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,,,] source, Func<TSource, bool> predicate) //4
            => Reverse(NullCheck(source)).FirstOrDefault(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition, or a default value if no such element is found.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(TSource[,], Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,,,,] source, Func<TSource, bool> predicate) //5
            => Reverse(NullCheck(source)).FirstOrDefault(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition, or a default value if no such element is found.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(TSource[,], Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,,,,,] source, Func<TSource, bool> predicate) //6
            => Reverse(NullCheck(source)).FirstOrDefault(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition, or a default value if no such element is found.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(TSource[,], Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,,,,,,] source, Func<TSource, bool> predicate) //7
            => Reverse(NullCheck(source)).FirstOrDefault(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition, or a default value if no such element is found.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(TSource[,], Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,,,,,,,] source, Func<TSource, bool> predicate) //8
            => Reverse(NullCheck(source)).FirstOrDefault(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition, or a default value if no such element is found.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(TSource[,], Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,,,,,,,,] source, Func<TSource, bool> predicate) //9
            => Reverse(NullCheck(source)).FirstOrDefault(predicate);

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition, or a default value if no such element is found.
        /// </summary>
        /// <inheritdoc cref="LastOrDefault{TSource}(TSource[,], Func{TSource, bool})" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="predicate"/> is null.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this TSource[,,,,,,,,,] source, Func<TSource, bool> predicate) //10
            => Reverse(NullCheck(source)).FirstOrDefault(predicate);

        // ---

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary><remarks>
        /// <inheritdoc cref="AsEnumerable{TSource}(TSource[,,,])" select="para[@id='deferredExec']"/>
        /// <inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks><returns>
        /// A sequence whose elements correspond to those of the input sequence in reverse order.
        /// </returns>
        /// <param name="source">A sequence of values to reverse. (See the Remarks section for what this means.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Reverse<TSource>(this TSource[,] source)
            => ArrayHelper.Reverse(NullCheck(source));

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary>
        /// <inheritdoc cref="Reverse{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">A sequence of values to reverse. (See the Remarks section for what this means.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Reverse<TSource>(this TSource[,,] source)
            => ArrayHelper.Reverse(NullCheck(source));

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary>
        /// <inheritdoc cref="Reverse{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">A sequence of values to reverse. (See the Remarks section for what this means.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Reverse<TSource>(this TSource[,,,] source) //4
        {
            foreach (var i in new Iter.IndexesReverseEnumerable(NullCheck(source)))
                yield return source[i[0], i[1], i[2], i[3]];
        }

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary>
        /// <inheritdoc cref="Reverse{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">A sequence of values to reverse. (See the Remarks section for what this means.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Reverse<TSource>(this TSource[,,,,] source) //4
        {
            foreach (var i in new Iter.IndexesReverseEnumerable(NullCheck(source)))
                yield return source[i[0], i[1], i[2], i[3], i[4]];
        }

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary>
        /// <inheritdoc cref="Reverse{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">A sequence of values to reverse. (See the Remarks section for what this means.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Reverse<TSource>(this TSource[,,,,,] source) //6
        {
            foreach (var i in new Iter.IndexesReverseEnumerable(NullCheck(source)))
                yield return source[i[0], i[1], i[2], i[3], i[4], i[5]];
        }

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary>
        /// <inheritdoc cref="Reverse{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">A sequence of values to reverse. (See the Remarks section for what this means.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Reverse<TSource>(this TSource[,,,,,,] source) //7
        {
            foreach (var i in new Iter.IndexesReverseEnumerable(NullCheck(source)))
                yield return source[i[0], i[1], i[2], i[3], i[4], i[5], i[6]];
        }

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary>
        /// <inheritdoc cref="Reverse{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">A sequence of values to reverse. (See the Remarks section for what this means.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Reverse<TSource>(this TSource[,,,,,,,] source) //8
        {
            foreach (var i in new Iter.IndexesReverseEnumerable(NullCheck(source)))
                yield return source[i[0], i[1], i[2], i[3], i[4], i[5], i[6], i[7]];
        }

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary>
        /// <inheritdoc cref="Reverse{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">A sequence of values to reverse. (See the Remarks section for what this means.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Reverse<TSource>(this TSource[,,,,,,,,] source) //9
        {
            foreach (var i in new Iter.IndexesReverseEnumerable(NullCheck(source)))
                yield return source[i[0], i[1], i[2], i[3], i[4], i[5], i[6], i[7], i[8]];
        }

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary>
        /// <inheritdoc cref="Reverse{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">A sequence of values to reverse. (See the Remarks section for what this means.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Reverse<TSource>(this TSource[,,,,,,,,,] source) //10
        {
            foreach (var i in new Iter.IndexesReverseEnumerable(NullCheck(source)))
                yield return source[i[0], i[1], i[2], i[3], i[4], i[5], i[6], i[7], i[8], i[9]];
        }

        // ---

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary><remarks>
        /// <inheritdoc cref="AsEnumerable{TSource}(TSource[,,,])" select="para[@id='deferredExec']"/>
        /// <inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// </remarks>
        /// <returns>
        /// An <c>IEnumerable&lt;T&gt;</c> that contains the elements that occur after the specified (enumeration*-)index in the <paramref name="source"/>. (*See Remarks)
        /// </returns>
        /// <param name="source">A multi-rank array to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements. (Negative values are clamped to zero.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Skip<TSource>(this TSource[,] source, int count) //2
            => ArrayHelper.Skip(NullCheck(source), count);

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <inheritdoc cref="Skip{TSource}(TSource[,], int)" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements. (Negative values are clamped to zero.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Skip<TSource>(this TSource[,,] source, int count) //3
            => ArrayHelper.Skip(NullCheck(source), count);

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <inheritdoc cref="Skip{TSource}(TSource[,], int)" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements. (Negative values are clamped to zero.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Skip<TSource>(this TSource[,,,] source, int count) //4
        {
            var iter = new Iter.IndexesIterator(NullCheck(source));
            if (iter.MoveTo(count < 0 ? 0 : count))
                do
                {
                    var i = iter.Current;
                    yield return source[i[0], i[1], i[2], i[3]];
                }
                while (iter.MoveNext());
        }

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <inheritdoc cref="Skip{TSource}(TSource[,], int)" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements. (Negative values are clamped to zero.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Skip<TSource>(this TSource[,,,,] source, int count) //5
        {
            var iter = new Iter.IndexesIterator(NullCheck(source));
            if (iter.MoveTo(count < 0 ? 0 : count))
                do
                {
                    var i = iter.Current;
                    yield return source[i[0], i[1], i[2], i[3], i[4]];
                }
                while (iter.MoveNext());
        }

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <inheritdoc cref="Skip{TSource}(TSource[,], int)" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements. (Negative values are clamped to zero.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Skip<TSource>(this TSource[,,,,,] source, int count) //6
        {
            var iter = new Iter.IndexesIterator(NullCheck(source));
            if (iter.MoveTo(count < 0 ? 0 : count))
                do
                {
                    var i = iter.Current;
                    yield return source[i[0], i[1], i[2], i[3], i[4], i[5]];
                }
                while (iter.MoveNext());
        }

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <inheritdoc cref="Skip{TSource}(TSource[,], int)" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements. (Negative values are clamped to zero.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Skip<TSource>(this TSource[,,,,,,] source, int count) //7
        {
            var iter = new Iter.IndexesIterator(NullCheck(source));
            if (iter.MoveTo(count < 0 ? 0 : count))
                do
                {
                    var i = iter.Current;
                    yield return source[i[0], i[1], i[2], i[3], i[4], i[5], i[6]];
                }
                while (iter.MoveNext());
        }

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <inheritdoc cref="Skip{TSource}(TSource[,], int)" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements. (Negative values are clamped to zero.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Skip<TSource>(this TSource[,,,,,,,] source, int count) //8
        {
            var iter = new Iter.IndexesIterator(NullCheck(source));
            if (iter.MoveTo(count < 0 ? 0 : count))
                do
                {
                    var i = iter.Current;
                    yield return source[i[0], i[1], i[2], i[3], i[4], i[5], i[6], i[7]];
                }
                while (iter.MoveNext());
        }

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <inheritdoc cref="Skip{TSource}(TSource[,], int)" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements. (Negative values are clamped to zero.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Skip<TSource>(this TSource[,,,,,,,,] source, int count) //9
        {
            var iter = new Iter.IndexesIterator(NullCheck(source));
            if (iter.MoveTo(count < 0 ? 0 : count))
                do
                {
                    var i = iter.Current;
                    yield return source[i[0], i[1], i[2], i[3], i[4], i[5], i[6], i[7], i[8]];
                }
                while (iter.MoveNext());
        }

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <inheritdoc cref="Skip{TSource}(TSource[,], int)" select="remarks|returns"/>
        /// <param name="source">A multi-rank array to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements. (Negative values are clamped to zero.)</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static IEnumerable<TSource> Skip<TSource>(this TSource[,,,,,,,,,] source, int count) //10
        {
            var iter = new Iter.IndexesIterator(NullCheck(source));
            if (iter.MoveTo(count < 0 ? 0 : count))
                do
                {
                    var i = iter.Current;
                    yield return source[i[0], i[1], i[2], i[3], i[4], i[5], i[6], i[7], i[8], i[9]];
                }
                while (iter.MoveNext());
        }

        // ---

        /// <summary>
        /// Creates a single rank array from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// <returns>
        /// A single-rank array that contains the elements of the multi-rank <paramref name="source"/> array in enumeration order*. (*See Remarks)
        /// </returns>
        /// <param name="source">Multi-rank array to create a single-rank array from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource[] ToArray<TSource>(this TSource[,] source) //2
            => ArrayHelper.ToArray(NullCheck(source));

        /// <summary>
        /// Creates a single rank array from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToArray{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">Multi-rank array to create a single-rank array from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource[] ToArray<TSource>(this TSource[,,] source) //3
            => ArrayHelper.ToArray(NullCheck(source));

        /// <summary>
        /// Creates a single rank array from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToArray{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">Multi-rank array to create a single-rank array from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource[] ToArray<TSource>(this TSource[,,,] source) //4
        {
            int i = NullCheck(source).Length;
            if (i == 0)
                return Empty<TSource>.Array;

            var arr = new TSource[i];

            i = 0;
            foreach (TSource val in source)
                arr[i++] = val;

            return arr;
        }

        /// <summary>
        /// Creates a single rank array from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToArray{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">Multi-rank array to create a single-rank array from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource[] ToArray<TSource>(this TSource[,,,,] source) //5
        {
            int i = NullCheck(source).Length;
            if (i == 0)
                return Empty<TSource>.Array;

            var arr = new TSource[i];

            i = 0;
            foreach (TSource val in source)
                arr[i++] = val;

            return arr;
        }

        /// <summary>
        /// Creates a single rank array from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToArray{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">Multi-rank array to create a single-rank array from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource[] ToArray<TSource>(this TSource[,,,,,] source) //6
        {
            int i = NullCheck(source).Length;
            if (i == 0)
                return Empty<TSource>.Array;

            var arr = new TSource[i];

            i = 0;
            foreach (TSource val in source)
                arr[i++] = val;

            return arr;
        }

        /// <summary>
        /// Creates a single rank array from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToArray{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">Multi-rank array to create a single-rank array from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource[] ToArray<TSource>(this TSource[,,,,,,] source) //7
        {
            int i = NullCheck(source).Length;
            if (i == 0)
                return Empty<TSource>.Array;

            var arr = new TSource[i];

            i = 0;
            foreach (TSource val in source)
                arr[i++] = val;

            return arr;
        }

        /// <summary>
        /// Creates a single rank array from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToArray{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">Multi-rank array to create a single-rank array from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource[] ToArray<TSource>(this TSource[,,,,,,,] source) //8
        {
            int i = NullCheck(source).Length;
            if (i == 0)
                return Empty<TSource>.Array;

            var arr = new TSource[i];

            i = 0;
            foreach (TSource val in source)
                arr[i++] = val;

            return arr;
        }

        /// <summary>
        /// Creates a single rank array from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToArray{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">Multi-rank array to create a single-rank array from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource[] ToArray<TSource>(this TSource[,,,,,,,,] source) //9
        {
            int i = NullCheck(source).Length;
            if (i == 0)
                return Empty<TSource>.Array;

            var arr = new TSource[i];

            i = 0;
            foreach (TSource val in source)
                arr[i++] = val;

            return arr;
        }

        /// <summary>
        /// Creates a single rank array from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToArray{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">Multi-rank array to create a single-rank array from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static TSource[] ToArray<TSource>(this TSource[,,,,,,,,,] source) //10
        {
            int i = NullCheck(source).Length;
            if (i == 0)
                return Empty<TSource>.Array;

            var arr = new TSource[i];

            i = 0;
            foreach (TSource val in source)
                arr[i++] = val;

            return arr;
        }

        /* A bit limited since it only works for reference types...
        private static T[] ToArray<T>(Array source) where T : class
        {
            AZAssert.NotNullInternal(source, nameof(source));
            AZAssert.Internal(source.GetType().GetElementType() == typeof(T), "array type mismatch");

            var arr = new T[NullCheck(source).Length];
            object[] result = arr;
            int i = 0;
            foreach (var val in source)
                result[i++] = val;
            return arr;
        }
        */

        // ---

        /// <summary>
        /// Creates a <c>List&lt;T&gt;</c> from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ArrayR2{T}.Enumerator" select="para[@id='enumerationOrder']"/>
        /// <returns>
        /// A <c>List&lt;T&gt;</c> that contains the elements of the multi-rank <paramref name="source"/> array in enumeration order*. (*See Remarks)
        /// </returns>
        /// <param name="source">Multi-rank array to create a List from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static List<TSource> ToList<TSource>(this TSource[,] source) //2
            => ArrayHelper.ToList(NullCheck(source));

        /// <summary>
        /// Creates a <c>List&lt;T&gt;</c> from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToList{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">Multi-rank array to create a List from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static List<TSource> ToList<TSource>(this TSource[,,] source) //3
            => ArrayHelper.ToList(NullCheck(source));

        /// <summary>
        /// Creates a <c>List&lt;T&gt;</c> from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToList{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">Multi-rank array to create a List from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static List<TSource> ToList<TSource>(this TSource[,,,] source) //4
        {
            var list = new List<TSource>(NullCheck(source).Length);

            foreach (TSource val in source)
                list.Add(val);

            return list;
        }

        /// <summary>
        /// Creates a <c>List&lt;T&gt;</c> from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToList{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">Multi-rank array to create a List from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static List<TSource> ToList<TSource>(this TSource[,,,,] source) //5
        {
            var list = new List<TSource>(NullCheck(source).Length);

            foreach (TSource val in source)
                list.Add(val);

            return list;
        }

        /// <summary>
        /// Creates a <c>List&lt;T&gt;</c> from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToList{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">Multi-rank array to create a List from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static List<TSource> ToList<TSource>(this TSource[,,,,,] source) //6
        {
            var list = new List<TSource>(NullCheck(source).Length);

            foreach (TSource val in source)
                list.Add(val);

            return list;
        }

        /// <summary>
        /// Creates a <c>List&lt;T&gt;</c> from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToList{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">Multi-rank array to create a List from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static List<TSource> ToList<TSource>(this TSource[,,,,,,] source) //7
        {
            var list = new List<TSource>(NullCheck(source).Length);

            foreach (TSource val in source)
                list.Add(val);

            return list;
        }

        /// <summary>
        /// Creates a <c>List&lt;T&gt;</c> from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToList{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">Multi-rank array to create a List from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static List<TSource> ToList<TSource>(this TSource[,,,,,,,] source) //8
        {
            var list = new List<TSource>(NullCheck(source).Length);

            foreach (TSource val in source)
                list.Add(val);

            return list;
        }

        /// <summary>
        /// Creates a <c>List&lt;T&gt;</c> from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToList{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">Multi-rank array to create a List from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static List<TSource> ToList<TSource>(this TSource[,,,,,,,,] source) //9
        {
            var list = new List<TSource>(NullCheck(source).Length);

            foreach (TSource val in source)
                list.Add(val);

            return list;
        }

        /// <summary>
        /// Creates a <c>List&lt;T&gt;</c> from a multi-rank array.
        /// </summary>
        /// <inheritdoc cref="ToList{TSource}(TSource[,])" select="remarks|returns"/>
        /// <param name="source">Multi-rank array to create a List from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> is null.
        /// </exception>
        public static List<TSource> ToList<TSource>(this TSource[,,,,,,,,,] source) //10
        {
            var list = new List<TSource>(NullCheck(source).Length);

            foreach (TSource val in source)
                list.Add(val);

            return list;
        }
    }
}
