using System;
using System.Globalization;

namespace AZCL.Tuples
{
    /// <summary>
    /// An immutable tuple of three Int32 values. (x|y|z)
    /// </summary>
    public struct Int3 : IEquatable<Int3>
    {
        /// <summary>
        /// The separator char used between the integer values in the tuples string representation.
        /// </summary>
        public const char SEPARATOR = '|';

        /// <summary>
        /// Instantiates an Int3 tuple with the specified values.
        /// </summary>
        public Int3(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

#pragma warning disable CS1591 // documentation warning
        public readonly int x, y, z;

        public override int GetHashCode()
            => Bits.Hash.Combine(x, y, z);

        public override bool Equals(object obj)
            => obj is Int3 && Equals((Int3)obj);

        public bool Equals(Int3 other)
            => x == other.x & y == other.y & z == other.z;

        public override string ToString()
            => ToString(CultureInfo.InvariantCulture);

        public string ToString(IFormatProvider format)
            => "("
            + x.ToString(format)
            + SEPARATOR.ToString()
            + y.ToString(format)
            + SEPARATOR.ToString()
            + z.ToString(format)
            + ")";

#pragma warning restore CS1591 // documentation warning

        // ---

        /// <summary>
        /// Parses a string as an Int3.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <returns>
        /// The parsed tuple.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the string argument is null.
        /// </exception>
        /// <exception cref="FormatException">
        /// Thrown if the string isn't in the correct format.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if any of the tuple values does not fit in an Int32.
        /// </exception>
        public static Int3 Parse(string input)
            => Parse(input, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parses a string as an Int3.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="format">The format provider to use when parsing. (Specifying <c>null</c> will use current thread culture.)</param>
        /// <param name="style">The number style (default is AllowLeadingSign).</param>
        /// <returns>
        /// The parsed tuple.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the string argument is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="style"/> is neither a <c>System.Globalization.NumberStyles</c> value, nor a combination
        /// of <c>System.Globalization.NumberStyles.AllowHexSpecifier</c> and <c>System.Globalization.NumberStyles.HexNumber</c>.
        /// </exception>
        /// <exception cref="FormatException">
        /// Thrown if the string isn't in the correct format.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown if any of the tuple values does not fit in an Int32.
        /// </exception>
        public static Int3 Parse(string input, IFormatProvider format, NumberStyles style = NumberStyles.AllowLeadingSign)
        {
            if (input == null)
                throw new ArgumentNullException();

            string sx, sy, sz;
            ParseSplit(input, out sx, out sy, out sz);
            if (sz == null)
                throw new FormatException();

            return new Int3(
                int.Parse(sx, style, format),
                int.Parse(sy, style, format),
                int.Parse(sz, style, format))
                ;
        }

        /// <summary>
        /// Tries to parse a string as an Int3.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="result">The parsed tuple value, if parsing was successful; otherwise an all zeroes tuple.</param>
        /// <returns>
        /// True if parsing succeeded; otherwise false.
        /// </returns>
        public static bool TryParse(string input, out Int3 result)
            => TryParse(input, out result, CultureInfo.InvariantCulture);

        /// <summary>
        /// Tries to parse a string as an Int3.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="result">The parsed tuple value, if parsing was successful; otherwise an all zeroes tuple.</param>
        /// <param name="format">The format provider to use when parsing. (Specifying <c>null</c> will use current thread culture.)</param>
        /// <param name="style">The number style (default is AllowLeadingSign).</param>
        /// <returns>
        /// True if parsing succeeded; otherwise false.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="style"/> is neither a <c>System.Globalization.NumberStyles</c> value, nor a combination
        /// of <c>System.Globalization.NumberStyles.AllowHexSpecifier</c> and <c>System.Globalization.NumberStyles.HexNumber</c>.
        /// </exception>
        public static bool TryParse(string input, out Int3 result, IFormatProvider format, NumberStyles style = NumberStyles.AllowLeadingSign)
        {
            result = default(Int3);

            if (input == null)
                return false;

            string sx, sy, sz;
            ParseSplit(input, out sx, out sy, out sz);
            if (sz == null)
                return false;

            int x, y, z;
            if (!int.TryParse(sx, style, format, out x) ||
                !int.TryParse(sy, style, format, out y) ||
                !int.TryParse(sz, style, format, out z))
                return false;

            result = new Int3(x, y, z);
            return true;
        }

        // sz will be null if it failed - otherwise it succeeded.
        private static void ParseSplit(string s, out string sx, out string sy, out string sz)
        {
            AZAssert.NotNullInternal(s, nameof(s));

            sx = sy = sz = null;

            int end = s.Length - 1;
            if (end < 6 || s[0] != '(' || s[end] != ')')
                return;

            int a = s.IndexOf(SEPARATOR, 1);
            if (a < 2)
                return;
            sx = s.Substring(1, a - 1);

            int b = s.IndexOf(SEPARATOR, ++a);
            if (b < a + 1)
                return;
            sy = s.Substring(a, b - a);

            if (++b == end)
                return;
            sz = s.Substring(b, end - b);
        }
    }
}
