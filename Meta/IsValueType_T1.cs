
namespace AZCL.Meta
{
    /// <summary>
    /// Static meta class that indicates whether <typeparamref name="T"/> is a value type.
    /// </summary>
    public static class IsValueType<T>
    {
        /// <summary>
        /// Indicates whether <typeparamref name="T"/> is a value type.
        /// </summary>
        public static readonly bool value = typeof(T).IsValueType;
    }
}
