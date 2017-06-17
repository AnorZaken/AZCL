using System;
using System.ComponentModel;

namespace AZCL
{
    /// <summary>
    /// Extends all primitive integer types with the ToStrBinary method.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ToStrBinaryExtensions
    {
        private const string PREFIX = "0b";

        /* Spelling note:
         * Zeros preferred for plural of zero (a lot of zeros), Zeroes preferred as verb form of zero (she zeroes in on the target). */
        
        /// <summary>
        /// Converts the 8-bit signed byte to its equivalent binary literal string, with "0b"-prefix.
        /// </summary><remarks>
        /// This method uses System.Convert.ToString and prepends the "0b" prefix.
        /// </remarks>
        public static string ToStrBinary(this sbyte i8, bool padWithZeros = true)
        {
            string s = Convert.ToString(i8, 2);
            return PREFIX + (padWithZeros ? new string('0', 8 - s.Length) + s : s);
        }

        /// <summary>
        /// Converts the 8-bit unsigned byte to its equivalent binary literal string, with "0b"-prefix.
        /// </summary><remarks>
        /// This method uses System.Convert.ToString and prepends the "0b" prefix.
        /// </remarks>
        public static string ToStrBinary(this byte u8, bool padWithZeros = true)
        {
            string s = Convert.ToString(u8, 2);
            return PREFIX + (padWithZeros ? new string('0', 8 - s.Length) + s : s);
        }

        /// <summary>
        /// Converts the 16-bit signed integer to its equivalent binary literal string, with "0b"-prefix.
        /// </summary><remarks>
        /// This method uses System.Convert.ToString and prepends the "0b" prefix.
        /// </remarks>
        public static string ToStrBinary(this short i16, bool padWithZeros = true)
        {
            string s = Convert.ToString(i16, 2);
            return PREFIX + (padWithZeros ? new string('0', 16 - s.Length) + s : s);
        }

        /// <summary>
        /// Converts the 16-bit unsigned integer to its equivalent binary literal string, with "0b"-prefix.
        /// </summary><remarks>
        /// This method uses System.Convert.ToString and prepends the "0b" prefix.
        /// </remarks>
        public static string ToStrBinary(this ushort u16, bool padWithZeros = true)
        {
            string s = Convert.ToString(u16, 2);
            return PREFIX + (padWithZeros ? new string('0', 16 - s.Length) + s : s);
        }

        /// <summary>
        /// Converts the 32-bit signed integer to its equivalent binary literal string, with "0b"-prefix.
        /// </summary><remarks>
        /// This method uses System.Convert.ToString and prepends the "0b" prefix.
        /// </remarks>
        public static string ToStrBinary(this int i32, bool padWithZeros = true)
        {
            string s = Convert.ToString(i32, 2);
            return PREFIX + (padWithZeros ? new string('0', 32 - s.Length) + s : s);
        }

        /// <summary>
        /// Converts the 32-bit unsigned integer to its equivalent binary literal string, with "0b"-prefix.
        /// </summary><remarks>
        /// This method uses System.Convert.ToString and prepends the "0b" prefix.
        /// </remarks>
        public static string ToStrBinary(this uint u32, bool padWithZeros = true)
        {
            string s = Convert.ToString(unchecked((int)u32), 2);
            return PREFIX + (padWithZeros ? new string('0', 32 - s.Length) + s : s);
        }

        /// <summary>
        /// Converts the 64-bit signed integer to its equivalent binary literal string, with "0b"-prefix.
        /// </summary><remarks>
        /// This method uses System.Convert.ToString and prepends the "0b" prefix.
        /// </remarks>
        public static string ToStrBinary(this long i64, bool padWithZeros = true)
        {
            string s = Convert.ToString(i64, 2);
            return PREFIX + (padWithZeros ? new string('0', 64 - s.Length) + s : s);
        }

        /// <summary>
        /// Converts the 64-bit unsigned integer to its equivalent binary literal string, with "0b"-prefix.
        /// </summary><remarks>
        /// This method uses System.Convert.ToString and prepends the "0b" prefix.
        /// </remarks>
        public static string ToStrBinary(this ulong u64, bool padWithZeros = true)
        {
            string s = Convert.ToString(unchecked((long)u64), 2);
            return PREFIX + (padWithZeros ? new string('0', 64 - s.Length) + s : s);
        }
    }
}
