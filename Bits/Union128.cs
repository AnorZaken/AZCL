using System;
using System.Runtime.InteropServices;

namespace AZCL.Bits
{
    /// <summary>
    /// An 128-bit Union of standard c# value-types: [unsigned] longs/ints/shorts/bytes, four floats and two doubles.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Union128
    {
        /// <summary>
        /// Creates an 128bit Union initialized from two int64 values.
        /// </summary>
        /// <param name="low">The lower 8 bytes / 64 bits. See <see cref="long_0_7"/>.</param>
        /// <param name="high">The higher 8 bytes / 64 bits. See <see cref="long_8_15"/></param>
        public Union128(long low, long high)
            : this()
        {
            this.long_0_7 = low;
            this.long_8_15 = high;
        }

        /// <summary>
        /// Creates an 128bit Union initialized from two double values.
        /// </summary>
        /// <param name="low">The lower 8 bytes / 64 bits. See <see cref="double_0_7"/>.</param>
        /// <param name="high">The higher 8 bytes / 64 bits. See <see cref="double_8_15"/></param>
        public Union128(double low, double high)
            : this()
        {
            this.double_0_7 = low;
            this.double_8_15 = high;
        }

        /// <summary>
        /// Gets or sets the i'th byte.
        /// </summary>
        /// <param name="i">The index of the byte to get or set [0-15].</param>
        public byte this[int i] // Union128 has 16 bytes.
        {
            get
            {
                unchecked
                {
                    if (i < 8) // from the lowest 8 bytes...
                    {
                        if (i < 0)
                            throw new IndexOutOfRangeException();

                        // shifting << 3 is the same as multiply by 8 (it is done to go from bytes to bits).
                        return (byte)(ulong_0_7 >> (i << 3));
                    }
                    else // from the highest 8 bytes...
                    {
                        if (i >= 16)
                            throw new IndexOutOfRangeException();

                        // subtract 8 to adjust the shift for the upper 8 bytes - also see above.
                        return (byte)(ulong_8_15 >> ((i - 8) << 3));
                    }
                }
            }
            set
            {
                const uint BYTE = 0xff;
                unchecked
                {
                    if (i < 8) // to the lowest 8 bytes...
                    {
                        if (i < 0)
                            throw new IndexOutOfRangeException();

                        int bits = i << 3; // <-- the same as multiply by 8 (it is done to go from bytes to bits).
                        ulong_0_7 &= ~(BYTE << bits); //   <--  erase current bits of byte #i.
                        ulong_0_7 |= ((ulong)value) << bits;//  <-- write new bits to byte #i.
                    }
                    else // to the highest 8 bytes...
                    {
                        if (i >= 16)
                            throw new IndexOutOfRangeException();

                        int bits = (i - 8) << 3; // <-- subtracting 8 to adjust into upper 8 bytes - also see above.
                        ulong_8_15 &= ~(BYTE << bits); //   <--  erase current bits of byte #i.
                        ulong_8_15 |= ((ulong)value) << bits;//  <-- write new bits to byte #i.
                    }
                }
            }
        }

        /// <summary>
        /// A double covering byte 0-7 (lower half).
        /// </summary>
        [FieldOffset(0)]
        public double double_0_7;

        /// <summary>
        /// A long covering byte 0-7 (lower half).
        /// </summary>
        [FieldOffset(0)]
        public long long_0_7;
        /// <summary>
        /// An ulong covering byte 0-7 (lower half).
        /// </summary>
        [FieldOffset(0)]
        public ulong ulong_0_7;

        /// <summary>
        /// A float covering byte 0-3 (lowest quarter).
        /// </summary>
        [FieldOffset(0)]
        public float float_0_3;

        /// <summary>
        /// An int covering byte 0-3 (lowest quarter).
        /// </summary>
        [FieldOffset(0)]
        public int int_0_3;
        /// <summary>
        /// An uint covering byte 0-3 (lowest quarter).
        /// </summary>
        [FieldOffset(0)]
        public uint uint_0_3;

        /// <summary>
        /// A short covering byte 0-1 (lowest eighth).
        /// </summary>
        [FieldOffset(0)]
        public short short_0_1;
        /// <summary>
        /// An ushort covering byte 0-1 (lowest eighth).
        /// </summary>
        [FieldOffset(0)]
        public ushort ushort_0_1;

        /// <summary>
        /// Signed byte 0 (lowest).
        /// </summary>
        [FieldOffset(0)]
        public sbyte sbyte_0;
        /// <summary>
        /// Byte 0 (lowest).
        /// </summary>
        [FieldOffset(0)]
        public byte byte_0;

        /// <summary>
        /// Signed byte 1 (second lowest).
        /// </summary>
        [FieldOffset(1)]
        public sbyte sbyte_1;
        /// <summary>
        /// Byte 1 (second lowest).
        /// </summary>
        [FieldOffset(1)]
        public byte byte_1;

        /// <summary>
        /// A short covering byte 2-3.
        /// </summary>
        [FieldOffset(2)]
        public short short_2_3;
        /// <summary>
        /// An ushort covering byte 2-3.
        /// </summary>
        [FieldOffset(2)]
        public ushort ushort_2_3;

        /// <summary>
        /// Signed byte 2 (third lowest).
        /// </summary>
        [FieldOffset(2)]
        public sbyte sbyte_2;
        /// <summary>
        /// Byte 2 (third lowest).
        /// </summary>
        [FieldOffset(2)]
        public byte byte_2;

        /// <summary>
        /// Signed byte 3 (fourth lowest).
        /// </summary>
        [FieldOffset(3)]
        public sbyte sbyte_3;
        /// <summary>
        /// Byte 3 (fourth lowest).
        /// </summary>
        [FieldOffset(3)]
        public byte byte_3;

        /// <summary>
        /// A float covering byte 4-7 (low quarter).
        /// </summary>
        [FieldOffset(4)]
        public float float_4_7;

        /// <summary>
        /// An int covering byte 4-7 (low quarter).
        /// </summary>
        [FieldOffset(4)]
        public int int_4_7;
        /// <summary>
        /// An uint covering byte 4-7 (low quarter).
        /// </summary>
        [FieldOffset(4)]
        public uint uint_4_7;

        /// <summary>
        /// A short covering byte 4-5.
        /// </summary>
        [FieldOffset(4)]
        public short short_4_5;
        /// <summary>
        /// An ushort covering byte 4-5.
        /// </summary>
        [FieldOffset(4)]
        public ushort ushort_4_5;

        /// <summary>
        /// Signed byte 4 (fifth lowest).
        /// </summary>
        [FieldOffset(4)]
        public sbyte sbyte_4;
        /// <summary>
        /// Byte 4 (fifth lowest).
        /// </summary>
        [FieldOffset(4)]
        public byte byte_4;

        /// <summary>
        /// Signed byte 5 (sixth lowest).
        /// </summary>
        [FieldOffset(5)]
        public sbyte sbyte_5;
        /// <summary>
        /// Byte 5 (sixth lowest).
        /// </summary>
        [FieldOffset(5)]
        public byte byte_5;

        /// <summary>
        /// A short covering byte 6-7.
        /// </summary>
        [FieldOffset(6)]
        public short short_6_7;
        /// <summary>
        /// An ushort covering byte 6-7.
        /// </summary>
        [FieldOffset(6)]
        public ushort ushort_6_7;

        /// <summary>
        /// Signed byte 6 (seventh lowest).
        /// </summary>
        [FieldOffset(6)]
        public sbyte sbyte_6;
        /// <summary>
        /// Byte 6 (seventh lowest).
        /// </summary>
        [FieldOffset(6)]
        public byte byte_6;

        /// <summary>
        /// Signed byte 7 (eighth lowest).
        /// </summary>
        [FieldOffset(7)]
        public sbyte sbyte_7;
        /// <summary>
        /// Byte 7 (eighth lowest).
        /// </summary>
        [FieldOffset(7)]
        public byte byte_7;

        /// <summary>
        /// A double covering byte 8-15 (higher half).
        /// </summary>
        [FieldOffset(8)]
        public double double_8_15;

        /// <summary>
        /// A long covering byte 8-15 (higher half).
        /// </summary>
        [FieldOffset(8)]
        public long long_8_15;
        /// <summary>
        /// An ulong covering byte 8-15 (higher half).
        /// </summary>
        [FieldOffset(8)]
        public ulong ulong_8_15;

        /// <summary>
        /// A float covering byte 8-11 (high quarter).
        /// </summary>
        [FieldOffset(8)]
        public float float_8_11;

        /// <summary>
        /// An int covering byte 8-11 (high quarter).
        /// </summary>
        [FieldOffset(8)]
        public int int_8_11;
        /// <summary>
        /// An uint covering byte 8-11 (high quarter).
        /// </summary>
        [FieldOffset(8)]
        public uint uint_8_11;

        /// <summary>
        /// A short covering byte 8-9.
        /// </summary>
        [FieldOffset(8)]
        public short short_8_9;
        /// <summary>
        /// An ushort covering byte 8-9.
        /// </summary>
        [FieldOffset(8)]
        public ushort ushort_8_9;

        /// <summary>
        /// Signed byte 8 (eighth highest).
        /// </summary>
        [FieldOffset(8)]
        public sbyte sbyte_8;
        /// <summary>
        /// Byte 8 (eighth highest).
        /// </summary>
        [FieldOffset(8)]
        public byte byte_8;

        /// <summary>
        /// Signed byte 9 (seventh highest).
        /// </summary>
        [FieldOffset(9)]
        public sbyte sbyte_9;
        /// <summary>
        /// Byte 9 (seventh highest).
        /// </summary>
        [FieldOffset(9)]
        public byte byte_9;

        /// <summary>
        /// A short covering byte 10-11.
        /// </summary>
        [FieldOffset(10)]
        public short short_10_11;
        /// <summary>
        /// An ushort covering byte 10-11.
        /// </summary>
        [FieldOffset(10)]
        public ushort ushort_10_11;

        /// <summary>
        /// Signed byte 10 (sixth highest).
        /// </summary>
        [FieldOffset(10)]
        public sbyte sbyte_10;
        /// <summary>
        /// Byte 10 (sixth highest).
        /// </summary>
        [FieldOffset(10)]
        public byte byte_10;

        /// <summary>
        /// Signed byte 11 (fifth highest).
        /// </summary>
        [FieldOffset(11)]
        public sbyte sbyte_11;
        /// <summary>
        /// Byte 11 (fifth highest).
        /// </summary>
        [FieldOffset(11)]
        public byte byte_11;

        /// <summary>
        /// A float covering byte 12-15 (highest quarter).
        /// </summary>
        [FieldOffset(12)]
        public float float_12_15;

        /// <summary>
        /// An int covering byte 12-15 (highest quarter).
        /// </summary>
        [FieldOffset(12)]
        public int int_12_15;
        /// <summary>
        /// An uint covering byte 12-15 (highest quarter).
        /// </summary>
        [FieldOffset(12)]
        public uint uint_12_15;

        /// <summary>
        /// A short covering byte 12-13.
        /// </summary>
        [FieldOffset(12)]
        public short short_12_13;
        /// <summary>
        /// An ushort covering byte 12-13.
        /// </summary>
        [FieldOffset(12)]
        public ushort ushort_12_13;

        /// <summary>
        /// Signed byte 12 (fourth highest).
        /// </summary>
        [FieldOffset(12)]
        public sbyte sbyte_12;
        /// <summary>
        /// Byte 12 (fourth highest).
        /// </summary>
        [FieldOffset(12)]
        public byte byte_12;

        /// <summary>
        /// Signed byte 13 (third highest).
        /// </summary>
        [FieldOffset(13)]
        public sbyte sbyte_13;
        /// <summary>
        /// Byte 13 (third highest).
        /// </summary>
        [FieldOffset(13)]
        public byte byte_13;

        /// <summary>
        /// A short covering byte 14-15 (highest eighth).
        /// </summary>
        [FieldOffset(14)]
        public short short_14_15;
        /// <summary>
        /// An ushort covering byte 14-15 (highest eighth).
        /// </summary>
        [FieldOffset(14)]
        public ushort ushort_14_15;

        /// <summary>
        /// Signed byte 14 (second highest).
        /// </summary>
        [FieldOffset(14)]
        public sbyte sbyte_14;
        /// <summary>
        /// Byte 14 (second highest).
        /// </summary>
        [FieldOffset(14)]
        public byte byte_14;

        /// <summary>
        /// Signed byte 15 (highest).
        /// </summary>
        [FieldOffset(15)]
        public sbyte sbyte_15;
        /// <summary>
        /// Byte 15 (highest).
        /// </summary>
        [FieldOffset(15)]
        public byte byte_15;
    }
}
