
namespace AZCL.Meta
{
    /// <summary>
    /// Static meta class that indicates whether <typeparamref name="T"/> is an enum or enum compatible primitive type.
    /// </summary>
    public static class IsEnumCompatible<T>
    {
        /// <summary>
        /// Indicates whether <typeparamref name="T"/> is an enum or enum compatible primitive type.
        /// </summary><remarks>
        /// The enum compatible types are: bool, char, sbyte, byte, short, ushort, int, uint, long, ulong,
        /// and of course all user defined enum types (not including the actual System.Enum class!).
        /// </remarks>
        public static readonly bool value = Evaluate.IsEnumCompatible<T>();
    }
}
