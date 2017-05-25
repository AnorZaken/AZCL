using System;

namespace AZCL
{
    // this whole construction is purely for performance reasons!
    // (ref is also for performance, because it is assumed we are already internally working on a copy.)
    internal interface IIndexFinder<T>
    {
        int IndexOf(T[,] array, ref T value);
        int IndexOf(T[,,] array, ref T value);

        int IndexOf(T[,] array, ref T value, int startValue);
        int IndexOf(T[,,] array, ref T value, int startValue);

        int IndexOf(T[,] array, ref T value, int startValue, int count);
        int IndexOf(T[,,] array, ref T value, int startValue, int count);

        int LastIndexOf(T[,] array, ref T value);
        int LastIndexOf(T[,,] array, ref T value);

        int LastIndexOf(T[,] array, ref T value, int startValue);
        int LastIndexOf(T[,,] array, ref T value, int startValue);

        int LastIndexOf(T[,] array, ref T value, int startValue, int count);
        int LastIndexOf(T[,,] array, ref T value, int startValue, int count);
    }

    // for finding the index of a value - with Equals(T) if T : IEquatable<T> otherwise with Equals(object).
    internal static class IndexFinder<T>
    {
        public static readonly IIndexFinder<T> instance = CreateInstance();

        private static IIndexFinder<T> CreateInstance()
        {
            if (Meta.Evaluate.IsEquatable<T>())
            {
                Type t_looper = typeof(IndexFinderEquatable<>).MakeGenericType(typeof(T));
                return (IIndexFinder<T>)Activator.CreateInstance(t_looper);
            }
            else
            {
                return new IndexFinderNonEquatable<T>();
            }
        }
    }
}
