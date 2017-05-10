using System;
using System.Runtime.InteropServices;

namespace AZCL
{
    /// <summary>
    /// A 16-bit Union of standard c# value-types, i.e. [unsigned] shorts and bytes.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Union16
    {
        /// <summary>
        /// Creates a 16bit Union initialized from an int16 value.
        /// </summary>
        /// <param name="value">The short value to initialize with.</param>
        public Union16(short value)
            : this()
        {
            this.short_0_1 = value;
        }

        /// <summary>
        /// Creates a 16bit Union initialized from an uint16 value.
        /// </summary>
        /// <param name="value">The ushort value to initialize with.</param>
        public Union16(ushort value)
            : this()
        {
            this.ushort_0_1 = value;
        }

        /// <summary>
        /// Gets or sets the i'th byte.
        /// </summary>
        /// <param name="i">The index of the byte to get or set [0-1].</param>
        public byte this[int i] // Union16 has 2 bytes.
        {
            get
            {
                if (i == 0)
                    return byte_0;
                else if (i == 1)
                    return byte_1;
                else
                    throw new IndexOutOfRangeException();
            }
            set
            {
                if (i == 0)
                    byte_0 = value;
                else if (i == 1)
                    byte_1 = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// A short covering byte 0-1 (all bytes).
        /// </summary>
        [FieldOffset(0)]
        public short short_0_1;
        /// <summary>
        /// An ushort covering byte 0-1 (all bytes).
        /// </summary>
        [FieldOffset(0)]
        public ushort ushort_0_1;

        /// <summary>
        /// Signed byte 0 (lower half).
        /// </summary>
        [FieldOffset(0)]
        public sbyte sbyte_0;
        /// <summary>
        /// Byte 0 (lower half).
        /// </summary>
        [FieldOffset(0)]
        public byte byte_0;

        /// <summary>
        /// Signed byte 1 (higher half).
        /// </summary>
        [FieldOffset(1)]
        public sbyte sbyte_1;
        /// <summary>
        /// Byte 1 (higher half).
        /// </summary>
        [FieldOffset(1)]
        public byte byte_1;
    }
}
