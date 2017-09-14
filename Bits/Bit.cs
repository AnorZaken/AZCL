
namespace AZCL.Bits
{
    /// <summary>
    /// Static class for setting, unsetting, and toggling bits, as well as reading bits as booleans.
    /// </summary>
    /// <seealso cref="Pack"/>
    public static class Bit
    {
        private const byte
            B0 = 1 << 0, B1 = 1 << 1,
            B2 = 1 << 2, B3 = 1 << 3,
            B4 = 1 << 4, B5 = 1 << 5,
            B6 = 1 << 6, B7 = 1 << 7;

        private const ushort
            B8 = 1 << 8, B9 = 1 << 9,
            B10 = 1 << 10, B11 = 1 << 11,
            B12 = 1 << 12, B13 = 1 << 13,
            B14 = 1 << 14, B15 = 1 << 15;

        private const uint
            B16 = 1 << 16, B17 = 1 << 17,
            B18 = 1 << 18, B19 = 1 << 19,
            B20 = 1 << 20, B21 = 1 << 21,
            B22 = 1 << 22, B23 = 1 << 23,

            B24 = 1 << 24, B25 = 1 << 25,
            B26 = 1 << 26, B27 = 1 << 27,
            B28 = 1 << 28, B29 = 1 << 29,
            B30 = 1 << 30, B31 = 1u << 31;

        private const ulong
            B32 = 1ul << 32, B33 = 1ul << 33,
            B34 = 1ul << 34, B35 = 1ul << 35,
            B36 = 1ul << 36, B37 = 1ul << 37,
            B38 = 1ul << 38, B39 = 1ul << 39,
            B40 = 1ul << 40, B41 = 1ul << 41,
            B42 = 1ul << 42, B43 = 1ul << 43,
            B44 = 1ul << 44, B45 = 1ul << 45,
            B46 = 1ul << 46, B47 = 1ul << 47,
            B48 = 1ul << 48, B49 = 1ul << 49,
            B50 = 1ul << 50, B51 = 1ul << 51,
            B52 = 1ul << 52, B53 = 1ul << 53,
            B54 = 1ul << 54, B55 = 1ul << 55,
            B56 = 1ul << 56, B57 = 1ul << 57,
            B58 = 1ul << 58, B59 = 1ul << 59,
            B60 = 1ul << 60, B61 = 1ul << 61,
            B62 = 1ul << 62, B63 = 1ul << 63;

        #region --- Byte ---

        /// <summary>
        /// Gets the specified bit on a byte, as a boolean.
        /// </summary>
        /// <param name="bits">The byte to get a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to get.</param>
        /// <remarks>
        /// Only the lowest three bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x07</c>.
        /// </remarks>
        public static bool Get(this byte bits, int bitIndex) => (bits & (1 << (bitIndex & 0x07))) != 0;

        /// <summary>
        /// Gets bit 0 of [0:7].
        /// </summary>
        public static bool Get0(this byte bits) => (bits & B0) != 0;

        /// <summary>
        /// Gets bit 1 of [0:7].
        /// </summary>
        public static bool Get1(this byte bits) => (bits & B1) != 0;

        /// <summary>
        /// Gets bit 2 of [0:7].
        /// </summary>
        public static bool Get2(this byte bits) => (bits & B2) != 0;

        /// <summary>
        /// Gets bit 3 of [0:7].
        /// </summary>
        public static bool Get3(this byte bits) => (bits & B3) != 0;

        /// <summary>
        /// Gets bit 4 of [0:7].
        /// </summary>
        public static bool Get4(this byte bits) => (bits & B4) != 0;

        /// <summary>
        /// Gets bit 5 of [0:7].
        /// </summary>
        public static bool Get5(this byte bits) => (bits & B5) != 0;

        /// <summary>
        /// Gets bit 6 of [0:7].
        /// </summary>
        public static bool Get6(this byte bits) => (bits & B6) != 0;

        /// <summary>
        /// Gets bit 7 of [0:7].
        /// </summary>
        public static bool Get7(this byte bits) => (bits & B7) != 0;

        // ---

        /// <summary>
        /// Takes a byte, sets the specified bit, and returns the result.
        /// </summary>
        /// <param name="bits">The byte before setting a bit.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest three bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x07</c>.
        /// </remarks>
        public static byte Set(byte bits, int bitIndex) => unchecked((byte)(bits | (1u << (bitIndex & 0x07))));

        /// <summary>
        /// Sets the specified bit on a byte.
        /// </summary>
        /// <param name="bits">The byte to set a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest three bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x07</c>.
        /// </remarks>
        public static void Set(ref byte bits, int bitIndex) => bits |= unchecked((byte)(1u << (bitIndex & 0x07)));

        // ---

        /// <summary>
        /// Takes a byte, sets bit 0 of [0:7], and returns the result.
        /// </summary>
        public static byte Set0(byte bits) => bits |= B0;

        /// <summary>
        /// Takes a byte, sets bit 1 of [0:7], and returns the result.
        /// </summary>
        public static byte Set1(byte bits) => bits |= B1;

        /// <summary>
        /// Takes a byte, sets bit 2 of [0:7], and returns the result.
        /// </summary>
        public static byte Set2(byte bits) => bits |= B2;

        /// <summary>
        /// Takes a byte, sets bit 3 of [0:7], and returns the result.
        /// </summary>
        public static byte Set3(byte bits) => bits |= B3;

        /// <summary>
        /// Takes a byte, sets bit 4 of [0:7], and returns the result.
        /// </summary>
        public static byte Set4(byte bits) => bits |= B4;

        /// <summary>
        /// Takes a byte, sets bit 5 of [0:7], and returns the result.
        /// </summary>
        public static byte Set5(byte bits) => bits |= B5;

        /// <summary>
        /// Takes a byte, sets bit 6 of [0:7], and returns the result.
        /// </summary>
        public static byte Set6(byte bits) => bits |= B6;

        /// <summary>
        /// Takes a byte, sets bit 7 of [0:7], and returns the result.
        /// </summary>
        public static byte Set7(byte bits) => bits |= B7;

        // ---

        /// <summary>
        /// Takes a byte, sets or unsets the specified bit according to a boolean value, and returns the result.
        /// </summary>
        /// <param name="bits">The byte before the set / unset.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set / unset.</param>
        /// <param name="value">If true then the bit will be set to 1; otherwise to 0 (unset).</param>
        /// <remarks>
        /// Only the lowest three bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x07</c>.
        /// </remarks>
        public static byte Set(byte bits, int bitIndex, bool value)
        {
            unchecked
            {
                uint mask = 1u << (bitIndex & 0x07);
                return (byte)(value ? bits | mask : bits & ~mask);
            }
        }

        /// <summary>
        /// Sets or unsets the specified bit according to a boolean value.
        /// </summary>
        /// <param name="bits">The byte to set / unset a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set / unset.</param>
        /// <param name="value">If true then the bit will be set to 1; otherwise to 0 (unset).</param>
        /// <remarks>
        /// Only the lowest three bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x07</c>.
        /// </remarks>
        public static void Set(ref byte bits, int bitIndex, bool value)
        {
            unchecked
            {
                uint mask = 1u << (bitIndex & 0x07);
                bits = (byte)(value ? bits | mask : bits & ~mask);
            }
        }

        // ---

        /// <summary>
        /// Takes a byte, sets or unsets bit 0 of [0:7] according to a boolean value, and returns the result.
        /// </summary>
        public static byte Set0(byte bits, bool value) => unchecked((byte)(value ? bits | B0 : bits & ~B0));

        /// <summary>
        /// Takes a byte, sets or unsets bit 1 of [0:7] according to a boolean value, and returns the result.
        /// </summary>
        public static byte Set1(byte bits, bool value) => unchecked((byte)(value ? bits | B1 : bits & ~B1));

        /// <summary>
        /// Takes a byte, sets or unsets bit 2 of [0:7] according to a boolean value, and returns the result.
        /// </summary>
        public static byte Set2(byte bits, bool value) => unchecked((byte)(value ? bits | B2 : bits & ~B2));

        /// <summary>
        /// Takes a byte, sets or unsets bit 3 of [0:7] according to a boolean value, and returns the result.
        /// </summary>
        public static byte Set3(byte bits, bool value) => unchecked((byte)(value ? bits | B3 : bits & ~B3));

        /// <summary>
        /// Takes a byte, sets or unsets bit 4 of [0:7] according to a boolean value, and returns the result.
        /// </summary>
        public static byte Set4(byte bits, bool value) => unchecked((byte)(value ? bits | B4 : bits & ~B4));

        /// <summary>
        /// Takes a byte, sets or unsets bit 5 of [0:7] according to a boolean value, and returns the result.
        /// </summary>
        public static byte Set5(byte bits, bool value) => unchecked((byte)(value ? bits | B5 : bits & ~B5));

        /// <summary>
        /// Takes a byte, sets or unsets bit 6 of [0:7] according to a boolean value, and returns the result.
        /// </summary>
        public static byte Set6(byte bits, bool value) => unchecked((byte)(value ? bits | B6 : bits & ~B6));

        /// <summary>
        /// Takes a byte, sets or unsets bit 7 of [0:7] according to a boolean value, and returns the result.
        /// </summary>
        public static byte Set7(byte bits, bool value) => unchecked((byte)(value ? bits | B7 : bits & ~B7));
        
        // ---

        /// <summary>
        /// Takes a byte, unsets the specified bit, and returns the result.
        /// </summary>
        /// <param name="bits">The byte before unsetting a bit.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest three bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x07</c>.
        /// </remarks>
        public static byte Unset(byte bits, int bitIndex) => unchecked((byte)(bits & ~(1u << (bitIndex & 0x07))));

        /// <summary>
        /// Unsets the specified bit on a byte.
        /// </summary>
        /// <param name="bits">The byte to unset a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to unset.</param>
        /// <remarks>
        /// Only the lowest three bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x07</c>.
        /// </remarks>
        public static void Unset(ref byte bits, int bitIndex) => bits &= unchecked((byte)~(1u << (bitIndex & 0x07)));

        // ---

        /// <summary>
        /// Takes a byte, unsets bit 0 of [0:7], and returns the result.
        /// </summary>
        public static byte Unset0(byte bits) => unchecked((byte)(bits & ~B0));

        /// <summary>
        /// Takes a byte, unsets bit 1 of [0:7], and returns the result.
        /// </summary>
        public static byte Unset1(byte bits) => unchecked((byte)(bits & ~B1));

        /// <summary>
        /// Takes a byte, unsets bit 2 of [0:7], and returns the result.
        /// </summary>
        public static byte Unset2(byte bits) => unchecked((byte)(bits & ~B2));

        /// <summary>
        /// Takes a byte, unsets bit 3 of [0:7], and returns the result.
        /// </summary>
        public static byte Unset3(byte bits) => unchecked((byte)(bits & ~B3));

        /// <summary>
        /// Takes a byte, unsets bit 4 of [0:7], and returns the result.
        /// </summary>
        public static byte Unset4(byte bits) => unchecked((byte)(bits & ~B4));

        /// <summary>
        /// Takes a byte, unsets bit 5 of [0:7], and returns the result.
        /// </summary>
        public static byte Unset5(byte bits) => unchecked((byte)(bits & ~B5));

        /// <summary>
        /// Takes a byte, unsets bit 6 of [0:7], and returns the result.
        /// </summary>
        public static byte Unset6(byte bits) => unchecked((byte)(bits & ~B6));

        /// <summary>
        /// Takes a byte, unsets bit 7 of [0:7], and returns the result.
        /// </summary>
        public static byte Unset7(byte bits) => unchecked((byte)(bits & ~B7));

        #endregion

        #region --- Short ---

        /// <summary>
        /// Gets the specified bit on a short, as a boolean.
        /// </summary>
        /// <param name="bits">The short to get a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to get.</param>
        /// <remarks>
        /// Only the lowest four bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x0f</c>.
        /// </remarks>
        public static bool Get(this short bits, int bitIndex) => (bits & (1 << (bitIndex & 0x0f))) != 0;

        /// <summary>
        /// Gets bit 0 of [0:15].
        /// </summary>
        public static bool Get0(this short bits) => (bits & B0) != 0;

        /// <summary>
        /// Gets bit 1 of [0:15].
        /// </summary>
        public static bool Get1(this short bits) => (bits & B1) != 0;

        /// <summary>
        /// Gets bit 2 of [0:15].
        /// </summary>
        public static bool Get2(this short bits) => (bits & B2) != 0;

        /// <summary>
        /// Gets bit 3 of [0:15].
        /// </summary>
        public static bool Get3(this short bits) => (bits & B3) != 0;

        /// <summary>
        /// Gets bit 4 of [0:15].
        /// </summary>
        public static bool Get4(this short bits) => (bits & B4) != 0;

        /// <summary>
        /// Gets bit 5 of [0:15].
        /// </summary>
        public static bool Get5(this short bits) => (bits & B5) != 0;

        /// <summary>
        /// Gets bit 6 of [0:15].
        /// </summary>
        public static bool Get6(this short bits) => (bits & B6) != 0;

        /// <summary>
        /// Gets bit 7 of [0:15].
        /// </summary>
        public static bool Get7(this short bits) => (bits & B7) != 0;

        /// <summary>
        /// Gets bit 8 of [0:15].
        /// </summary>
        public static bool Get8(this short bits) => (bits & B8) != 0;

        /// <summary>
        /// Gets bit 9 of [0:15].
        /// </summary>
        public static bool Get9(this short bits) => (bits & B9) != 0;

        /// <summary>
        /// Gets bit 10 of [0:15].
        /// </summary>
        public static bool Get10(this short bits) => (bits & B10) != 0;

        /// <summary>
        /// Gets bit 11 of [0:15].
        /// </summary>
        public static bool Get11(this short bits) => (bits & B11) != 0;

        /// <summary>
        /// Gets bit 12 of [0:15].
        /// </summary>
        public static bool Get12(this short bits) => (bits & B12) != 0;

        /// <summary>
        /// Gets bit 13 of [0:15].
        /// </summary>
        public static bool Get13(this short bits) => (bits & B13) != 0;

        /// <summary>
        /// Gets bit 14 of [0:15].
        /// </summary>
        public static bool Get14(this short bits) => (bits & B14) != 0;

        /// <summary>
        /// Gets bit 15 of [0:15].
        /// </summary>
        public static bool Get15(this short bits) => (bits & B15) != 0;

        // ---

        /// <summary>
        /// Takes a short, sets the specified bit, and returns the result.
        /// </summary>
        /// <param name="bits">The short before setting a bit.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest four bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0xf</c>.
        /// </remarks>
        public static short Set(short bits, int bitIndex) => bits |= unchecked((short)(1 << (bitIndex & 0x0f)));

        /// <summary>
        /// Sets the specified bit on a short.
        /// </summary>
        /// <param name="bits">The short to set a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest four bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x0f</c>.
        /// </remarks>
        public static void Set(ref short bits, int bitIndex) => bits |= unchecked((short)(1 << (bitIndex & 0x0f)));

        // ---

        /// <summary>
        /// Takes a short, sets bit 0 of [0:15], and returns the result.
        /// </summary>
        public static short Set0(short bits) => bits |= B0;

        /// <summary>
        /// Takes a short, sets bit 1 of [0:15], and returns the result.
        /// </summary>
        public static short Set1(short bits) => bits |= B1;

        /// <summary>
        /// Takes a short, sets bit 2 of [0:15], and returns the result.
        /// </summary>
        public static short Set2(short bits) => bits |= B2;

        /// <summary>
        /// Takes a short, sets bit 3 of [0:15], and returns the result.
        /// </summary>
        public static short Set3(short bits) => bits |= B3;

        /// <summary>
        /// Takes a short, sets bit 4 of [0:15], and returns the result.
        /// </summary>
        public static short Set4(short bits) => bits |= B4;

        /// <summary>
        /// Takes a short, sets bit 5 of [0:15], and returns the result.
        /// </summary>
        public static short Set5(short bits) => bits |= B5;

        /// <summary>
        /// Takes a short, sets bit 6 of [0:15], and returns the result.
        /// </summary>
        public static short Set6(short bits) => bits |= B6;

        /// <summary>
        /// Takes a short, sets bit 7 of [0:15], and returns the result.
        /// </summary>
        public static short Set7(short bits) => bits |= B7;

        /// <summary>
        /// Takes a short, sets bit 8 of [0:15], and returns the result.
        /// </summary>
        public static short Set8(short bits) => bits |= (short)B8;

        /// <summary>
        /// Takes a short, sets bit 9 of [0:15], and returns the result.
        /// </summary>
        public static short Set9(short bits) => bits |= (short)B9;

        /// <summary>
        /// Takes a short, sets bit 10 of [0:15], and returns the result.
        /// </summary>
        public static short Set10(short bits) => bits |= (short)B10;

        /// <summary>
        /// Takes a short, sets bit 11 of [0:15], and returns the result.
        /// </summary>
        public static short Set11(short bits) => bits |= (short)B11;

        /// <summary>
        /// Takes a short, sets bit 12 of [0:15], and returns the result.
        /// </summary>
        public static short Set12(short bits) => bits |= (short)B12;

        /// <summary>
        /// Takes a short, sets bit 13 of [0:15], and returns the result.
        /// </summary>
        public static short Set13(short bits) => bits |= (short)B13;

        /// <summary>
        /// Takes a short, sets bit 14 of [0:15], and returns the result.
        /// </summary>
        public static short Set14(short bits) => bits |= (short)B14;

        /// <summary>
        /// Takes a short, sets bit 15 of [0:15], and returns the result.
        /// </summary>
        public static short Set15(short bits) => bits |= unchecked((short)B15);

        // ---

        /// <summary>
        /// Takes a short, sets or unsets the specified bit according to a boolean value, and returns the result.
        /// </summary>
        /// <param name="bits">The short before the set / unset.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set / unset.</param>
        /// <param name="value">If true then the bit will be set to 1; otherwise to 0 (unset).</param>
        /// <remarks>
        /// Only the lowest four bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x0f</c>.
        /// </remarks>
        public static short Set(short bits, int bitIndex, bool value)
        {
            unchecked
            {
                uint mask = 1u << (bitIndex & 0x0f);
                return (short)(value ? (ushort)bits | mask : (ushort)bits & ~mask);
            }
        }

        /// <summary>
        /// Sets or unsets the specified bit according to a boolean value.
        /// </summary>
        /// <param name="bits">The short to set / unset a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set / unset.</param>
        /// <param name="value">If true then the bit will be set to 1; otherwise to 0 (unset).</param>
        /// <remarks>
        /// Only the lowest four bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x0f</c>.
        /// </remarks>
        public static void Set(ref short bits, int bitIndex, bool value)
        {
            unchecked
            {
                uint mask = 1u << (bitIndex & 0x0f);
                bits = (short)(value ? (ushort)bits | mask : (ushort)bits & ~mask);
            }
        }

        // ---

        /// <summary>
        /// Takes a short, sets or unsets bit 0 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static short Set0(short bits, bool value) => unchecked((short)(value ? bits | B0 : bits & ~B0));

        /// <summary>
        /// Takes a short, sets or unsets bit 1 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static short Set1(short bits, bool value) => unchecked((short)(value ? bits | B1 : bits & ~B1));

        /// <summary>
        /// Takes a short, sets or unsets bit 2 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static short Set2(short bits, bool value) => unchecked((short)(value ? bits | B2 : bits & ~B2));

        /// <summary>
        /// Takes a short, sets or unsets bit 3 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static short Set3(short bits, bool value) => unchecked((short)(value ? bits | B3 : bits & ~B3));

        /// <summary>
        /// Takes a short, sets or unsets bit 4 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static short Set4(short bits, bool value) => unchecked((short)(value ? bits | B4 : bits & ~B4));

        /// <summary>
        /// Takes a short, sets or unsets bit 5 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static short Set5(short bits, bool value) => unchecked((short)(value ? bits | B5 : bits & ~B5));

        /// <summary>
        /// Takes a short, sets or unsets bit 6 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static short Set6(short bits, bool value) => unchecked((short)(value ? bits | B6 : bits & ~B6));

        /// <summary>
        /// Takes a short, sets or unsets bit 7 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static short Set7(short bits, bool value) => unchecked((short)(value ? bits | B7 : bits & ~B7));

        /// <summary>
        /// Takes a short, sets or unsets bit 8 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static short Set8(short bits, bool value) => unchecked((short)(value ? bits | B8 : bits & ~B8));

        /// <summary>
        /// Takes a short, sets or unsets bit 9 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static short Set9(short bits, bool value) => unchecked((short)(value ? bits | B9 : bits & ~B9));

        /// <summary>
        /// Takes a short, sets or unsets bit 10 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static short Set10(short bits, bool value) => unchecked((short)(value ? bits | B10 : bits & ~B10));

        /// <summary>
        /// Takes a short, sets or unsets bit 11 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static short Set11(short bits, bool value) => unchecked((short)(value ? bits | B11 : bits & ~B11));

        /// <summary>
        /// Takes a short, sets or unsets bit 12 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static short Set12(short bits, bool value) => unchecked((short)(value ? bits | B12 : bits & ~B12));

