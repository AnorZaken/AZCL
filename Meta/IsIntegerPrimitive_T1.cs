
namespace AZCL.Meta
{
    /// <summary>
    /// Static meta class that checks if <typeparamref name="T"/> is a primitive numeric integer type.
    /// </summary>
    public static class IsIntegerPrimitive<T>
    {
        /// <summary>
        /// True if <typeparamref name="T"/> is primitive numeric integer type.
        /// </summary><remarks>
        /// The primitive numeric integer types are: sbyte, byte, short, ushort, int, uint, long and ulong.
        /// </remarks>
        public static bool Value { get { return Numeric<T>.info.IsInteger; } }
    }
}
