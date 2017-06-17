using System;
using System.Collections;
using System.Collections.Generic;
using AZCL.Collections;

namespace AZCL
{
    internal sealed class EnumValues<TEnumeration> : IEnumValues<TEnumeration>
        where TEnumeration : Enumeration<TEnumeration>
    {
        private readonly TEnumeration[] values;

        // internal instantiation only!
        internal EnumValues(TEnumeration[] values)
        {
            AZAssert.NotNullInternal(values, nameof(values));
            this.values = values;
        }

        // ---

        public ReadOnlyArray<TEnumeration> Array
            => values;

        public TEnumeration this[int ordinal]
            => values[ordinal];

        public int Count
            => values.Length;

        public TEnumeration First
            => values.Length == 0 ? null : values[0];

        public TEnumeration Last
            => values.Length == 0 ? null : values[values.Length - 1];

        public bool HasEnumValues
            => values.Length == 0 ? false : values[0].HasEnumValueImpl;

        public Type EnumValueType
        {
            get
            {
                if (Count == 0) // this makes it more expensive...
                {
                    var tBase = typeof(TEnumeration).BaseType;
                    if (tBase.IsGenericType && tBase.GetGenericTypeDefinition() == typeof(Enumeration<,>))
                        return tBase.GetGenericArguments()[1];

                    return null;
                }
                else // ...as opposed to this nice and simple case.
                {
                    return values[0].EnumValueType;
                }
            }
        }

        public TEnumeration TryParse(string name)
        {
            for (int i = 0; i < values.Length; ++i)
            {
                var v = values[i];
                if (v.Name == name)
                    return v;
            }
            return null;
        }

        public TEnumeration TryParse<TEnum>(TEnum enumValue, bool allowConversion)
            where TEnum : struct, IConvertible
            => First?.TryParse(enumValue, allowConversion);

        public ReadOnlyArray<TEnumeration>.Enumerator GetEnumerator()
            => new ReadOnlyArray<TEnumeration>.Enumerator(values);

        // --- (explicit) ---

        ReadOnlyArray<Enumeration> IEnumValues.Array
            => values;

        Enumeration IEnumValues.this[int ordinal]
            => values[ordinal];

        Enumeration IEnumValues.First
            => First;

        Enumeration IEnumValues.Last
            => Last;

        Enumeration IEnumValues.TryParse(string name)
            => TryParse(name);

        Enumeration IEnumValues.TryParse<TEnum>(TEnum enumValue, bool allowConversion)
            => First?.TryParse(enumValue, allowConversion);

        ReadOnlyArray<Enumeration>.Enumerator IEnumValues.GetEnumerator()
            => new ReadOnlyArray<Enumeration>.Enumerator(values);

        IEnumerator<TEnumeration> IEnumerable<TEnumeration>.GetEnumerator()
            => new ReadOnlyArray<TEnumeration>.Enumerator(values);

        IEnumerator IEnumerable.GetEnumerator()
            => new ReadOnlyArray<TEnumeration>.Enumerator(values);
    }
}
