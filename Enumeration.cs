using System;

namespace AZCL
{
    /// <summary>
    /// Represents a Java-style pure enumeration, providing base features such as Ordinal, Name, and equality operators.
    /// </summary><remarks>
    /// This is the non-generic abstract base class of all pure enumeration types.
    /// <b>Note that when creating your own enumeration you must inherit from either the <see cref="Enumeration{TEnumeration}"/> or
    /// the <see cref="Enumeration{TEnumeration, TEnum}"/> class.</b>
    /// (It is impossible to create Enumeration types that inherit directly from the non-generic enumeration type.)<br/>
    /// See the Remarks for the two aforementioned classes to read about their contracts / implementation requirements. 
    /// <para/>
    /// The term "pure" comes from the fact that this enumeration type only allows the unique set of values defined in it at design time.
    /// Only this fixed set of values (and null) is allowed and no additional other values can exist or be created at runtime.
    /// <br/>
    /// Unfortunately such is not the case for System.Enum, since it was designed to match C++ style integer based enumerations and flags.
    /// This means that for uses where only a fixed set of discrete input values are valid, the theoretically ideal use case of an
    /// enumeration, complicated validation can sometimes be required since C# provides no guarantee an Enum has a valid value belonging
    /// to this fixed set, and no elegant and performant way of verifying it either.
    /// <para/>
    /// System.Enum is an old C++ legacy that should never have been "ported over to" (hacked into) a strongly typed language,
    /// something that the designers of C# themselves said was one of their biggest mistakes in designing the language.
    /// Conceptually Enumerations and Flags are different and should have been two different types:
    /// Whereas an enumeration value is always a single plain value, a flag represents a set of values - possibly the empty set.
    /// </remarks>
    /// <seealso cref="Enumeration{TEnumeration}"/>
    /// <seealso cref="Enumeration{TEnumeration, TEnum}"/>
    public abstract class Enumeration : IEquatable<Enumeration>
    {
        /// <summary>
        /// The ordinal of this enumeration value.
        /// </summary><remarks>
        /// The ordinal is determined by the textual order in which the enumeration values are defined in the user defined enumeration type.<br/>
        /// (See ECMA-334 (C# language specification), Variable Initializers, section 17.4.5 
        /// <a href="http://www.ecma-international.org/publications/files/ECMA-ST/Ecma-334.pdf">http://www.ecma-international.org/publications/files/ECMA-ST/Ecma-334.pdf</a>)
        /// <para/>
        /// The ordinal is guaranteed to be unique among the ordinals of enumerations values of the same enumeration type.
        /// It is also guaranteed to have a value on the continuous range 0 to (Count - 1), where Count is the number of enumeration values defined for the enumeration type.
        /// <br/>A static Count property exists on all types derived from Enumeration.
        /// </remarks>
        /// <seealso cref="Enumeration{TEnumeration}.Count"/>
        public int Ordinal { get; }

        /// <summary>
        /// Name of this enumeration value.
        /// </summary><remarks>
        /// The name is guaranteed to be unique among the names of enumeration values of the same enumeration type.
        /// <para/>
        /// IMPORTANT:<br/>
        /// This property is late-initialized. It is not guaranteed to be initialized yet if accessed from an instance
        /// constructor of the user defined enumeration type. Accessing it from the static constructor is OK assuming
        /// all the (public static) fields of the user defined enumeration type have already been assigned to.
        /// </remarks>
        /// <seealso cref="Enumeration{TEnumeration}.IsNamesInitialized"/>
        public string Name { get; private set; }

        /// <summary>
        /// Indicates whether the Enumeration type that this value belongs to is paired to a System.Enum type, i.e. if this is an Enumeration&lt;,&gt; type.
        /// </summary>
        /// <seealso cref="GetEnumValueType"/>
        public bool HasEnumValue
            => HasEnumValueImpl;

        /// <summary>
        /// Gets the type of the System.Enum, or null if this Enumeration type isn't paired with a System.Enum.
        /// </summary>
        /// <seealso cref="HasEnumValue"/>
        public Type GetEnumValueType()
            => EnumValueType;

