using System.ComponentModel;

namespace AZCL
{
    /// <summary>
    /// Extends all primitive integer types with the ToStrHex method.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ToStrHexExtensions
    {
        private const string PREFIX = "0x";

        /* Spelling note:
         * Zeros preferred for plural of zero (a lot of zeros), Zeroes preferred as verb form of zero (she zeroes in on the target). */
        
        /// <summary>
        /// Converts the 8-bit signed byte to its equivalent hexadecimal literal string, with "0x"-prefix.
        /// </summary><remarks>
        /// This method calls ToString("Xn"), where n is a number for padding, and prepends the "0x" prefix.
        /// </remarks>
        public static string ToStrHex(this sbyte i8, bool padWithZeros = true)
        {
            return "0x" + i8.ToString(padWithZeros ? "X2" : "X");
        }

        /// <summary>
        /// Converts the 8-bit unsigned byte to its equivalent hexadecimal literal string, with "0x"-prefix.
        /// </summary><remarks>
        /// This method calls ToString("Xn"), where n is a number for padding, and prepends the "0x" prefix.
        /// </remarks>
        public static string ToStrHex(this byte u8, bool padWithZeros = true)
        {
            return "0x" + u8.ToString(padWithZeros ? "X2" : "X");
        }

        /// <summary>
        /// Converts the 16-bit signed integer to its equivalent hexadecimal literal string, with "0x"-prefix.
        /// </summary><remarks>
        /// This method calls ToString("Xn"), where n is a number for padding, and prepends the "0x" prefix.
        /// </remarks>
        public static string ToStrHex(this short i16, bool padWithZeros = true)
        {
            return "0x" + i16.ToString(padWithZeros ? "X4" : "X");
        }

        /// <summary>
        /// Converts the 16-bit unsigned integer to its equivalent hexadecimal literal string, with "0x"-prefix.
        /// </summary><remarks>
        /// This method calls ToString("Xn"), where n is a number for padding, and prepends the "0x" prefix.
        /// </remarks>
        public static string ToStrHex(this ushort u16, bool padWithZeros = true)
        {
            return "0x" + u16.ToString(padWithZeros ? "X4" : "X");
        }

        /// <summary>
        /// Converts the 32-bit signed integer to its equivalent hexadecimal literal string, with "0x"-prefix.
        /// </summary><remarks>
        /// This method calls ToString("Xn"), where n is a number for padding, and prepends the "0x" prefix.
        /// </remarks>
        public static string ToStrHex(this int i32, bool padWithZeros = true)
        {
            return "0x" + i32.ToString(padWithZeros ? "X8" : "X");
        }

        /// <summary>
        /// Converts the 32-bit unsigned integer to its equivalent hexadecimal literal string, with "0x"-prefix.
        /// </summary><remarks>
        /// This method calls ToString("Xn"), where n is a number for padding, and prepends the "0x" prefix.
        /// </remarks>
        public static string ToStrHex(this uint u32, bool padWithZeros = true)
        {
            return "0x" + u32.ToString(padWithZeros ? "X8" : "X");
        }

        /// <summary>
        /// Converts the 64-bit signed integer to its equivalent hexadecimal literal string, with "0x"-prefix.
        /// </summary><remarks>
        /// This method calls ToString("Xn"), where n is a number for padding, and prepends the "0x" prefix.
        /// </remarks>
        public static string ToStrHex(this long i64, bool padWithZeros = true)
        {
            return "0x" + i64.ToString(padWithZeros ? "X16" : "X");
        }

        /// <summary>
        /// Converts the 64-bit unsigned integer to its equivalent hexadecimal literal string, with "0x"-prefix.
        /// </summary><remarks>
        /// This method calls ToString("Xn"), where n is a number for padding, and prepends the "0x" prefix.
        /// </remarks>
        public static string ToStrHex(this ulong u64, bool padWithZeros = true)
        {
            return "0x" + u64.ToString(padWithZeros ? "X16" : "X");
        }
    }
}
