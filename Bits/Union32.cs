using System;
using System.Runtime.InteropServices;

namespace AZCL.Bits
{
    /// <summary>
    /// A 32-bit Union of standard c# value-types: [unsigned] int/shorts/bytes and a float value.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Union32
    {
        /// <summary>
        /// Creates a 32bit Union initialized from an int32 value.
        /// </summary>
        /// <param name="value">The int value to initialize with.</param>
        public Union32(int value)
            : this()
        {
            this.int_0_3 = value;
        }
        /// <summary>
        /// Creates a 32bit Union initialized from an uint32 value.
        /// </summary>
        /// <param name="value">The unsigned int value to initialize with.</param>
        public Union32(uint value)
            : this()
        {
            this.uint_0_3 = value;
        }

        /// <summary>
        /// Creates a 32bit Union initialized from a float value.
        /// </summary>
        /// <param name="value">The float value to initialize with.</param>
        public Union32(float value)
            : this()
        {
            this.float_0_3 = value;
        }

        /// <summary>
        /// Gets or sets the i-th byte.
        /// </summary>
        /// <param name="i">The index of the byte to get or set [0-3].</param>
        public byte this[int i] // Union32 has 4 bytes.
        {
            get
            {
                unchecked
                {
                    if ((uint)i >= 4u)
                        throw new IndexOutOfRangeException();

                    // shifting << 3 is the same as multiply by 8 (it is done to go from bytes to bits).
                    return (byte)(uint_0_3 >> (i << 3));
                }
            }
            set
            {
                const uint BYTE = 0xff;
                unchecked
                {
                    if ((uint)i >= 4u)
                        throw new IndexOutOfRangeException();

                    int bits = i << 3; // <-- the same as multiply by 8 (it is done to go from bytes to bits).
                    uint_0_3 &= ~(BYTE << bits); //   <--  erase current bits of byte #i.
                    uint_0_3 |= ((uint)value) << bits; //  <-- write new bits to byte #i.
                }
            }
        }

        /// <summary>
        /// A float covering byte 0-3 (all bytes).
        /// </summary>
        [FieldOffset(0)]
        public float float_0_3;

        /// <summary>
        /// An int covering byte 0-3 (all bytes).
        /// </summary>
        [FieldOffset(0)]
        public int int_0_3;
        /// <summary>
        /// An uint covering byte 0-3 (all bytes).
        /// </summary>
        [FieldOffset(0)]
        public uint uint_0_3;

        /// <summary>
        /// A short covering byte 0-1 (lower half).
        /// </summary>
        [FieldOffset(0)]
        public short short_0_1;
        /// <summary>
        /// An ushort covering byte 0-1 (lower half).
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
        /// A short covering byte 2-3 (higher half).
        /// </summary>
        [FieldOffset(2)]
        public short short_2_3;
        /// <summary>
        /// An ushort covering byte 2-3 (higher half).
        /// </summary>
        [FieldOffset(2)]
        public ushort ushort_2_3;

        /// <summary>
        /// Signed byte 2 (second highest).
        /// </summary>
        [FieldOffset(2)]
        public sbyte sbyte_2;
        /// <summary>
        /// Byte 2 (second highest).
        /// </summary>
        [FieldOffset(2)]
        public byte byte_2;

        /// <summary>
        /// Signed byte 3 (highest).
        /// </summary>
        [FieldOffset(3)]
        public sbyte sbyte_3;
        /// <summary>
        /// Byte 3 (highest).
        /// </summary>
        [FieldOffset(3)]
        public byte byte_3;
    }
}
