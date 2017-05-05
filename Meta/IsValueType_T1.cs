
namespace AZCL.Meta
{
    /// <summary>
    /// Static meta class that checks if <typeparamref name="T"/> is a value type.
    /// </summary>
    public static class IsValueType<T>
    {
        /// <summary>
        /// True if <typeparamref name="T"/> is a value type.
        /// </summary>
        public static readonly bool value = typeof(T).IsValueType;
    }
}