        /// <summary>
        /// Takes a short, sets or unsets bit 13 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static short Set13(short bits, bool value) => unchecked((short)(value ? bits | B13 : bits & ~B13));

        /// <summary>
        /// Takes a short, sets or unsets bit 14 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static short Set14(short bits, bool value) => unchecked((short)(value ? bits | B14 : bits & ~B14));

        /// <summary>
        /// Takes a short, sets or unsets bit 15 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static short Set15(short bits, bool value) => unchecked((short)(value ? bits | B15 : bits & ~B15));

        // ---

        /// <summary>
        /// Takes a short, unsets the specified bit, and returns the result.
        /// </summary>
        /// <param name="bits">The short before unsetting a bit.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest four bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x0f</c>.
        /// </remarks>
        public static short Unset(short bits, int bitIndex) => bits &= unchecked((short)~(1u << (bitIndex & 0x0f)));

        /// <summary>
        /// Unsets the specified bit on a short.
        /// </summary>
        /// <param name="bits">The short to unset a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to unset.</param>
        /// <remarks>
        /// Only the lowest four bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x0f</c>.
        /// </remarks>
        public static void Unset(ref short bits, int bitIndex) => bits &= unchecked((short)~(1u << (bitIndex & 0x0f)));

        // ---

        /// <summary>
        /// Takes a short, unsets bit 0 of [0:15], and returns the result.
        /// </summary>
        public static short Unset0(short bits) => bits &= ~B0;

        /// <summary>
        /// Takes a short, unsets bit 1 of [0:15], and returns the result.
        /// </summary>
        public static short Unset1(short bits) => bits &= ~B1;

        /// <summary>
        /// Takes a short, unsets bit 2 of [0:15], and returns the result.
        /// </summary>
        public static short Unset2(short bits) => bits &= ~B2;

        /// <summary>
        /// Takes a short, unsets bit 3 of [0:15], and returns the result.
        /// </summary>
        public static short Unset3(short bits) => bits &= ~B3;

        /// <summary>
        /// Takes a short, unsets bit 4 of [0:15], and returns the result.
        /// </summary>
        public static short Unset4(short bits) => bits &= ~B4;

        /// <summary>
        /// Takes a short, unsets bit 5 of [0:15], and returns the result.
        /// </summary>
        public static short Unset5(short bits) => bits &= ~B5;

        /// <summary>
        /// Takes a short, unsets bit 6 of [0:15], and returns the result.
        /// </summary>
        public static short Unset6(short bits) => bits &= ~B6;

        /// <summary>
        /// Takes a short, unsets bit 7 of [0:15], and returns the result.
        /// </summary>
        public static short Unset7(short bits) => bits &= ~B7;

        /// <summary>
        /// Takes a short, unsets bit 8 of [0:15], and returns the result.
        /// </summary>
        public static short Unset8(short bits) => bits &= ~B8;

        /// <summary>
        /// Takes a short, unsets bit 9 of [0:15], and returns the result.
        /// </summary>
        public static short Unset9(short bits) => bits &= ~B9;

        /// <summary>
        /// Takes a short, unsets bit 10 of [0:15], and returns the result.
        /// </summary>
        public static short Unset10(short bits) => bits &= ~B10;

        /// <summary>
        /// Takes a short, unsets bit 11 of [0:15], and returns the result.
        /// </summary>
        public static short Unset11(short bits) => bits &= ~B11;

        /// <summary>
        /// Takes a short, unsets bit 12 of [0:15], and returns the result.
        /// </summary>
        public static short Unset12(short bits) => bits &= ~B12;

        /// <summary>
        /// Takes a short, unsets bit 13 of [0:15], and returns the result.
        /// </summary>
        public static short Unset13(short bits) => bits &= ~B13;

        /// <summary>
        /// Takes a short, unsets bit 14 of [0:15], and returns the result.
        /// </summary>
        public static short Unset14(short bits) => bits &= ~B14;

        /// <summary>
        /// Takes a short, unsets bit 15 of [0:15], and returns the result.
        /// </summary>
        public static short Unset15(short bits) => bits &= unchecked((short)~B15);

        #endregion

        #region --- UShort ---

        /// <summary>
        /// Gets the specified bit on an unsigned short, as a boolean.
        /// </summary>
        /// <param name="bits">The unsigned short to get a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to get.</param>
        /// <remarks>
        /// Only the lowest four bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x0f</c>.
        /// </remarks>
        public static bool Get(this ushort bits, int bitIndex) => (bits & (1 << (bitIndex & 0x0f))) != 0;

        /// <summary>
        /// Gets bit 0 of [0:15].
        /// </summary>
        public static bool Get0(this ushort bits) => (bits & B0) != 0;

        /// <summary>
        /// Gets bit 1 of [0:15].
        /// </summary>
        public static bool Get1(this ushort bits) => (bits & B1) != 0;

        /// <summary>
        /// Gets bit 2 of [0:15].
        /// </summary>
        public static bool Get2(this ushort bits) => (bits & B2) != 0;

        /// <summary>
        /// Gets bit 3 of [0:15].
        /// </summary>
        public static bool Get3(this ushort bits) => (bits & B3) != 0;

        /// <summary>
        /// Gets bit 4 of [0:15].
        /// </summary>
        public static bool Get4(this ushort bits) => (bits & B4) != 0;

        /// <summary>
        /// Gets bit 5 of [0:15].
        /// </summary>
        public static bool Get5(this ushort bits) => (bits & B5) != 0;

        /// <summary>
        /// Gets bit 6 of [0:15].
        /// </summary>
        public static bool Get6(this ushort bits) => (bits & B6) != 0;

        /// <summary>
        /// Gets bit 7 of [0:15].
        /// </summary>
        public static bool Get7(this ushort bits) => (bits & B7) != 0;

        /// <summary>
        /// Gets bit 8 of [0:15].
        /// </summary>
        public static bool Get8(this ushort bits) => (bits & B8) != 0;

        /// <summary>
        /// Gets bit 9 of [0:15].
        /// </summary>
        public static bool Get9(this ushort bits) => (bits & B9) != 0;

        /// <summary>
        /// Gets bit 10 of [0:15].
        /// </summary>
        public static bool Get10(this ushort bits) => (bits & B10) != 0;

        /// <summary>
        /// Gets bit 11 of [0:15].
        /// </summary>
        public static bool Get11(this ushort bits) => (bits & B11) != 0;

        /// <summary>
        /// Gets bit 12 of [0:15].
        /// </summary>
        public static bool Get12(this ushort bits) => (bits & B12) != 0;

        /// <summary>
        /// Gets bit 13 of [0:15].
        /// </summary>
        public static bool Get13(this ushort bits) => (bits & B13) != 0;

        /// <summary>
        /// Gets bit 14 of [0:15].
        /// </summary>
        public static bool Get14(this ushort bits) => (bits & B14) != 0;

        /// <summary>
        /// Gets bit 15 of [0:15].
        /// </summary>
        public static bool Get15(this ushort bits) => (bits & B15) != 0;

        // ---

        /// <summary>
        /// Takes an unsigned short, sets the specified bit, and returns the result.
        /// </summary>
        /// <param name="bits">The unsigned short before setting a bit.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest four bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x0f</c>.
        /// </remarks>
        public static ushort Set(ushort bits, int bitIndex) => unchecked((ushort)(bits | (1u << (bitIndex & 0x0f))));

        /// <summary>
        /// Sets the specified bit on an unsigned short.
        /// </summary>
        /// <param name="bits">The unsigned short to set a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest four bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x0f</c>.
        /// </remarks>
        public static void Set(ref ushort bits, int bitIndex) => bits |= unchecked((ushort)(1 << (bitIndex & 0x0f)));

        // ---

        /// <summary>
        /// Takes a unsigned short, sets bit 0 of [0:15], and returns the result.
        /// </summary>
        public static ushort Set0(ushort bits) => bits |= B0;

        /// <summary>
        /// Takes an unsigned short, sets bit 1 of [0:15], and returns the result.
        /// </summary>
        public static ushort Set1(ushort bits) => bits |= B1;

        /// <summary>
        /// Takes an unsigned short, sets bit 2 of [0:15], and returns the result.
        /// </summary>
        public static ushort Set2(ushort bits) => bits |= B2;

        /// <summary>
        /// Takes an unsigned short, sets bit 3 of [0:15], and returns the result.
        /// </summary>
        public static ushort Set3(ushort bits) => bits |= B3;

        /// <summary>
        /// Takes an unsigned short, sets bit 4 of [0:15], and returns the result.
        /// </summary>
        public static ushort Set4(ushort bits) => bits |= B4;

        /// <summary>
        /// Takes an unsigned short, sets bit 5 of [0:15], and returns the result.
        /// </summary>
        public static ushort Set5(ushort bits) => bits |= B5;

        /// <summary>
        /// Takes an unsigned short, sets bit 6 of [0:15], and returns the result.
        /// </summary>
        public static ushort Set6(ushort bits) => bits |= B6;

        /// <summary>
        /// Takes an unsigned short, sets bit 7 of [0:15], and returns the result.
        /// </summary>
        public static ushort Set7(ushort bits) => bits |= B7;

        /// <summary>
        /// Takes an unsigned short, sets bit 8 of [0:15], and returns the result.
        /// </summary>
        public static ushort Set8(ushort bits) => bits |= B8;

        /// <summary>
        /// Takes an unsigned short, sets bit 9 of [0:15], and returns the result.
        /// </summary>
        public static ushort Set9(ushort bits) => bits |= B9;

        /// <summary>
        /// Takes an unsigned short, sets bit 10 of [0:15], and returns the result.
        /// </summary>
        public static ushort Set10(ushort bits) => bits |= B10;

        /// <summary>
        /// Takes an unsigned short, sets bit 11 of [0:15], and returns the result.
        /// </summary>
        public static ushort Set11(ushort bits) => bits |= B11;

        /// <summary>
        /// Takes an unsigned short, sets bit 12 of [0:15], and returns the result.
        /// </summary>
        public static ushort Set12(ushort bits) => bits |= B12;

        /// <summary>
        /// Takes an unsigned short, sets bit 13 of [0:15], and returns the result.
        /// </summary>
        public static ushort Set13(ushort bits) => bits |= B13;

        /// <summary>
        /// Takes an unsigned short, sets bit 14 of [0:15], and returns the result.
        /// </summary>
        public static ushort Set14(ushort bits) => bits |= B14;

        /// <summary>
        /// Takes an unsigned short, sets bit 15 of [0:15], and returns the result.
        /// </summary>
        public static ushort Set15(ushort bits) => bits |= B15;

        // ---

        /// <summary>
        /// Takes an unsigned short, sets or unsets the specified bit according to a boolean value, and returns the result.
        /// </summary>
        /// <param name="bits">The unsigned short before the set / unset.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set / unset.</param>
        /// <param name="value">If true then the bit will be set to 1; otherwise to 0 (unset).</param>
        /// <remarks>
        /// Only the lowest four bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x0f</c>.
        /// </remarks>
        public static ushort Set(ushort bits, int bitIndex, bool value)
        {
            unchecked
            {
                uint mask = 1u << (bitIndex & 0x0f);
                return (ushort)(value ? bits | mask : bits & ~mask);
            }
        }

        /// <summary>
        /// Sets or unsets the specified bit according to a boolean value.
        /// </summary>
        /// <param name="bits">The unsigned short to set / unset a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set / unset.</param>
        /// <param name="value">If true then the bit will be set to 1; otherwise to 0 (unset).</param>
        /// <remarks>
        /// Only the lowest four bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x00f</c>.
        /// </remarks>
        public static void Set(ref ushort bits, int bitIndex, bool value)
        {
            unchecked
            {
                uint mask = 1u << (bitIndex & 0x0f);
                bits = (ushort)(value ? bits | mask : bits & ~mask);
            }
        }

        // ---

        /// <summary>
        /// Takes an unsigned short, sets or unsets bit 0 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static ushort Set0(ushort bits, bool value) => unchecked((ushort)(value ? bits | B0 : bits & ~B0));

        /// <summary>
        /// Takes an unsigned short, sets or unsets bit 1 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static ushort Set1(ushort bits, bool value) => unchecked((ushort)(value ? bits | B1 : bits & ~B1));

        /// <summary>
        /// Takes an unsigned short, sets or unsets bit 2 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static ushort Set2(ushort bits, bool value) => unchecked((ushort)(value ? bits | B2 : bits & ~B2));

        /// <summary>
        /// Takes an unsigned short, sets or unsets bit 3 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static ushort Set3(ushort bits, bool value) => unchecked((ushort)(value ? bits | B3 : bits & ~B3));

        /// <summary>
        /// Takes an unsigned short, sets or unsets bit 4 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static ushort Set4(ushort bits, bool value) => unchecked((ushort)(value ? bits | B4 : bits & ~B4));

        /// <summary>
        /// Takes an unsigned short, sets or unsets bit 5 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static ushort Set5(ushort bits, bool value) => unchecked((ushort)(value ? bits | B5 : bits & ~B5));

        /// <summary>
        /// Takes an unsigned short, sets or unsets bit 6 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static ushort Set6(ushort bits, bool value) => unchecked((ushort)(value ? bits | B6 : bits & ~B6));

        /// <summary>
        /// Takes an unsigned short, sets or unsets bit 7 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static ushort Set7(ushort bits, bool value) => unchecked((ushort)(value ? bits | B7 : bits & ~B7));

        /// <summary>
        /// Takes an unsigned short, sets or unsets bit 8 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static ushort Set8(ushort bits, bool value) => unchecked((ushort)(value ? bits | B8 : bits & ~B8));

        /// <summary>
        /// Takes an unsigned short, sets or unsets bit 9 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static ushort Set9(ushort bits, bool value) => unchecked((ushort)(value ? bits | B9 : bits & ~B9));

        /// <summary>
        /// Takes an unsigned short, sets or unsets bit 10 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static ushort Set10(ushort bits, bool value) => unchecked((ushort)(value ? bits | B10 : bits & ~B10));

        /// <summary>
        /// Takes an unsigned short, sets or unsets bit 11 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static ushort Set11(ushort bits, bool value) => unchecked((ushort)(value ? bits | B11 : bits & ~B11));

        /// <summary>
        /// Takes an unsigned short, sets or unsets bit 12 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static ushort Set12(ushort bits, bool value) => unchecked((ushort)(value ? bits | B12 : bits & ~B12));

        /// <summary>
        /// Takes an unsigned short, sets or unsets bit 13 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static ushort Set13(ushort bits, bool value) => unchecked((ushort)(value ? bits | B13 : bits & ~B13));

        /// <summary>
        /// Takes an unsigned short, sets or unsets bit 14 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static ushort Set14(ushort bits, bool value) => unchecked((ushort)(value ? bits | B14 : bits & ~B14));

        /// <summary>
        /// Takes an unsigned short, sets or unsets bit 15 of [0:15] according to a boolean value, and returns the result.
        /// </summary>
        public static ushort Set15(ushort bits, bool value) => unchecked((ushort)(value ? bits | B15 : bits & ~B15));

        // ---

        /// <summary>
        /// Takes an unsigned short, unsets the specified bit, and returns the result.
        /// </summary>
        /// <param name="bits">The unsigned short before unsetting a bit.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest four bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x0f</c>.
        /// </remarks>
        public static ushort Unset(ushort bits, int bitIndex) => bits &= unchecked((ushort)~(1u << (bitIndex & 0x0f)));

        /// <summary>
        /// Unsets the specified bit on an unsigned short.
        /// </summary>
        /// <param name="bits">The unsigned short to unset a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to unset.</param>
        /// <remarks>
        /// Only the lowest four bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x0f</c>.
        /// </remarks>
        public static void Unset(ref ushort bits, int bitIndex) => bits &= unchecked((ushort)~(1u << (bitIndex & 0x0f)));

        // ---

        /// <summary>
        /// Takes an unsigned short, unsets bit 0 of [0:15], and returns the result.
        /// </summary>
        public static ushort Unset0(ushort bits) => unchecked((ushort)(bits & ~B0));

        /// <summary>
        /// Takes an unsigned short, unsets bit 1 of [0:15], and returns the result.
        /// </summary>
        public static ushort Unset1(ushort bits) => unchecked((ushort)(bits & ~B1));

        /// <summary>
        /// Takes an unsigned short, unsets bit 2 of [0:15], and returns the result.
        /// </summary>
        public static ushort Unset2(ushort bits) => unchecked((ushort)(bits & ~B2));

        /// <summary>
        /// Takes an unsigned short, unsets bit 3 of [0:15], and returns the result.
        /// </summary>
        public static ushort Unset3(ushort bits) => unchecked((ushort)(bits & ~B3));

        /// <summary>
        /// Takes an unsigned short, unsets bit 4 of [0:15], and returns the result.
        /// </summary>
        public static ushort Unset4(ushort bits) => unchecked((ushort)(bits & ~B4));

        /// <summary>
        /// Takes an unsigned short, unsets bit 5 of [0:15], and returns the result.
        /// </summary>
        public static ushort Unset5(ushort bits) => unchecked((ushort)(bits & ~B5));

        /// <summary>
        /// Takes an unsigned short, unsets bit 6 of [0:15], and returns the result.
        /// </summary>
        public static ushort Unset6(ushort bits) => unchecked((ushort)(bits & ~B6));

        /// <summary>
        /// Takes an unsigned short, unsets bit 7 of [0:15], and returns the result.
        /// </summary>
        public static ushort Unset7(ushort bits) => unchecked((ushort)(bits & ~B7));

        /// <summary>
        /// Takes an unsigned short, unsets bit 8 of [0:15], and returns the result.
        /// </summary>
        public static ushort Unset8(ushort bits) => unchecked((ushort)(bits & ~B8));

        /// <summary>
        /// Takes an unsigned short, unsets bit 9 of [0:15], and returns the result.
        /// </summary>
        public static ushort Unset9(ushort bits) => unchecked((ushort)(bits & ~B9));

        /// <summary>
        /// Takes an unsigned short, unsets bit 10 of [0:15], and returns the result.
        /// </summary>
        public static ushort Unset10(ushort bits) => unchecked((ushort)(bits & ~B10));

        /// <summary>
        /// Takes an unsigned short, unsets bit 11 of [0:15], and returns the result.
        /// </summary>
        public static ushort Unset11(ushort bits) => unchecked((ushort)(bits & ~B11));

        /// <summary>
        /// Takes an unsigned short, unsets bit 12 of [0:15], and returns the result.
        /// </summary>
        public static ushort Unset12(ushort bits) => unchecked((ushort)(bits & ~B12));

        /// <summary>
        /// Takes an unsigned short, unsets bit 13 of [0:15], and returns the result.
        /// </summary>
        public static ushort Unset13(ushort bits) => unchecked((ushort)(bits & ~B13));

        /// <summary>
        /// Takes an unsigned short, unsets bit 14 of [0:15], and returns the result.
        /// </summary>
        public static ushort Unset14(ushort bits) => unchecked((ushort)(bits & ~B14));

        /// <summary>
        /// Takes an unsigned short, unsets bit 15 of [0:15], and returns the result.
        /// </summary>
        public static ushort Unset15(ushort bits) => unchecked((ushort)(bits & ~B15));

        #endregion

        #region --- Int ---

        /// <summary>
        /// Gets the specified bit on an int, as a boolean.
        /// </summary>
        /// <param name="bits">The int to get a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to get.</param>
        /// <remarks>
        /// Only the lowest five bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x1f</c>.
        /// </remarks>
        public static bool Get(this int bits, int bitIndex) => (bits & (1 << (bitIndex & 0x1f))) != 0;

        /// <summary>
        /// Gets bit 0 of [0:31].
        /// </summary>
        public static bool Get0(this int bits) => (bits & B0) != 0;

        /// <summary>
        /// Gets bit 1 of [0:31].
        /// </summary>
        public static bool Get1(this int bits) => (bits & B1) != 0;

        /// <summary>
        /// Gets bit 2 of [0:31].
        /// </summary>
        public static bool Get2(this int bits) => (bits & B2) != 0;

        /// <summary>
        /// Gets bit 3 of [0:31].
        /// </summary>
        public static bool Get3(this int bits) => (bits & B3) != 0;

        /// <summary>
        /// Gets bit 4 of [0:31].
        /// </summary>
        public static bool Get4(this int bits) => (bits & B4) != 0;

        /// <summary>
        /// Gets bit 5 of [0:31].
        /// </summary>
        public static bool Get5(this int bits) => (bits & B5) != 0;

        /// <summary>
        /// Gets bit 6 of [0:31].
        /// </summary>
        public static bool Get6(this int bits) => (bits & B6) != 0;

        /// <summary>
        /// Gets bit 7 of [0:31].
        /// </summary>
        public static bool Get7(this int bits) => (bits & B7) != 0;

        /// <summary>
        /// Gets bit 8 of [0:31].
        /// </summary>
        public static bool Get8(this int bits) => (bits & B8) != 0;

        /// <summary>
        /// Gets bit 9 of [0:31].
        /// </summary>
        public static bool Get9(this int bits) => (bits & B9) != 0;

        /// <summary>
        /// Gets bit 10 of [0:31].
        /// </summary>
        public static bool Get10(this int bits) => (bits & B10) != 0;

