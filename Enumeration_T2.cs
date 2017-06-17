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
    /// <note type="inheritinfo">
    /// Inherit directly from this type if you need your Enumeration values to be paired with (identically named) System.Enum values
    /// (of <typeparamref name="TEnum>"/> type).
    /// Pairing your Enumeration type with a System.Enum type is most notably useful if you want to be able to switch on its values
    /// (since switch case statements only accept const values and literals). Switching without a System.Enum type is still possible
    /// (beginning with C# 6) using the 'nameof' operator since it returns string literals at compile time - but switching on strings
    /// is not as efficient as switching on System.Enum values.
    /// </note>
    /// <note type="inheritinfo">
    /// To inherit from this class, to create a pure Enumeration,type, there are several important but simple contract requirements.
    /// (The contract requirements for <see cref="Enumeration{TEnumeration, TEnum}"/> are strikingly similar to those of <see cref="Enumeration{TEnumeration}"/>.)
    /// <br/>
    /// To simplify the description of the contract (by means of example) lets assume we are implementing an Enumeration class
    /// called MyEnum and look at what requirements the MyEnum class must satisfy.
    /// <br/>
    /// First lets look at the contract for the MyEnum type declaration:
    /// <list type="bullet">
    ///     <item>The MyEnum class must inherit directly from the Enumeration&lt;,&gt; class, i.e. Enumeration&lt;,&gt; must be its base class.</item>
    ///     <item>The MyEnum class must be sealed, preventing any other class from inheriting from MyEnum.</item>
    ///     <item>The MyEnum class must specify itself as the first generic type parameter to Enumeration&lt;,&gt;.</item>
    ///     <item>The MyEnum class must specify the System.Enum type it wishes to pair with as the second generic type parameter to Enumeration&lt;,&gt;.</item>
    /// </list>
    /// In other words the MyEnum class declaration should look something like this:
    /// <code>
    /// sealed class MyEnum : Enumeration&lt;MyEnum, MyEnum.Enum&gt;
    /// {
    ///     // ...
    ///     
    ///     public enum Enum
    ///     {
    ///         // ...
    ///     }
    /// }
    /// </code>
    /// Note that the paired System.Enum type (here simply named Enum) does not need to be declared inside the MyEnum class, it just makes
    /// the pairing more obvious. It also makes MyEnum's paired System.Enum type easy to find for the programmer.
    /// <br/>
    /// Second lets look at the contract details to follow when declaring Enumeration values inside the MyEnum class:
    /// <list type="bullet">
    ///     <item>All values defined for an Enumeration type must be declared as public static readonly.</item>
    ///     <item>All values defined for an Enumeration type must be of the same type as the type they are declared in.</item>
    ///     <item>All values defined for an Enumeration type must be initialized, either with initializers or in a static constructor.</item>
    ///     <item>For every value defined for an Enumeration type there must exist an identically named Enum value in the paired System.Enum type. (Case sensitive!)</item>
    ///     <item>Every System.Enum value that is paired to an Enumeration value must be numerically unique among all values paired to that Enumeration type.</item>
    /// </list>
    /// Again looking at the MyEnum example, our code should now either look something like this:
    /// <code>
    /// sealed class MyEnum : Enumeration&lt;MyEnum, MyEnum.Enum&gt;
    /// {
    ///     public static readonly MyEnum Foo = new MyEnum();
    ///     public static readonly MyEnum Bar = new MyEnum();
    ///     public static readonly MyEnum Bas = new MyEnum();
    ///     
    ///     public enum Enum
    ///     {
    ///         Foo,
    ///         Bar,
    ///         Bas,
    ///     }
    /// }
    /// </code>
    /// Or this:
    /// <code>
    /// sealed class MyEnum : Enumeration&lt;MyEnum, MyEnum.Enum&gt;
    /// {
    ///     public static readonly MyEnum Foo;
    ///     public static readonly MyEnum Bar;
    ///     public static readonly MyEnum Bas;
    ///     
    ///     public enum Enum
    ///     {
    ///         Foo,
    ///         Bar,
    ///         Bas,
    ///     }
    ///     
    ///     static MyEnum()
    ///     {
    ///         Foo = new MyEnum();
    ///         Bar = new MyEnum();
    ///         Bas = new MyEnum();
    ///     }
    /// }
    /// </code>
    /// In the latter case it is important to note that the ordinal of each value is determined by the order of initialization.
    /// In the former case the C# specification guarantees that initialization takes place in textual order, meaning Foo will
    /// have ordinal 0, Bar will have ordinal 1, and Bas will have ordinal 2 (matching the default behavior the System.Enum type).
    /// <br/>
    /// <b>It is VERY important that all Enumeration values are statically initialized!</b>
    /// Only once Count initializations have been performed, where Count is the number of public static MyEnum fields declared
    /// inside the MyEnum class, will data such as the Name properties of the Enumeration values be initialized. This is referred
    /// to throughout the Enumeration documentation as "Late initialization". Furthermore the above three contract requirements
    /// are not asserted / evaluated until late initialization takes place. This could make a missing initialization error or
    /// other breach of the three contract requirements for Enumeration value declaration potentially easy to miss.
    /// <br/>
    /// To mitigate the above risks the Enumeration class provides the protected property <see cref="Enumeration{TEnumeration}._IsInitialized"/>.
    /// It is recommended to assert this property in the Enumeration type's static constructor:
    /// <code>
    /// sealed class MyEnum : Enumeration&lt;MyEnum, MyEnum.Enum&gt;
    /// {
    ///     public static readonly MyEnum Foo = new MyEnum();
    ///     public static readonly MyEnum Bar = new MyEnum();
    ///     public static readonly MyEnum Bas = new MyEnum();
    ///     
    ///     public enum Enum
    ///     {
    ///         Foo,
    ///         Bar,
    ///         Bas,
    ///     }
    ///     
    ///     static MyEnum()
    ///     {
    ///         Debug.Assert(_IsInitialized);
    ///     }
    /// }
    /// </code>
    /// Going back to the paired System.Enum type it is worth to take note of the following points:
    /// <list type="nobullet">
    ///     <item>Several Enumeration types can be paired to the same System.Enum type, however this is discouraged.</item>
    ///     <item>There is no restriction on the underlying type of a paired System.Enum type - it can have any underlying type.
    ///         (For reference the types supported by System.Enum are: bool, char, sbyte, byte, short, ushort, int, uint, long, and ulong.)</item>
    ///     <item>The numerical values assigned to the values of the paired System.Enum can be arbitrary (so long as the uniqueness requirement is met).</item>
    ///     <item>A paired System.Enum type is allowed to define other values outside of the required ones
    ///         (and those values need not be numerically unique), however extra values are discouraged.</item>
    ///     <item>Reverse lookup from System.Enum to Enumeration simplifies into an O(1) operation if all paired System.Enum values have a numerical
    ///         value that matches the Ordinal of the Enumeration value it is paired with. (Otherwise reverse lookup is an O(n) operation.)</item>
    /// </list>
    /// Speaking of reverse lookup, that and much more can be done through the IEnumValues instance that can be acquired from either the
    /// <see cref="Enumeration{TEnumeration}.Values"/> property or the static <see cref="Enumeration{TEnumeration}.GetValues"/> method.)
    /// <br/>
    /// Third lets look at the one and only usage invariant, again phrased with the MyEnum example in mind:
    /// <list type="bullet">
    ///     <item>
    ///         Exactly Count instances of MyEnum can be created, where Count is the number of public static
    ///         MyEnum fields declared inside the MyEnum class. Further instantiation is not allowed.
    ///     </item>
    /// </list>
    /// Although it's not required, <b>it is STRONGLY recommended that all constructors be declared as private</b>.
    /// This recommendation is partly due to how late initialization works, but more generally speaking it should never be possible
    /// to instantiate a new value of a "pure" enumeration type. Assuming a properly initialized Enumeration type, attempting to
    /// create further instances will throw an exception (and fail an assert in debug build) during construction.
    /// Declaring all constructors private will prevent third party code from trying to instantiate your Enumeration type,
    /// because chances are not every programmer using your code will read its documentation, and ideally they shouldn't need to.
    /// <br/>
    /// In conclusion, a properly implemented System.Enum paired Enumeration type should ideally look something like this:
    /// <code>
    /// sealed class MyEnum : Enumeration&lt;MyEnum, MyEnum.Enum&gt;
    /// {
    ///     private MyEnum() { }
    ///     
    ///     public static readonly MyEnum Foo = new MyEnum();
    ///     public static readonly MyEnum Bar = new MyEnum();
    ///     public static readonly MyEnum Bas = new MyEnum();
    ///     
    ///     public enum Enum
    ///     {
    ///         Foo,
    ///         Bar,
    ///         Bas,
    ///     }
    ///     
    ///     static MyEnum()
    ///     {
    ///         Debug.Assert(_IsInitialized);
    ///     }
    /// }
    /// </code>
    /// To recap the static and private constructors are optional but highly recommended.
    /// <br/>
    /// (For implementing a non-public Enumeration additionally see <see cref="Enumeration{TEnumeration}.GetStaticFieldValue"/>.) 
    /// </note>
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
        protected Enumeration()
        {
            if (Enumeration<TEnumeration>._IsInitialized)
                InitializeEnumValues();
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

        /* TODO: feature creep? the value of this is..?
        /// <summary>
        /// Indicates whether the names (as accessed from the <see cref="Enumeration.Name"/> property) and System.Enum values
        /// (as accessed from the <see cref="EnumValue"/> property) for this enumeration type have been initialized yet.
        /// </summary><remarks>
        /// The names and System.Enum values are late-initialized (when the last of the enumeration values declared in the enumeration type is instantiated).
        /// Thus these properties are not guaranteed to be initialized yet if accessed from an instance constructor of the user defined enumeration type.
        /// (The Name property will return null before it is initialized and the <see cref="EnumValue"/> property will similarly return default(TEnum).)
        /// <br/>
        /// However all names and System.Enum values are guaranteed to be initialized / paired before entering a static constructor - assuming no field
        /// initializer was accidentally omitted in the user defined Enumeration type.
        /// <br/>
        /// The easiest way to verify that all fields have an initializer is thus for the user defined enumeration type to assert this property in a static constructor.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        new protected static bool _IsInitialized
            => ???;
        */

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

        // Enum.TryParse doesn't exist in .net 3.5 :(
        private static void InitializeEnumValues()
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