        /// <summary>
        /// Returns whether the specified Type is an Enumeration class or not.
        /// </summary><remarks>
        /// If <paramref name="requireSealedClosedConstructed"/> is true, the type must be sealed and closed constructed.<br/>
        /// (See <a href="https://msdn.microsoft.com/en-us/library/aa479859.aspx">Generics FAQ: Fundamentals</a> at MSDN.)<br/>
        /// Enforcing the sealed type requirement means that expressions such as "IsEnumeration(typeof(Enumeration), true)" and
        /// "IsEnumeration(typeof(Enumeration&lt;MyEnum&gt;), true)" will return false.
        /// Enforcing the closed constructed type requirement means that expressions such as "IsEnumeration(typeof(Enumeration&lt;,&gt;), true)" and
        /// "IsEnumeration(typeof(MyGenericEnum&lt;&gt;))" will return false.
        /// <para/>
        /// Examples of expressions that would satisfy the aforementioned requirements and return true are "IsEnumeration(typeof(MyEnum), true)" and
        /// "IsEnumeration(typeof(MyGenericEnum&lt;int&gt;), true)" - assuming of course that MyEnum and MyGenericEnum inherit from one of the
        /// Enumeration classes (and that 'int' is a valid type parameter to the MyGenericEnum&lt;&gt; class).
        /// <para/>
        /// <note type="note">
        /// If you're looking for a proper generic type constraint for Enumerations, there are two options:
        /// <code>MyMethod&lt;T&gt;() where T : Enumeration</code>
        /// OR
        /// <code>MyMethod&lt;T&gt;() where T : Enumeration&lt;T&gt;</code>
        /// the difference being that in the former case <b>T</b> can be an abstract type (which is usually not what you want),
        /// whereas in the latter case <b>T</b> will always be an "actual" (user defined) enumeration type (and would satisfy "IsEnumeration(typeof(T))").
        /// In the latter case you can also get access to the static <see cref="Enumeration{TEnumeration}.Count"/> and <see cref="Enumeration{TEnumeration}.Values"/>
        /// properties. For example to access the Count property you would write:
        /// <code>
        /// int MyMethod&lt;TEnumeration&gt;() where TEnumeration : Enumeration&lt;TEnumeration&gt;
        /// {
        ///     return Enumeration&lt;TEnumeration&gt;.Count;
        /// }
        /// </code>
        /// </note>
        /// </remarks><returns>
        /// True if the specified Type is class that is assignable to <see cref="Enumeration"/> and either <paramref name="requireSealedClosedConstructed"/> is false
        /// or the type must also be sealed and closed constructed; otherwise false.
        /// </returns>
        /// <param name="type">The type to inspect.</param>
        /// <param name="requireSealedClosedConstructed">If this is true then the type must also be sealed and closed constructed.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="type"/> is null.
        /// </exception>
        public static bool IsEnumeration(Type type, bool requireSealedClosedConstructed = false)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (requireSealedClosedConstructed)
                return type.IsClass && type.IsSealed && typeof(Enumeration).IsAssignableFrom(type) && !type.ContainsGenericParameters;
            else
                return type.IsClass && typeof(Enumeration).IsAssignableFrom(type);
        }

        /// <summary>
        /// Tries to retrieve the System.Enum value paired with the specified Enumeration value.
        /// </summary><returns>
        /// The System.Enum value paired to the specified Enumeration value;
        /// or null if the specified Enumeration value isn't paired to any System.Enum value.
        /// </returns>
        /// <param name="value">An Enumeration value to attempt to retrieve a System.Enum value from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the Enumeration <paramref name="value"/> is null.
        /// </exception>
        public static System.Enum TryGetEnumValue(Enumeration value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (!value.HasEnumValueImpl)
                return null;

            System.Enum enumValue;
            value.TryGetEnumValue(out enumValue, false);
            return enumValue;
        }

