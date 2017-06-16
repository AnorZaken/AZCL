using System;
using System.Collections.Generic;
using System.Diagnostics;
using AZCL.Meta;

/* An example implementation:

    public sealed class ExampleEnum : Enumeration<ExampleEnum, ExampleEnum.Enum>
    {
        private ExampleEnum()
        { }

        public static readonly ExampleEnum Foo = new ExampleEnum();
        public static readonly ExampleEnum Bar = new ExampleEnum();
        public static readonly ExampleEnum Bas = new ExampleEnum();

        public enum Enum
        {
            Foo,
            Bar,
            Bas,
        }
    }
*/

#pragma warning disable CS0660, CS0661
namespace AZCL
{
    /// <summary>
    /// Represents a Java-style "pure" enumeration, while also providing value pairing with a normal System.Enum type
    /// (in addition to all features of the <see cref="Enumeration{TEnumeration}"/> class).
    /// </summary><remarks>
    /// Inherit directly from this type if you need your Enumeration values to be paired with (identically named) System.Enum values
    /// (of <typeparamref name="TEnum>"/> type).
    /// Pairing your Enumeration type with a System.Enum type is most notably useful if you want to be able to switch on its values
    /// (since switch case statements only accept const values and literals). Switching without a System.Enum type is still possible
    /// (beginning with C# 6) using the 'nameof' operator since it returns string literals at compile time - but switching on strings
    /// is not as efficient as switching on System.Enum values.
    /// <para/>
    /// The term "pure" comes from the fact that this enumeration type only allows the unique set of values defined in it at design time.
    /// Only this fixed set of values (and null) is allowed and no additional other values can exist or be created at runtime.
    /// <br/>
    /// Unfortunately such is not the case for System.Enum, since it was designed to match C++ style integer based enumerations and flags.
    /// This means that for uses where only a fixed set of discrete input values are valid, the theoretically ideal use case of an
    /// enumeration, error-prone validation is required since C# provides no guarantee that a System.Enum has a valid value belonging
    /// to this fixed set, and no elegant and performant way of verifying it either.
    /// With the Enumeration class the only value validation needed is a simple null-check.
    /// <para/>
    /// System.Enum is an old C++ legacy that should never have been "ported over to" (hacked into) a strongly typed language,
    /// something that the designers of C# themselves said was one of their top ten biggest mistakes in designing the language.
    /// Conceptually Enumerations and Flags are different and should have been two different types:
    /// An enumeration value is always a single plain value, whereas a flag represents a set of values or even the empty set.
    /// </remarks>
    /// <typeparam name="TEnumeration">
    /// A sealed user defined enumeration type that inherits directly from this abstract class.
    /// </typeparam>
    /// <typeparam name="TEnum">
    /// A System.Enum type to pair this Enumeration type with.
    /// For each public static field of type <typeparamref name="TEnumeration"/> declared in the <typeparamref name="TEnumeration"/> class,
    /// there has to exist a constant declared in this <typeparamref name="TEnum"/> type that exactly match the name of that field (and has
    /// a unique numerical value).
    /// </typeparam>
    /// <seealso cref="Enumeration"/>
    /// <seealso cref="Enumeration{TEnumeration}"/>
    public abstract class Enumeration<TEnumeration, TEnum>
        : Enumeration<TEnumeration>, IEquatable<Enumeration<TEnumeration, TEnum>>, IEquatable<TEnum>
        where TEnumeration : Enumeration<TEnumeration, TEnum>
        where TEnum : struct, IConvertible
    {
        /// <summary>
        /// Initializes the Ordinal, Name*, and EnumValue* of this enumeration value.
        /// </summary><remarks>
        /// Ordinal gets determined here, and will be assigned such that it corresponds to the textual order in which the enumeration values are declared.<br/>
        /// (See comments in the Enumeration&lt;TEnumeration&gt; source code for more on how and why this works, along with relevant ECMA specification quotes.)
        /// <para/>
        /// *Name and EnumValue is late initialized:
        /// Names and EnumValues for all instances are initialized during the constructor call for the initialization of the last (public static) TEnumeration field.
        /// </remarks>
        /// <exception cref="Exception">
        /// Thrown if this operation causes the number of created instances to exceed the number of enumeration values declared for this enumeration type.
        /// </exception>
        /// <seealso cref="Enumeration{TEnumeration}._IsInitialized"/>
        protected Enumeration() : this(null)
        { }

        /// <summary>
        /// Initializes the Ordinal, Name*, and EnumValue* of this enumeration value.
        /// </summary>
        /// <inheritdoc cref="Enumeration{TEnumeration, TEnum}.Enumeration()" select="remarks"/>
        /// <param name="getFieldFunc">Provide this method in case you want to create a non-public Enumeration type for use in a low-trust environment.</param>
        /// <exception cref="Exception">
        /// Thrown if this operation causes the number of created instances to exceed the number of enumeration values declared for this enumeration type.
        /// </exception>
        /// <seealso cref="Enumeration{TEnumeration}.GetStaticFieldValue"/>
        /// <seealso cref="Enumeration{TEnumeration}._IsInitialized"/>
        protected Enumeration(GetStaticFieldValue getFieldFunc)
            : base(getFieldFunc)
        {
            if (_IsInitialized)
                InitializeValues();
        }

        /// <summary>
        /// System.Enum value associated with this Enumeration value.
        /// </summary><remarks>
        /// The returned System.Enum value is guaranteed to have a name that exactly matches the name of this Enumeration value.
        /// </remarks>
        public TEnum EnumValue { get; private set; }

        bool IEquatable<Enumeration<TEnumeration, TEnum>>.Equals(Enumeration<TEnumeration, TEnum> other)
            => ReferenceEquals(this, other);

        bool IEquatable<TEnum>.Equals(TEnum other)
            => unchecked((ulong)Ordinal) == ToUInt64(other);

        // -----

        // will be set to null if for all enumeration values their ordinal match the numerical value of the TEnum value they are paired with.
        private static EqualityComparer<TEnum> eqcTEnum; // assigned in static cctor

        static Enumeration()
        {
            var tEnum = typeof(TEnum);
            if (!tEnum.IsEnum)
            {
                Debug.Assert(false, ErrPrefix + ERR_TENUM);
                throw new Exception(ErrPrefix + ERR_TENUM);
            }

            // this is excessive - don't do this in release.
            Debug.Assert(IsEnumCompatible<TEnum>(), ErrPrefix + ": Unsupported System.Enum type - underlying type must be char, bool, or an integral type.");

            eqcTEnum = EqualityComparer<TEnum>.Default;
        }

        // True if for all enumeration values their ordinal match the numerical value of the TEnum value they are paired with.
        private static bool IsOrdinalPaired
            => eqcTEnum == null;

        internal sealed override bool HasEnumValueImpl
            => true;

        internal sealed override Type EnumValueType
            => typeof(TEnum);

        // TEnumX must either be System.Enum or a valueType.
        internal sealed override bool TryGetEnumValueInternal<TEnumX>(out TEnumX enumValue, bool allowConversion) // (TEnumX : IConvertible)
        {
            if (EnumValue is TEnumX) // the System.Enum case will fall in here:
            {
                enumValue = (TEnumX)(object)EnumValue; // not sure how to get around this ugly double-cast... System.Enum is a bastard! :(
                return true;
            }
            else
            {
                AZAssert.Internal(Evaluate.IsEnumCompatible<TEnumX>(), "not enum compatible");

                if (allowConversion)
                {
                    // we *might* be able to perform some conversion magic even if the type is wrong ;)
                    return Numeric.TryConvertInteger(EnumValue, out enumValue);
                }
                else
                {
                    enumValue = default(TEnumX);
                    return false;
                }
            }
        }

        private TEnumeration TryParse(TEnum enumValue)
        {
            var values = ValArrayInternal;

            var eq = eqcTEnum;
            if (eq == null) // see IsOrdinalPaired
            {
                ulong ev_u64 = ToUInt64(enumValue);
                if (ev_u64 < (ulong)values.Length)
                    return values[(int)ev_u64];
            }
            else // perform an O(n) search:
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    var v = values[i];
                    if (eq.Equals(v.EnumValue, enumValue))
                        return v;
                }
            }

