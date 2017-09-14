using System;
using AZCL.Bits;

namespace AZCL.Tuples
{
    /// <summary>
    /// An immutable tuple of eight bool values. (b0 ... b7)
    /// </summary>
    /// <remarks>
    /// The bool values are bit-packed into a byte. (See the <see cref="bits"/> field.)<br/>
    /// Supports logical operators as well as implicit casting to and from byte.
    /// </remarks>
    public struct Bool8 : IEquatable<Bool8>
    {
        /// <summary>
        /// The char used to represent True bool values in the tuples string representation.
        /// </summary>
        /// <seealso cref="ToString(byte)"/>
        public const char TRUE_CHAR = 'T';

        /// <summary>
        /// The char used to represent False bool values in the tuples string representation.
        /// </summary>
        /// <seealso cref="ToString(byte)"/>
        public const char FALSE_CHAR = 'F';

        /// <summary>
        /// Byte with one of its bits set, as indicated by the suffix number (0:7).
        /// </summary>
        private const byte // can this safely be made public without introducing risk for typo bugs?
            B0 = 1 << 0, B1 = 1 << 1,
            B2 = 1 << 2, B3 = 1 << 3,
            B4 = 1 << 4, B5 = 1 << 5,
            B6 = 1 << 6, B7 = 1 << 7;

        // ---

        /// <summary>
        /// The byte which stores the boolean values in its bits (from lowest to highest bit).
        /// </summary>
        public readonly byte bits;

        /// <summary>
        /// Initializes a new <see cref="Bool8"/> instance from a <c>byte</c>.
        /// </summary>
        public Bool8(byte bits)
        {
            this.bits = bits;
        }

        /// <summary>
        /// Initializes a new <see cref="Bool8"/> instance from another.
        /// </summary>
        public Bool8(Bool8 bool8)
            : this(bool8.bits)
        { }

        /// <summary>
        /// Initializes a new <see cref="Bool8"/> instance with all bits set to the specified <paramref name="initialValue"/>.
        /// </summary>
        /// <param name="initialValue">The initial value to set all booleans in this new <see cref="Bool8"/> instance to.</param>
        public Bool8(bool initialValue)
            : this (initialValue ? byte.MaxValue : (byte)0)
        { }

        /// <summary>
        /// Initializes a new <see cref="Bool8"/> instance with the specified boolean values. (Unspecified values will be initialized to <c>false</c>.)
        /// </summary>
        /// <remarks>
        /// This overload exists for the sake of better performance when 4 or less booleans are needed.
        /// </remarks>
        public Bool8(bool b0, bool b1, bool b2 = false, bool b3 = false)
        {
            bits = 0;
            if (b0) bits |= B0;
            if (b1) bits |= B1;
            if (b2) bits |= B2;
            if (b3) bits |= B3;
        }

        /// <summary>
        /// Initializes a new <see cref="Bool8"/> instance with the specified boolean values. (Unspecified values will be initialized to <c>false</c>.)
        /// </summary>
        public Bool8(
            bool b0, bool b1, bool b2, bool b3, bool b4,
            bool b5 = false, bool b6 = false, bool b7 = false)
        {
            bits = 0;
            if (b0) bits |= B0;
            if (b1) bits |= B1;
            if (b2) bits |= B2;
            if (b3) bits |= B3;
            if (b4) bits |= B4;
            if (b5) bits |= B5;
            if (b6) bits |= B6;
            if (b7) bits |= B7;
        }

        // ---

        /// <summary>
        /// Gets bit 0 of [0:7].
        /// </summary>
        public bool b0 => bits.Get0();

        /// <summary>
        /// Gets bit 1 of [0:7].
        /// </summary>
        public bool b1 => bits.Get1();

        /// <summary>
        /// Gets bit 2 of [0:7].
        /// </summary>
        public bool b2 => bits.Get2();

        /// <summary>
        /// Gets bit 3 of [0:7].
        /// </summary>
        public bool b3 => bits.Get3();

        /// <summary>
        /// Gets bit 4 of [0:7].
        /// </summary>
        public bool b4 => bits.Get4();

        /// <summary>
        /// Gets bit 5 of [0:7].
        /// </summary>
        public bool b5 => bits.Get5();

        /// <summary>
        /// Gets bit 6 of [0:7].
        /// </summary>
        public bool b6 => bits.Get6();

        /// <summary>
        /// Gets bit 7 of [0:7].
        /// </summary>
        public bool b7 => bits.Get7();