        /// <summary>
        /// Gets bit 11 of [0:31].
        /// </summary>
        public static bool Get11(this int bits) => (bits & B11) != 0;

        /// <summary>
        /// Gets bit 12 of [0:31].
        /// </summary>
        public static bool Get12(this int bits) => (bits & B12) != 0;

        /// <summary>
        /// Gets bit 13 of [0:31].
        /// </summary>
        public static bool Get13(this int bits) => (bits & B13) != 0;

        /// <summary>
        /// Gets bit 14 of [0:31].
        /// </summary>
        public static bool Get14(this int bits) => (bits & B14) != 0;

        /// <summary>
        /// Gets bit 15 of [0:31].
        /// </summary>
        public static bool Get15(this int bits) => (bits & B15) != 0;

        /// <summary>
        /// Gets bit 16 of [0:31].
        /// </summary>
        public static bool Get16(this int bits) => (bits & (int)B16) != 0;

        /// <summary>
        /// Gets bit 17 of [0:31].
        /// </summary>
        public static bool Get17(this int bits) => (bits & (int)B17) != 0;

        /// <summary>
        /// Gets bit 18 of [0:31].
        /// </summary>
        public static bool Get18(this int bits) => (bits & (int)B18) != 0;

        /// <summary>
        /// Gets bit 19 of [0:31].
        /// </summary>
        public static bool Get19(this int bits) => (bits & (int)B19) != 0;

        /// <summary>
        /// Gets bit 20 of [0:31].
        /// </summary>
        public static bool Get20(this int bits) => (bits & (int)B20) != 0;

        /// <summary>
        /// Gets bit 21 of [0:31].
        /// </summary>
        public static bool Get21(this int bits) => (bits & (int)B21) != 0;

        /// <summary>
        /// Gets bit 22 of [0:31].
        /// </summary>
        public static bool Get22(this int bits) => (bits & (int)B22) != 0;

        /// <summary>
        /// Gets bit 23 of [0:31].
        /// </summary>
        public static bool Get23(this int bits) => (bits & (int)B23) != 0;

        /// <summary>
        /// Gets bit 24 of [0:31].
        /// </summary>
        public static bool Get24(this int bits) => (bits & (int)B24) != 0;

        /// <summary>
        /// Gets bit 25 of [0:31].
        /// </summary>
        public static bool Get25(this int bits) => (bits & (int)B25) != 0;

        /// <summary>
        /// Gets bit 26 of [0:31].
        /// </summary>
        public static bool Get26(this int bits) => (bits & (int)B26) != 0;

        /// <summary>
        /// Gets bit 27 of [0:31].
        /// </summary>
        public static bool Get27(this int bits) => (bits & (int)B27) != 0;

        /// <summary>
        /// Gets bit 28 of [0:31].
        /// </summary>
        public static bool Get28(this int bits) => (bits & (int)B28) != 0;

        /// <summary>
        /// Gets bit 29 of [0:31].
        /// </summary>
        public static bool Get29(this int bits) => (bits & (int)B29) != 0;

        /// <summary>
        /// Gets bit 30 of [0:31].
        /// </summary>
        public static bool Get30(this int bits) => (bits & (int)B30) != 0;

        /// <summary>
        /// Gets bit 31 of [0:31].
        /// </summary>
        public static bool Get31(this int bits) => (bits & unchecked((int)B31)) != 0;

        // ---

        /// <summary>
        /// Takes an int, sets the specified bit, and returns the result.
        /// </summary>
        /// <param name="bits">The int before setting a bit.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest five bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x1f</c>.
        /// </remarks>
        public static int Set(int bits, int bitIndex) => bits | (1 << (bitIndex & 0x1f));

        /// <summary>
        /// Sets the specified bit on an int.
        /// </summary>
        /// <param name="bits">The int to set a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest five bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x1f</c>.
        /// </remarks>
        public static void Set(ref int bits, int bitIndex) => bits |= (1 << (bitIndex & 0x1f));

        // ---

        /// <summary>
        /// Takes an int, sets bit 0 of [0:31], and returns the result.
        /// </summary>
        public static int Set0(int bits) => bits | B0;

        /// <summary>
        /// Takes an int, sets bit 1 of [0:31], and returns the result.
        /// </summary>
        public static int Set1(int bits) => bits | B1;

        /// <summary>
        /// Takes an int, sets bit 2 of [0:31], and returns the result.
        /// </summary>
        public static int Set2(int bits) => bits | B2;

        /// <summary>
        /// Takes an int, sets bit 3 of [0:31], and returns the result.
        /// </summary>
        public static int Set3(int bits) => bits | B3;

        /// <summary>
        /// Takes an int, sets bit 4 of [0:31], and returns the result.
        /// </summary>
        public static int Set4(int bits) => bits | B4;

        /// <summary>
        /// Takes an int, sets bit 5 of [0:31], and returns the result.
        /// </summary>
        public static int Set5(int bits) => bits | B5;

        /// <summary>
        /// Takes an int, sets bit 6 of [0:31], and returns the result.
        /// </summary>
        public static int Set6(int bits) => bits | B6;

        /// <summary>
        /// Takes an int, sets bit 7 of [0:31], and returns the result.
        /// </summary>
        public static int Set7(int bits) => bits | B7;

        /// <summary>
        /// Takes an int, sets bit 8 of [0:31], and returns the result.
        /// </summary>
        public static int Set8(int bits) => bits | B8;

        /// <summary>
        /// Takes an int, sets bit 9 of [0:31], and returns the result.
        /// </summary>
        public static int Set9(int bits) => bits | B9;

        /// <summary>
        /// Takes an int, sets bit 10 of [0:31], and returns the result.
        /// </summary>
        public static int Set10(int bits) => bits | B10;

        /// <summary>
        /// Takes an int, sets bit 11 of [0:31], and returns the result.
        /// </summary>
        public static int Set11(int bits) => bits | B11;

        /// <summary>
        /// Takes an int, sets bit 12 of [0:31], and returns the result.
        /// </summary>
        public static int Set12(int bits) => bits | B12;

        /// <summary>
        /// Takes an int, sets bit 13 of [0:31], and returns the result.
        /// </summary>
        public static int Set13(int bits) => bits | B13;

        /// <summary>
        /// Takes an int, sets bit 14 of [0:31], and returns the result.
        /// </summary>
        public static int Set14(int bits) => bits | B14;

        /// <summary>
        /// Takes an int, sets bit 15 of [0:31], and returns the result.
        /// </summary>
        public static int Set15(int bits) => bits | B15;

        /// <summary>
        /// Takes an int, sets bit 16 of [0:31], and returns the result.
        /// </summary>
        public static int Set16(int bits) => bits | (int)B16;

        /// <summary>
        /// Takes an int, sets bit 17 of [0:31], and returns the result.
        /// </summary>
        public static int Set17(int bits) => bits | (int)B17;

        /// <summary>
        /// Takes an int, sets bit 18 of [0:31], and returns the result.
        /// </summary>
        public static int Set18(int bits) => bits | (int)B18;

        /// <summary>
        /// Takes an int, sets bit 19 of [0:31], and returns the result.
        /// </summary>
        public static int Set19(int bits) => bits | (int)B19;

        /// <summary>
        /// Takes an int, sets bit 20 of [0:31], and returns the result.
        /// </summary>
        public static int Set20(int bits) => bits | (int)B20;

        /// <summary>
        /// Takes an int, sets bit 21 of [0:31], and returns the result.
        /// </summary>
        public static int Set21(int bits) => bits | (int)B21;

        /// <summary>
        /// Takes an int, sets bit 22 of [0:31], and returns the result.
        /// </summary>
        public static int Set22(int bits) => bits | (int)B22;

        /// <summary>
        /// Takes an int, sets bit 23 of [0:31], and returns the result.
        /// </summary>
        public static int Set23(int bits) => bits | (int)B23;

        /// <summary>
        /// Takes an int, sets bit 24 of [0:31], and returns the result.
        /// </summary>
        public static int Set24(int bits) => bits | (int)B24;

        /// <summary>
        /// Takes an int, sets bit 25 of [0:31], and returns the result.
        /// </summary>
        public static int Set25(int bits) => bits | (int)B25;

        /// <summary>
        /// Takes an int, sets bit 26 of [0:31], and returns the result.
        /// </summary>
        public static int Set26(int bits) => bits | (int)B26;

        /// <summary>
        /// Takes an int, sets bit 27 of [0:31], and returns the result.
        /// </summary>
        public static int Set27(int bits) => bits | (int)B27;

        /// <summary>
        /// Takes an int, sets bit 28 of [0:31], and returns the result.
        /// </summary>
        public static int Set28(int bits) => bits | (int)B28;

        /// <summary>
        /// Takes an int, sets bit 29 of [0:31], and returns the result.
        /// </summary>
        public static int Set29(int bits) => bits | (int)B29;

        /// <summary>
        /// Takes an int, sets bit 30 of [0:31], and returns the result.
        /// </summary>
        public static int Set30(int bits) => bits | (int)B30;

        /// <summary>
        /// Takes an int, sets bit 31 of [0:31], and returns the result.
        /// </summary>
        public static int Set31(int bits) => bits | unchecked((int)B31);

        // ---

        /// <summary>
        /// Takes an int, sets or unsets the specified bit according to a boolean value, and returns the result.
        /// </summary>
        /// <param name="bits">The int before the set / unset.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set / unset.</param>
        /// <param name="value">If true then the bit will be set to 1; otherwise to 0 (unset).</param>
        /// <remarks>
        /// Only the lowest five bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x1f</c>.
        /// </remarks>
        public static int Set(int bits, int bitIndex, bool value)
        {
            unchecked
            {
                int mask = 1 << (bitIndex & 0x1f);
                return value ? bits | mask : bits & ~mask;
            }
        }

        /// <summary>
        /// Sets or unsets the specified bit according to a boolean value.
        /// </summary>
        /// <param name="bits">The int to set / unset a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set / unset.</param>
        /// <param name="value">If true then the bit will be set to 1; otherwise to 0 (unset).</param>
        /// <remarks>
        /// Only the lowest five bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x1f</c>.
        /// </remarks>
        public static void Set(ref int bits, int bitIndex, bool value)
        {
            unchecked
            {
                int mask = 1 << (bitIndex & 0x1f);
                bits = value ? bits | mask : bits & ~mask;
            }
        }

        // ---

        /// <summary>
        /// Takes an int, sets or unsets bit 0 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set0(int bits, bool value) => value ? bits | B0 : bits & ~B0;

        /// <summary>
        /// Takes an int, sets or unsets bit 1 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set1(int bits, bool value) => value ? bits | B1 : bits & ~B1;

        /// <summary>
        /// Takes an int, sets or unsets bit 2 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set2(int bits, bool value) => value ? bits | B2 : bits & ~B2;

        /// <summary>
        /// Takes an int, sets or unsets bit 3 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set3(int bits, bool value) => value ? bits | B3 : bits & ~B3;

        /// <summary>
        /// Takes an int, sets or unsets bit 4 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set4(int bits, bool value) => value ? bits | B4 : bits & ~B4;

        /// <summary>
        /// Takes an int, sets or unsets bit 5 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set5(int bits, bool value) => value ? bits | B5 : bits & ~B5;

        /// <summary>
        /// Takes an int, sets or unsets bit 6 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set6(int bits, bool value) => value ? bits | B6 : bits & ~B6;

        /// <summary>
        /// Takes an int, sets or unsets bit 7 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set7(int bits, bool value) => value ? bits | B7 : bits & ~B7;

        /// <summary>
        /// Takes an int, sets or unsets bit 8 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set8(int bits, bool value) => value ? bits | B8 : bits & ~B8;

        /// <summary>
        /// Takes an int, sets or unsets bit 9 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set9(int bits, bool value) => value ? bits | B9 : bits & ~B9;

        /// <summary>
        /// Takes an int, sets or unsets bit 10 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set10(int bits, bool value) => value ? bits | B10 : bits & ~B10;

        /// <summary>
        /// Takes an int, sets or unsets bit 11 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set11(int bits, bool value) => value ? bits | B11 : bits & ~B11;

        /// <summary>
        /// Takes an int, sets or unsets bit 12 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set12(int bits, bool value) => value ? bits | B12 : bits & ~B12;

        /// <summary>
        /// Takes an int, sets or unsets bit 13 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set13(int bits, bool value) => value ? bits | B13 : bits & ~B13;

        /// <summary>
        /// Takes an int, sets or unsets bit 14 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set14(int bits, bool value) => value ? bits | B14 : bits & ~B14;

        /// <summary>
        /// Takes an int, sets or unsets bit 15 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set15(int bits, bool value) => value ? bits | B15 : bits & ~B15;

        /// <summary>
        /// Takes an int, sets or unsets bit 16 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set16(int bits, bool value) => value ? bits | (int)B16 : bits & ~(int)B16;

        /// <summary>
        /// Takes an int, sets or unsets bit 17 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set17(int bits, bool value) => value ? bits | (int)B17 : bits & ~(int)B17;

        /// <summary>
        /// Takes an int, sets or unsets bit 18 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set18(int bits, bool value) => value ? bits | (int)B18 : bits & ~(int)B18;

        /// <summary>
        /// Takes an int, sets or unsets bit 19 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set19(int bits, bool value) => value ? bits | (int)B19 : bits & ~(int)B19;

        /// <summary>
        /// Takes an int, sets or unsets bit 20 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set20(int bits, bool value) => value ? bits | (int)B20 : bits & ~(int)B20;

        /// <summary>
        /// Takes an int, sets or unsets bit 21 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set21(int bits, bool value) => value ? bits | (int)B21 : bits & ~(int)B21;

        /// <summary>
        /// Takes an int, sets or unsets bit 22 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set22(int bits, bool value) => value ? bits | (int)B22 : bits & ~(int)B22;

        /// <summary>
        /// Takes an int, sets or unsets bit 23 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set23(int bits, bool value) => value ? bits | (int)B23 : bits & ~(int)B23;

        /// <summary>
        /// Takes an int, sets or unsets bit 24 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set24(int bits, bool value) => value ? bits | (int)B24 : bits & ~(int)B24;

        /// <summary>
        /// Takes an int, sets or unsets bit 25 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set25(int bits, bool value) => value ? bits | (int)B25 : bits & ~(int)B25;

        /// <summary>
        /// Takes an int, sets or unsets bit 26 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set26(int bits, bool value) => value ? bits | (int)B26 : bits & ~(int)B26;

        /// <summary>
        /// Takes an int, sets or unsets bit 27 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set27(int bits, bool value) => value ? bits | (int)B27 : bits & ~(int)B27;

        /// <summary>
        /// Takes an int, sets or unsets bit 28 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set28(int bits, bool value) => value ? bits | (int)B28 : bits & ~(int)B28;

        /// <summary>
        /// Takes an int, sets or unsets bit 29 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set29(int bits, bool value) => value ? bits | (int)B29 : bits & ~(int)B29;

        /// <summary>
        /// Takes an int, sets or unsets bit 30 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set30(int bits, bool value) => value ? bits | (int)B30 : bits & ~(int)B30;

        /// <summary>
        /// Takes an int, sets or unsets bit 31 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static int Set31(int bits, bool value) => value ? bits | unchecked((int)B31) : bits & (int)~B31;

        // ---

        /// <summary>
        /// Takes an int, unsets the specified bit, and returns the result.
        /// </summary>
        /// <param name="bits">The int before unsetting a bit.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest five bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x1f</c>.
        /// </remarks>
        public static int Unset(int bits, int bitIndex) => bits & ~(1 << (bitIndex & 0x1f));

        /// <summary>
        /// Unsets the specified bit on an int.
        /// </summary>
        /// <param name="bits">The int to unset a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to unset.</param>
        /// <remarks>
        /// Only the lowest five bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x1f</c>.
        /// </remarks>
        public static void Unset(ref int bits, int bitIndex) => bits &= ~(1 << (bitIndex & 0x1f));

        // ---

        /// <summary>
        /// Takes an int, unsets bit 0 of [0:31], and returns the result.
        /// </summary>
        public static int Unset0(int bits) => bits & ~B0;

        /// <summary>
        /// Takes an int, unsets bit 1 of [0:31], and returns the result.
        /// </summary>
        public static int Unset1(int bits) => bits & ~B1;

        /// <summary>
        /// Takes an int, unsets bit 2 of [0:31], and returns the result.
        /// </summary>
        public static int Unset2(int bits) => bits & ~B2;

        /// <summary>
        /// Takes an int, unsets bit 3 of [0:31], and returns the result.
        /// </summary>
        public static int Unset3(int bits) => bits & ~B3;

        /// <summary>
        /// Takes an int, unsets bit 4 of [0:31], and returns the result.
        /// </summary>
        public static int Unset4(int bits) => bits & ~B4;

        /// <summary>
        /// Takes an int, unsets bit 5 of [0:31], and returns the result.
        /// </summary>
        public static int Unset5(int bits) => bits & ~B5;

        /// <summary>
        /// Takes an int, unsets bit 6 of [0:31], and returns the result.
        /// </summary>
        public static int Unset6(int bits) => bits & ~B6;

        /// <summary>
        /// Takes an int, unsets bit 7 of [0:31], and returns the result.
        /// </summary>
        public static int Unset7(int bits) => bits & ~B7;

        /// <summary>
        /// Takes an int, unsets bit 8 of [0:31], and returns the result.
        /// </summary>
        public static int Unset8(int bits) => bits & ~B8;

        /// <summary>
        /// Takes an int, unsets bit 9 of [0:31], and returns the result.
        /// </summary>
        public static int Unset9(int bits) => bits & ~B9;

        /// <summary>
        /// Takes an int, unsets bit 10 of [0:31], and returns the result.
        /// </summary>
        public static int Unset10(int bits) => bits & ~B10;

        /// <summary>
        /// Takes an int, unsets bit 11 of [0:31], and returns the result.
        /// </summary>
        public static int Unset11(int bits) => bits & ~B11;

        /// <summary>
        /// Takes an int, unsets bit 12 of [0:31], and returns the result.
        /// </summary>
        public static int Unset12(int bits) => bits & ~B12;

        /// <summary>
        /// Takes an int, unsets bit 13 of [0:31], and returns the result.
        /// </summary>
        public static int Unset13(int bits) => bits & ~B13;

        /// <summary>
        /// Takes an int, unsets bit 14 of [0:31], and returns the result.
        /// </summary>
        public static int Unset14(int bits) => bits & ~B14;

        /// <summary>
        /// Takes an int, unsets bit 15 of [0:31], and returns the result.
        /// </summary>
        public static int Unset15(int bits) => bits & ~B15;

        /// <summary>
        /// Takes an int, unsets bit 16 of [0:31], and returns the result.
        /// </summary>
        public static int Unset16(int bits) => bits & ~(int)B16;

        /// <summary>
        /// Takes an int, unsets bit 17 of [0:31], and returns the result.
        /// </summary>
        public static int Unset17(int bits) => bits & ~(int)B17;

        /// <summary>
        /// Takes an int, unsets bit 18 of [0:31], and returns the result.
        /// </summary>
        public static int Unset18(int bits) => bits & ~(int)B18;

        /// <summary>
        /// Takes an int, unsets bit 19 of [0:31], and returns the result.
        /// </summary>
        public static int Unset19(int bits) => bits & ~(int)B19;

        /// <summary>
        /// Takes an int, unsets bit 20 of [0:31], and returns the result.
        /// </summary>
        public static int Unset20(int bits) => bits & ~(int)B20;

        /// <summary>
        /// Takes an int, unsets bit 21 of [0:31], and returns the result.
        /// </summary>
        public static int Unset21(int bits) => bits & ~(int)B21;

        /// <summary>
        /// Takes an int, unsets bit 22 of [0:31], and returns the result.
        /// </summary>
        public static int Unset22(int bits) => bits & ~(int)B22;

        /// <summary>
        /// Takes an int, unsets bit 23 of [0:31], and returns the result.
        /// </summary>
        public static int Unset23(int bits) => bits & ~(int)B23;

        /// <summary>
        /// Takes an int, unsets bit 24 of [0:31], and returns the result.
        /// </summary>
        public static int Unset24(int bits) => bits & ~(int)B24;

        /// <summary>
        /// Takes an int, unsets bit 25 of [0:31], and returns the result.
        /// </summary>
        public static int Unset25(int bits) => bits & ~(int)B25;

        /// <summary>
        /// Takes an int, unsets bit 26 of [0:31], and returns the result.
        /// </summary>
        public static int Unset26(int bits) => bits & ~(int)B26;

        /// <summary>
        /// Takes an int, unsets bit 27 of [0:31], and returns the result.
        /// </summary>
        public static int Unset27(int bits) => bits & ~(int)B27;

        /// <summary>
        /// Takes an int, unsets bit 28 of [0:31], and returns the result.
        /// </summary>
        public static int Unset28(int bits) => bits & ~(int)B28;

        /// <summary>
        /// Takes an int, unsets bit 29 of [0:31], and returns the result.
        /// </summary>
        public static int Unset29(int bits) => bits & ~(int)B29;