            return null;
        }

        internal sealed override TEnumeration TryParse<TEnum2>(TEnum2 enumValue, bool allowConversion = false)
        {
            if (IsEnumCompatible(enumValue))
            {
                if (enumValue is TEnum)
                    return TryParse((TEnum)(object)enumValue);

                TEnum eVal;
                if (allowConversion && Numeric.TryConvertInteger(enumValue, out eVal))
                    return TryParse(eVal);
            }

            return null;
        }

        internal sealed override Enumeration TryParseBasic<TEnum2>(TEnum2 enumValue, bool allowConversion = false)
        {
            return TryParse(enumValue, allowConversion);
        }

        // needed because the Convert class, which all Enum types use to implement IConvertible, does bounds checks!
        private static ulong ToUInt64(TEnum enumValue)
        {
            var tc = enumValue.GetTypeCode();
            bool u = Numeric.IsUnsigned(tc);
            return u ? enumValue.ToUInt64(null) : unchecked((ulong)enumValue.ToInt64(null)); // (The IFormatter argument is unused)
        }

        private static bool ValueExists(TEnumeration[] values, TEnum value, int count, EqualityComparer<TEnum> eq)
        {
            AZAssert.NotNullInternal(values, nameof(values));
            AZAssert.NotNullInternal(eq, nameof(eq));
            AZAssert.BoundsInternal(values, count, nameof(count), true);

            for (int i = 0; i < count; ++i)
            {
                if (eq.Equals(values[i].EnumValue, value))
                    return true;
            }
            return false;
        }

        private static void InitializeValues()
        {
            AZAssert.Internal(_IsInitialized, "names not initialized");

            bool isOrdinalPaired = true;
            var eq = eqcTEnum;
            TEnumeration v = null;
            try
            {
                var tEnum = typeof(TEnum);
                var values = ValArrayInternal;
                for (int i = 0; i < values.Length; ++i)
                {
                    v = values[i];
                    v.EnumValue = (TEnum)Enum.Parse(tEnum, v.Name);

                    // Verify requirement: Enum value uniqueness
                    if (ValueExists(values, v.EnumValue, i, eq))
                    {
                        Debug.Assert(false, ErrPrefix + "." + v.Name + ERR_ENUM_UNIQUE);
                        throw new Exception(ErrPrefix + "." + v.Name + ERR_ENUM_UNIQUE);
                    }

                    isOrdinalPaired &= (ulong)i == ToUInt64(v.EnumValue); // i == Ordinal
                }
            }
            catch (ArgumentException e)
            {
                Debug.Assert(false, ErrPrefix + v.Name + ERR_ENUM_MISSING, e.Message);
                throw new Exception(ErrPrefix + v.Name + ERR_ENUM_MISSING, e);
            }
            catch (Exception e)
            {
                throw new Exception(ErrPrefix + ERR_BASE, e); // This should never happen!
            }

            if (isOrdinalPaired)
                eqcTEnum = null;
        }

        /* Enum.TryParse doesn't exist in .net 3.5 :(
        private static void InitializeValues()
        {
            AZAssert.Internal(IsNamesInitialized, "names not initialized");

            bool isOrdinalPaired = true;
            var eq = eqcTEnum;
            var values = Values.Array;
            for (int i = 0; i < values.Length; ++i)
            {
                var v = values[i];
                TEnum v_enum;
                if (!Enum.TryParse(v.Name, out v_enum))
                {
                    Debug.Assert(false, Err_TName + v.Name + ERR_ENUM_MISSING);
                    throw new Exception(Err_TName + v.Name + ERR_ENUM_MISSING);
                }
                v.Value = v_enum;

                // Verify requirement: Enum value uniqueness
                if (ValueExists(values, v.Value, i, eq))
                {
                    Debug.Assert(false, Err_TName + "." + v.Name + ERR_ENUM_UNIQUE);
                    throw new Exception(Err_TName + "." + v.Name + ERR_ENUM_UNIQUE);
                }

                isOrdinalPaired &= (ulong)i == ToUInt64(v.EnumValue); // i == Ordinal
            }

            if (isOrdinalPaired)
                eqcTEnum = null;
        }
        */
    }
}
#pragma warning restore CS0660, CS0661
