using System;
using System.Globalization;

namespace AZCL.Tuples
{
    /// <summary>
    /// An immutable tuple of four Int32 values. (w,x,y,z)
    /// </summary>
    public struct Int4 : IEquatable<Int4>
    {
        /// <summary>
        /// The separator char used between the integer values in the tuples string representation.
        /// </summary>
        public const char SEPARATOR = Int3.SEPARATOR;

        /// <summary>
        /// Instantiates an Int4 tuple with the specified values.
        /// </summary>
        public Int4(int w, int x, int y, int z)
        {
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
        }

#pragma warning disable CS1591 // documentation warning
        public readonly int w, x, y, z;

        public override int GetHashCode()
            => Bits.Hash.Combine(w, x, y, z);

        public override bool Equals(object obj)
            => obj is Int4 && Equals((Int4)obj);

        public bool Equals(Int4 other)
            => w == other.w & x == other.x & y == other.y & z == other.z;

        public override string ToString()
            => "("
            + w.ToString(CultureInfo.InvariantCulture)
            + SEPARATOR.ToString()
            + x.ToString(CultureInfo.InvariantCulture)
            + SEPARATOR.ToString()
            + y.ToString(CultureInfo.InvariantCulture)
            + SEPARATOR.ToString()
            + z.ToString(CultureInfo.InvariantCulture)
            + ")";

#pragma warning restore CS1591 // documentation warning

        // ---

        /// <summary>
        /// Parses a string as an Int4.
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
        public static Int4 Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException();

            string sw, sx, sy, sz;
            ParseSplit(input, out sw, out sx, out sy, out sz);
            if (sz == null)
                throw new FormatException();

            return new Int4(
                int.Parse(sw, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture),
                int.Parse(sx, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture),
                int.Parse(sy, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture),
                int.Parse(sz, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture))
                ;
        }

        /// <summary>
        /// Tries to parse a string as an Int4.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="result">The parsed tuple value, if parsing was successful; otherwise an all zeroes tuple.</param>
        /// <returns>
        /// True if parsing succeeded; otherwise false.
        /// </returns>
        public static bool TryParse(string input, out Int4 result)
        {
            result = default(Int4);

            if (input == null)
                return false;
            
            string sw, sx, sy, sz;
            ParseSplit(input, out sw, out sx, out sy, out sz);
            if (sz == null)
                return false;

            int w, x, y, z;
            if (!int.TryParse(sw, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out w) ||
                !int.TryParse(sx, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out x) ||
                !int.TryParse(sy, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out y) ||
                !int.TryParse(sz, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out z) )
                return false;

            result = new Int4(w, x, y, z);
            return true;
        }

        // sz will be null if it failed - otherwise it succeeded.
        private static void ParseSplit(string s, out string sw, out string sx, out string sy, out string sz)
        {
            AZAssert.NotNullInternal(s, nameof(s));

            sw = sx = sy = sz = null;

            int end = s.Length - 1;
            if (end < 8 || s[0] != '(' || s[end] != ')')
                return;

            int a = s.IndexOf(SEPARATOR, 1);
            if (a < 2)
                return;
            sw = s.Substring(1, a - 1);

            int b = s.IndexOf(SEPARATOR, ++a);
            if (b < a + 1)
                return;
            sx = s.Substring(a, b - a);

            a = s.IndexOf(SEPARATOR, ++b);
            if (a < b + 1)
                return;
            sy = s.Substring(b, a - b);

            if (++a == end)
                return;
            sz = s.Substring(a, end - a);
        }
    }
}
