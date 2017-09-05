
namespace AZCL.Bits
{
    /// <summary>
    /// Static class for performing bit rotations on integral types.
    /// </summary><remarks>
    /// A bit rotation is a bit shift with wrap-around. Bits that would get shifted out get shifted back in at the other end. Thus no bits / information is lost.
    /// </remarks>
    public static class Rotate
    {
        /// <summary>
        /// Bit rotates <paramref name="value"/> to the left (low towards high).
        /// </summary><remarks>
        /// The <paramref name="rotation"/> parameter is anded with 7 (0000'0111) to force it into appropriate range.
        /// </remarks>
        /// <param name="value">A value whose bits are to be rotated.</param>
        /// <param name="rotation">The number of bits to rotate.</param>
        public static sbyte Left(sbyte value, int rotation)
        {
            const int L = 8;
            unchecked
            {
                rotation &= L - 1;
                uint val = (byte)value; // (casting directly to uint causes sign extension!)
                val = val << rotation | val >> L - rotation;
                return (sbyte)val;
            }
        }

        /// <summary>
        /// Bit rotates <paramref name="value"/> to the left (low towards high).
        /// </summary><remarks>
        /// The <paramref name="rotation"/> parameter is anded with 7 (0000'0111) to force it into appropriate range.
        /// </remarks>
        /// <param name="value">A value whose bits are to be rotated.</param>
        /// <param name="rotation">The number of bits to rotate.</param>
        public static byte Left(byte value, int rotation)
        {
            const int L = 8;
            unchecked
            {
                rotation &= L - 1;
                uint val = value;
                val = val << rotation | val >> L - rotation;
                return (byte)val;
            }
        }

        /// <summary>
        /// Bit rotates <paramref name="value"/> to the left (low towards high).
        /// </summary><remarks>
        /// The <paramref name="rotation"/> parameter is anded with 15 (0000'1111) to force it into appropriate range.
        /// </remarks>
        /// <param name="value">A value whose bits are to be rotated.</param>
        /// <param name="rotation">The number of bits to rotate.</param>
        public static short Left(short value, int rotation)
        {
            const int L = 16;
            unchecked
            {
                rotation &= L - 1;
                uint val = (ushort)value; // (casting directly to uint causes sign extension!)
                val = val << rotation | val >> L - rotation;
                return (short)val;
            }
        }

        /// <summary>
        /// Bit rotates <paramref name="value"/> to the left (low towards high).
        /// </summary><remarks>
        /// The <paramref name="rotation"/> parameter is anded with 15 (0000'1111) to force it into appropriate range.
        /// </remarks>
        /// <param name="value">A value whose bits are to be rotated.</param>
        /// <param name="rotation">The number of bits to rotate.</param>
        public static ushort Left(ushort value, int rotation)
        {
            const int L = 16;
            unchecked
            {
                rotation &= L - 1;
                uint val = value;
                val = val << rotation | val >> L - rotation;
                return (ushort)val;
            }
        }

        /// <summary>
        /// Bit rotates <paramref name="value"/> to the left (low towards high).
        /// </summary><remarks>
        /// The <paramref name="rotation"/> parameter is anded with 31 (0001'1111) to force it into appropriate range.
        /// </remarks>
        /// <param name="value">A value whose bits are to be rotated.</param>
        /// <param name="rotation">The number of bits to rotate.</param>
        public static int Left(int value, int rotation)
        {
            const int L = 32;
            unchecked
            {
                return value << rotation | (int)((uint)value >> L - rotation);
            }
        }

        /// <summary>
        /// Bit rotates <paramref name="value"/> to the left (low towards high).
        /// </summary><remarks>
        /// The <paramref name="rotation"/> parameter is anded with 31 (0001'1111) to force it into appropriate range.
        /// </remarks>
        /// <param name="value">A value whose bits are to be rotated.</param>
        /// <param name="rotation">The number of bits to rotate.</param>
        public static uint Left(uint value, int rotation)
        {
            const int L = 32;
            unchecked
            {
                return value << rotation | value >> L - rotation; // ("L - rotation" can overflow)
            }
        }

        /// <summary>
        /// Bit rotates <paramref name="value"/> to the left (low towards high).
        /// </summary><remarks>
        /// The <paramref name="rotation"/> parameter is anded with 63 (0011'1111) to force it into appropriate range.
        /// </remarks>
        /// <param name="value">A value whose bits are to be rotated.</param>
        /// <param name="rotation">The number of bits to rotate.</param>
        public static long Left(long value, int rotation)
        {
            const int L = 64;
            unchecked
            {
                return value << rotation | (long)((ulong)value >> L - rotation);
            }
        }

