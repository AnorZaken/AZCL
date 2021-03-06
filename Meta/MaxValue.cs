﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using AZCL.Bits;

namespace AZCL.Meta
{
    /// <summary>
    /// Represents a max-value for a numeric simple type. (Struct size: 1 byte)
    /// </summary><remarks>
    /// An uninitialized instance will correspond to sbyte.MaxValue.
    /// </remarks>
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    public struct MaxValue : IComparable<TypeCode>, IComparable<MaxValue>,
        IComparable<sbyte>, IComparable<byte>, IComparable<short>, IComparable<ushort>,
        IComparable<int>, IComparable<uint>, IComparable<long>, IComparable<ulong>
    {
        private readonly byte tc5;

        /// <summary>
        /// Initializes a new MaxValue instance for a numeric simple type.
        /// </summary>
        /// <param name="typeCode">TypeCode of a numeric simple type.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="typeCode"/> doesn't correspond to a numeric simple type.
        /// </exception>
        /// <seealso cref="Numeric.IsNumeric(TypeCode)"/>
        public MaxValue(TypeCode typeCode)
        {
            int tc5 = (int)typeCode - 5;

            if (unchecked((uint)tc5 > 10u)) // not numeric
                throw new ArgumentException(Numeric.ERR_TYPECODE, nameof(typeCode));

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
        /// Indicates whether this MaxValue is of an integral type.
        /// </summary><remarks>
        /// The integral types are sbyte, byte, short, ushort, int, uint, long, and ulong.
        /// </remarks>
        public bool IsIntegral
        {
            get { return tc5 < 8; }
        }

        /// <summary>
        /// Indicates whether this MaxValue is of an unsigned integral type.
        /// </summary>
        public bool IsUnsigned
        {
            get { return (tc5 < 8) & ((tc5 & 1) == 0); } // (is actually < 7 - but 7 is an odd number anyway so w/e)
        }

        /// <summary>
        /// Indicates whether this MaxValue fits inside a 32-bit signed integer.
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
        /// <exception cref="OverflowException">
        /// Thrown if this MaxValue is greater than Int32.MaxValue.
        /// </exception>
        /// <seealso cref="FitsInt32"/>
        public int AsInt32
        {
            get
            {
                if (tc5 > 4)
                    throw new OverflowException(Numeric.ERR_MAX_OVERFLOW);

                int t6 = tc5 & 6;
                return int.MaxValue >> 24 - (tc5 & 1) - (t6 + t6 + (t6 & 4) << 1);
            }
        }

        /// <summary>
        /// Indicates whether this MaxValue fits inside a 32-bit unsigned integer.
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
        /// <exception cref="OverflowException">
        /// Thrown if this MaxValue is greater than UInt32.MaxValue.
        /// </exception>
        /// <seealso cref="FitsUInt32"/>
        public uint AsUInt32
        {
            get
            {
                if (tc5 > 5)
                    throw new OverflowException(Numeric.ERR_MAX_OVERFLOW);

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
        /// Indicates whether this MaxValue fits inside a 64-bit signed integer.
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
        /// <exception cref="OverflowException">
        /// Thrown if this MaxValue is greater than Int64.MaxValue.
        /// </exception>
        /// <seealso cref="FitsInt64"/>
        public long AsInt64
        {
            get
            {
                return tc5 == 6 ? long.MaxValue : AsUInt32;
            }
        }

        /// <summary>
        /// Indicates whether this MaxValue fits inside a 64-bit unsigned integer.
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
        /// <exception cref="OverflowException">
        /// Thrown if this MaxValue is greater than UInt64.MaxValue.
        /// </exception>
        /// <seealso cref="FitsUInt64"/>
        public ulong AsUInt64
        {
            get
            {
                return tc5 == 6 | tc5 == 7 ? ulong.MaxValue >> (~tc5 & 1) : AsUInt32;
            }
        }

        /// <summary>
        /// Compares this MaxValue to the MaxValue of a numeric simple type.
        /// </summary><returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared.
        /// The return value has the following meanings:
        /// <br/>Less than zero: This max-value is less than the max-value of other.
        /// <br/>Zero: These max-values represent the same type.
        /// <br/>Greater than zero: This max-value is greater than the max-value of other.
        /// </returns>
        /// <param name="other">TypeCode of a numeric simple type to compare max-value against.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="other"/> doesn't correspond to a numeric simple type.
        /// </exception>
        /// <seealso cref="Numeric.IsNumeric(TypeCode)"/>
        public int CompareTo(TypeCode other)
        {
            int tc1 = tc5;
            int tc2 = (int)other - 5;

            if (tc1 == tc2) // same type?
                return 0;

            if (unchecked((uint)tc2 > 10u)) // not numeric
                throw new ArgumentException(Numeric.ERR_TYPECODE, nameof(other));

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

        /// <summary>
        /// Compares this MaxValue to another MaxValue.
        /// </summary>
        /// <param name="other">MaxValue to compare against.</param>
        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        public int CompareTo(MaxValue other)
        {
            return CompareTo(other.TypeCode);
        }

        /// <summary>
        /// Compares this MaxValue to an sbyte value.
        /// </summary><returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared.
        /// The return value has the following meanings:
        /// <br/>Less than zero: This max-value is less than the other value.
        /// <br/>Zero: This max-value equals the other value.
        /// <br/>Greater than zero: This max-value is greater than the other value.
        /// </returns>
        /// <param name="other">SByte value to compare against.</param>
        public int CompareTo(sbyte other)
        {
            return sbyte.MaxValue - other + tc5;
        }

        /// <summary>
        /// Compares this MaxValue to a byte value.
        /// </summary>
        /// <param name="other">Byte value to compare against.</param>
        /// <inheritdoc cref="CompareTo(sbyte)"/>
        public int CompareTo(byte other)
        {
            return (0xFffff >> 13 - tc5) - other;
        }

        /// <summary>
        /// Compares this MaxValue to a signed short value.
        /// </summary>
        /// <param name="other">Int16 value to compare against.</param>
        /// <inheritdoc cref="CompareTo(sbyte)"/>
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
        /// <inheritdoc cref="CompareTo(sbyte)"/>
        public int CompareTo(ushort other)
        {
            return (0xFffff >> 13 - tc5 - (1 - tc5 >> 1 & 6)) - other;
        }

        /// <summary>
        /// Compares this MaxValue to a signed int32 value.
        /// </summary>
        /// <param name="other">Int32 value to compare against.</param>
        /// <inheritdoc cref="CompareTo(sbyte)"/>
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
        /// <inheritdoc cref="CompareTo(sbyte)"/>
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
        /// <inheritdoc cref="CompareTo(sbyte)"/>
        public int CompareTo(long other)
        {
            Union64 u64 = new Union64(other);

            if (u64.int_4_7 == 0)
                return CompareTo(u64.uint_0_3);

            if (tc5 == 6)
                return ~u64.int_0_3 == 0 ? unchecked(int.MaxValue - u64.int_4_7) : u64.int_4_7;

            return tc5 - 6;
        }

        /// <summary>
        /// Compares this MaxValue to an unsigned int64 value.
        /// </summary>
        /// <param name="other">UInt64 value to compare against.</param>
        /// <inheritdoc cref="CompareTo(sbyte)"/>
        public int CompareTo(ulong other)
        {
            Union64 u64 = new Union64(other);

            if (u64.int_4_7 == 0)
                return CompareTo(u64.uint_0_3);
            /*
            if (tc5 < 6) // more common..?
                return -1;
            */
            if (tc5 == 6)
                return ~u64.int_0_3 == 0 ? unchecked(int.MaxValue - u64.int_4_7) : u64.int_4_7;

            if (tc5 == 7)
            {
                u64.uint_0_3 = ~(u64.uint_4_7 & u64.uint_0_3); // will be zero if other is ulong.Max
                return u64.ushort_0_1 | u64.ushort_2_3;
            }

            return tc5 - 6;
        }

        /// <summary>
        /// Compares this MaxValue to an integral value of type <typeparamref name="TIntegral"/>.
        /// </summary><remarks>
        /// The integral types are: sbyte, byte, short, ushort, int, uint, long, and ulong.
        /// </remarks><returns>
        /// A nullable 32-bit signed integer that indicates the relative order of the objects being compared.
        /// The return value has the following meanings:
        /// <br/>Less than zero: This max-value is less than the other value.
        /// <br/>Zero: This max-value equals the other value.
        /// <br/>Greater than zero: This max-value is greater than the other value.
        /// <br/>No Value: The type <typeparamref name="TIntegral"/> is not an Integral type.
        /// </returns>
        /// <param name="other">Integral value to compare against.</param>
        /// <seealso cref="Numeric.IsInteger(TypeCode)"/>
        public int? CompareToInt<TIntegral>(TIntegral other)
        {
            var conv = other as IConvertible;
            if (conv == null || !Numeric.IsInteger(conv.GetTypeCode()))
                return null;

            if (Numeric.IsUnsigned(conv.GetTypeCode()))
                return CompareTo(conv.ToUInt64(null));
            else
                return CompareTo(conv.ToInt64(null));
        }
    }
}