        // ---

        /// <summary>
        /// Takes a Bool8, sets the specified bool, and returns the result.
        /// </summary>
        /// <param name="b8">The Bool8 before setting a bool.</param>
        /// <param name="boolIndex">The zero-based index of the bool to set.</param>
        /// <remarks>
        /// Only the lowest three bits of <paramref name="boolIndex"/> are used, i.e. <c>boolIndex &amp; 0x07</c>.
        /// </remarks>
        public static Bool8 Set(Bool8 b8, int boolIndex) => new Bool8(Bit.Set(b8.bits, boolIndex));

        /// <summary>
        /// Takes a Bool8, sets or unsets the specified bit according to a boolean value, and returns the result.
        /// </summary>
        /// <param name="b8">Bool8 input.</param>
        /// <param name="boolIndex">The zero-based index of the bool to set / unset.</param>
        /// <param name="value">If true then the bit will be set to 1; otherwise to 0 (unset).</param>
        /// <remarks>
        /// Only the lowest three bits of <paramref name="boolIndex"/> are used, i.e. <c>boolIndex &amp; 0x07</c>.
        /// </remarks>
        public static Bool8 Set(Bool8 b8, int boolIndex, bool value) => new Bool8(Bit.Set(b8.bits, boolIndex, value));

        /// <summary>
        /// Takes a Bool8, unsets the specified bool, and returns the result.
        /// </summary>
        /// <param name="b8">The Bool8 before unsetting a bool.</param>
        /// <param name="boolIndex">The zero-based index of the bool to unset.</param>
        /// <remarks>
        /// Only the lowest three bits of <paramref name="boolIndex"/> are used, i.e. <c>boolIndex &amp; 0x07</c>.
        /// </remarks>
        public static Bool8 Unset(Bool8 b8, int boolIndex) => new Bool8(Bit.Unset(b8.bits, boolIndex));

        // ---

        /// <summary>
        /// Implicit conversion from <see cref="Bool8"/> to <c>byte</c>.
        /// </summary>
        public static implicit operator byte(Bool8 bool8)
            => bool8.bits;

        /// <summary>
        /// Implicit conversion from <c>byte</c> to <see cref="Bool8"/>.
        /// </summary>
        public static implicit operator Bool8(byte bits)
            => new Bool8(bits);

        // ---

        /// <summary>
        /// "Boolwise" logical negation operator.
        /// </summary>
        public static Bool8 operator !(Bool8 bool8)
            => new Bool8(unchecked((byte)~bool8.bits));

        /// <summary>
        /// "Boolwise" logical or operator.
        /// </summary>
        public static Bool8 operator |(Bool8 left, Bool8 right)
            => new Bool8(unchecked((byte)(left.bits | right.bits)));

        /// <summary>
        /// "Boolwise" logical and operator.
        /// </summary>
        public static Bool8 operator &(Bool8 left, Bool8 right)
            => new Bool8(unchecked((byte)(left.bits & right.bits)));

        /// <summary>
        /// "Boolwise" logical exclusive-or operator.
        /// </summary>
        public static Bool8 operator ^(Bool8 left, Bool8 right)
            => new Bool8(unchecked((byte)(left.bits ^ right.bits)));

        // ---

        /// <summary>
        /// Returns the hash code for this Bool8 instance.
        /// </summary><remarks>
        /// This is the same as calling <c>bits.GetHashCode()</c>.
        /// </remarks>
        public override int GetHashCode()
            => bits.GetHashCode();
        
        /// <summary>
        /// Returns whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance, or null.</param>
        /// <returns>True if <paramref name="obj"/> is an instance of Bool8 and is "boolwise" equal to this instance; otherwise false.</returns>
        public override bool Equals(object obj)
            => obj is Bool8 && ((Bool8)obj).bits == bits;

        /// <summary>
        /// Returns whether this and another Bool8 instance are "boolwise" equal.
        /// </summary>
        public bool Equals(Bool8 other)
            => other.bits == bits;

        /// <summary>
        /// Returns the parsable string representation of this Bool8 instance.
        /// </summary>
        /// <returns>
        /// The 10 char string representation of the bits / 8 packed booleans in low to high order. (One char per bit + opening and closing parenthesis.)
        /// </returns>
        /// <seealso cref="TRUE_CHAR"/>
        /// <seealso cref="FALSE_CHAR"/>
        public override string ToString()
            => ToString(bits);

