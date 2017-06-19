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
    ///     <item><see cref="ArrayR2{T}"/></item>
    ///     <item><see cref="ArrayR3{T}"/></item>
    ///     <item><see cref="ReadOnlyArray{T}"/></item>
    ///     <item><see cref="ReadOnlyArrayR2{T}"/></item>
    ///     <item><see cref="ReadOnlyArrayR3{T}"/></item>
    /// </list>
    /// The following extensions are provided:
    /// <list type="nobullet">
    ///     <item><see cref="O:AZCL.Collections.LinqForArrayWrappers.Cast">Cast</see>*</item>
    ///     <item><see cref="O:AZCL.Collections.LinqForArrayWrappers.Count">Count</see></item>
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
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class LinqForArrayWrappers // TODO: make public when deemed ready.
    {
        /* Unfortunately these Cast extensions cannot exactly match the Linq equivalent. :( */

        public static IEnumerable<TResult> Cast<TResult, TSource>(ArrayR2<TSource> source)
        {
            var array = source.Array;
            var typed = array as TResult[,];
            return typed == null ? array == null ? Empty<TResult>.IEnumerable : ArrayHelper.CastIter<TResult>(array) : new ArrayR2<TResult>(typed);
        }

        public static IEnumerable<TResult> Cast<TResult, TSource>(ArrayR3<TSource> source)
        {
            var array = source.Array;
            var typed = array as TResult[,,];
            return typed == null ? array == null ? Empty<TResult>.IEnumerable : ArrayHelper.CastIter<TResult>(array) : new ArrayR3<TResult>(typed);
        }

        public static IEnumerable<TResult> Cast<TResult, TSource>(ReadOnlyArrayR2<TSource> source)
        {
            var array = source.Array;
            var typed = array as TResult[,];
            return typed == null ? array == null ? Empty<TResult>.IEnumerable : ArrayHelper.CastIter<TResult>(array) : new ReadOnlyArrayR2<TResult>(typed);
        }

        public static IEnumerable<TResult> Cast<TResult, TSource>(ReadOnlyArrayR3<TSource> source)
        {
            var array = source.Array;
            var typed = array as TResult[,,];
            return typed == null ? array == null ? Empty<TResult>.IEnumerable : ArrayHelper.CastIter<TResult>(array) : new ReadOnlyArrayR3<TResult>(typed);
        }

        public static IEnumerable<TResult> Cast<TResult, TSource>(ReadOnlyArray<TSource> source)
            => source.Array?.Cast<TResult>() ?? Empty<TResult>.IEnumerable;

        // ---

        public static int Count<TSource>(this ArrayR2<TSource> source)
            => source.Length;

        public static int Count<TSource>(this ArrayR3<TSource> source)
            => source.Length;

        public static int Count<TSource>(this ReadOnlyArrayR2<TSource> source)
            => source.Length;

        public static int Count<TSource>(this ReadOnlyArrayR3<TSource> source)
            => source.Length;

        public static int Count<TSource>(this ReadOnlyArray<TSource> source)
            => source.Length;

        // ---

        public static TSource ElementAt<TSource>(this ArrayR2<TSource> source, int index)
        {
            try { return source.GetValue1D(index); }
            catch (IndexOutOfRangeException e)
            { throw new ArgumentOutOfRangeException(paramName: nameof(index), message: e.Message); }
        }

        public static TSource ElementAt<TSource>(this ArrayR3<TSource> source, int index)
        {
            try { return source.GetValue1D(index); }
            catch (IndexOutOfRangeException e)
            { throw new ArgumentOutOfRangeException(paramName: nameof(index), message: e.Message); }
        }

        public static TSource ElementAt<TSource>(this ReadOnlyArrayR2<TSource> source, int index)
        {
            try { return source.GetValue1D(index); }
            catch (IndexOutOfRangeException e)
            { throw new ArgumentOutOfRangeException(paramName: nameof(index), message: e.Message); }
        }

        public static TSource ElementAt<TSource>(this ReadOnlyArrayR3<TSource> source, int index)
        {
            try { return source.GetValue1D(index); }
            catch (IndexOutOfRangeException e)
            { throw new ArgumentOutOfRangeException(paramName: nameof(index), message: e.Message); }
        }

        public static TSource ElementAt<TSource>(this ReadOnlyArray<TSource> source, int index)
        {
            try { return source[index]; }
            catch (IndexOutOfRangeException e)
            { throw new ArgumentOutOfRangeException(paramName: nameof(index), message: e.Message); }
        }

        // ---

        public static TSource ElementAtOrDefault<TSource>(this ArrayR2<TSource> source, int index)
            => source.GetValueOrDefault(index);

        public static TSource ElementAtOrDefault<TSource>(this ArrayR3<TSource> source, int index)
            => source.GetValueOrDefault(index);

        public static TSource ElementAtOrDefault<TSource>(this ReadOnlyArrayR2<TSource> source, int index)
            => source.GetValueOrDefault(index);

        public static TSource ElementAtOrDefault<TSource>(this ReadOnlyArrayR3<TSource> source, int index)
            => source.GetValueOrDefault(index);

        public static TSource ElementAtOrDefault<TSource>(this ReadOnlyArray<TSource> source, int index)
            => unchecked((uint)index < (uint)source.Length) ? source[index] : default(TSource);

        // ---

        public static TSource Last<TSource>(this ArrayR2<TSource> source)
            => ArrayHelper.Last(source.Array);

        public static TSource Last<TSource>(this ArrayR3<TSource> source)
            => ArrayHelper.Last(source.Array);

        public static TSource Last<TSource>(this ReadOnlyArrayR2<TSource> source)
            => ArrayHelper.Last(source.Array);

        public static TSource Last<TSource>(this ReadOnlyArrayR3<TSource> source)
            => ArrayHelper.Last(source.Array);

        public static TSource Last<TSource>(this ReadOnlyArray<TSource> source)
        {
            int len = source.Length;
            if (len == 0)
                throw new InvalidOperationException(AZCL.ERR.SOURCE_EMPTY);
            return source.Array[len - 1];
        }

        // ---

        public static TSource Last<TSource>(this ArrayR2<TSource> source, Func<TSource, bool> predicate)
            => ArrayHelper.Reverse(source.Array).First(predicate);

        public static TSource Last<TSource>(this ArrayR3<TSource> source, Func<TSource, bool> predicate)
            => ArrayHelper.Reverse(source.Array).First(predicate);

        public static TSource Last<TSource>(this ReadOnlyArrayR2<TSource> source, Func<TSource, bool> predicate)
            => ArrayHelper.Reverse(source.Array).First(predicate);

        public static TSource Last<TSource>(this ReadOnlyArrayR3<TSource> source, Func<TSource, bool> predicate)
            => ArrayHelper.Reverse(source.Array).First(predicate);

        public static TSource Last<TSource>(this ReadOnlyArray<TSource> source, Func<TSource, bool> predicate)
            => Reverse(source).First(predicate);

        // ---

        public static TSource LastOrDefault<TSource>(this ArrayR2<TSource> source)
            => ArrayHelper.LastOrDefault(source.Array);

        public static TSource LastOrDefault<TSource>(this ArrayR3<TSource> source)
            => ArrayHelper.LastOrDefault(source.Array);

        public static TSource LastOrDefault<TSource>(this ReadOnlyArrayR2<TSource> source)
            => ArrayHelper.LastOrDefault(source.Array);

        public static TSource LastOrDefault<TSource>(this ReadOnlyArrayR3<TSource> source)
            => ArrayHelper.LastOrDefault(source.Array);

        public static TSource LastOrDefault<TSource>(this ReadOnlyArray<TSource> source)
        {
            int len = source.Length;
            if (len == 0)
                return default(TSource);
            return source.Array[len - 1];
        }

        // ---

        public static TSource LastOrDefault<TSource>(this ArrayR2<TSource> source, Func<TSource, bool> predicate)
            => ArrayHelper.Reverse(source.Array).FirstOrDefault(predicate);

        public static TSource LastOrDefault<TSource>(this ArrayR3<TSource> source, Func<TSource, bool> predicate)
            => ArrayHelper.Reverse(source.Array).FirstOrDefault(predicate);

        public static TSource LastOrDefault<TSource>(this ReadOnlyArrayR2<TSource> source, Func<TSource, bool> predicate)
            => ArrayHelper.Reverse(source.Array).FirstOrDefault(predicate);

        public static TSource LastOrDefault<TSource>(this ReadOnlyArrayR3<TSource> source, Func<TSource, bool> predicate)
            => ArrayHelper.Reverse(source.Array).FirstOrDefault(predicate);

        public static TSource LastOrDefault<TSource>(this ReadOnlyArray<TSource> source, Func<TSource, bool> predicate)
            => Reverse(source).FirstOrDefault(predicate);

        // ---

        public static IEnumerable<TSource> Reverse<TSource>(this ArrayR2<TSource> source)
            => ArrayHelper.Reverse(source.Array);

        public static IEnumerable<TSource> Reverse<TSource>(this ArrayR3<TSource> source)
            => ArrayHelper.Reverse(source.Array);

        public static IEnumerable<TSource> Reverse<TSource>(this ReadOnlyArrayR2<TSource> source)
            => ArrayHelper.Reverse(source.Array);

        public static IEnumerable<TSource> Reverse<TSource>(this ReadOnlyArrayR3<TSource> source)
            => ArrayHelper.Reverse(source.Array);

        public static IEnumerable<TSource> Reverse<TSource>(this ReadOnlyArray<TSource> source)
        {
            var arr = source.Array;
            for (int i = source.Length - 1; i >= 0; --i)
                yield return arr[i];
        }

        // ---

        public static IEnumerable<TSource> Skip<TSource>(this ArrayR2<TSource> source, int count)
            => ArrayHelper.Skip(source.Array, count);

        public static IEnumerable<TSource> Skip<TSource>(this ArrayR3<TSource> source, int count)
            => ArrayHelper.Skip(source.Array, count);

        public static IEnumerable<TSource> Skip<TSource>(this ReadOnlyArrayR2<TSource> source, int count)
            => ArrayHelper.Skip(source.Array, count);

        public static IEnumerable<TSource> Skip<TSource>(this ReadOnlyArrayR3<TSource> source, int count)
            => ArrayHelper.Skip(source.Array, count);

        public static IEnumerable<TSource> Skip<TSource>(this ReadOnlyArray<TSource> source, int count)
            => (source.Array ?? Empty<TSource>.Array).Skip(count);

        // ---

        public static TSource[] ToArray<TSource>(this ArrayR2<TSource> source)
            => ArrayHelper.ToArray(source.Array);

        public static TSource[] ToArray<TSource>(this ArrayR3<TSource> source)
            => ArrayHelper.ToArray(source.Array);

        public static TSource[] ToArray<TSource>(this ReadOnlyArrayR2<TSource> source)
            => ArrayHelper.ToArray(source.Array);

        public static TSource[] ToArray<TSource>(this ReadOnlyArrayR3<TSource> source)
            => ArrayHelper.ToArray(source.Array);

        public static TSource[] ToArray<TSource>(this ReadOnlyArray<TSource> source)
            => source.CopyBacking();

        // ---

        public static List<TSource> ToList<TSource>(this ArrayR2<TSource> source)
            => ArrayHelper.ToList(source.Array);

        public static List<TSource> ToList<TSource>(this ArrayR3<TSource> source)
            => ArrayHelper.ToList(source.Array);

        public static List<TSource> ToList<TSource>(this ReadOnlyArrayR2<TSource> source)
            => ArrayHelper.ToList(source.Array);

        public static List<TSource> ToList<TSource>(this ReadOnlyArrayR3<TSource> source)
            => ArrayHelper.ToList(source.Array);

        public static List<TSource> ToList<TSource>(this ReadOnlyArray<TSource> source)
            => new List<TSource>(source.Array ?? Empty<TSource>.Array);
    }
}