        /// <summary>
        /// Takes an int, unsets bit 30 of [0:31], and returns the result.
        /// </summary>
        public static int Unset30(int bits) => bits & ~(int)B30;

        /// <summary>
        /// Takes an int, unsets bit 31 of [0:31], and returns the result.
        /// </summary>
        public static int Unset31(int bits) => bits & (int)~B31;

        #endregion

        #region --- UInt ---

        /// <summary>
        /// Gets the specified bit on an unsigned int, as a boolean.
        /// </summary>
        /// <param name="bits">The unsigned int to get a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to get.</param>
        /// <remarks>
        /// Only the lowest five bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x1f</c>.
        /// </remarks>
        public static bool Get(this uint bits, int bitIndex) => (bits & (1u << (bitIndex & 0x1f))) != 0;

        /// <summary>
        /// Gets bit 0 of [0:31].
        /// </summary>
        public static bool Get0(this uint bits) => (bits & B0) != 0;

        /// <summary>
        /// Gets bit 1 of [0:31].
        /// </summary>
        public static bool Get1(this uint bits) => (bits & B1) != 0;

        /// <summary>
        /// Gets bit 2 of [0:31].
        /// </summary>
        public static bool Get2(this uint bits) => (bits & B2) != 0;

        /// <summary>
        /// Gets bit 3 of [0:31].
        /// </summary>
        public static bool Get3(this uint bits) => (bits & B3) != 0;

        /// <summary>
        /// Gets bit 4 of [0:31].
        /// </summary>
        public static bool Get4(this uint bits) => (bits & B4) != 0;

        /// <summary>
        /// Gets bit 5 of [0:31].
        /// </summary>
        public static bool Get5(this uint bits) => (bits & B5) != 0;

        /// <summary>
        /// Gets bit 6 of [0:31].
        /// </summary>
        public static bool Get6(this uint bits) => (bits & B6) != 0;

        /// <summary>
        /// Gets bit 7 of [0:31].
        /// </summary>
        public static bool Get7(this uint bits) => (bits & B7) != 0;

        /// <summary>
        /// Gets bit 8 of [0:31].
        /// </summary>
        public static bool Get8(this uint bits) => (bits & B8) != 0;

        /// <summary>
        /// Gets bit 9 of [0:31].
        /// </summary>
        public static bool Get9(this uint bits) => (bits & B9) != 0;

        /// <summary>
        /// Gets bit 10 of [0:31].
        /// </summary>
        public static bool Get10(this uint bits) => (bits & B10) != 0;

        /// <summary>
        /// Gets bit 11 of [0:31].
        /// </summary>
        public static bool Get11(this uint bits) => (bits & B11) != 0;

        /// <summary>
        /// Gets bit 12 of [0:31].
        /// </summary>
        public static bool Get12(this uint bits) => (bits & B12) != 0;

        /// <summary>
        /// Gets bit 13 of [0:31].
        /// </summary>
        public static bool Get13(this uint bits) => (bits & B13) != 0;

        /// <summary>
        /// Gets bit 14 of [0:31].
        /// </summary>
        public static bool Get14(this uint bits) => (bits & B14) != 0;

        /// <summary>
        /// Gets bit 15 of [0:31].
        /// </summary>
        public static bool Get15(this uint bits) => (bits & B15) != 0;

        /// <summary>
        /// Gets bit 16 of [0:31].
        /// </summary>
        public static bool Get16(this uint bits) => (bits & B16) != 0;

        /// <summary>
        /// Gets bit 17 of [0:31].
        /// </summary>
        public static bool Get17(this uint bits) => (bits & B17) != 0;

        /// <summary>
        /// Gets bit 18 of [0:31].
        /// </summary>
        public static bool Get18(this uint bits) => (bits & B18) != 0;

        /// <summary>
        /// Gets bit 19 of [0:31].
        /// </summary>
        public static bool Get19(this uint bits) => (bits & B19) != 0;

        /// <summary>
        /// Gets bit 20 of [0:31].
        /// </summary>
        public static bool Get20(this uint bits) => (bits & B20) != 0;

        /// <summary>
        /// Gets bit 21 of [0:31].
        /// </summary>
        public static bool Get21(this uint bits) => (bits & B21) != 0;

        /// <summary>
        /// Gets bit 22 of [0:31].
        /// </summary>
        public static bool Get22(this uint bits) => (bits & B22) != 0;

        /// <summary>
        /// Gets bit 23 of [0:31].
        /// </summary>
        public static bool Get23(this uint bits) => (bits & B23) != 0;

        /// <summary>
        /// Gets bit 24 of [0:31].
        /// </summary>
        public static bool Get24(this uint bits) => (bits & B24) != 0;

        /// <summary>
        /// Gets bit 25 of [0:31].
        /// </summary>
        public static bool Get25(this uint bits) => (bits & B25) != 0;

        /// <summary>
        /// Gets bit 26 of [0:31].
        /// </summary>
        public static bool Get26(this uint bits) => (bits & B26) != 0;

        /// <summary>
        /// Gets bit 27 of [0:31].
        /// </summary>
        public static bool Get27(this uint bits) => (bits & B27) != 0;

        /// <summary>
        /// Gets bit 28 of [0:31].
        /// </summary>
        public static bool Get28(this uint bits) => (bits & B28) != 0;

        /// <summary>
        /// Gets bit 29 of [0:31].
        /// </summary>
        public static bool Get29(this uint bits) => (bits & B29) != 0;

        /// <summary>
        /// Gets bit 30 of [0:31].
        /// </summary>
        public static bool Get30(this uint bits) => (bits & B30) != 0;

        /// <summary>
        /// Gets bit 31 of [0:31].
        /// </summary>
        public static bool Get31(this uint bits) => (bits & B31) != 0;

        // ---

        /// <summary>
        /// Takes an unsigned int, sets the specified bit, and returns the result.
        /// </summary>
        /// <param name="bits">The unsigned int before setting a bit.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest five bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x1f</c>.
        /// </remarks>
        public static uint Set(uint bits, int bitIndex) => bits | (1u << (bitIndex & 0x1f));

        /// <summary>
        /// Sets the specified bit on an unsigned int.
        /// </summary>
        /// <param name="bits">The unsigned int to set a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest five bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x1f</c>.
        /// </remarks>
        public static void Set(ref uint bits, int bitIndex) => bits |= (1u << (bitIndex & 0x1f));

        // ---

        /// <summary>
        /// Takes an unsigned int, sets bit 0 of [0:31], and returns the result.
        /// </summary>
        public static uint Set0(uint bits) => bits | B0;

        /// <summary>
        /// Takes an unsigned int, sets bit 1 of [0:31], and returns the result.
        /// </summary>
        public static uint Set1(uint bits) => bits | B1;

        /// <summary>
        /// Takes an unsigned int, sets bit 2 of [0:31], and returns the result.
        /// </summary>
        public static uint Set2(uint bits) => bits | B2;

        /// <summary>
        /// Takes an unsigned int, sets bit 3 of [0:31], and returns the result.
        /// </summary>
        public static uint Set3(uint bits) => bits | B3;

        /// <summary>
        /// Takes an unsigned int, sets bit 4 of [0:31], and returns the result.
        /// </summary>
        public static uint Set4(uint bits) => bits | B4;

        /// <summary>
        /// Takes an unsigned int, sets bit 5 of [0:31], and returns the result.
        /// </summary>
        public static uint Set5(uint bits) => bits | B5;

        /// <summary>
        /// Takes an unsigned int, sets bit 6 of [0:31], and returns the result.
        /// </summary>
        public static uint Set6(uint bits) => bits | B6;

        /// <summary>
        /// Takes an unsigned int, sets bit 7 of [0:31], and returns the result.
        /// </summary>
        public static uint Set7(uint bits) => bits | B7;

        /// <summary>
        /// Takes an unsigned int, sets bit 8 of [0:31], and returns the result.
        /// </summary>
        public static uint Set8(uint bits) => bits | B8;

        /// <summary>
        /// Takes an unsigned int, sets bit 9 of [0:31], and returns the result.
        /// </summary>
        public static uint Set9(uint bits) => bits | B9;

        /// <summary>
        /// Takes an unsigned int, sets bit 10 of [0:31], and returns the result.
        /// </summary>
        public static uint Set10(uint bits) => bits | B10;

        /// <summary>
        /// Takes an unsigned int, sets bit 11 of [0:31], and returns the result.
        /// </summary>
        public static uint Set11(uint bits) => bits | B11;

        /// <summary>
        /// Takes an unsigned int, sets bit 12 of [0:31], and returns the result.
        /// </summary>
        public static uint Set12(uint bits) => bits | B12;

        /// <summary>
        /// Takes an unsigned int, sets bit 13 of [0:31], and returns the result.
        /// </summary>
        public static uint Set13(uint bits) => bits | B13;

        /// <summary>
        /// Takes an unsigned int, sets bit 14 of [0:31], and returns the result.
        /// </summary>
        public static uint Set14(uint bits) => bits | B14;

        /// <summary>
        /// Takes an unsigned int, sets bit 15 of [0:31], and returns the result.
        /// </summary>
        public static uint Set15(uint bits) => bits | B15;

        /// <summary>
        /// Takes an unsigned int, sets bit 16 of [0:31], and returns the result.
        /// </summary>
        public static uint Set16(uint bits) => bits | B16;

        /// <summary>
        /// Takes an unsigned int, sets bit 17 of [0:31], and returns the result.
        /// </summary>
        public static uint Set17(uint bits) => bits | B17;

        /// <summary>
        /// Takes an unsigned int, sets bit 18 of [0:31], and returns the result.
        /// </summary>
        public static uint Set18(uint bits) => bits | B18;

        /// <summary>
        /// Takes an unsigned int, sets bit 19 of [0:31], and returns the result.
        /// </summary>
        public static uint Set19(uint bits) => bits | B19;

        /// <summary>
        /// Takes an unsigned int, sets bit 20 of [0:31], and returns the result.
        /// </summary>
        public static uint Set20(uint bits) => bits | B20;

        /// <summary>
        /// Takes an unsigned int, sets bit 21 of [0:31], and returns the result.
        /// </summary>
        public static uint Set21(uint bits) => bits | B21;

        /// <summary>
        /// Takes an unsigned int, sets bit 22 of [0:31], and returns the result.
        /// </summary>
        public static uint Set22(uint bits) => bits | B22;

        /// <summary>
        /// Takes an unsigned int, sets bit 23 of [0:31], and returns the result.
        /// </summary>
        public static uint Set23(uint bits) => bits | B23;

        /// <summary>
        /// Takes an unsigned int, sets bit 24 of [0:31], and returns the result.
        /// </summary>
        public static uint Set24(uint bits) => bits | B24;

        /// <summary>
        /// Takes an unsigned int, sets bit 25 of [0:31], and returns the result.
        /// </summary>
        public static uint Set25(uint bits) => bits | B25;

        /// <summary>
        /// Takes an unsigned int, sets bit 26 of [0:31], and returns the result.
        /// </summary>
        public static uint Set26(uint bits) => bits | B26;

        /// <summary>
        /// Takes an unsigned int, sets bit 27 of [0:31], and returns the result.
        /// </summary>
        public static uint Set27(uint bits) => bits | B27;

        /// <summary>
        /// Takes an unsigned int, sets bit 28 of [0:31], and returns the result.
        /// </summary>
        public static uint Set28(uint bits) => bits | B28;

        /// <summary>
        /// Takes an unsigned int, sets bit 29 of [0:31], and returns the result.
        /// </summary>
        public static uint Set29(uint bits) => bits | B29;

        /// <summary>
        /// Takes an unsigned int, sets bit 30 of [0:31], and returns the result.
        /// </summary>
        public static uint Set30(uint bits) => bits | B30;

        /// <summary>
        /// Takes an unsigned int, sets bit 31 of [0:31], and returns the result.
        /// </summary>
        public static uint Set31(uint bits) => bits | B31;

        // ---

        /// <summary>
        /// Takes an unsigned int, sets or unsets the specified bit according to a boolean value, and returns the result.
        /// </summary>
        /// <param name="bits">The unsigned int before the set / unset.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set / unset.</param>
        /// <param name="value">If true then the bit will be set to 1; otherwise to 0 (unset).</param>
        /// <remarks>
        /// Only the lowest five bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x1f</c>.
        /// </remarks>
        public static uint Set(uint bits, int bitIndex, bool value)
        {
            unchecked
            {
                uint mask = 1u << (bitIndex & 0x1f);
                return value ? bits | mask : bits & ~mask;
            }
        }

        /// <summary>
        /// Sets or unsets the specified bit according to a boolean value.
        /// </summary>
        /// <param name="bits">The unsigned int to set / unset a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set / unset.</param>
        /// <param name="value">If true then the bit will be set to 1; otherwise to 0 (unset).</param>
        /// <remarks>
        /// Only the lowest five bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x1f</c>.
        /// </remarks>
        public static void Set(ref uint bits, int bitIndex, bool value)
        {
            unchecked
            {
                uint mask = 1u << (bitIndex & 0x1f);
                bits = value ? bits | mask : bits & ~mask;
            }
        }

        // ---

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 0 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set0(uint bits, bool value) => value ? bits | B0 : bits & ~(uint)B0;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 1 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set1(uint bits, bool value) => value ? bits | B1 : bits & ~(uint)B1;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 2 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set2(uint bits, bool value) => value ? bits | B2 : bits & ~(uint)B2;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 3 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set3(uint bits, bool value) => value ? bits | B3 : bits & ~(uint)B3;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 4 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set4(uint bits, bool value) => value ? bits | B4 : bits & ~(uint)B4;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 5 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set5(uint bits, bool value) => value ? bits | B5 : bits & ~(uint)B5;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 6 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set6(uint bits, bool value) => value ? bits | B6 : bits & ~(uint)B6;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 7 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set7(uint bits, bool value) => value ? bits | B7 : bits & ~(uint)B7;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 8 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set8(uint bits, bool value) => value ? bits | B8 : bits & ~(uint)B8;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 9 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set9(uint bits, bool value) => value ? bits | B9 : bits & ~(uint)B9;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 10 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set10(uint bits, bool value) => value ? bits | B10 : bits & ~(uint)B10;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 11 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set11(uint bits, bool value) => value ? bits | B11 : bits & ~(uint)B11;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 12 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set12(uint bits, bool value) => value ? bits | B12 : bits & ~(uint)B12;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 13 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set13(uint bits, bool value) => value ? bits | B13 : bits & ~(uint)B13;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 14 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set14(uint bits, bool value) => value ? bits | B14 : bits & ~(uint)B14;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 15 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set15(uint bits, bool value) => value ? bits | B15 : bits & ~(uint)B15;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 16 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set16(uint bits, bool value) => value ? bits | B16 : bits & ~B16;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 17 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set17(uint bits, bool value) => value ? bits | B17 : bits & ~B17;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 18 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set18(uint bits, bool value) => value ? bits | B18 : bits & ~B18;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 19 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set19(uint bits, bool value) => value ? bits | B19 : bits & ~B19;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 20 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set20(uint bits, bool value) => value ? bits | B20 : bits & ~B20;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 21 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set21(uint bits, bool value) => value ? bits | B21 : bits & ~B21;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 22 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set22(uint bits, bool value) => value ? bits | B22 : bits & ~B22;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 23 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set23(uint bits, bool value) => value ? bits | B23 : bits & ~B23;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 24 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set24(uint bits, bool value) => value ? bits | B24 : bits & ~B24;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 25 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set25(uint bits, bool value) => value ? bits | B25 : bits & ~B25;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 26 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set26(uint bits, bool value) => value ? bits | B26 : bits & ~B26;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 27 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set27(uint bits, bool value) => value ? bits | B27 : bits & ~B27;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 28 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set28(uint bits, bool value) => value ? bits | B28 : bits & ~B28;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 29 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set29(uint bits, bool value) => value ? bits | B29 : bits & ~B29;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 30 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set30(uint bits, bool value) => value ? bits | B30 : bits & ~B30;

        /// <summary>
        /// Takes an unsigned int, sets or unsets bit 31 of [0:31] according to a boolean value, and returns the result.
        /// </summary>
        public static uint Set31(uint bits, bool value) => value ? bits | B31 : bits & ~B31;

        // ---

        /// <summary>
        /// Takes an unsigned int, unsets the specified bit, and returns the result.
        /// </summary>
        /// <param name="bits">The unsigned int before unsetting a bit.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest five bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x1f</c>.
        /// </remarks>
        public static uint Unset(uint bits, int bitIndex) => bits & ~(1u << (bitIndex & 0x1f));

        /// <summary>
        /// Unsets the specified bit on an unsigned int.
        /// </summary>
        /// <param name="bits">The unsigned int to unset a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to unset.</param>
        /// <remarks>
        /// Only the lowest five bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x1f</c>.
        /// </remarks>
        public static void Unset(ref uint bits, int bitIndex) => bits &= ~(1u << (bitIndex & 0x1f));

        // ---

        /// <summary>
        /// Takes an unsigned int, unsets bit 0 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset0(uint bits) => bits & ~(uint)B0;

        /// <summary>
        /// Takes an unsigned int, unsets bit 1 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset1(uint bits) => bits & ~(uint)B1;

        /// <summary>
        /// Takes an unsigned int, unsets bit 2 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset2(uint bits) => bits & ~(uint)B2;

        /// <summary>
        /// Takes an unsigned int, unsets bit 3 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset3(uint bits) => bits & ~(uint)B3;

        /// <summary>
        /// Takes an unsigned int, unsets bit 4 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset4(uint bits) => bits & ~(uint)B4;

        /// <summary>
        /// Takes an unsigned int, unsets bit 5 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset5(uint bits) => bits & ~(uint)B5;

        /// <summary>
        /// Takes an unsigned int, unsets bit 6 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset6(uint bits) => bits & ~(uint)B6;

        /// <summary>
        /// Takes an unsigned int, unsets bit 7 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset7(uint bits) => bits & ~(uint)B7;

        /// <summary>
        /// Takes an unsigned int, unsets bit 8 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset8(uint bits) => bits & ~(uint)B8;

        /// <summary>
        /// Takes an unsigned int, unsets bit 9 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset9(uint bits) => bits & ~(uint)B9;

        /// <summary>
        /// Takes an unsigned int, unsets bit 10 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset10(uint bits) => bits & ~(uint)B10;

        /// <summary>
        /// Takes an unsigned int, unsets bit 11 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset11(uint bits) => bits & ~(uint)B11;

        /// <summary>
        /// Takes an unsigned int, unsets bit 12 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset12(uint bits) => bits & ~(uint)B12;

        /// <summary>
        /// Takes an unsigned int, unsets bit 13 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset13(uint bits) => bits & ~(uint)B13;

        /// <summary>
        /// Takes an unsigned int, unsets bit 14 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset14(uint bits) => bits & ~(uint)B14;

        /// <summary>
        /// Takes an unsigned int, unsets bit 15 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset15(uint bits) => bits & ~(uint)B15;

        /// <summary>
        /// Takes an unsigned int, unsets bit 16 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset16(uint bits) => bits & ~B16;

        /// <summary>
        /// Takes an unsigned int, unsets bit 17 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset17(uint bits) => bits & ~B17;

        /// <summary>
        /// Takes an unsigned int, unsets bit 18 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset18(uint bits) => bits & ~B18;

        /// <summary>
        /// Takes an unsigned int, unsets bit 19 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset19(uint bits) => bits & ~B19;

        /// <summary>
        /// Takes an unsigned int, unsets bit 20 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset20(uint bits) => bits & ~B20;

        /// <summary>
        /// Takes an unsigned int, unsets bit 21 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset21(uint bits) => bits & ~B21;

        /// <summary>
        /// Takes an unsigned int, unsets bit 22 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset22(uint bits) => bits & ~B22;

        /// <summary>
        /// Takes an unsigned int, unsets bit 23 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset23(uint bits) => bits & ~B23;

        /// <summary>
        /// Takes an unsigned int, unsets bit 24 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset24(uint bits) => bits & ~B24;

        /// <summary>
        /// Takes an unsigned int, unsets bit 25 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset25(uint bits) => bits & ~B25;

        /// <summary>
        /// Takes an unsigned int, unsets bit 26 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset26(uint bits) => bits & ~B26;

        /// <summary>
        /// Takes an unsigned int, unsets bit 27 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset27(uint bits) => bits & ~B27;

        /// <summary>
        /// Takes an unsigned int, unsets bit 28 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset28(uint bits) => bits & ~B28;

        /// <summary>
        /// Takes an unsigned int, unsets bit 29 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset29(uint bits) => bits & ~B29;

        /// <summary>
        /// Takes an unsigned int, unsets bit 30 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset30(uint bits) => bits & ~B30;

        /// <summary>
        /// Takes an unsigned int, unsets bit 31 of [0:31], and returns the result.
        /// </summary>
        public static uint Unset31(uint bits) => bits & ~B31;

        #endregion

        #region --- Long ---

        /// <summary>
        /// Gets the specified bit on a long, as a boolean.
        /// </summary>
        /// <param name="bits">The long to get a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to get.</param>
        /// <remarks>
        /// Only the lowest six bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x3f</c>.
        /// </remarks>
        public static bool Get(this long bits, int bitIndex) => (bits & (1L << (bitIndex & 0x3f))) != 0;

