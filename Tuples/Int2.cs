using System;
using System.Globalization;

namespace AZCL.Tuples
{
    /// <summary>
    /// An immutable tuple of two Int32 values. (x,y)
    /// </summary>
    public struct Int2 : IEquatable<Int2>
    {
        /// <summary>
        /// The separator char used between the integer values in the tuples string representation.
        /// </summary>
        public const char SEPARATOR = ',';

        /// <summary>
        /// Instantiates an Int2 tuple with the specified values.
        /// </summary>
        public Int2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

#pragma warning disable CS1591 // documentation warning
        public readonly int x, y;

        public override int GetHashCode()
            => Bits.Hash.Combine(x, y);

        public override bool Equals(object obj)
            => obj is Int2 && Equals((Int2)obj);

        public bool Equals(Int2 other)
            => x == other.x & y == other.y;

        public override string ToString()
            => "("
            + x.ToString(CultureInfo.InvariantCulture)
            + SEPARATOR.ToString()
            + y.ToString(CultureInfo.InvariantCulture)
            + ")";

#pragma warning restore CS1591 // documentation warning

        // ---

        /// <summary>
        /// Parses a string as an Int2.
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
        /// Thrown if any of the tuple values does not fit in an Int22.
        /// </exception>
        public static Int2 Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException();

            string sx, sy;
            ParseSplit(input, out sx, out sy);
            if (sy == null)
                throw new FormatException();

            return new Int2(
                int.Parse(sx, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture),
                int.Parse(sy, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture))
                ;
        }

        /// <summary>
        /// Tries to parse a string as an Int2.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="result">The parsed tuple value, if parsing was successful; otherwise an all zeroes tuple.</param>
        /// <returns>
        /// True if parsing succeeded; otherwise false.
        /// </returns>
        public static bool TryParse(string input, out Int2 result)
        {
            result = default(Int2);

            if (input == null)
                return false;

            string sx, sy;
            ParseSplit(input, out sx, out sy);
            if (sy == null)
                return false;

            int x, y;
            if (!int.TryParse(sx, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out x) ||
                !int.TryParse(sy, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out y))
                return false;

            result = new Int2(x, y);
            return true;
        }

        // sy will be null if it failed - otherwise it succeeded.
        private static void ParseSplit(string s, out string sx, out string sy)
        {
            AZAssert.NotNullInternal(s, nameof(s));

            sx = sy = null;

            int end = s.Length - 1;
            if (end < 4 || s[0] != '(' || s[end] != ')')
                return;

            int a = s.IndexOf(SEPARATOR, 1);
            if (a < 2)
                return;
            sx = s.Substring(1, a - 1);
            
            if (++a == end)
                return;
            sy = s.Substring(a, end - a);
        }
    }
}
