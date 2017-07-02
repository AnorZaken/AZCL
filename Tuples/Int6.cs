using System;
using System.Globalization;

namespace AZCL.Tuples
{
    /// <summary>
    /// An immutable tuple of six Int32 values. (a,b,c,d,e,f)
    /// </summary>
    public struct Int6 : IEquatable<Int6>
    {
        /// <summary>
        /// The separator char used between the integer values in the tuples string representation.
        /// </summary>
        public const char SEPARATOR = Int3.SEPARATOR;

        /// <summary>
        /// Instantiates an Int6 tuple with the specified values.
        /// </summary>
        public Int6(int a, int b, int c, int d, int e, int f)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            this.f = f;
        }

#pragma warning disable CS1591 // documentation warning
        public readonly int a, b, c, d, e, f;

        public override int GetHashCode()
            => Bits.Hash.Combine(Bits.Hash.Combine(a, b, c, d), e, f);

        public override bool Equals(object obj)
            => obj is Int6 && Equals((Int6)obj);

        public bool Equals(Int6 other)
            => a == other.a & b == other.b & c == other.c && d == other.d & e == other.e & f == other.f;

        public override string ToString()
            => "("
            + a.ToString(CultureInfo.InvariantCulture)
            + SEPARATOR.ToString()
            + b.ToString(CultureInfo.InvariantCulture)
            + SEPARATOR.ToString()
            + c.ToString(CultureInfo.InvariantCulture)
            + SEPARATOR.ToString()
            + d.ToString(CultureInfo.InvariantCulture)
            + SEPARATOR.ToString()
            + e.ToString(CultureInfo.InvariantCulture)
            + SEPARATOR.ToString()
            + f.ToString(CultureInfo.InvariantCulture)
            + ")";

#pragma warning restore CS1591 // documentation warning

        // ---

        /// <summary>
        /// Parses a string as an Int6.
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
        public static Int6 Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException();

            int[] i = ParseSplit(input);
            if (i == null)
                throw new FormatException();

            return new Int6(
                int.Parse(input.Substring(1, i[0] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture),
                int.Parse(input.Substring(i[0] + 1, i[1] - i[0] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture),
                int.Parse(input.Substring(i[1] + 1, i[2] - i[1] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture),
                int.Parse(input.Substring(i[2] + 1, i[3] - i[2] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture),
                int.Parse(input.Substring(i[3] + 1, i[4] - i[3] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture),
                int.Parse(input.Substring(i[4] + 1, input.Length - i[4] - 2), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture))
                ;
        }

        /// <summary>
        /// Tries to parse a string as an Int6.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="result">The parsed tuple value, if parsing was successful; otherwise an all zeroes tuple.</param>
        /// <returns>
        /// True if parsing succeeded; otherwise false.
        /// </returns>
        public static bool TryParse(string input, out Int6 result)
        {
            result = default(Int6);

            if (input == null)
                return false;
            
            int[] i = ParseSplit(input);
            if (i == null)
                return false; // (1,3,5)

            int a, b, c, d, e, f;
            if (!int.TryParse(input.Substring(1, i[0] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out a) ||
                !int.TryParse(input.Substring(i[0] + 1, i[1] - i[0] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out b) ||
                !int.TryParse(input.Substring(i[1] + 1, i[2] - i[1] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out c) ||
                !int.TryParse(input.Substring(i[2] + 1, i[3] - i[2] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out d) ||
                !int.TryParse(input.Substring(i[3] + 1, i[4] - i[3] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out e) ||
                !int.TryParse(input.Substring(i[4] + 1, input.Length - i[4] - 2), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out f) )
                return false;

            result = new Int6(a, b, c, d, e, f);
            return true;
        }

        // returns null if it failed - otherwise it succeeded.
        // returns the indexes of the separators.
        private static int[] ParseSplit(string s)
        {
            AZAssert.NotNullInternal(s, nameof(s));
            
            int i = s.Length - 1;
            if (i < 12 || s[0] != '(' || s[i] != ')')
                return null;

            var r = new int[5];

            i = s.IndexOf(SEPARATOR, 1);
            if (i < 2)
                return null;
            r[0] = i;

            i = s.IndexOf(SEPARATOR, i + 1);
            if (i < r[0] + 2)
                return null;
            r[1] = i;

            i = s.IndexOf(SEPARATOR, i + 1);
            if (i < r[1] + 2)
                return null;
            r[2] = i;

            i = s.IndexOf(SEPARATOR, i + 1);
            if (i < r[2] + 2)
                return null;
            r[3] = i;

            i = s.IndexOf(SEPARATOR, i + 1);
            if (i < r[3] + 2)
                return null;
            r[4] = i;

            if (r[4] + 2 == s.Length)
                return null;

            return r;
        }
    }
}