        // ---

        /// <summary>
        /// Gets bit 0 of [0:63].
        /// </summary>
        public static bool Get0(this long bits) => (bits & B0) != 0;

        /// <summary>
        /// Gets bit 1 of [0:63].
        /// </summary>
        public static bool Get1(this long bits) => (bits & B1) != 0;

        /// <summary>
        /// Gets bit 2 of [0:63].
        /// </summary>
        public static bool Get2(this long bits) => (bits & B2) != 0;

        /// <summary>
        /// Gets bit 3 of [0:63].
        /// </summary>
        public static bool Get3(this long bits) => (bits & B3) != 0;

        /// <summary>
        /// Gets bit 4 of [0:63].
        /// </summary>
        public static bool Get4(this long bits) => (bits & B4) != 0;

        /// <summary>
        /// Gets bit 5 of [0:63].
        /// </summary>
        public static bool Get5(this long bits) => (bits & B5) != 0;

        /// <summary>
        /// Gets bit 6 of [0:63].
        /// </summary>
        public static bool Get6(this long bits) => (bits & B6) != 0;

        /// <summary>
        /// Gets bit 7 of [0:63].
        /// </summary>
        public static bool Get7(this long bits) => (bits & B7) != 0;

        /// <summary>
        /// Gets bit 8 of [0:63].
        /// </summary>
        public static bool Get8(this long bits) => (bits & B8) != 0;

        /// <summary>
        /// Gets bit 9 of [0:63].
        /// </summary>
        public static bool Get9(this long bits) => (bits & B9) != 0;

        /// <summary>
        /// Gets bit 10 of [0:63].
        /// </summary>
        public static bool Get10(this long bits) => (bits & B10) != 0;

        /// <summary>
        /// Gets bit 11 of [0:63].
        /// </summary>
        public static bool Get11(this long bits) => (bits & B11) != 0;

        /// <summary>
        /// Gets bit 12 of [0:63].
        /// </summary>
        public static bool Get12(this long bits) => (bits & B12) != 0;

        /// <summary>
        /// Gets bit 13 of [0:63].
        /// </summary>
        public static bool Get13(this long bits) => (bits & B13) != 0;

        /// <summary>
        /// Gets bit 14 of [0:63].
        /// </summary>
        public static bool Get14(this long bits) => (bits & B14) != 0;

        /// <summary>
        /// Gets bit 15 of [0:63].
        /// </summary>
        public static bool Get15(this long bits) => (bits & B15) != 0;

        /// <summary>
        /// Gets bit 16 of [0:63].
        /// </summary>
        public static bool Get16(this long bits) => (bits & B16) != 0;

        /// <summary>
        /// Gets bit 17 of [0:63].
        /// </summary>
        public static bool Get17(this long bits) => (bits & B17) != 0;

        /// <summary>
        /// Gets bit 18 of [0:63].
        /// </summary>
        public static bool Get18(this long bits) => (bits & B18) != 0;

        /// <summary>
        /// Gets bit 19 of [0:63].
        /// </summary>
        public static bool Get19(this long bits) => (bits & B19) != 0;

        /// <summary>
        /// Gets bit 20 of [0:63].
        /// </summary>
        public static bool Get20(this long bits) => (bits & B20) != 0;

        /// <summary>
        /// Gets bit 21 of [0:63].
        /// </summary>
        public static bool Get21(this long bits) => (bits & B21) != 0;

        /// <summary>
        /// Gets bit 22 of [0:63].
        /// </summary>
        public static bool Get22(this long bits) => (bits & B22) != 0;

        /// <summary>
        /// Gets bit 23 of [0:63].
        /// </summary>
        public static bool Get23(this long bits) => (bits & B23) != 0;

        /// <summary>
        /// Gets bit 24 of [0:63].
        /// </summary>
        public static bool Get24(this long bits) => (bits & B24) != 0;

        /// <summary>
        /// Gets bit 25 of [0:63].
        /// </summary>
        public static bool Get25(this long bits) => (bits & B25) != 0;

        /// <summary>
        /// Gets bit 26 of [0:63].
        /// </summary>
        public static bool Get26(this long bits) => (bits & B26) != 0;

        /// <summary>
        /// Gets bit 27 of [0:63].
        /// </summary>
        public static bool Get27(this long bits) => (bits & B27) != 0;

        /// <summary>
        /// Gets bit 28 of [0:63].
        /// </summary>
        public static bool Get28(this long bits) => (bits & B28) != 0;

        /// <summary>
        /// Gets bit 29 of [0:63].
        /// </summary>
        public static bool Get29(this long bits) => (bits & B29) != 0;

        /// <summary>
        /// Gets bit 30 of [0:63].
        /// </summary>
        public static bool Get30(this long bits) => (bits & B30) != 0;

        /// <summary>
        /// Gets bit 31 of [0:63].
        /// </summary>
        public static bool Get31(this long bits) => (bits & B31) != 0;

        /// <summary>
        /// Gets bit 32 of [0:63].
        /// </summary>
        public static bool Get32(this long bits) => (bits & (long)B32) != 0;

        /// <summary>
        /// Gets bit 33 of [0:63].
        /// </summary>
        public static bool Get33(this long bits) => (bits & (long)B33) != 0;

        /// <summary>
        /// Gets bit 34 of [0:63].
        /// </summary>
        public static bool Get34(this long bits) => (bits & (long)B34) != 0;

        /// <summary>
        /// Gets bit 35 of [0:63].
        /// </summary>
        public static bool Get35(this long bits) => (bits & (long)B35) != 0;

        /// <summary>
        /// Gets bit 36 of [0:63].
        /// </summary>
        public static bool Get36(this long bits) => (bits & (long)B36) != 0;

        /// <summary>
        /// Gets bit 37 of [0:63].
        /// </summary>
        public static bool Get37(this long bits) => (bits & (long)B37) != 0;

        /// <summary>
        /// Gets bit 38 of [0:63].
        /// </summary>
        public static bool Get38(this long bits) => (bits & (long)B38) != 0;

        /// <summary>
        /// Gets bit 39 of [0:63].
        /// </summary>
        public static bool Get39(this long bits) => (bits & (long)B39) != 0;

        /// <summary>
        /// Gets bit 40 of [0:63].
        /// </summary>
        public static bool Get40(this long bits) => (bits & (long)B40) != 0;

        /// <summary>
        /// Gets bit 41 of [0:63].
        /// </summary>
        public static bool Get41(this long bits) => (bits & (long)B41) != 0;

        /// <summary>
        /// Gets bit 42 of [0:63].
        /// </summary>
        public static bool Get42(this long bits) => (bits & (long)B42) != 0;

        /// <summary>
        /// Gets bit 43 of [0:63].
        /// </summary>
        public static bool Get43(this long bits) => (bits & (long)B43) != 0;

        /// <summary>
        /// Gets bit 44 of [0:63].
        /// </summary>
        public static bool Get44(this long bits) => (bits & (long)B44) != 0;

        /// <summary>
        /// Gets bit 45 of [0:63].
        /// </summary>
        public static bool Get45(this long bits) => (bits & (long)B45) != 0;

        /// <summary>
        /// Gets bit 46 of [0:63].
        /// </summary>
        public static bool Get46(this long bits) => (bits & (long)B46) != 0;

        /// <summary>
        /// Gets bit 47 of [0:63].
        /// </summary>
        public static bool Get47(this long bits) => (bits & (long)B47) != 0;

        /// <summary>
        /// Gets bit 48 of [0:63].
        /// </summary>
        public static bool Get48(this long bits) => (bits & (long)B48) != 0;

        /// <summary>
        /// Gets bit 49 of [0:63].
        /// </summary>
        public static bool Get49(this long bits) => (bits & (long)B49) != 0;

        /// <summary>
        /// Gets bit 50 of [0:63].
        /// </summary>
        public static bool Get50(this long bits) => (bits & (long)B50) != 0;

        /// <summary>
        /// Gets bit 51 of [0:63].
        /// </summary>
        public static bool Get51(this long bits) => (bits & (long)B51) != 0;

        /// <summary>
        /// Gets bit 52 of [0:63].
        /// </summary>
        public static bool Get52(this long bits) => (bits & (long)B52) != 0;

        /// <summary>
        /// Gets bit 53 of [0:63].
        /// </summary>
        public static bool Get53(this long bits) => (bits & (long)B53) != 0;

        /// <summary>
        /// Gets bit 54 of [0:63].
        /// </summary>
        public static bool Get54(this long bits) => (bits & (long)B54) != 0;

        /// <summary>
        /// Gets bit 55 of [0:63].
        /// </summary>
        public static bool Get55(this long bits) => (bits & (long)B55) != 0;

        /// <summary>
        /// Gets bit 56 of [0:63].
        /// </summary>
        public static bool Get56(this long bits) => (bits & (long)B56) != 0;

        /// <summary>
        /// Gets bit 57 of [0:63].
        /// </summary>
        public static bool Get57(this long bits) => (bits & (long)B57) != 0;

        /// <summary>
        /// Gets bit 58 of [0:63].
        /// </summary>
        public static bool Get58(this long bits) => (bits & (long)B58) != 0;

        /// <summary>
        /// Gets bit 59 of [0:63].
        /// </summary>
        public static bool Get59(this long bits) => (bits & (long)B59) != 0;

        /// <summary>
        /// Gets bit 60 of [0:63].
        /// </summary>
        public static bool Get60(this long bits) => (bits & (long)B60) != 0;

        /// <summary>
        /// Gets bit 61 of [0:63].
        /// </summary>
        public static bool Get61(this long bits) => (bits & (long)B61) != 0;

        /// <summary>
        /// Gets bit 62 of [0:63].
        /// </summary>
        public static bool Get62(this long bits) => (bits & (long)B62) != 0;

        /// <summary>
        /// Gets bit 63 of [0:63].
        /// </summary>
        public static bool Get63(this long bits) => (bits & unchecked((long)B63)) != 0;

        // ---

        /// <summary>
        /// Takes a long, sets the specified bit, and returns the result.
        /// </summary>
        /// <param name="bits">The long before setting a bit.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest six bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x3f</c>.
        /// </remarks>
        public static long Set(long bits, int bitIndex) => bits | (1L << (bitIndex & 0x3f));

        /// <summary>
        /// Sets the specified bit on a long.
        /// </summary>
        /// <param name="bits">The long to set a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest six bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x3f</c>.
        /// </remarks>
        public static void Set(ref long bits, int bitIndex) => bits |= (1L << (bitIndex & 0x3f));

        // ---

        /// <summary>
        /// Takes a long, sets bit 0 of [0:63], and returns the result.
        /// </summary>
        public static long Set0(long bits) => bits | B0;

        /// <summary>
        /// Takes a long, sets bit 1 of [0:63], and returns the result.
        /// </summary>
        public static long Set1(long bits) => bits | B1;

        /// <summary>
        /// Takes a long, sets bit 2 of [0:63], and returns the result.
        /// </summary>
        public static long Set2(long bits) => bits | B2;

        /// <summary>
        /// Takes a long, sets bit 3 of [0:63], and returns the result.
        /// </summary>
        public static long Set3(long bits) => bits | B3;

        /// <summary>
        /// Takes a long, sets bit 4 of [0:63], and returns the result.
        /// </summary>
        public static long Set4(long bits) => bits | B4;

        /// <summary>
        /// Takes a long, sets bit 5 of [0:63], and returns the result.
        /// </summary>
        public static long Set5(long bits) => bits | B5;

        /// <summary>
        /// Takes a long, sets bit 6 of [0:63], and returns the result.
        /// </summary>
        public static long Set6(long bits) => bits | B6;

        /// <summary>
        /// Takes a long, sets bit 7 of [0:63], and returns the result.
        /// </summary>
        public static long Set7(long bits) => bits | B7;

        /// <summary>
        /// Takes a long, sets bit 8 of [0:63], and returns the result.
        /// </summary>
        public static long Set8(long bits) => bits | B8;

        /// <summary>
        /// Takes a long, sets bit 9 of [0:63], and returns the result.
        /// </summary>
        public static long Set9(long bits) => bits | B9;

        /// <summary>
        /// Takes a long, sets bit 10 of [0:63], and returns the result.
        /// </summary>
        public static long Set10(long bits) => bits | B10;

        /// <summary>
        /// Takes a long, sets bit 11 of [0:63], and returns the result.
        /// </summary>
        public static long Set11(long bits) => bits | B11;

        /// <summary>
        /// Takes a long, sets bit 12 of [0:63], and returns the result.
        /// </summary>
        public static long Set12(long bits) => bits | B12;

        /// <summary>
        /// Takes a long, sets bit 13 of [0:63], and returns the result.
        /// </summary>
        public static long Set13(long bits) => bits | B13;

        /// <summary>
        /// Takes a long, sets bit 14 of [0:63], and returns the result.
        /// </summary>
        public static long Set14(long bits) => bits | B14;

        /// <summary>
        /// Takes a long, sets bit 15 of [0:63], and returns the result.
        /// </summary>
        public static long Set15(long bits) => bits | B15;

        /// <summary>
        /// Takes a long, sets bit 16 of [0:63], and returns the result.
        /// </summary>
        public static long Set16(long bits) => bits | B16;

        /// <summary>
        /// Takes a long, sets bit 17 of [0:63], and returns the result.
        /// </summary>
        public static long Set17(long bits) => bits | B17;

        /// <summary>
        /// Takes a long, sets bit 18 of [0:63], and returns the result.
        /// </summary>
        public static long Set18(long bits) => bits | B18;

        /// <summary>
        /// Takes a long, sets bit 19 of [0:63], and returns the result.
        /// </summary>
        public static long Set19(long bits) => bits | B19;

        /// <summary>
        /// Takes a long, sets bit 20 of [0:63], and returns the result.
        /// </summary>
        public static long Set20(long bits) => bits | B20;

        /// <summary>
        /// Takes a long, sets bit 21 of [0:63], and returns the result.
        /// </summary>
        public static long Set21(long bits) => bits | B21;

        /// <summary>
        /// Takes a long, sets bit 22 of [0:63], and returns the result.
        /// </summary>
        public static long Set22(long bits) => bits | B22;

        /// <summary>
        /// Takes a long, sets bit 23 of [0:63], and returns the result.
        /// </summary>
        public static long Set23(long bits) => bits | B23;

        /// <summary>
        /// Takes a long, sets bit 24 of [0:63], and returns the result.
        /// </summary>
        public static long Set24(long bits) => bits | B24;

        /// <summary>
        /// Takes a long, sets bit 25 of [0:63], and returns the result.
        /// </summary>
        public static long Set25(long bits) => bits | B25;

        /// <summary>
        /// Takes a long, sets bit 26 of [0:63], and returns the result.
        /// </summary>
        public static long Set26(long bits) => bits | B26;

        /// <summary>
        /// Takes a long, sets bit 27 of [0:63], and returns the result.
        /// </summary>
        public static long Set27(long bits) => bits | B27;

        /// <summary>
        /// Takes a long, sets bit 28 of [0:63], and returns the result.
        /// </summary>
        public static long Set28(long bits) => bits | B28;

        /// <summary>
        /// Takes a long, sets bit 29 of [0:63], and returns the result.
        /// </summary>
        public static long Set29(long bits) => bits | B29;

        /// <summary>
        /// Takes a long, sets bit 30 of [0:63], and returns the result.
        /// </summary>
        public static long Set30(long bits) => bits | B30;

        /// <summary>
        /// Takes a long, sets bit 31 of [0:63], and returns the result.
        /// </summary>
        public static long Set31(long bits) => bits | B31;

        /// <summary>
        /// Takes a long, sets bit 32 of [0:63], and returns the result.
        /// </summary>
        public static long Set32(long bits) => bits | (long)B32;

        /// <summary>
        /// Takes a long, sets bit 33 of [0:63], and returns the result.
        /// </summary>
        public static long Set33(long bits) => bits | (long)B33;

        /// <summary>
        /// Takes a long, sets bit 34 of [0:63], and returns the result.
        /// </summary>
        public static long Set34(long bits) => bits | (long)B34;

        /// <summary>
        /// Takes a long, sets bit 35 of [0:63], and returns the result.
        /// </summary>
        public static long Set35(long bits) => bits | (long)B35;

        /// <summary>
        /// Takes a long, sets bit 36 of [0:63], and returns the result.
        /// </summary>
        public static long Set36(long bits) => bits | (long)B36;

        /// <summary>
        /// Takes a long, sets bit 37 of [0:63], and returns the result.
        /// </summary>
        public static long Set37(long bits) => bits | (long)B37;

        /// <summary>
        /// Takes a long, sets bit 38 of [0:63], and returns the result.
        /// </summary>
        public static long Set38(long bits) => bits | (long)B38;

        /// <summary>
        /// Takes a long, sets bit 39 of [0:63], and returns the result.
        /// </summary>
        public static long Set39(long bits) => bits | (long)B39;

        /// <summary>
        /// Takes a long, sets bit 40 of [0:63], and returns the result.
        /// </summary>
        public static long Set40(long bits) => bits | (long)B40;

        /// <summary>
        /// Takes a long, sets bit 41 of [0:63], and returns the result.
        /// </summary>
        public static long Set41(long bits) => bits | (long)B41;

        /// <summary>
        /// Takes a long, sets bit 42 of [0:63], and returns the result.
        /// </summary>
        public static long Set42(long bits) => bits | (long)B42;

        /// <summary>
        /// Takes a long, sets bit 43 of [0:63], and returns the result.
        /// </summary>
        public static long Set43(long bits) => bits | (long)B43;

        /// <summary>
        /// Takes a long, sets bit 44 of [0:63], and returns the result.
        /// </summary>
        public static long Set44(long bits) => bits | (long)B44;

        /// <summary>
        /// Takes a long, sets bit 45 of [0:63], and returns the result.
        /// </summary>
        public static long Set45(long bits) => bits | (long)B45;

        /// <summary>
        /// Takes a long, sets bit 46 of [0:63], and returns the result.
        /// </summary>
        public static long Set46(long bits) => bits | (long)B46;

        /// <summary>
        /// Takes a long, sets bit 47 of [0:63], and returns the result.
        /// </summary>
        public static long Set47(long bits) => bits | (long)B47;

        /// <summary>
        /// Takes a long, sets bit 48 of [0:63], and returns the result.
        /// </summary>
        public static long Set48(long bits) => bits | (long)B48;

        /// <summary>
        /// Takes a long, sets bit 49 of [0:63], and returns the result.
        /// </summary>
        public static long Set49(long bits) => bits | (long)B49;

        /// <summary>
        /// Takes a long, sets bit 50 of [0:63], and returns the result.
        /// </summary>
        public static long Set50(long bits) => bits | (long)B50;

        /// <summary>
        /// Takes a long, sets bit 51 of [0:63], and returns the result.
        /// </summary>
        public static long Set51(long bits) => bits | (long)B51;

        /// <summary>
        /// Takes a long, sets bit 52 of [0:63], and returns the result.
        /// </summary>
        public static long Set52(long bits) => bits | (long)B52;

        /// <summary>
        /// Takes a long, sets bit 53 of [0:63], and returns the result.
        /// </summary>
        public static long Set53(long bits) => bits | (long)B53;

        /// <summary>
        /// Takes a long, sets bit 54 of [0:63], and returns the result.
        /// </summary>
        public static long Set54(long bits) => bits | (long)B54;

        /// <summary>
        /// Takes a long, sets bit 55 of [0:63], and returns the result.
        /// </summary>
        public static long Set55(long bits) => bits | (long)B55;

        /// <summary>
        /// Takes a long, sets bit 56 of [0:63], and returns the result.
        /// </summary>
        public static long Set56(long bits) => bits | (long)B56;

        /// <summary>
        /// Takes a long, sets bit 57 of [0:63], and returns the result.
        /// </summary>
        public static long Set57(long bits) => bits | (long)B57;

        /// <summary>
        /// Takes a long, sets bit 58 of [0:63], and returns the result.
        /// </summary>
        public static long Set58(long bits) => bits | (long)B58;

        /// <summary>
        /// Takes a long, sets bit 59 of [0:63], and returns the result.
        /// </summary>
        public static long Set59(long bits) => bits | (long)B59;

        /// <summary>
        /// Takes a long, sets bit 60 of [0:63], and returns the result.
        /// </summary>
        public static long Set60(long bits) => bits | (long)B60;

        /// <summary>
        /// Takes a long, sets bit 61 of [0:63], and returns the result.
        /// </summary>
        public static long Set61(long bits) => bits | (long)B61;

        /// <summary>
        /// Takes a long, sets bit 62 of [0:63], and returns the result.
        /// </summary>
        public static long Set62(long bits) => bits | (long)B62;

        /// <summary>
        /// Takes a long, sets bit 63 of [0:63], and returns the result.
        /// </summary>
        public static long Set63(long bits) => bits | unchecked((long)B63);

        // ---

