using System;
using System.Runtime.InteropServices;

namespace AZCL.Meta
{
    /// <summary>
    /// A struct that describes the min-value of a primitive numeric type. (Struct size: 1 byte)
    /// </summary><remarks>
    /// An uninitialized instance will correspond to sbyte.
    /// </remarks>
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
    public struct MinValue : IComparable<TypeCode>, IComparable<MinValue>,
        IComparable<sbyte>, IComparable<byte>, IComparable<short>, IComparable<ushort>,
        IComparable<int>, IComparable<uint>, IComparable<long>, IComparable<ulong>
    {
        private readonly byte tc5;

        private const string _ERR_Overflow = "This min-value can not be represented in that type.";

        /// <summary>
        /// Creates a MinValue instance for a primitive numeric type.
        /// </summary>
        /// <param name="typeCode">TypeCode of a primitive numeric type.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="typeCode"/> doesn't correspond to a primitive numeric type.
        /// </exception>
        /// <seealso cref="Numeric.IsNumeric(TypeCode)"/>
        public MinValue(TypeCode typeCode)
        {
            int tc5 = (int)typeCode - 5;

            if (unchecked((uint)tc5 > 10u)) // not numeric
                throw new ArgumentException(Numeric._ERR_TC_NOT_NUMERIC, nameof(typeCode));

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
        /// True if this MinValue is for an Integer type.
        /// </summary><remarks>
        /// The Integer types are sbyte, byte, short, ushort, int, uint, long, ulong.
        /// </remarks>
        public bool IsInteger
        {
            get { return tc5 < 8; }
        }

        /// <summary>
        /// True if this MinValue fits inside a 32-bit signed integer.
        /// </summary>
        /// <seealso cref="AsInt32"/>
        public bool FitsInt32
        {
            get { return tc5 < 6 | tc5 == 7; }
        }

        /// <summary>
        /// This MinValue as an Int32. (Throwing!)
        /// </summary>
        /// <exception cref="OverflowException">Thrown if this MinValue is less than Int32.MinValue.</exception>
        /// <seealso cref="FitsInt32"/>
        public int AsInt32
        {
            get
            {
                if (FitsInt32)
                    return (tc5 & 1) - 1 & int.MinValue >> 24 - (tc5 + tc5 + (tc5 & 4) << 1); // hint: (tc5 & 1) is for signed / unsigned
                else
                    throw new OverflowException(_ERR_Overflow);
            }
        }

        /// <summary>
        /// True if this MinValue fits inside a 32-bit signed integer.
        /// </summary>
        /// <seealso cref="AsInt64"/>
        public bool FitsInt64
        {
            get { return tc5 < 8; }
        }

        /// <summary>
        /// This MinValue as an Int64. (Throwing!)
        /// </summary>
        /// <exception cref="OverflowException">Thrown if this MinValue is less than Int64.MinValue.</exception>
        /// <seealso cref="FitsInt64"/>
        public long AsInt64
        {
            get { return tc5 == 6 ? long.MinValue : AsInt32; }
        }

        /// <summary>
        /// Compares this MinValue to the MinValue of another primitive numeric type.
        /// </summary>
        /// <param name="other">TypeCode of a primitive numeric type to compare min-value against.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared.
        /// The return value has the following meanings:
        /// <br/>Less than zero: This min-value is less than the min-value of other.
        /// <br/>Zero: The min-values are the same (same type or both unsigned).
        /// <br/>Greater than zero: This min-value is greater than the min-value of other.
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
                throw new ArgumentException(Numeric._ERR_TC_NOT_NUMERIC, nameof(other));

            if ((tc1 | tc2) < 8) // "both are integer type"
            {
                // signed integer types are even (0), unsigned are odd (1).
                int i1 = tc1 & 1;
                int i2 = tc2 & 1;
                return (i1 | i2) == 0 ? tc2 - tc1 : (i1 - i2); // "both signed" : "else... see below:"
                // ... (i1 - i2) == both unsigned: 1-1=0 , this unsigned: 1-0=1 , other unsigned: 0-1=-1
            }
            else if (((tc1 | tc2) & 2) == 0) // "neither is decimal"
            {
                return tc2 - tc1;
            }
            else if (tc1 == 10) // "this is decimal" (other is not!)
            {
                return (tc2 | 1) - 8; // (8 becomes 9, returns +1. Everything below 8 becomes negative)
            }
            else // other is decimal (this is not!)
            {
                return 8 - (tc1 | 1); // (8 becomes 9, returns -1. Everything below 8 becomes positive)
            }
        }

        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        /// <param name="other">MinValue to compare against.</param>
        public int CompareTo(MinValue other)
        {
            return CompareTo(other.TypeCode);
        }

        /// <summary>
        /// Compares this MinValue to an sbyte value.
        /// </summary>
        /// <param name="other">SByte value to compare against.</param>
        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        public int CompareTo(sbyte other)
        {
            //int i0 = -128 - tc5;      // tc5 is zero if TC is sbyte.
            //int i1 = (tc5 & 1) - 1;  // unsigned:0, signed:-1
            //int i2 = -(tc5 >> 3);   // integer:0, float/double/decimal:-1
            //return (i0 & (i1 | i2)) - other;
            return ((-128 - tc5) & ((tc5 & 1) - 1 | -(tc5 >> 3))) - other;

            //other : tc0    tc1     tc6     tc7     tc8     tc9
            // 127  : -255   -127    -261    -127    -263    -264
            // 1    : -129   -1      -135    -1      -137    -138
            // 0    : -128   0       -134    0       -136    -137
            // -1   : -127   1       -133    1       -135    -136
            // -127 : -1     127     -7      127     -9      -10
            // -128 : 0      128     -6      128     -8      -9
            // OK!
        }

        /// <summary>
        /// Compares this MinValue to a byte value.
        /// </summary>
        /// <param name="other">Byte value to compare against.</param>
        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        public int CompareTo(byte other)
        {
            //return (tc5 & 1) - 1 - other; // <-- wrong for tc5 == 9
            return (tc5 & 1) - (tc5 & 8 | 1) - other;
        }

        /// <summary>
        /// Compares this MinValue to a signed short value.
        /// </summary>
        /// <param name="other">Int16 value to compare against.</param>
        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        public int CompareTo(short other)
        {
            int min = (tc5 & 1) - (tc5 & 8 | 1) & int.MinValue >> 24 - tc5 - (1 - tc5 >> 1 & 6);
            return min - other;

            //tc5:shift     :and-prefix : num of bits unset
            // 0 : 24-0-0   :0-1        :31-24 = 7 <-- sbyte.Min
            // 1 : 24-1-0   :1-1        :32 <-- byte.Min
            // 2 : 24-2-6   :0-1        :31-5 = 15 <-- short.Min
            // 3 : 24-3-6   :1-1        :32 <-- ushort.Min
            // 4 : 24-4-6   :0-1        :31-3 = 17 <-- (arbitrary)
            // 5 : 24-5-6   :1-1        :32 <-- uint.Min
            // 6 : 24-6-4   :0-1        :31-3 = 17 <-- (arbitrary)
            // 7 : 24-7-4   :1-1        :32 <-- ulong.Min
            // 8 : 24-8-4   :0-9        :31-1 = 19 <-- (arbitrary)
            // 9 : 24-9-4   :1-9        :31-0 = 20 <-- (arbitrary)
            //10 : 24-10-2  :0-9        :31-1 = 19 <-- (arbitrary)
        }

        /// <summary>
        /// Compares this MinValue to an unsigned short value.
        /// </summary>
        /// <param name="other">UInt16 value to compare against.</param>
        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        public int CompareTo(ushort other)
        {
            return (tc5 & 1) - (tc5 & 8 | 1) - other;
        }

        /// <summary>
        /// Compares this MinValue to a signed int32 value.
        /// </summary>
        /// <param name="other">Int32 value to compare against.</param>
        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        public int CompareTo(int other)
        {
            if ((tc5 & 9) == 0) // signed and NOT float/double/decimal
                return tc5 == 6 ? -1 : (int.MinValue >> 24 - (tc5 + tc5 + (tc5 & 4) << 1)) - (other & other >> 31); // AND 31-shift prevents overflow!
            else
                return 1 - (tc5 & 9) - (other >> 31) + (unchecked(-other & ~other) >> 31);
            // (tc5 & 9) is either 1, 8 or 9  (1 if unsigned, otherwise float/double/decimal)
            // hint: see AZCL.Numerics.MathB.SignOrZero(int) for the other part.
        }

        /// <summary>
        /// Compares this MinValue to an unsigned int32 value.
        /// </summary>
        /// <param name="other">UInt32 value to compare against.</param>
        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        public int CompareTo(uint other)
        {
            return (tc5 & 1) - (tc5 & 8 | 1) - unchecked((int)(other & 3u | other >> 2)); //PS: shifting only 1 bit can cause overflow!
        }

        /// <summary>
        /// Compares this MinValue to a signed int64 value.
        /// </summary>
        /// <param name="other">Int64 value to compare against.</param>
        /// <inheritdoc cref="CompareTo(TypeCode)"/>
        public int CompareTo(long other)
        {
            Union64 u64 = new Union64(other);
            if ((tc5 & 9) == 0) // signed and NOT float/double/decimal
            {
                if (u64.int_4_7 >= 0) // other >= 0 ?
                    return -1;
                //else: other < 0

                if (tc5 == 6)
                    return other == long.MinValue ? 0 : -1;
                //else: this is int/short/sbyte

                if (~u64.int_4_7 == 0 & u64.int_0_3 < 0) // other >= int.MinValue ?
                    return (int.MinValue >> 24 - (tc5 + tc5 + (tc5 & 4) << 1)) - u64.int_0_3; //(cant overflow because: other < 0)
                //else: other < int.MinValue

                return 1;
            }
            else
            {
                return 1 - (tc5 & 9) - (u64.int_4_7 >> 31) + (unchecked(-(u64.int_4_7 | u64.ushort_0_1 | u64.ushort_2_3) & ~u64.int_4_7) >> 31);
                //return 1 - (tc5 & 9) - (u64.int_4_7 >> 31) + (new Union64(unchecked(-other & ~other)).int_4_7 >> 31);
            }
        }

        public int CompareTo(ulong other)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
