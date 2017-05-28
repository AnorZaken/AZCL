
namespace AZCL.Meta
{
    /// <summary>
    /// Static meta class that indicates whether <typeparamref name="T"/> is a numeric simple type.
    /// </summary>
    public static class IsNumeric<T>
    {
        /// <summary>
        /// Indicates whether <typeparamref name="T"/> is numeric simple type.
        /// </summary><remarks>
        /// The numeric simple types are: sbyte, byte, short, ushort, int, uint, long, ulong, float, double, and decimal.
        /// </remarks>
        public static bool Value
        {
            get { return Numeric<T>.info.IsNumeric; }
        }
    }
}
