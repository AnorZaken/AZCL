
namespace AZCL.Meta
{
    /// <summary>
    /// Static meta class that checks if <typeparamref name="T"/> is some Nullable&lt;&gt; type.
    /// </summary>
    public static class IsNullable<T>
    {
        /// <summary>
        /// True if <typeparamref name="T"/> is a Nullable&lt;&gt; type.
        /// </summary><remarks>
        /// Also true if the type is the generic type definition for Nullable&lt;&gt;,
        /// i.e. the result of the expression: <code>typeof(Nullable&lt;&gt;)</code>
        /// </remarks>
        public static readonly bool value = typeof(T).IsNullable();
    }
}
