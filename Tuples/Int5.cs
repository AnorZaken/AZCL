﻿using System;
using System.Globalization;

namespace AZCL.Tuples
{
    /// <summary>
    /// An immutable tuple of five Int32 values. (v,w,x,y,z)
    /// </summary>
    public struct Int5 : IEquatable<Int5>
    {
        /// <summary>
        /// The separator char used between the integer values in the tuples string representation.
        /// </summary>
        public const char SEPARATOR = Int3.SEPARATOR;

        /// <summary>
        /// Instantiates an Int5 tuple with the specified values.
        /// </summary>
        public Int5(int v, int w, int x, int y, int z)
        {
            this.v = v;
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
        }

#pragma warning disable CS1591 // documentation warning
        public readonly int v, w, x, y, z;

        public override int GetHashCode()
            => Bits.Hash.Combine(Bits.Hash.Combine(v, w, x, y), z);

        public override bool Equals(object obj)
            => obj is Int5 && Equals((Int5)obj);

        public bool Equals(Int5 other)
            => v == other.v & w == other.w && x == other.x & y == other.y & z == other.z;

        public override string ToString()
            => "("
            + v.ToString(CultureInfo.InvariantCulture)
            + SEPARATOR.ToString()
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
        /// Parses a string as an Int5.
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
        public static Int5 Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException();

            int[] i = ParseSplit(input);
            if (i == null)
                throw new FormatException();

            return new Int5(
                int.Parse(input.Substring(1, i[0] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture),
                int.Parse(input.Substring(i[0] + 1, i[1] - i[0] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture),
                int.Parse(input.Substring(i[1] + 1, i[2] - i[1] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture),
                int.Parse(input.Substring(i[2] + 1, i[3] - i[2] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture),
                int.Parse(input.Substring(i[3] + 1, input.Length - i[3] - 2), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture))
                ;
        }

        /// <summary>
        /// Tries to parse a string as an Int5.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="result">The parsed tuple value, if parsing was successful; otherwise an all zeroes tuple.</param>
        /// <returns>
        /// True if parsing succeeded; otherwise false.
        /// </returns>
        public static bool TryParse(string input, out Int5 result)
        {
            result = default(Int5);

            if (input == null)
                return false;
            
            int[] i = ParseSplit(input);
            if (i == null)
                return false; // (1,3,5)

            int v, w, x, y, z;
            if (!int.TryParse(input.Substring(1, i[0] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out v) ||
                !int.TryParse(input.Substring(i[0] + 1, i[1] - i[0] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out w) ||
                !int.TryParse(input.Substring(i[1] + 1, i[2] - i[1] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out x) ||
                !int.TryParse(input.Substring(i[2] + 1, i[3] - i[2] - 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out y) ||
                !int.TryParse(input.Substring(i[3] + 1, input.Length - i[3] - 2), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out z) )
                return false;

            result = new Int5(v, w, x, y, z);
            return true;
        }

        // returns null if it failed - otherwise it succeeded.
        // returns the indexes of the separators.
        private static int[] ParseSplit(string s)
        {
            AZAssert.NotNullInternal(s, nameof(s));
            
            int i = s.Length - 1;
            if (i < 10 || s[0] != '(' || s[i] != ')')
                return null;

            var r = new int[4];

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

            if (r[3] + 2 == s.Length)
                return null;

            return r;
        }
    }
}