        /// <summary>
        /// Bit rotates <paramref name="value"/> to the left (low towards high).
        /// </summary><remarks>
        /// The <paramref name="rotation"/> parameter is anded with 63 (0011'1111) to force it into appropriate range.
        /// </remarks>
        /// <param name="value">A value whose bits are to be rotated.</param>
        /// <param name="rotation">The number of bits to rotate.</param>
        public static ulong Left(ulong value, int rotation)
        {
            const int L = 64;
            unchecked
            {
                return value << rotation | value >> L - rotation; // ("L - rotation" can overflow)
            }
        }

        // -----

        /// <summary>
        /// Bit rotates <paramref name="value"/> to the right (high towards low).
        /// </summary><remarks>
        /// The <paramref name="rotation"/> parameter is anded with 7 (0000'0111) to force it into appropriate range.
        /// </remarks>
        /// <param name="value">A value whose bits are to be rotated.</param>
        /// <param name="rotation">The number of bits to rotate.</param>
        public static sbyte Right(sbyte value, int rotation)
            => Left(value, -rotation);
        
        /// <summary>
        /// Bit rotates <paramref name="value"/> to the right (high towards low).
        /// </summary><remarks>
        /// The <paramref name="rotation"/> parameter is anded with 7 (0000'0111) to force it into appropriate range.
        /// </remarks>
        /// <param name="value">A value whose bits are to be rotated.</param>
        /// <param name="rotation">The number of bits to rotate.</param>
        public static byte Right(byte value, int rotation)
            => Left(value, -rotation);
        
        /// <summary>
        /// Bit rotates <paramref name="value"/> to the right (high towards low).
        /// </summary><remarks>
        /// The <paramref name="rotation"/> parameter is anded with 15 (0000'1111) to force it into appropriate range.
        /// </remarks>
        /// <param name="value">A value whose bits are to be rotated.</param>
        /// <param name="rotation">The number of bits to rotate.</param>
        public static short Right(short value, int rotation)
            => Left(value, -rotation);
        
        /// <summary>
        /// Bit rotates <paramref name="value"/> to the right (high towards low).
        /// </summary><remarks>
        /// The <paramref name="rotation"/> parameter is anded with 15 (0000'1111) to force it into appropriate range.
        /// </remarks>
        /// <param name="value">A value whose bits are to be rotated.</param>
        /// <param name="rotation">The number of bits to rotate.</param>
        public static ushort Right(ushort value, int rotation)
            => Left(value, -rotation);
        
        /// <summary>
        /// Bit rotates <paramref name="value"/> to the right (high towards low).
        /// </summary><remarks>
        /// The <paramref name="rotation"/> parameter is anded with 31 (0001'1111) to force it into appropriate range.
        /// </remarks>
        /// <param name="value">A value whose bits are to be rotated.</param>
        /// <param name="rotation">The number of bits to rotate.</param>
        public static int Right(int value, int rotation)
        {
            const int L = 32;
            unchecked
            {
                return (int)((uint)value >> rotation) | value << L - rotation;
            }
        }

        /// <summary>
        /// Bit rotates <paramref name="value"/> to the right (high towards low).
        /// </summary><remarks>
        /// The <paramref name="rotation"/> parameter is anded with 31 (0001'1111) to force it into appropriate range.
        /// </remarks>
        /// <param name="value">A value whose bits are to be rotated.</param>
        /// <param name="rotation">The number of bits to rotate.</param>
        public static uint Right(uint value, int rotation)
        {
            const int L = 32;
            unchecked
            {
                return value >> rotation | value << L - rotation;
            }
        }

        /// <summary>
        /// Bit rotates <paramref name="value"/> to the right (high towards low).
        /// </summary><remarks>
        /// The <paramref name="rotation"/> parameter is anded with 63 (0011'1111) to force it into appropriate range.
        /// </remarks>
        /// <param name="value">A value whose bits are to be rotated.</param>
        /// <param name="rotation">The number of bits to rotate.</param>
        public static long Right(long value, int rotation)
        {
            const int L = 64;
            unchecked
            {
                return (long)((ulong)value >> rotation) | value << L - rotation;
            }
        }

        /// <summary>
        /// Bit rotates <paramref name="value"/> to the right (high towards low).
        /// </summary><remarks>
        /// The <paramref name="rotation"/> parameter is anded with 63 (0011'1111) to force it into appropriate range.
        /// </remarks>
        /// <param name="value">A value whose bits are to be rotated.</param>
        /// <param name="rotation">The number of bits to rotate.</param>
        public static ulong Right(ulong value, int rotation)
        {
            const int L = 64;
            unchecked
            {
                return value >> rotation | value << L - rotation;
            }
        }
    }
}