        // ---

        /// <summary>
        /// Converts a byte to Bool8 string representation.
        /// </summary>
        /// <param name="bits">The byte to treat as bit-packed booleans.</param>
        /// <returns>
        /// The 10 char string representation of the bits / 8 packed booleans in low to high order. (One char per bit + opening and closing parenthesis.)
        /// </returns>
        /// <seealso cref="TRUE_CHAR"/>
        /// <seealso cref="FALSE_CHAR"/>
        public static string ToString(byte bits)
            => new string(new char[]
            {
                '(',
                (bits & B0) != 0 ? TRUE_CHAR : FALSE_CHAR,
                (bits & B1) != 0 ? TRUE_CHAR : FALSE_CHAR,
                (bits & B2) != 0 ? TRUE_CHAR : FALSE_CHAR,
                (bits & B3) != 0 ? TRUE_CHAR : FALSE_CHAR,
                (bits & B4) != 0 ? TRUE_CHAR : FALSE_CHAR,
                (bits & B5) != 0 ? TRUE_CHAR : FALSE_CHAR,
                (bits & B6) != 0 ? TRUE_CHAR : FALSE_CHAR,
                (bits & B7) != 0 ? TRUE_CHAR : FALSE_CHAR,
                ')',
            });

        /// <summary>
        /// Parses a string as a <see cref="Bool8"/>.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <returns>
        /// The parsed result.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the string argument is null.
        /// </exception>
        /// <exception cref="FormatException">
        /// Thrown if the string isn't in the correct format.
        /// </exception>
        public static Bool8 Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException();

            Bool8 b;
            if (TryParse(input, out b))
                return b;

            throw new FormatException();
        }

        /// <summary>
        /// Tries to parse a string as a <see cref="Bool8"/>.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <param name="result">The parsed <see cref="Bool8"/> value, if parsing was successful; otherwise an all false <see cref="Bool8"/>.</param>
        /// <returns>
        /// True if parsing succeeded; otherwise false.
        /// </returns>
        public static bool TryParse(string input, out Bool8 result)
        {
            result = default(Bool8);

            if (input == null)
                return false;

            if (input.Length != 10 || input[0] != '(' || input[9] != ')')
                return false;

            byte b = 0;
            char c;

            c = input[1];
            if (c == TRUE_CHAR)
                b |= B0;
            else if (c != FALSE_CHAR)
                return false;

            c = input[2];
            if (c == TRUE_CHAR)
                b |= B1;
            else if (c != FALSE_CHAR)
                return false;

            c = input[3];
            if (c == TRUE_CHAR)
                b |= B2;
            else if (c != FALSE_CHAR)
                return false;

            c = input[4];
            if (c == TRUE_CHAR)
                b |= B3;
            else if (c != FALSE_CHAR)
                return false;

            c = input[5];
            if (c == TRUE_CHAR)
                b |= B4;
            else if (c != FALSE_CHAR)
                return false;

            c = input[6];
            if (c == TRUE_CHAR)
                b |= B5;
            else if (c != FALSE_CHAR)
                return false;

            c = input[7];
            if (c == TRUE_CHAR)
                b |= B6;
            else if (c != FALSE_CHAR)
                return false;

            c = input[8];
            if (c == TRUE_CHAR)
                b |= B7;
            else if (c != FALSE_CHAR)
                return false;

            result = new Bool8(b);
            return true;
        }

        /* ...mutability...
        public bool b0
        {
            get { return bits.Get0(); }
            set { bits = Bit.Set0(bits, value); }
        }

        public bool b1
        {
            get { return bits.Get1(); }
            set { bits = Bit.Set1(bits, value); }
        }

        public bool b2
        {
            get { return bits.Get2(); }
            set { bits = Bit.Set2(bits, value); }
        }

        public bool b3
        {
            get { return bits.Get3(); }
            set { bits = Bit.Set3(bits, value); }
        }

        public bool b4
        {
            get { return bits.Get4(); }
            set { bits = Bit.Set4(bits, value); }
        }

        public bool b5
        {
            get { return bits.Get5(); }
            set { bits = Bit.Set5(bits, value); }
        }

        public bool b6
        {
            get { return bits.Get6(); }
            set { bits = Bit.Set6(bits, value); }
        }

        public bool b7
        {
            get { return bits.Get7(); }
            set { bits = Bit.Set7(bits, value); }
        }
        */
    }
}