        /// <summary>
        /// Takes a long, sets or unsets the specified bit according to a boolean value, and returns the result.
        /// </summary>
        /// <param name="bits">The long before the set / unset.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set / unset.</param>
        /// <param name="value">If true then the bit will be set to 1; otherwise to 0 (unset).</param>
        /// <remarks>
        /// Only the lowest six bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x3f</c>.
        /// </remarks>
        public static long Set(long bits, int bitIndex, bool value)
        {
            unchecked
            {
                long mask = 1L << (bitIndex & 0x3f);
                return value ? bits | mask : bits & ~mask;
            }
        }

        /// <summary>
        /// Sets or unsets the specified bit according to a boolean value.
        /// </summary>
        /// <param name="bits">The long to set / unset a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set / unset.</param>
        /// <param name="value">If true then the bit will be set to 1; otherwise to 0 (unset).</param>
        /// <remarks>
        /// Only the lowest six bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x3f</c>.
        /// </remarks>
        public static void Set(ref long bits, int bitIndex, bool value)
        {
            unchecked
            {
                long mask = 1L << (bitIndex & 0x3f);
                bits = value ? bits | mask : bits & ~mask;
            }
        }

        // ---

        /// <summary>
        /// Takes a long, sets or unsets bit 0 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set0(long bits, bool value) => value ? bits | B0 : bits & ~B0;

        /// <summary>
        /// Takes a long, sets or unsets bit 1 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set1(long bits, bool value) => value ? bits | B1 : bits & ~B1;

        /// <summary>
        /// Takes a long, sets or unsets bit 2 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set2(long bits, bool value) => value ? bits | B2 : bits & ~B2;

        /// <summary>
        /// Takes a long, sets or unsets bit 3 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set3(long bits, bool value) => value ? bits | B3 : bits & ~B3;

        /// <summary>
        /// Takes a long, sets or unsets bit 4 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set4(long bits, bool value) => value ? bits | B4 : bits & ~B4;

        /// <summary>
        /// Takes a long, sets or unsets bit 5 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set5(long bits, bool value) => value ? bits | B5 : bits & ~B5;

        /// <summary>
        /// Takes a long, sets or unsets bit 6 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set6(long bits, bool value) => value ? bits | B6 : bits & ~B6;

        /// <summary>
        /// Takes a long, sets or unsets bit 7 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set7(long bits, bool value) => value ? bits | B7 : bits & ~B7;

        /// <summary>
        /// Takes a long, sets or unsets bit 8 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set8(long bits, bool value) => value ? bits | B8 : bits & ~B8;

        /// <summary>
        /// Takes a long, sets or unsets bit 9 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set9(long bits, bool value) => value ? bits | B9 : bits & ~B9;

        /// <summary>
        /// Takes a long, sets or unsets bit 10 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set10(long bits, bool value) => value ? bits | B10 : bits & ~B10;

        /// <summary>
        /// Takes a long, sets or unsets bit 11 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set11(long bits, bool value) => value ? bits | B11 : bits & ~B11;

        /// <summary>
        /// Takes a long, sets or unsets bit 12 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set12(long bits, bool value) => value ? bits | B12 : bits & ~B12;

        /// <summary>
        /// Takes a long, sets or unsets bit 13 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set13(long bits, bool value) => value ? bits | B13 : bits & ~B13;

        /// <summary>
        /// Takes a long, sets or unsets bit 14 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set14(long bits, bool value) => value ? bits | B14 : bits & ~B14;

        /// <summary>
        /// Takes a long, sets or unsets bit 15 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set15(long bits, bool value) => value ? bits | B15 : bits & ~B15;

        /// <summary>
        /// Takes a long, sets or unsets bit 16 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set16(long bits, bool value) => value ? bits | B16 : bits & ~B16;

        /// <summary>
        /// Takes a long, sets or unsets bit 17 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set17(long bits, bool value) => value ? bits | B17 : bits & ~B17;

        /// <summary>
        /// Takes a long, sets or unsets bit 18 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set18(long bits, bool value) => value ? bits | B18 : bits & ~B18;

        /// <summary>
        /// Takes a long, sets or unsets bit 19 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set19(long bits, bool value) => value ? bits | B19 : bits & ~B19;

        /// <summary>
        /// Takes a long, sets or unsets bit 20 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set20(long bits, bool value) => value ? bits | B20 : bits & ~B20;

        /// <summary>
        /// Takes a long, sets or unsets bit 21 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set21(long bits, bool value) => value ? bits | B21 : bits & ~B21;

        /// <summary>
        /// Takes a long, sets or unsets bit 22 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set22(long bits, bool value) => value ? bits | B22 : bits & ~B22;

        /// <summary>
        /// Takes a long, sets or unsets bit 23 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set23(long bits, bool value) => value ? bits | B23 : bits & ~B23;

        /// <summary>
        /// Takes a long, sets or unsets bit 24 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set24(long bits, bool value) => value ? bits | B24 : bits & ~B24;

        /// <summary>
        /// Takes a long, sets or unsets bit 25 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set25(long bits, bool value) => value ? bits | B25 : bits & ~B25;

        /// <summary>
        /// Takes a long, sets or unsets bit 26 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set26(long bits, bool value) => value ? bits | B26 : bits & ~B26;

        /// <summary>
        /// Takes a long, sets or unsets bit 27 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set27(long bits, bool value) => value ? bits | B27 : bits & ~B27;

        /// <summary>
        /// Takes a long, sets or unsets bit 28 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set28(long bits, bool value) => value ? bits | B28 : bits & ~B28;

        /// <summary>
        /// Takes a long, sets or unsets bit 29 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set29(long bits, bool value) => value ? bits | B29 : bits & ~B29;

        /// <summary>
        /// Takes a long, sets or unsets bit 30 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set30(long bits, bool value) => value ? bits | B30 : bits & ~B30;

        /// <summary>
        /// Takes a long, sets or unsets bit 31 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set31(long bits, bool value) => value ? bits | B31 : bits & ~B31;

        /// <summary>
        /// Takes a long, sets or unsets bit 32 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set32(long bits, bool value) => value ? bits | (long)B32 : bits & ~(long)B32;

        /// <summary>
        /// Takes a long, sets or unsets bit 33 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set33(long bits, bool value) => value ? bits | (long)B33 : bits & ~(long)B33;

        /// <summary>
        /// Takes a long, sets or unsets bit 34 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set34(long bits, bool value) => value ? bits | (long)B34 : bits & ~(long)B34;

        /// <summary>
        /// Takes a long, sets or unsets bit 35 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set35(long bits, bool value) => value ? bits | (long)B35 : bits & ~(long)B35;

        /// <summary>
        /// Takes a long, sets or unsets bit 36 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set36(long bits, bool value) => value ? bits | (long)B36 : bits & ~(long)B36;

        /// <summary>
        /// Takes a long, sets or unsets bit 37 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set37(long bits, bool value) => value ? bits | (long)B37 : bits & ~(long)B37;

        /// <summary>
        /// Takes a long, sets or unsets bit 38 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set38(long bits, bool value) => value ? bits | (long)B38 : bits & ~(long)B38;

        /// <summary>
        /// Takes a long, sets or unsets bit 39 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set39(long bits, bool value) => value ? bits | (long)B39 : bits & ~(long)B39;

        /// <summary>
        /// Takes a long, sets or unsets bit 40 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set40(long bits, bool value) => value ? bits | (long)B40 : bits & ~(long)B40;

        /// <summary>
        /// Takes a long, sets or unsets bit 41 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set41(long bits, bool value) => value ? bits | (long)B41 : bits & ~(long)B41;

        /// <summary>
        /// Takes a long, sets or unsets bit 42 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set42(long bits, bool value) => value ? bits | (long)B42 : bits & ~(long)B42;

        /// <summary>
        /// Takes a long, sets or unsets bit 43 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set43(long bits, bool value) => value ? bits | (long)B43 : bits & ~(long)B43;

        /// <summary>
        /// Takes a long, sets or unsets bit 44 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set44(long bits, bool value) => value ? bits | (long)B44 : bits & ~(long)B44;

        /// <summary>
        /// Takes a long, sets or unsets bit 45 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set45(long bits, bool value) => value ? bits | (long)B45 : bits & ~(long)B45;

        /// <summary>
        /// Takes a long, sets or unsets bit 46 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set46(long bits, bool value) => value ? bits | (long)B46 : bits & ~(long)B46;

        /// <summary>
        /// Takes a long, sets or unsets bit 47 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set47(long bits, bool value) => value ? bits | (long)B47 : bits & ~(long)B47;

        /// <summary>
        /// Takes a long, sets or unsets bit 48 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set48(long bits, bool value) => value ? bits | (long)B48 : bits & ~(long)B48;

        /// <summary>
        /// Takes a long, sets or unsets bit 49 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set49(long bits, bool value) => value ? bits | (long)B49 : bits & ~(long)B49;

        /// <summary>
        /// Takes a long, sets or unsets bit 50 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set50(long bits, bool value) => value ? bits | (long)B50 : bits & ~(long)B50;

        /// <summary>
        /// Takes a long, sets or unsets bit 51 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set51(long bits, bool value) => value ? bits | (long)B51 : bits & ~(long)B51;

        /// <summary>
        /// Takes a long, sets or unsets bit 52 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set52(long bits, bool value) => value ? bits | (long)B52 : bits & ~(long)B52;

        /// <summary>
        /// Takes a long, sets or unsets bit 53 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set53(long bits, bool value) => value ? bits | (long)B53 : bits & ~(long)B53;

        /// <summary>
        /// Takes a long, sets or unsets bit 54 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set54(long bits, bool value) => value ? bits | (long)B54 : bits & ~(long)B54;

        /// <summary>
        /// Takes a long, sets or unsets bit 55 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set55(long bits, bool value) => value ? bits | (long)B55 : bits & ~(long)B55;

        /// <summary>
        /// Takes a long, sets or unsets bit 56 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set56(long bits, bool value) => value ? bits | (long)B56 : bits & ~(long)B56;

        /// <summary>
        /// Takes a long, sets or unsets bit 57 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set57(long bits, bool value) => value ? bits | (long)B57 : bits & ~(long)B57;

        /// <summary>
        /// Takes a long, sets or unsets bit 58 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set58(long bits, bool value) => value ? bits | (long)B58 : bits & ~(long)B58;

        /// <summary>
        /// Takes a long, sets or unsets bit 59 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set59(long bits, bool value) => value ? bits | (long)B59 : bits & ~(long)B59;

        /// <summary>
        /// Takes a long, sets or unsets bit 60 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set60(long bits, bool value) => value ? bits | (long)B60 : bits & ~(long)B60;

        /// <summary>
        /// Takes a long, sets or unsets bit 61 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set61(long bits, bool value) => value ? bits | (long)B61 : bits & ~(long)B61;

        /// <summary>
        /// Takes a long, sets or unsets bit 62 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set62(long bits, bool value) => value ? bits | (long)B62 : bits & ~(long)B62;

        /// <summary>
        /// Takes a long, sets or unsets bit 63 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static long Set63(long bits, bool value) => value ? bits | unchecked((long)B63) : bits & (long)~B63;

        // ---

        /// <summary>
        /// Takes a long, unsets the specified bit, and returns the result.
        /// </summary>
        /// <param name="bits">The long before unsetting a bit.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest six bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x3f</c>.
        /// </remarks>
        public static long Unset(long bits, int bitIndex) => bits & ~(1L << (bitIndex & 0x3f));

        /// <summary>
        /// Unsets the specified bit on a long.
        /// </summary>
        /// <param name="bits">The long to unset a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to unset.</param>
        /// <remarks>
        /// Only the lowest six bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x3f</c>.
        /// </remarks>
        public static void Unset(ref long bits, int bitIndex) => bits &= ~(1L << (bitIndex & 0x3f));

        // ---

        /// <summary>
        /// Takes a long, unsets bit 0 of [0:63], and returns the result.
        /// </summary>
        public static long Unset0(long bits) => bits & ~B0;

        /// <summary>
        /// Takes a long, unsets bit 1 of [0:63], and returns the result.
        /// </summary>
        public static long Unset1(long bits) => bits & ~B1;

        /// <summary>
        /// Takes a long, unsets bit 2 of [0:63], and returns the result.
        /// </summary>
        public static long Unset2(long bits) => bits & ~B2;

        /// <summary>
        /// Takes a long, unsets bit 3 of [0:63], and returns the result.
        /// </summary>
        public static long Unset3(long bits) => bits & ~B3;

        /// <summary>
        /// Takes a long, unsets bit 4 of [0:63], and returns the result.
        /// </summary>
        public static long Unset4(long bits) => bits & ~B4;

        /// <summary>
        /// Takes a long, unsets bit 5 of [0:63], and returns the result.
        /// </summary>
        public static long Unset5(long bits) => bits & ~B5;

        /// <summary>
        /// Takes a long, unsets bit 6 of [0:63], and returns the result.
        /// </summary>
        public static long Unset6(long bits) => bits & ~B6;

        /// <summary>
        /// Takes a long, unsets bit 7 of [0:63], and returns the result.
        /// </summary>
        public static long Unset7(long bits) => bits & ~B7;

        /// <summary>
        /// Takes a long, unsets bit 8 of [0:63], and returns the result.
        /// </summary>
        public static long Unset8(long bits) => bits & ~B8;

        /// <summary>
        /// Takes a long, unsets bit 9 of [0:63], and returns the result.
        /// </summary>
        public static long Unset9(long bits) => bits & ~B9;

        /// <summary>
        /// Takes a long, unsets bit 10 of [0:63], and returns the result.
        /// </summary>
        public static long Unset10(long bits) => bits & ~B10;

        /// <summary>
        /// Takes a long, unsets bit 11 of [0:63], and returns the result.
        /// </summary>
        public static long Unset11(long bits) => bits & ~B11;

        /// <summary>
        /// Takes a long, unsets bit 12 of [0:63], and returns the result.
        /// </summary>
        public static long Unset12(long bits) => bits & ~B12;

        /// <summary>
        /// Takes a long, unsets bit 13 of [0:63], and returns the result.
        /// </summary>
        public static long Unset13(long bits) => bits & ~B13;

        /// <summary>
        /// Takes a long, unsets bit 14 of [0:63], and returns the result.
        /// </summary>
        public static long Unset14(long bits) => bits & ~B14;

        /// <summary>
        /// Takes a long, unsets bit 15 of [0:63], and returns the result.
        /// </summary>
        public static long Unset15(long bits) => bits & ~B15;

        /// <summary>
        /// Takes a long, unsets bit 16 of [0:63], and returns the result.
        /// </summary>
        public static long Unset16(long bits) => bits & ~B16;

        /// <summary>
        /// Takes a long, unsets bit 17 of [0:63], and returns the result.
        /// </summary>
        public static long Unset17(long bits) => bits & ~B17;

        /// <summary>
        /// Takes a long, unsets bit 18 of [0:63], and returns the result.
        /// </summary>
        public static long Unset18(long bits) => bits & ~B18;

        /// <summary>
        /// Takes a long, unsets bit 19 of [0:63], and returns the result.
        /// </summary>
        public static long Unset19(long bits) => bits & ~B19;

        /// <summary>
        /// Takes a long, unsets bit 20 of [0:63], and returns the result.
        /// </summary>
        public static long Unset20(long bits) => bits & ~B20;

        /// <summary>
        /// Takes a long, unsets bit 21 of [0:63], and returns the result.
        /// </summary>
        public static long Unset21(long bits) => bits & ~B21;

        /// <summary>
        /// Takes a long, unsets bit 22 of [0:63], and returns the result.
        /// </summary>
        public static long Unset22(long bits) => bits & ~B22;

        /// <summary>
        /// Takes a long, unsets bit 23 of [0:63], and returns the result.
        /// </summary>
        public static long Unset23(long bits) => bits & ~B23;

        /// <summary>
        /// Takes a long, unsets bit 24 of [0:63], and returns the result.
        /// </summary>
        public static long Unset24(long bits) => bits & ~B24;

        /// <summary>
        /// Takes a long, unsets bit 25 of [0:63], and returns the result.
        /// </summary>
        public static long Unset25(long bits) => bits & ~B25;

        /// <summary>
        /// Takes a long, unsets bit 26 of [0:63], and returns the result.
        /// </summary>
        public static long Unset26(long bits) => bits & ~B26;

        /// <summary>
        /// Takes a long, unsets bit 27 of [0:63], and returns the result.
        /// </summary>
        public static long Unset27(long bits) => bits & ~B27;

        /// <summary>
        /// Takes a long, unsets bit 28 of [0:63], and returns the result.
        /// </summary>
        public static long Unset28(long bits) => bits & ~B28;

        /// <summary>
        /// Takes a long, unsets bit 29 of [0:63], and returns the result.
        /// </summary>
        public static long Unset29(long bits) => bits & ~B29;

        /// <summary>
        /// Takes a long, unsets bit 30 of [0:63], and returns the result.
        /// </summary>
        public static long Unset30(long bits) => bits & ~B30;

        /// <summary>
        /// Takes a long, unsets bit 31 of [0:63], and returns the result.
        /// </summary>
        public static long Unset31(long bits) => bits & ~B31;

        /// <summary>
        /// Takes a long, unsets bit 32 of [0:63], and returns the result.
        /// </summary>
        public static long Unset32(long bits) => bits & ~(long)B32;

        /// <summary>
        /// Takes a long, unsets bit 33 of [0:63], and returns the result.
        /// </summary>
        public static long Unset33(long bits) => bits & ~(long)B33;

        /// <summary>
        /// Takes a long, unsets bit 34 of [0:63], and returns the result.
        /// </summary>
        public static long Unset34(long bits) => bits & ~(long)B34;

        /// <summary>
        /// Takes a long, unsets bit 35 of [0:63], and returns the result.
        /// </summary>
        public static long Unset35(long bits) => bits & ~(long)B35;

        /// <summary>
        /// Takes a long, unsets bit 36 of [0:63], and returns the result.
        /// </summary>
        public static long Unset36(long bits) => bits & ~(long)B36;

        /// <summary>
        /// Takes a long, unsets bit 37 of [0:63], and returns the result.
        /// </summary>
        public static long Unset37(long bits) => bits & ~(long)B37;

        /// <summary>
        /// Takes a long, unsets bit 38 of [0:63], and returns the result.
        /// </summary>
        public static long Unset38(long bits) => bits & ~(long)B38;

        /// <summary>
        /// Takes a long, unsets bit 39 of [0:63], and returns the result.
        /// </summary>
        public static long Unset39(long bits) => bits & ~(long)B39;

        /// <summary>
        /// Takes a long, unsets bit 40 of [0:63], and returns the result.
        /// </summary>
        public static long Unset40(long bits) => bits & ~(long)B40;

        /// <summary>
        /// Takes a long, unsets bit 41 of [0:63], and returns the result.
        /// </summary>
        public static long Unset41(long bits) => bits & ~(long)B41;

        /// <summary>
        /// Takes a long, unsets bit 42 of [0:63], and returns the result.
        /// </summary>
        public static long Unset42(long bits) => bits & ~(long)B42;

        /// <summary>
        /// Takes a long, unsets bit 43 of [0:63], and returns the result.
        /// </summary>
        public static long Unset43(long bits) => bits & ~(long)B43;

        /// <summary>
        /// Takes a long, unsets bit 44 of [0:63], and returns the result.
        /// </summary>
        public static long Unset44(long bits) => bits & ~(long)B44;

        /// <summary>
        /// Takes a long, unsets bit 45 of [0:63], and returns the result.
        /// </summary>
        public static long Unset45(long bits) => bits & ~(long)B45;

        /// <summary>
        /// Takes a long, unsets bit 46 of [0:63], and returns the result.
        /// </summary>
        public static long Unset46(long bits) => bits & ~(long)B46;

        /// <summary>
        /// Takes a long, unsets bit 47 of [0:63], and returns the result.
        /// </summary>
        public static long Unset47(long bits) => bits & ~(long)B47;

        /// <summary>
        /// Takes a long, unsets bit 48 of [0:63], and returns the result.
        /// </summary>
        public static long Unset48(long bits) => bits & ~(long)B48;

        /// <summary>
        /// Takes a long, unsets bit 49 of [0:63], and returns the result.
        /// </summary>
        public static long Unset49(long bits) => bits & ~(long)B49;

        /// <summary>
        /// Takes a long, unsets bit 50 of [0:63], and returns the result.
        /// </summary>
        public static long Unset50(long bits) => bits & ~(long)B50;

        /// <summary>
        /// Takes a long, unsets bit 51 of [0:63], and returns the result.
        /// </summary>
        public static long Unset51(long bits) => bits & ~(long)B51;

        /// <summary>
        /// Takes a long, unsets bit 52 of [0:63], and returns the result.
        /// </summary>
        public static long Unset52(long bits) => bits & ~(long)B52;

        /// <summary>
        /// Takes a long, unsets bit 53 of [0:63], and returns the result.
        /// </summary>
        public static long Unset53(long bits) => bits & ~(long)B53;

        /// <summary>
        /// Takes a long, unsets bit 54 of [0:63], and returns the result.
        /// </summary>
        public static long Unset54(long bits) => bits & ~(long)B54;

        /// <summary>
        /// Takes a long, unsets bit 55 of [0:63], and returns the result.
        /// </summary>
        public static long Unset55(long bits) => bits & ~(long)B55;