        /// <summary>
        /// Tries to retrieve the System.Enum value paired with the specified Enumeration value, as a <typeparamref name="TEnum"/> value.
        /// </summary><remarks>
        /// Conversions are only performed if <paramref name="allowConversion"/> is true and the System.Enum value paired to the Enumeration
        /// <paramref name="value"/> can be converted to the <typeparamref name="TEnum"/> type without loss of information, including the sign.
        /// In other words the conversion will only be performed if the <i>numeric value</i> of the <typeparamref name="TEnum"/> result will
        /// exactly match the <i>numeric value</i> of the paired System.Enum value.
        /// <para/>
        /// For example given a user implemented Enumeration type called MyEnum paired to the following System.Enum declaration:
        /// <code>
        /// public enum InnerEnum : long
        /// {
        ///     Foo = 42, Bar = -100, Bas = long.MaxValue
        /// }
        /// </code>
        /// And given the following example method:
        /// <code>
        /// public void TestUIntConversion(Enumeration value)
        /// {
        ///     uint result;
        ///     bool success = Enumeration.TryGetEnumValue(value, out result, true);
        ///     Console.WriteLine("Conversion to uint successful? " + success + ". Result: " + result);
        /// }
        /// </code>
        /// Calling "TestUIntConversion(MyEnum.Foo);" would print "Conversion to uint successful? True. Result: 42".<br/>
        /// Calling "TestUIntConversion(MyEnum.Bar);" would print "Conversion to uint successful? False. Result: 0".<br/>
        /// Calling "TestUIntConversion(MyEnum.Bas);" would print "Conversion to uint successful? False. Result: 0".
        /// <para/>
        /// <note type="caution">
        /// The conversion option does not currently work to or from bool and char types!
        /// Support for this will be added in a future release, but for the time being these conversions should be considered U.B.
        /// </note>
        /// <note type="note">
        /// This method does not rely on nasty try-catch logic for its control flow.<br/>
        /// The bounds are properly tested using meta programming <i>before</i> any conversion and casting is attempted.<br/>
        /// (See the AZCL.Meta namespace.)
        /// </note>
        /// </remarks><returns>
        /// True if either the specified Enumeration value is paired to a System.Enum value of type <typeparamref name="TEnum"/>, or
        /// <paramref name="allowConversion"/> is true and the specified Enumeration value is paired to a System.Enum value that could
        /// be converted to a value of type <typeparamref name="TEnum"/> that has an identical numerical value (including sign);
        /// otherwise false.
        /// </returns>
        /// <typeparam name="TEnum">A System.Enum compatible type to try and cast the result to.</typeparam>
        /// <param name="value">An Enumeration value to attempt to retrieve a System.Enum value from.</param>
        /// <param name="enumValue">
        /// The System.Enum value paired to the specified Enumeration value cast or converted to the type <typeparamref name="TEnum"/>;
        /// or default(<typeparamref name="TEnum"/>) if the specified Enumeration value isn't paired to any System.Enum value, or the
        /// specified Enumeration value has a paired System.Enum value but either <paramref name="allowConversion"/> was false and the
        /// System.Enum value wasn't of type <typeparamref name="TEnum"/> or <paramref name="allowConversion"/> was true but the
        /// System.Enum value can't be converted to the requested type without loss of information (or change of sign).
        /// </param>
        /// <param name="allowConversion">
        /// True to attempt a (numerically lossless) conversion to the specified <typeparamref name="TEnum"/> type. (Default: false)
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the Enumeration <paramref name="value"/> is null.
        /// </exception>
        public static bool TryGetEnumValue<TEnum>(Enumeration value, out TEnum enumValue, bool allowConversion = false)
            where TEnum : struct, IConvertible
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value.HasEnumValueImpl && (typeof(TEnum).IsEnum || IsEnumCompatible<TEnum>()))
                return value.TryGetEnumValue(out enumValue, allowConversion);

            enumValue = default(TEnum);
            return false;
        }

        /// <summary>
        /// Indicates whether this enumeration value is equal to another enumeration value.
        /// </summary><returns>
        /// True if these enumeration values are reference equal; otherwise false.
        /// </returns>
        /// <param name="other">An enumeration value to compare against.</param>
        public bool Equals(Enumeration other)
            => ReferenceEquals(this, other);

        /// <summary>
        /// Indicates whether this enumeration value is equal to the specified object.
        /// </summary><returns>
        /// True if this enumeration value and the object are reference equal; otherwise false.
        /// </returns>
        /// <param name="obj">An object to compare against.</param>
        public sealed override bool Equals(object obj)
            => ReferenceEquals(this, obj);

        /// <summary>
        /// Returns the hash code for this enumeration value.
        /// </summary>
        public sealed override int GetHashCode()
            => Ordinal;

        /// <summary>
        /// Returns the name of this enumeration value.
        /// </summary>
        public sealed override string ToString()
            => Name;

        /// <summary>
        /// Indicates whether two enumeration values are equal.
        /// </summary>
        public static bool operator ==(Enumeration left, Enumeration right)
        {
            return ReferenceEquals(left, right);
        }

        /// <summary>
        /// Indicates whether two enumeration values are unequal.
        /// </summary>
        public static bool operator !=(Enumeration left, Enumeration right)
        {
            return !ReferenceEquals(left, right);
        }

        // -----

        // Must be internal to ensures that no external classes can inherit directly from this type.
        internal Enumeration(int ordinal)
        {
            Ordinal = ordinal;
        }

        // assumes name parameter gets a non-null argument!
        internal bool TryInitializeName(string name)
        {
            if (Name == null)
            {
                Name = name;
                return true;
            }
            return false;
        }
        
        internal virtual bool HasEnumValueImpl
            => false;

        internal virtual Type EnumValueType
            => null;

        // the enum-compatible types are: bool, char, sbyte, byte, int16, uint16, int32, uint32, int64, and uint64.
        internal static bool IsEnumCompatible<T>() // similar to AZCL.Meta.Evaluate.IsEnumCompatible but with constraints that make it more efficient.
            where T : struct, IConvertible
        {
            var val = default(T);
            return unchecked((uint)val.GetTypeCode() - 3u) < 10u;
        }

        // assumes TEnumX is either the System.Enum class or an EnumCompatible primitive type (the latter of which includes user defined System.Enums)!!
        internal virtual bool TryGetEnumValue<TEnumX>(out TEnumX enumValue, bool allowConversion) where TEnumX : IConvertible
        {
            enumValue = default(TEnumX);
            return false;
        }

        /*
        public static ReadOnlyArray<Enumeration> GetValues(Type type) // <-- feature creep... It could have made more sense if it wasn't so expensive and nasty!
        {
            if (!IsEnumeration(type))
                return null;

            return type.BaseType
                ?.GetMethod(nameof(_Enum.GetValuesInternal), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
                ?.Invoke(null, BindingFlags.Static | BindingFlags.NonPublic, null, null, null) as Enumeration[];
        }

        private class _Enum : Enumeration<_Enum> // <-- dummy type used for accessing member names with nameof, for refactor-friendliness.
        {
            private _Enum() { }
        }
        */
    }
}
