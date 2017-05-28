
namespace AZCL.Meta
{
    /// <summary>
    /// Static meta class that indicates whether <typeparamref name="T"/> : IComparable&lt;<typeparamref name="T"/>&gt;.
    /// </summary>
    public static class IsComparable<T>
    {
        /// <summary>
        /// Indicates whether <typeparamref name="T"/> : IComparable&lt;<typeparamref name="T"/>&gt;.
        /// </summary>
        public static readonly bool value = Evaluate.IsComparable<T>();
    }
}