        /// <summary>
        /// Takes a long, unsets bit 56 of [0:63], and returns the result.
        /// </summary>
        public static long Unset56(long bits) => bits & ~(long)B56;

        /// <summary>
        /// Takes a long, unsets bit 57 of [0:63], and returns the result.
        /// </summary>
        public static long Unset57(long bits) => bits & ~(long)B57;

        /// <summary>
        /// Takes a long, unsets bit 58 of [0:63], and returns the result.
        /// </summary>
        public static long Unset58(long bits) => bits & ~(long)B58;

        /// <summary>
        /// Takes a long, unsets bit 59 of [0:63], and returns the result.
        /// </summary>
        public static long Unset59(long bits) => bits & ~(long)B59;

        /// <summary>
        /// Takes a long, unsets bit 60 of [0:63], and returns the result.
        /// </summary>
        public static long Unset60(long bits) => bits & ~(long)B60;

        /// <summary>
        /// Takes a long, unsets bit 61 of [0:63], and returns the result.
        /// </summary>
        public static long Unset61(long bits) => bits & ~(long)B61;

        /// <summary>
        /// Takes a long, unsets bit 62 of [0:63], and returns the result.
        /// </summary>
        public static long Unset62(long bits) => bits & ~(long)B62;

        /// <summary>
        /// Takes a long, unsets bit 63 of [0:63], and returns the result.
        /// </summary>
        public static long Unset63(long bits) => bits & (long)~B63;

        #endregion

        #region --- ULong ---

        /// <summary>
        /// Gets the specified bit on an ulong, as a boolean.
        /// </summary>
        /// <param name="bits">The ulong to get a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to get.</param>
        /// <remarks>
        /// Only the lowest six bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x3f</c>.
        /// </remarks>
        public static bool Get(this ulong bits, int bitIndex) => (bits & (1ul << (bitIndex & 0x3f))) != 0;

        // ---

        /// <summary>
        /// Gets bit 0 of [0:63].
        /// </summary>
        public static bool Get0(this ulong bits) => (bits & B0) != 0;

        /// <summary>
        /// Gets bit 1 of [0:63].
        /// </summary>
        public static bool Get1(this ulong bits) => (bits & B1) != 0;

        /// <summary>
        /// Gets bit 2 of [0:63].
        /// </summary>
        public static bool Get2(this ulong bits) => (bits & B2) != 0;

        /// <summary>
        /// Gets bit 3 of [0:63].
        /// </summary>
        public static bool Get3(this ulong bits) => (bits & B3) != 0;

        /// <summary>
        /// Gets bit 4 of [0:63].
        /// </summary>
        public static bool Get4(this ulong bits) => (bits & B4) != 0;

        /// <summary>
        /// Gets bit 5 of [0:63].
        /// </summary>
        public static bool Get5(this ulong bits) => (bits & B5) != 0;

        /// <summary>
        /// Gets bit 6 of [0:63].
        /// </summary>
        public static bool Get6(this ulong bits) => (bits & B6) != 0;

        /// <summary>
        /// Gets bit 7 of [0:63].
        /// </summary>
        public static bool Get7(this ulong bits) => (bits & B7) != 0;

        /// <summary>
        /// Gets bit 8 of [0:63].
        /// </summary>
        public static bool Get8(this ulong bits) => (bits & B8) != 0;

        /// <summary>
        /// Gets bit 9 of [0:63].
        /// </summary>
        public static bool Get9(this ulong bits) => (bits & B9) != 0;

        /// <summary>
        /// Gets bit 10 of [0:63].
        /// </summary>
        public static bool Get10(this ulong bits) => (bits & B10) != 0;

        /// <summary>
        /// Gets bit 11 of [0:63].
        /// </summary>
        public static bool Get11(this ulong bits) => (bits & B11) != 0;

        /// <summary>
        /// Gets bit 12 of [0:63].
        /// </summary>
        public static bool Get12(this ulong bits) => (bits & B12) != 0;

        /// <summary>
        /// Gets bit 13 of [0:63].
        /// </summary>
        public static bool Get13(this ulong bits) => (bits & B13) != 0;

        /// <summary>
        /// Gets bit 14 of [0:63].
        /// </summary>
        public static bool Get14(this ulong bits) => (bits & B14) != 0;

        /// <summary>
        /// Gets bit 15 of [0:63].
        /// </summary>
        public static bool Get15(this ulong bits) => (bits & B15) != 0;

        /// <summary>
        /// Gets bit 16 of [0:63].
        /// </summary>
        public static bool Get16(this ulong bits) => (bits & B16) != 0;

        /// <summary>
        /// Gets bit 17 of [0:63].
        /// </summary>
        public static bool Get17(this ulong bits) => (bits & B17) != 0;

        /// <summary>
        /// Gets bit 18 of [0:63].
        /// </summary>
        public static bool Get18(this ulong bits) => (bits & B18) != 0;

        /// <summary>
        /// Gets bit 19 of [0:63].
        /// </summary>
        public static bool Get19(this ulong bits) => (bits & B19) != 0;

        /// <summary>
        /// Gets bit 20 of [0:63].
        /// </summary>
        public static bool Get20(this ulong bits) => (bits & B20) != 0;

        /// <summary>
        /// Gets bit 21 of [0:63].
        /// </summary>
        public static bool Get21(this ulong bits) => (bits & B21) != 0;

        /// <summary>
        /// Gets bit 22 of [0:63].
        /// </summary>
        public static bool Get22(this ulong bits) => (bits & B22) != 0;

        /// <summary>
        /// Gets bit 23 of [0:63].
        /// </summary>
        public static bool Get23(this ulong bits) => (bits & B23) != 0;

        /// <summary>
        /// Gets bit 24 of [0:63].
        /// </summary>
        public static bool Get24(this ulong bits) => (bits & B24) != 0;

        /// <summary>
        /// Gets bit 25 of [0:63].
        /// </summary>
        public static bool Get25(this ulong bits) => (bits & B25) != 0;

        /// <summary>
        /// Gets bit 26 of [0:63].
        /// </summary>
        public static bool Get26(this ulong bits) => (bits & B26) != 0;

        /// <summary>
        /// Gets bit 27 of [0:63].
        /// </summary>
        public static bool Get27(this ulong bits) => (bits & B27) != 0;

        /// <summary>
        /// Gets bit 28 of [0:63].
        /// </summary>
        public static bool Get28(this ulong bits) => (bits & B28) != 0;

        /// <summary>
        /// Gets bit 29 of [0:63].
        /// </summary>
        public static bool Get29(this ulong bits) => (bits & B29) != 0;

        /// <summary>
        /// Gets bit 30 of [0:63].
        /// </summary>
        public static bool Get30(this ulong bits) => (bits & B30) != 0;

        /// <summary>
        /// Gets bit 31 of [0:63].
        /// </summary>
        public static bool Get31(this ulong bits) => (bits & B31) != 0;

        /// <summary>
        /// Gets bit 32 of [0:63].
        /// </summary>
        public static bool Get32(this ulong bits) => (bits & B32) != 0;

        /// <summary>
        /// Gets bit 33 of [0:63].
        /// </summary>
        public static bool Get33(this ulong bits) => (bits & B33) != 0;

        /// <summary>
        /// Gets bit 34 of [0:63].
        /// </summary>
        public static bool Get34(this ulong bits) => (bits & B34) != 0;

        /// <summary>
        /// Gets bit 35 of [0:63].
        /// </summary>
        public static bool Get35(this ulong bits) => (bits & B35) != 0;

        /// <summary>
        /// Gets bit 36 of [0:63].
        /// </summary>
        public static bool Get36(this ulong bits) => (bits & B36) != 0;

        /// <summary>
        /// Gets bit 37 of [0:63].
        /// </summary>
        public static bool Get37(this ulong bits) => (bits & B37) != 0;

        /// <summary>
        /// Gets bit 38 of [0:63].
        /// </summary>
        public static bool Get38(this ulong bits) => (bits & B38) != 0;

        /// <summary>
        /// Gets bit 39 of [0:63].
        /// </summary>
        public static bool Get39(this ulong bits) => (bits & B39) != 0;

        /// <summary>
        /// Gets bit 40 of [0:63].
        /// </summary>
        public static bool Get40(this ulong bits) => (bits & B40) != 0;

        /// <summary>
        /// Gets bit 41 of [0:63].
        /// </summary>
        public static bool Get41(this ulong bits) => (bits & B41) != 0;

        /// <summary>
        /// Gets bit 42 of [0:63].
        /// </summary>
        public static bool Get42(this ulong bits) => (bits & B42) != 0;

        /// <summary>
        /// Gets bit 43 of [0:63].
        /// </summary>
        public static bool Get43(this ulong bits) => (bits & B43) != 0;

        /// <summary>
        /// Gets bit 44 of [0:63].
        /// </summary>
        public static bool Get44(this ulong bits) => (bits & B44) != 0;

        /// <summary>
        /// Gets bit 45 of [0:63].
        /// </summary>
        public static bool Get45(this ulong bits) => (bits & B45) != 0;

        /// <summary>
        /// Gets bit 46 of [0:63].
        /// </summary>
        public static bool Get46(this ulong bits) => (bits & B46) != 0;

        /// <summary>
        /// Gets bit 47 of [0:63].
        /// </summary>
        public static bool Get47(this ulong bits) => (bits & B47) != 0;

        /// <summary>
        /// Gets bit 48 of [0:63].
        /// </summary>
        public static bool Get48(this ulong bits) => (bits & B48) != 0;

        /// <summary>
        /// Gets bit 49 of [0:63].
        /// </summary>
        public static bool Get49(this ulong bits) => (bits & B49) != 0;

        /// <summary>
        /// Gets bit 50 of [0:63].
        /// </summary>
        public static bool Get50(this ulong bits) => (bits & B50) != 0;

        /// <summary>
        /// Gets bit 51 of [0:63].
        /// </summary>
        public static bool Get51(this ulong bits) => (bits & B51) != 0;

        /// <summary>
        /// Gets bit 52 of [0:63].
        /// </summary>
        public static bool Get52(this ulong bits) => (bits & B52) != 0;

        /// <summary>
        /// Gets bit 53 of [0:63].
        /// </summary>
        public static bool Get53(this ulong bits) => (bits & B53) != 0;

        /// <summary>
        /// Gets bit 54 of [0:63].
        /// </summary>
        public static bool Get54(this ulong bits) => (bits & B54) != 0;

        /// <summary>
        /// Gets bit 55 of [0:63].
        /// </summary>
        public static bool Get55(this ulong bits) => (bits & B55) != 0;

        /// <summary>
        /// Gets bit 56 of [0:63].
        /// </summary>
        public static bool Get56(this ulong bits) => (bits & B56) != 0;

        /// <summary>
        /// Gets bit 57 of [0:63].
        /// </summary>
        public static bool Get57(this ulong bits) => (bits & B57) != 0;

        /// <summary>
        /// Gets bit 58 of [0:63].
        /// </summary>
        public static bool Get58(this ulong bits) => (bits & B58) != 0;

        /// <summary>
        /// Gets bit 59 of [0:63].
        /// </summary>
        public static bool Get59(this ulong bits) => (bits & B59) != 0;

        /// <summary>
        /// Gets bit 60 of [0:63].
        /// </summary>
        public static bool Get60(this ulong bits) => (bits & B60) != 0;

        /// <summary>
        /// Gets bit 61 of [0:63].
        /// </summary>
        public static bool Get61(this ulong bits) => (bits & B61) != 0;

        /// <summary>
        /// Gets bit 62 of [0:63].
        /// </summary>
        public static bool Get62(this ulong bits) => (bits & B62) != 0;

        /// <summary>
        /// Gets bit 63 of [0:63].
        /// </summary>
        public static bool Get63(this ulong bits) => (bits & B63) != 0;

        // ---

        /// <summary>
        /// Takes an ulong, sets the specified bit, and returns the result.
        /// </summary>
        /// <param name="bits">The ulong before setting a bit.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest six bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x3f</c>.
        /// </remarks>
        public static ulong Set(ulong bits, int bitIndex) => bits | (1ul << (bitIndex & 0x3f));

        /// <summary>
        /// Sets the specified bit on an ulong.
        /// </summary>
        /// <param name="bits">The ulong to set a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest six bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x3f</c>.
        /// </remarks>
        public static void Set(ref ulong bits, int bitIndex) => bits |= (1ul << (bitIndex & 0x3f));

        // ---

        /// <summary>
        /// Takes an ulong, sets bit 0 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set0(ulong bits) => bits | B0;

        /// <summary>
        /// Takes an ulong, sets bit 1 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set1(ulong bits) => bits | B1;

        /// <summary>
        /// Takes an ulong, sets bit 2 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set2(ulong bits) => bits | B2;

        /// <summary>
        /// Takes an ulong, sets bit 3 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set3(ulong bits) => bits | B3;

        /// <summary>
        /// Takes an ulong, sets bit 4 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set4(ulong bits) => bits | B4;

        /// <summary>
        /// Takes an ulong, sets bit 5 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set5(ulong bits) => bits | B5;

        /// <summary>
        /// Takes an ulong, sets bit 6 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set6(ulong bits) => bits | B6;

        /// <summary>
        /// Takes an ulong, sets bit 7 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set7(ulong bits) => bits | B7;

        /// <summary>
        /// Takes an ulong, sets bit 8 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set8(ulong bits) => bits | B8;

        /// <summary>
        /// Takes an ulong, sets bit 9 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set9(ulong bits) => bits | B9;

        /// <summary>
        /// Takes an ulong, sets bit 10 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set10(ulong bits) => bits | B10;

        /// <summary>
        /// Takes an ulong, sets bit 11 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set11(ulong bits) => bits | B11;

        /// <summary>
        /// Takes an ulong, sets bit 12 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set12(ulong bits) => bits | B12;

        /// <summary>
        /// Takes an ulong, sets bit 13 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set13(ulong bits) => bits | B13;

        /// <summary>
        /// Takes an ulong, sets bit 14 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set14(ulong bits) => bits | B14;

        /// <summary>
        /// Takes an ulong, sets bit 15 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set15(ulong bits) => bits | B15;

        /// <summary>
        /// Takes an ulong, sets bit 16 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set16(ulong bits) => bits | B16;

        /// <summary>
        /// Takes an ulong, sets bit 17 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set17(ulong bits) => bits | B17;

        /// <summary>
        /// Takes an ulong, sets bit 18 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set18(ulong bits) => bits | B18;

        /// <summary>
        /// Takes an ulong, sets bit 19 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set19(ulong bits) => bits | B19;

        /// <summary>
        /// Takes an ulong, sets bit 20 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set20(ulong bits) => bits | B20;

        /// <summary>
        /// Takes an ulong, sets bit 21 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set21(ulong bits) => bits | B21;

        /// <summary>
        /// Takes an ulong, sets bit 22 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set22(ulong bits) => bits | B22;

        /// <summary>
        /// Takes an ulong, sets bit 23 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set23(ulong bits) => bits | B23;

        /// <summary>
        /// Takes an ulong, sets bit 24 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set24(ulong bits) => bits | B24;

        /// <summary>
        /// Takes an ulong, sets bit 25 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set25(ulong bits) => bits | B25;

        /// <summary>
        /// Takes an ulong, sets bit 26 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set26(ulong bits) => bits | B26;

        /// <summary>
        /// Takes an ulong, sets bit 27 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set27(ulong bits) => bits | B27;

        /// <summary>
        /// Takes an ulong, sets bit 28 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set28(ulong bits) => bits | B28;

        /// <summary>
        /// Takes an ulong, sets bit 29 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set29(ulong bits) => bits | B29;

        /// <summary>
        /// Takes an ulong, sets bit 30 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set30(ulong bits) => bits | B30;

        /// <summary>
        /// Takes an ulong, sets bit 31 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set31(ulong bits) => bits | B31;

        /// <summary>
        /// Takes an ulong, sets bit 32 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set32(ulong bits) => bits | B32;

        /// <summary>
        /// Takes an ulong, sets bit 33 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set33(ulong bits) => bits | B33;

        /// <summary>
        /// Takes an ulong, sets bit 34 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set34(ulong bits) => bits | B34;

        /// <summary>
        /// Takes an ulong, sets bit 35 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set35(ulong bits) => bits | B35;

        /// <summary>
        /// Takes an ulong, sets bit 36 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set36(ulong bits) => bits | B36;

        /// <summary>
        /// Takes an ulong, sets bit 37 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set37(ulong bits) => bits | B37;

        /// <summary>
        /// Takes an ulong, sets bit 38 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set38(ulong bits) => bits | B38;

        /// <summary>
        /// Takes an ulong, sets bit 39 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set39(ulong bits) => bits | B39;

        /// <summary>
        /// Takes an ulong, sets bit 40 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set40(ulong bits) => bits | B40;

        /// <summary>
        /// Takes an ulong, sets bit 41 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set41(ulong bits) => bits | B41;

        /// <summary>
        /// Takes an ulong, sets bit 42 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set42(ulong bits) => bits | B42;

        /// <summary>
        /// Takes an ulong, sets bit 43 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set43(ulong bits) => bits | B43;

        /// <summary>
        /// Takes an ulong, sets bit 44 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set44(ulong bits) => bits | B44;

        /// <summary>
        /// Takes an ulong, sets bit 45 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set45(ulong bits) => bits | B45;

        /// <summary>
        /// Takes an ulong, sets bit 46 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set46(ulong bits) => bits | B46;

        /// <summary>
        /// Takes an ulong, sets bit 47 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set47(ulong bits) => bits | B47;

        /// <summary>
        /// Takes an ulong, sets bit 48 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set48(ulong bits) => bits | B48;

        /// <summary>
        /// Takes an ulong, sets bit 49 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set49(ulong bits) => bits | B49;

        /// <summary>
        /// Takes an ulong, sets bit 50 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set50(ulong bits) => bits | B50;

        /// <summary>
        /// Takes an ulong, sets bit 51 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set51(ulong bits) => bits | B51;

        /// <summary>
        /// Takes an ulong, sets bit 52 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set52(ulong bits) => bits | B52;

        /// <summary>
        /// Takes an ulong, sets bit 53 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set53(ulong bits) => bits | B53;

        /// <summary>
        /// Takes an ulong, sets bit 54 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set54(ulong bits) => bits | B54;

        /// <summary>
        /// Takes an ulong, sets bit 55 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set55(ulong bits) => bits | B55;

        /// <summary>
        /// Takes an ulong, sets bit 56 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set56(ulong bits) => bits | B56;

        /// <summary>
        /// Takes an ulong, sets bit 57 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set57(ulong bits) => bits | B57;

        /// <summary>
        /// Takes an ulong, sets bit 58 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set58(ulong bits) => bits | B58;

        /// <summary>
        /// Takes an ulong, sets bit 59 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set59(ulong bits) => bits | B59;

        /// <summary>
        /// Takes an ulong, sets bit 60 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set60(ulong bits) => bits | B60;

        /// <summary>
        /// Takes an ulong, sets bit 61 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set61(ulong bits) => bits | B61;

        /// <summary>
        /// Takes an ulong, sets bit 62 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set62(ulong bits) => bits | B62;

        /// <summary>
        /// Takes an ulong, sets bit 63 of [0:63], and returns the result.
        /// </summary>
        public static ulong Set63(ulong bits) => bits | B63;

        // ---

        /// <summary>
        /// Takes an ulong, sets or unsets the specified bit according to a boolean value, and returns the result.
        /// </summary>
        /// <param name="bits">The ulong before the set / unset.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set / unset.</param>
        /// <param name="value">If true then the bit will be set to 1; otherwise to 0 (unset).</param>
        /// <remarks>
        /// Only the lowest six bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x3f</c>.
        /// </remarks>
        public static ulong Set(ulong bits, int bitIndex, bool value)
        {
            unchecked
            {
                ulong mask = 1ul << (bitIndex & 0x3f);
                return value ? bits | mask : bits & ~mask;
            }
        }

        /// <summary>
        /// Sets or unsets the specified bit according to a boolean value.
        /// </summary>
        /// <param name="bits">The ulong to set / unset a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set / unset.</param>
        /// <param name="value">If true then the bit will be set to 1; otherwise to 0 (unset).</param>
        /// <remarks>
        /// Only the lowest six bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x3f</c>.
        /// </remarks>
        public static void Set(ref ulong bits, int bitIndex, bool value)
        {
            unchecked
            {
                ulong mask = 1ul << (bitIndex & 0x3f);
                bits = value ? bits | mask : bits & ~mask;
            }
        }

        // ---

