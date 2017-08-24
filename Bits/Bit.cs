
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
    }
}
