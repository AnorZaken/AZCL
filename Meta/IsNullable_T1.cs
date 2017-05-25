
namespace AZCL.Meta
{
    /// <summary>
    /// Static meta class that indicates whether <typeparamref name="T"/> is some Nullable&lt;?&gt; type.
    /// </summary>
    public static class IsNullable<T>
    {
        /// <summary>
        /// Indicates whether <typeparamref name="T"/> is a Nullable&lt;?&gt;.
        /// </summary>
        public static readonly bool value = Evaluate.IsNullable<T>();
    }
}
