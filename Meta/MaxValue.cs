using System;
using System.Runtime.InteropServices;

namespace AZCL.Meta
{
    /// <summary>
    /// A struct that describes the max-value of a primitive numeric type. (Struct size: 1 byte)
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    public struct MaxValue : IComparable<TypeCode>, IComparable<MaxValue>,
        IComparable<sbyte>, IComparable<byte>, IComparable<short>, IComparable<ushort>,
        IComparable<int>, IComparable<uint>, IComparable<long>, IComparable<ulong>
    {
        private readonly byte tc5;

        private const string _ERR_Overflow = "This max-value can not be represented in that type.";

        /// <summary>
        /// Creates a MaxValue instance for a primitive numeric type.
        /// </summary>
        /// <param name="typeCode">TypeCode of a primitive numeric type.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="typeCode"/> doesn't correspond to a primitive numeric type.
        /// </exception>
        /// <seealso cref="Numeric.IsNumeric(TypeCode)"/>
        public MaxValue(TypeCode typeCode)
        {
            int tc5 = (int)typeCode - 5;

            if (unchecked((uint)tc5 > 10u)) // not numeric
                throw new ArgumentException("TypeCode does not correspond to a numeric type.", nameof(typeCode));

            this.tc5 = (byte)tc5;
        }

        /// <summary>
        /// TypeCode of the represented type.
        /// </summary>
        public TypeCode TypeCode
        {
            get { return (TypeCode)(tc5 + 5); }
        }

        /// <summary>
        /// True if this MaxValue is for an Integer type.
        /// </summary><remarks>
        /// The Integer types are sbyte, byte, short, ushort, int, uint, long, ulong.
        /// </remarks>
        public bool IsInteger
        {
            get { return tc5 < 8; }
        }

        /// <summary>
        /// True if this MaxValue is for an unsigned Integer type.
        /// </summary>
        public bool IsUnsigned
        {
            get { return (tc5 < 8) & ((tc5 & 1) == 0); } // (is actually < 7 - but 7 is an odd number anyway so w/e)
        }

        /// <summary>
        /// True if this MaxValue fits inside a 32-bit signed integer.
        /// </summary>
        /// <seealso cref="AsInt32"/>
        /// <seealso cref="FitsUInt32"/>
        public bool FitsInt32
        {
            get { return tc5 <= 4; }
        }

        /// <summary>
        /// This MaxValue as an Int32. (Throwing!)
        /// </summary>
        /// <exception cref="OverflowException">Thrown if this MaxValue is greater than Int32.MaxValue.</exception>
        /// <seealso cref="FitsInt32"/>
        public int AsInt32
        {
            get
            {
                if (tc5 > 4)
                    throw new OverflowException(_ERR_Overflow);

                int t6 = tc5 & 6;
                return int.MaxValue >> 24 - (tc5 & 1) - (t6 + t6 + (t6 & 4) << 1);
            }
        }

        /// <summary>
        /// True if this MaxValue fits inside a 32-bit unsigned integer.
        /// </summary>
        /// <seealso cref="AsUInt32"/>
        /// <seealso cref="FitsInt32"/>
        public bool FitsUInt32
        {
            get { return tc5 <= 5; }
        }

        /// <summary>
        /// This MaxValue as an UInt32. (Throwing!)
        /// </summary>
        /// <exception cref="OverflowException">Thrown if this MaxValue is greater than UInt32.MaxValue.</exception>
        /// <seealso cref="FitsUInt32"/>
        public uint AsUInt32
        {
            get
            {
                if (tc5 > 5)
                    throw new OverflowException(_ERR_Overflow);

                int t6 = tc5 & 6;
                return uint.MaxValue >> 25 - (tc5 & 1) - (t6 + t6 + (t6 & 4) << 1);
                // 0: >> 25 , 25 - 0 - 0
                // 1: >> 24 , 25 - 1 - 0
                // 2: >> 17 , 25 - 0 - (2+2+0)*2
                // 3: >> 16 , 25 - 1 - (2+2+0)*2
                // 4: >> 1  , 25 - 0 - (4+4+4)*2
                // 5: >> 0  , 25 - 1 - (4+4+4)*2
            }
        }

        /// <summary>
        /// True if this MaxValue fits inside a 64-bit signed integer.
        /// </summary>
        /// <seealso cref="AsInt64"/>
        /// <seealso cref="FitsUInt64"/>
        public bool FitsInt64
        {
            get { return tc5 <= 6; }
        }

        /// <summary>
        /// This MaxValue as an Int64. (Throwing!)
        /// </summary>
        /// <exception cref="OverflowException">Thrown if this MaxValue is greater than Int64.MaxValue.</exception>
        /// <seealso cref="FitsInt64"/>
        public long AsInt64
        {
            get
            {
                return tc5 == 6 ? long.MaxValue : AsUInt32;
            }
        }

        /// <summary>
        /// True if this MaxValue fits inside a 64-bit unsigned integer.
        /// </summary>
        /// <seealso cref="AsUInt64"/>
        /// <seealso cref="FitsInt64"/>
        public bool FitsUInt64
        {
            get { return tc5 < 8; }
        }

        /// <summary>
        /// This MaxValue as an UInt64. (Throwing!)
        /// </summary>
        /// <exception cref="OverflowException">Thrown if this MaxValue is greater than UInt64.MaxValue.</exception>
        /// <seealso cref="FitsUInt64"/>
        public ulong AsUInt64
        {
            get
            {
                return tc5 == 6 | tc5 == 7 ? ulong.MaxValue >> (~tc5 & 1) : AsUInt32;
            }
        }

        /// <summary>
        /// Compares this MaxValue to the MaxValue of another primitive numeric type.
        /// </summary>
        /// <param name="other">TypeCode of a primitive numeric type to compare max-value against.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared.
        /// The return value has the following meanings:
        /// <br/>Less than zero: This max-value is less than the max-value of other.
        /// <br/>Zero: These max-values represent the same type.
        /// <br/>Greater than zero: This max-value is greater than the max-value of other.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="other"/> doesn't correspond to a primitive numeric type.
        /// </exception>
        /// <seealso cref="Numeric.IsNumeric(TypeCode)"/>
        public int CompareTo(TypeCode other)
        {
            int tc1 = tc5;
            int tc2 = (int)other - 5;

            if (tc1 == tc2) // same type?
                return 0;

            if (unchecked((uint)tc2 > 10u)) // not numeric
                throw new ArgumentException("TypeCode does not correspond to a primitive numeric type.", nameof(other));

            if ((tc1 | tc2) < 10) // "neither is decimal"
            {
                return tc1 - tc2;
            }
            else if (tc1 == 10) // this is decimal, other isn't
            {
                return 1 - (tc2 & 8);
            }
            else // other is decimal, this isn't
            {
                return (tc1 & 8) - 1;
            }
        }

        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        /// <param name="other">MaxValue to compare against.</param>
        public int CompareTo(MaxValue other)
        {
            return CompareTo(other.TypeCode);
        }

        /// <summary>
        /// Compares this MaxValue to an sbyte value.
        /// </summary>
        /// <param name="other">SByte value to compare against.</param>
        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        public int CompareTo(sbyte other)
        {
            return sbyte.MaxValue - other + tc5;
        }

        /// <summary>
        /// Compares this MaxValue to a byte value.
        /// </summary>
        /// <param name="other">Byte value to compare against.</param>
        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        public int CompareTo(byte other)
        {
            return (0xFffff >> 13 - tc5) - other;
        }

        /// <summary>
        /// Compares this MaxValue to a signed short value.
        /// </summary>
        /// <param name="other">Int16 value to compare against.</param>
        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        public int CompareTo(short other)
        {
            return (0xFffff >> 13 - tc5 - (1 - tc5 >> 1 & 6)) - other;
            //tc5:shift     : num of bits set
            // 0 : 13-0-0   :20-13 = 7 <-- sbyte.Max
            // 1 : 13-1-0   :20-12 = 8 <-- byte.Max
            // 2 : 13-2-6   :20-5 = 15 <-- short.Max
            // 3 : 13-3-6   :20-4 = 16 <-- ushort.Max
            // 4 : 13-4-6   :20-3 = 17 <-- (arbitrary)
            // 5 : 13-5-6   :20-2 = 18 <-- (arbitrary)
            // 6 : 13-6-4   :20-3 = 17 <-- (arbitrary)
            // 7 : 13-7-4   :20-2 = 18 <-- (arbitrary)
            // 8 : 13-8-4   :20-1 = 19 <-- (arbitrary)
            // 9 : 13-9-4   :20-0 = 20 <-- (arbitrary)
            //10 : 13-10-2  :20-1 = 19 <-- (arbitrary)
        }

        /// <summary>
        /// Compares this MaxValue to an unsigned short value.
        /// </summary>
        /// <param name="other">UInt16 value to compare against.</param>
        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        public int CompareTo(ushort other)
        {
            return (0xFffff >> 13 - tc5 - (1 - tc5 >> 1 & 6)) - other;
        }

        /// <summary>
        /// Compares this MaxValue to a signed int32 value.
        /// </summary>
        /// <param name="other">Int32 value to compare against.</param>
        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        public int CompareTo(int other)
        {
            if (tc5 > 4)
                return 1;

            int t6 = tc5 & 6;
            return (int.MaxValue >> 24 - (tc5 & 1) - (t6 + t6 + (t6 & 4) << 1)) - (other & ~other >> 31);
        }

        /// <summary>
        /// Compares this MaxValue to an unsigned int32 value.
        /// </summary>
        /// <param name="other">UInt32 value to compare against.</param>
        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        public int CompareTo(uint other)
        {
            if (tc5 > 5)
                return 1;

            int t6 = tc5 & 6;
            int max_w_shift2 = int.MaxValue >> 26 - (tc5 & 1) - (t6 + t6 + (t6 & 4) << 1);
            // ^ the 7 lowest bits are always set, so we can shift and AND up to 3 bits.
            return max_w_shift2 - unchecked((int)(other >> 2 & (other | 0xFFFFfffcU)));
        }

        /// <summary>
        /// Compares this MaxValue to a signed int64 value.
        /// </summary>
        /// <param name="other">Int64 value to compare against.</param>
        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        public int CompareTo(long other)
        {
            Union64 u64 = new Union64(other);

            if (u64.int_4_7 == 0)
                return CompareTo(u64.uint_0_3);

            if (u64.int_4_7 < 0 | tc5 > 6)
                return 1;

            if (tc5 < 6)
                return -1;

            //else: tc5 == 6
            return ~u64.int_0_3 == 0 ? int.MaxValue - u64.int_4_7 : 1;
        }

        public int CompareTo(ulong other)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
