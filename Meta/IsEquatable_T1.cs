
namespace AZCL.Meta
{
    /// <summary>
    /// Static meta class that indicates whether <typeparamref name="T"/> : IEquatable&lt;<typeparamref name="T"/>&gt;.
    /// </summary>
    public static class IsEquatable<T>
    {
        /// <summary>
        /// Indicates whether <typeparamref name="T"/> : IEquatable&lt;<typeparamref name="T"/>&gt;.
        /// </summary>
        public static readonly bool value = Evaluate.IsEquatable<T>();
    }
}