        /// <summary>
        /// Takes an ulong, sets or unsets bit 0 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set0(ulong bits, bool value) => value ? bits | B0 : bits & ~(ulong)B0;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 1 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set1(ulong bits, bool value) => value ? bits | B1 : bits & ~(ulong)B1;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 2 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set2(ulong bits, bool value) => value ? bits | B2 : bits & ~(ulong)B2;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 3 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set3(ulong bits, bool value) => value ? bits | B3 : bits & ~(ulong)B3;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 4 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set4(ulong bits, bool value) => value ? bits | B4 : bits & ~(ulong)B4;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 5 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set5(ulong bits, bool value) => value ? bits | B5 : bits & ~(ulong)B5;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 6 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set6(ulong bits, bool value) => value ? bits | B6 : bits & ~(ulong)B6;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 7 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set7(ulong bits, bool value) => value ? bits | B7 : bits & ~(ulong)B7;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 8 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set8(ulong bits, bool value) => value ? bits | B8 : bits & ~(ulong)B8;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 9 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set9(ulong bits, bool value) => value ? bits | B9 : bits & ~(ulong)B9;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 10 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set10(ulong bits, bool value) => value ? bits | B10 : bits & ~(ulong)B10;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 11 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set11(ulong bits, bool value) => value ? bits | B11 : bits & ~(ulong)B11;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 12 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set12(ulong bits, bool value) => value ? bits | B12 : bits & ~(ulong)B12;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 13 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set13(ulong bits, bool value) => value ? bits | B13 : bits & ~(ulong)B13;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 14 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set14(ulong bits, bool value) => value ? bits | B14 : bits & ~(ulong)B14;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 15 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set15(ulong bits, bool value) => value ? bits | B15 : bits & ~(ulong)B15;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 16 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set16(ulong bits, bool value) => value ? bits | B16 : bits & ~(ulong)B16;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 17 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set17(ulong bits, bool value) => value ? bits | B17 : bits & ~(ulong)B17;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 18 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set18(ulong bits, bool value) => value ? bits | B18 : bits & ~(ulong)B18;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 19 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set19(ulong bits, bool value) => value ? bits | B19 : bits & ~(ulong)B19;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 20 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set20(ulong bits, bool value) => value ? bits | B20 : bits & ~(ulong)B20;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 21 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set21(ulong bits, bool value) => value ? bits | B21 : bits & ~(ulong)B21;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 22 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set22(ulong bits, bool value) => value ? bits | B22 : bits & ~(ulong)B22;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 23 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set23(ulong bits, bool value) => value ? bits | B23 : bits & ~(ulong)B23;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 24 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set24(ulong bits, bool value) => value ? bits | B24 : bits & ~(ulong)B24;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 25 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set25(ulong bits, bool value) => value ? bits | B25 : bits & ~(ulong)B25;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 26 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set26(ulong bits, bool value) => value ? bits | B26 : bits & ~(ulong)B26;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 27 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set27(ulong bits, bool value) => value ? bits | B27 : bits & ~(ulong)B27;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 28 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set28(ulong bits, bool value) => value ? bits | B28 : bits & ~(ulong)B28;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 29 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set29(ulong bits, bool value) => value ? bits | B29 : bits & ~(ulong)B29;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 30 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set30(ulong bits, bool value) => value ? bits | B30 : bits & ~(ulong)B30;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 31 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set31(ulong bits, bool value) => value ? bits | B31 : bits & ~(ulong)B31;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 32 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set32(ulong bits, bool value) => value ? bits | B32 : bits & ~B32;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 33 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set33(ulong bits, bool value) => value ? bits | B33 : bits & ~B33;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 34 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set34(ulong bits, bool value) => value ? bits | B34 : bits & ~B34;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 35 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set35(ulong bits, bool value) => value ? bits | B35 : bits & ~B35;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 36 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set36(ulong bits, bool value) => value ? bits | B36 : bits & ~B36;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 37 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set37(ulong bits, bool value) => value ? bits | B37 : bits & ~B37;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 38 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set38(ulong bits, bool value) => value ? bits | B38 : bits & ~B38;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 39 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set39(ulong bits, bool value) => value ? bits | B39 : bits & ~B39;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 40 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set40(ulong bits, bool value) => value ? bits | B40 : bits & ~B40;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 41 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set41(ulong bits, bool value) => value ? bits | B41 : bits & ~B41;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 42 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set42(ulong bits, bool value) => value ? bits | B42 : bits & ~B42;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 43 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set43(ulong bits, bool value) => value ? bits | B43 : bits & ~B43;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 44 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set44(ulong bits, bool value) => value ? bits | B44 : bits & ~B44;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 45 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set45(ulong bits, bool value) => value ? bits | B45 : bits & ~B45;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 46 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set46(ulong bits, bool value) => value ? bits | B46 : bits & ~B46;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 47 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set47(ulong bits, bool value) => value ? bits | B47 : bits & ~B47;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 48 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set48(ulong bits, bool value) => value ? bits | B48 : bits & ~B48;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 49 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set49(ulong bits, bool value) => value ? bits | B49 : bits & ~B49;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 50 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set50(ulong bits, bool value) => value ? bits | B50 : bits & ~B50;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 51 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set51(ulong bits, bool value) => value ? bits | B51 : bits & ~B51;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 52 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set52(ulong bits, bool value) => value ? bits | B52 : bits & ~B52;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 53 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set53(ulong bits, bool value) => value ? bits | B53 : bits & ~B53;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 54 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set54(ulong bits, bool value) => value ? bits | B54 : bits & ~B54;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 55 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set55(ulong bits, bool value) => value ? bits | B55 : bits & ~B55;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 56 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set56(ulong bits, bool value) => value ? bits | B56 : bits & ~B56;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 57 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set57(ulong bits, bool value) => value ? bits | B57 : bits & ~B57;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 58 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set58(ulong bits, bool value) => value ? bits | B58 : bits & ~B58;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 59 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set59(ulong bits, bool value) => value ? bits | B59 : bits & ~B59;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 60 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set60(ulong bits, bool value) => value ? bits | B60 : bits & ~B60;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 61 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set61(ulong bits, bool value) => value ? bits | B61 : bits & ~B61;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 62 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set62(ulong bits, bool value) => value ? bits | B62 : bits & ~B62;

        /// <summary>
        /// Takes an ulong, sets or unsets bit 63 of [0:63] according to a boolean value, and returns the result.
        /// </summary>
        public static ulong Set63(ulong bits, bool value) => value ? bits | B63 : bits & ~B63;

        // ---

        /// <summary>
        /// Takes an ulong, unsets the specified bit, and returns the result.
        /// </summary>
        /// <param name="bits">The ulong before unsetting a bit.</param>
        /// <param name="bitIndex">The zero-based index of the bit to set.</param>
        /// <remarks>
        /// Only the lowest six bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x3f</c>.
        /// </remarks>
        public static ulong Unset(ulong bits, int bitIndex) => bits & ~(1ul << (bitIndex & 0x3f));

        /// <summary>
        /// Unsets the specified bit on an ulong.
        /// </summary>
        /// <param name="bits">The ulong to unset a bit on.</param>
        /// <param name="bitIndex">The zero-based index of the bit to unset.</param>
        /// <remarks>
        /// Only the lowest six bits of <paramref name="bitIndex"/> are used, i.e. <c>bitIndex &amp; 0x3f</c>.
        /// </remarks>
        public static void Unset(ref ulong bits, int bitIndex) => bits &= ~(1ul << (bitIndex & 0x3f));

        // ---

        /// <summary>
        /// Takes an ulong, unsets bit 0 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset0(ulong bits) => bits & ~(ulong)B0;

        /// <summary>
        /// Takes an ulong, unsets bit 1 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset1(ulong bits) => bits & ~(ulong)B1;

        /// <summary>
        /// Takes an ulong, unsets bit 2 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset2(ulong bits) => bits & ~(ulong)B2;

        /// <summary>
        /// Takes an ulong, unsets bit 3 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset3(ulong bits) => bits & ~(ulong)B3;

        /// <summary>
        /// Takes an ulong, unsets bit 4 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset4(ulong bits) => bits & ~(ulong)B4;

        /// <summary>
        /// Takes an ulong, unsets bit 5 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset5(ulong bits) => bits & ~(ulong)B5;

        /// <summary>
        /// Takes an ulong, unsets bit 6 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset6(ulong bits) => bits & ~(ulong)B6;

        /// <summary>
        /// Takes an ulong, unsets bit 7 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset7(ulong bits) => bits & ~(ulong)B7;

        /// <summary>
        /// Takes an ulong, unsets bit 8 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset8(ulong bits) => bits & ~(ulong)B8;

        /// <summary>
        /// Takes an ulong, unsets bit 9 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset9(ulong bits) => bits & ~(ulong)B9;

        /// <summary>
        /// Takes an ulong, unsets bit 10 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset10(ulong bits) => bits & ~(ulong)B10;

        /// <summary>
        /// Takes an ulong, unsets bit 11 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset11(ulong bits) => bits & ~(ulong)B11;

        /// <summary>
        /// Takes an ulong, unsets bit 12 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset12(ulong bits) => bits & ~(ulong)B12;

        /// <summary>
        /// Takes an ulong, unsets bit 13 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset13(ulong bits) => bits & ~(ulong)B13;

        /// <summary>
        /// Takes an ulong, unsets bit 14 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset14(ulong bits) => bits & ~(ulong)B14;

        /// <summary>
        /// Takes an ulong, unsets bit 15 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset15(ulong bits) => bits & ~(ulong)B15;

        /// <summary>
        /// Takes an ulong, unsets bit 16 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset16(ulong bits) => bits & ~(ulong)B16;

        /// <summary>
        /// Takes an ulong, unsets bit 17 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset17(ulong bits) => bits & ~(ulong)B17;

        /// <summary>
        /// Takes an ulong, unsets bit 18 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset18(ulong bits) => bits & ~(ulong)B18;

        /// <summary>
        /// Takes an ulong, unsets bit 19 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset19(ulong bits) => bits & ~(ulong)B19;

        /// <summary>
        /// Takes an ulong, unsets bit 20 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset20(ulong bits) => bits & ~(ulong)B20;

        /// <summary>
        /// Takes an ulong, unsets bit 21 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset21(ulong bits) => bits & ~(ulong)B21;

        /// <summary>
        /// Takes an ulong, unsets bit 22 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset22(ulong bits) => bits & ~(ulong)B22;

        /// <summary>
        /// Takes an ulong, unsets bit 23 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset23(ulong bits) => bits & ~(ulong)B23;

        /// <summary>
        /// Takes an ulong, unsets bit 24 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset24(ulong bits) => bits & ~(ulong)B24;

        /// <summary>
        /// Takes an ulong, unsets bit 25 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset25(ulong bits) => bits & ~(ulong)B25;

        /// <summary>
        /// Takes an ulong, unsets bit 26 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset26(ulong bits) => bits & ~(ulong)B26;

        /// <summary>
        /// Takes an ulong, unsets bit 27 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset27(ulong bits) => bits & ~(ulong)B27;

        /// <summary>
        /// Takes an ulong, unsets bit 28 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset28(ulong bits) => bits & ~(ulong)B28;

        /// <summary>
        /// Takes an ulong, unsets bit 29 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset29(ulong bits) => bits & ~(ulong)B29;

        /// <summary>
        /// Takes an ulong, unsets bit 30 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset30(ulong bits) => bits & ~(ulong)B30;

        /// <summary>
        /// Takes an ulong, unsets bit 31 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset31(ulong bits) => bits & ~(ulong)B31;

        /// <summary>
        /// Takes an ulong, unsets bit 32 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset32(ulong bits) => bits & ~B32;

        /// <summary>
        /// Takes an ulong, unsets bit 33 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset33(ulong bits) => bits & ~B33;

        /// <summary>
        /// Takes an ulong, unsets bit 34 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset34(ulong bits) => bits & ~B34;

        /// <summary>
        /// Takes an ulong, unsets bit 35 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset35(ulong bits) => bits & ~B35;

        /// <summary>
        /// Takes an ulong, unsets bit 36 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset36(ulong bits) => bits & ~B36;

        /// <summary>
        /// Takes an ulong, unsets bit 37 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset37(ulong bits) => bits & ~B37;

        /// <summary>
        /// Takes an ulong, unsets bit 38 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset38(ulong bits) => bits & ~B38;

        /// <summary>
        /// Takes an ulong, unsets bit 39 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset39(ulong bits) => bits & ~B39;

        /// <summary>
        /// Takes an ulong, unsets bit 40 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset40(ulong bits) => bits & ~B40;

        /// <summary>
        /// Takes an ulong, unsets bit 41 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset41(ulong bits) => bits & ~B41;

        /// <summary>
        /// Takes an ulong, unsets bit 42 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset42(ulong bits) => bits & ~B42;

        /// <summary>
        /// Takes an ulong, unsets bit 43 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset43(ulong bits) => bits & ~B43;

        /// <summary>
        /// Takes an ulong, unsets bit 44 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset44(ulong bits) => bits & ~B44;

        /// <summary>
        /// Takes an ulong, unsets bit 45 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset45(ulong bits) => bits & ~B45;

        /// <summary>
        /// Takes an ulong, unsets bit 46 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset46(ulong bits) => bits & ~B46;

        /// <summary>
        /// Takes an ulong, unsets bit 47 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset47(ulong bits) => bits & ~B47;

        /// <summary>
        /// Takes an ulong, unsets bit 48 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset48(ulong bits) => bits & ~B48;

        /// <summary>
        /// Takes an ulong, unsets bit 49 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset49(ulong bits) => bits & ~B49;

        /// <summary>
        /// Takes an ulong, unsets bit 50 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset50(ulong bits) => bits & ~B50;

        /// <summary>
        /// Takes an ulong, unsets bit 51 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset51(ulong bits) => bits & ~B51;

        /// <summary>
        /// Takes an ulong, unsets bit 52 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset52(ulong bits) => bits & ~B52;

        /// <summary>
        /// Takes an ulong, unsets bit 53 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset53(ulong bits) => bits & ~B53;

        /// <summary>
        /// Takes an ulong, unsets bit 54 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset54(ulong bits) => bits & ~B54;

        /// <summary>
        /// Takes an ulong, unsets bit 55 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset55(ulong bits) => bits & ~B55;

        /// <summary>
        /// Takes an ulong, unsets bit 56 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset56(ulong bits) => bits & ~B56;

        /// <summary>
        /// Takes an ulong, unsets bit 57 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset57(ulong bits) => bits & ~B57;

        /// <summary>
        /// Takes an ulong, unsets bit 58 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset58(ulong bits) => bits & ~B58;

        /// <summary>
        /// Takes an ulong, unsets bit 59 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset59(ulong bits) => bits & ~B59;

        /// <summary>
        /// Takes an ulong, unsets bit 60 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset60(ulong bits) => bits & ~B60;

        /// <summary>
        /// Takes an ulong, unsets bit 61 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset61(ulong bits) => bits & ~B61;

        /// <summary>
        /// Takes an ulong, unsets bit 62 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset62(ulong bits) => bits & ~B62;

        /// <summary>
        /// Takes an ulong, unsets bit 63 of [0:63], and returns the result.
        /// </summary>
        public static ulong Unset63(ulong bits) => bits & ~B63;

        #endregion

        #region ((( CodeGen )))
#if AZ_CODEGEN_INTERNAL
        internal static string _CG_GetLong()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            const string TYPE = "long";
            const int MAX = 63;

            for (int i = 0; i <= MAX / 2; ++i)
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine($"/// Gets bit {i} of [0:{MAX}].");
                sb.AppendLine("/// </summary>");
                sb.AppendLine($"public static bool Get{i}(this {TYPE} bits) => (bits & B{i}) != 0;");
                sb.AppendLine();
            }

            for (int i = MAX / 2 + 1; i <= MAX; ++i)
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine($"/// Gets bit {i} of [0:{MAX}].");
                sb.AppendLine("/// </summary>");
                sb.AppendLine($"public static bool Get{i}(this {TYPE} bits) => (bits & ({TYPE})B{i}) != 0;");
                sb.AppendLine();
            }

            return sb.ToString();
        }

        internal static string _CG_SetLong()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            const string TYPE = "long";
            const int MAX = 63;

            for (int i = 0; i <= MAX / 2; ++i)
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine($"/// Takes a {TYPE}, sets bit {i} of [0:{MAX}], and returns the result.");
                sb.AppendLine("/// </summary>");
                sb.AppendLine($"public static {TYPE} Set{i}({TYPE} bits) => bits | B{i};");
                sb.AppendLine();
            }

            for (int i = MAX / 2 + 1; i <= MAX; ++i)
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine($"/// Takes a {TYPE}, sets bit {i} of [0:{MAX}], and returns the result.");
                sb.AppendLine("/// </summary>");
                sb.AppendLine($"public static {TYPE} Set{i}({TYPE} bits) => bits | ({TYPE})B{i};");
                sb.AppendLine();
            }

            return sb.ToString();
        }

        internal static string _CG_SetBoolLong()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            const string TYPE = "long";
            const int MAX = 63;

            for (int i = 0; i <= MAX / 2; ++i)
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine($"/// Takes a {TYPE}, sets or unsets bit {i} of [0:{MAX}] according to a boolean value, and returns the result.");
                sb.AppendLine("/// </summary>");
                sb.AppendLine($"public static {TYPE} Set{i}({TYPE} bits, bool value) => value ? bits | B{i} : bits & ~B{i};");
                sb.AppendLine();
            }

            for (int i = MAX / 2 + 1; i <= MAX; ++i)
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine($"/// Takes a {TYPE}, sets or unsets bit {i} of [0:{MAX}] according to a boolean value, and returns the result.");
                sb.AppendLine("/// </summary>");
                sb.AppendLine($"public static {TYPE} Set{i}({TYPE} bits, bool value) => value ? bits | ({TYPE})B{i} : bits & ~({TYPE})B{i};");
                sb.AppendLine();
            }

            return sb.ToString();
        }

        internal static string _CG_UnsetLong()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            const string TYPE = "long";
            const int MAX = 63;

            for (int i = 0; i <= MAX / 2; ++i)
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine($"/// Takes a {TYPE}, unsets bit {i} of [0:{MAX}], and returns the result.");
                sb.AppendLine("/// </summary>");
                sb.AppendLine($"public static {TYPE} Unset{i}({TYPE} bits) => bits & ~B{i};");
                sb.AppendLine();
            }

            for (int i = MAX / 2 + 1; i <= MAX; ++i)
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine($"/// Takes a {TYPE}, unsets bit {i} of [0:{MAX}], and returns the result.");
                sb.AppendLine("/// </summary>");
                sb.AppendLine($"public static {TYPE} Unset{i}({TYPE} bits) => bits & ~({TYPE})B{i};");
                sb.AppendLine();
            }

            return sb.ToString();
        }
        
        internal static string _CG_GetULong()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            const string TYPE = "ulong";
            const int MAX = 63;

            for (int i = 0; i <= MAX; ++i)
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine($"/// Gets bit {i} of [0:{MAX}].");
                sb.AppendLine("/// </summary>");
                sb.AppendLine($"public static bool Get{i}(this {TYPE} bits) => (bits & B{i}) != 0;");
                sb.AppendLine();
            }

            return sb.ToString();
        }

        internal static string _CG_SetULong()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            const string TYPE = "ulong";
            const int MAX = 63;

            for (int i = 0; i <= MAX; ++i)
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine($"/// Takes an {TYPE}, sets bit {i} of [0:{MAX}], and returns the result.");
                sb.AppendLine("/// </summary>");
                sb.AppendLine($"public static {TYPE} Set{i}({TYPE} bits) => bits | B{i};");
                sb.AppendLine();
            }

            return sb.ToString();
        }

        internal static string _CG_SetBoolULong()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            const string TYPE = "ulong";
            const int MAX = 63;

            for (int i = 0; i <= MAX / 2; ++i)
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine($"/// Takes an {TYPE}, sets or unsets bit {i} of [0:{MAX}] according to a boolean value, and returns the result.");
                sb.AppendLine("/// </summary>");
                sb.AppendLine($"public static {TYPE} Set{i}({TYPE} bits, bool value) => value ? bits | B{i} : bits & ~({TYPE})B{i};");
                sb.AppendLine();
            }

            for (int i = MAX / 2 + 1; i <= MAX; ++i)
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine($"/// Takes an {TYPE}, sets or unsets bit {i} of [0:{MAX}] according to a boolean value, and returns the result.");
                sb.AppendLine("/// </summary>");
                sb.AppendLine($"public static {TYPE} Set{i}({TYPE} bits, bool value) => value ? bits | B{i} : bits & ~B{i};");
                sb.AppendLine();
            }

            return sb.ToString();
        }

        internal static string _CG_UnsetULong()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            const string TYPE = "ulong";
            const int MAX = 63;

            for (int i = 0; i <= MAX / 2; ++i)
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine($"/// Takes an {TYPE}, unsets bit {i} of [0:{MAX}], and returns the result.");
                sb.AppendLine("/// </summary>");
                sb.AppendLine($"public static {TYPE} Unset{i}({TYPE} bits) => bits & ~({TYPE})B{i};");
                sb.AppendLine();
            }

            for (int i = MAX / 2 + 1; i <= MAX; ++i)
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine($"/// Takes an {TYPE}, unsets bit {i} of [0:{MAX}], and returns the result.");
                sb.AppendLine("/// </summary>");
                sb.AppendLine($"public static {TYPE} Unset{i}({TYPE} bits) => bits & ~B{i};");
                sb.AppendLine();
            }

            return sb.ToString();
        }
#endif
        #endregion
    }
}
