
namespace AZCL.Meta
{
    /// <summary>
    /// Static meta class that indicates whether <typeparamref name="T"/> is an integral simple type.
    /// </summary>
    public static class IsInteger<T>
    {
        /// <summary>
        /// Indicates whether <typeparamref name="T"/> is an integral simple type.
        /// </summary><remarks>
        /// The integral types are: sbyte, byte, short, ushort, int, uint, long, and ulong.
        /// </remarks>
        public static bool Value
        {
            get { return Numeric<T>.info.IsInteger; }
        }
    }
}
