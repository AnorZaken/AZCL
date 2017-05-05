
namespace AZCL.Meta
{
    /// <summary>
    /// Static meta class that checks if <typeparamref name="T"/> is a primitive numeric type.
    /// </summary>
    public static class IsNumericPrimitive<T>
    {
        /// <summary>
        /// True if <typeparamref name="T"/> is primitive numeric type.
        /// </summary><remarks>
        /// The primitive numeric types are: sbyte, byte, short, ushort, int, uint, long, ulong, float, double and decimal.
        /// </remarks>
        public static bool Value { get { return Numeric<T>.info.IsNumeric; } }
    }
}
