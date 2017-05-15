using System;
using System.Runtime.InteropServices;

namespace AZCL.Bits
{
    /// <summary>
    /// A 64-bit Union of standard c# value-types: [unsigned] long/ints/shorts/bytes, two floats and a double value.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Union64
    {
        /// <summary>
        /// Creates a 64bit Union initialized from two int32 values.
        /// </summary>
        /// <param name="low">The lower 4 bytes / 32 bits. See <see cref="int_0_3"/>.</param>
        /// <param name="high">The higher 4 bytes / 32 bits. See <see cref="int_4_7"/></param>
        public Union64(int low, int high)
            : this()
        {
            this.int_0_3 = low;
            this.int_4_7 = high;
        }

        /// <summary>
        /// Creates a 64bit Union initialized from an int64 value.
        /// </summary>
        /// <param name="value">The long value to initialize with.</param>
        public Union64(long value)
            : this()
        {
            this.long_0_7 = value;
        }

        /// <summary>
        /// Creates a 64bit Union initialized from an uint64 value.
        /// </summary>
        /// <param name="value">The unsigned long value to initialize with.</param>
        public Union64(ulong value)
            : this()
        {
            this.ulong_0_7 = value;
        }

        /// <summary>
        /// Creates a 64bit Union initialized from a double value.
        /// </summary>
        /// <param name="value">The double value to initialize with.</param>
        public Union64(double value)
            : this()
        {
            this.double_0_7 = value;
        }

        /// <summary>
        /// Gets or sets the i'th byte.
        /// </summary>
        /// <param name="i">The index of the byte to get or set [0-7].</param>
        public byte this[int i] // Union64 has 8 bytes.
        {
            get
            {
                unchecked
                {
                    if (i < 4) // from the lowest 4 bytes...
                    {
                        if (i < 0)
                            throw new IndexOutOfRangeException();

                        // shifting << 3 is the same as multiply by 8 (it is done to go from bytes to bits).
                        return (byte)(uint_0_3 >> (i << 3));
                    }
                    else // from the highest 4 bytes...
                    {
                        if (i >= 8)
                            throw new IndexOutOfRangeException();

                        // subtract 4 to adjust the shift for the upper 4 bytes - also see above.
                        return (byte)(uint_4_7 >> ((i - 4) << 3));
                    }
                }
            }
            set
            {
                const uint BYTE = 0xff;
                unchecked
                {
                    if (i < 4) // to the lowest 4 bytes...
                    {
                        if (i < 0)
                            throw new IndexOutOfRangeException();

                        int bits = i << 3; // <-- the same as multiply by 8 (it is done to go from bytes to bits).
                        uint_0_3 &= ~(BYTE << bits); //   <--  erase current bits of byte #i.
                        uint_0_3 |= ((uint)value) << bits; //  <-- write new bits to byte #i.
                    }
                    else // to the highest 4 bytes...
                    {
                        if (i >= 8)
                            throw new IndexOutOfRangeException();

                        int bits = (i - 4) << 3; // <-- subtracting 4 to adjust into upper 4 bytes - also see above.
                        uint_4_7 &= ~(BYTE << bits); //   <--  erase current bits of byte #i.
                        uint_4_7 |= ((uint)value) << bits; //  <-- write new bits to byte #i.
                    }
                }
            }
        }

        /// <summary>
        /// A double covering byte 0-7 (all bytes).
        /// </summary>
        [FieldOffset(0)]
        public double double_0_7;

        /// <summary>
        /// A long covering byte 0-7 (all bytes).
        /// </summary>
        [FieldOffset(0)]
        public long long_0_7;
        /// <summary>
        /// An ulong covering byte 0-7 (all bytes).
        /// </summary>
        [FieldOffset(0)]
        public ulong ulong_0_7;

        /// <summary>
        /// A float covering byte 0-3 (lower half).
        /// </summary>
        [FieldOffset(0)]
        public float float_0_3;

        /// <summary>
        /// An int covering byte 0-3 (lower half).
        /// </summary>
        [FieldOffset(0)]
        public int int_0_3;
        /// <summary>
        /// An uint covering byte 0-3 (lower half).
        /// </summary>
        [FieldOffset(0)]
        public uint uint_0_3;

        /// <summary>
        /// A short covering byte 0-1 (lowest quarter).
        /// </summary>
        [FieldOffset(0)]
        public short short_0_1;
        /// <summary>
        /// An ushort covering byte 0-1 (lowest quarter).
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
        /// A short covering byte 2-3 (low quarter).
        /// </summary>
        [FieldOffset(2)]
        public short short_2_3;
        /// <summary>
        /// An ushort covering byte 2-3 (low quarter).
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
        /// A float covering byte 4-7 (higher half).
        /// </summary>
        [FieldOffset(4)]
        public float float_4_7;

        /// <summary>
        /// An int covering byte 4-7 (higher half).
        /// </summary>
        [FieldOffset(4)]
        public int int_4_7;
        /// <summary>
        /// An uint covering byte 4-7 (higher half).
        /// </summary>
        [FieldOffset(4)]
        public uint uint_4_7;

        /// <summary>
        /// A short covering byte 4-5 (high quarter).
        /// </summary>
        [FieldOffset(4)]
        public short short_4_5;
        /// <summary>
        /// An ushort covering byte 4-5 (high quarter).
        /// </summary>
        [FieldOffset(4)]
        public ushort ushort_4_5;

        /// <summary>
        /// Signed byte 4 (fourth highest).
        /// </summary>
        [FieldOffset(4)]
        public sbyte sbyte_4;
        /// <summary>
        /// Byte 4 (fourth highest).
        /// </summary>
        [FieldOffset(4)]
        public byte byte_4;

        /// <summary>
        /// Signed byte 5 (third highest).
        /// </summary>
        [FieldOffset(5)]
        public sbyte sbyte_5;
        /// <summary>
        /// Byte 5 (third highest).
        /// </summary>
        [FieldOffset(5)]
        public byte byte_5;

        /// <summary>
        /// A short covering byte 6-7 (highest quarter).
        /// </summary>
        [FieldOffset(6)]
        public short short_6_7;
        /// <summary>
        /// An ushort covering byte 6-7 (highest quarter).
        /// </summary>
        [FieldOffset(6)]
        public ushort ushort_6_7;

        /// <summary>
        /// Signed byte 6 (second highest).
        /// </summary>
        [FieldOffset(6)]
        public sbyte sbyte_6;
        /// <summary>
        /// Byte 6 (second highest).
        /// </summary>
        [FieldOffset(6)]
        public byte byte_6;

        /// <summary>
        /// Signed byte 7 (highest).
        /// </summary>
        [FieldOffset(7)]
        public sbyte sbyte_7;
        /// <summary>
        /// Byte 7 (highest).
        /// </summary>
        [FieldOffset(7)]
        public byte byte_7;
    }
}
