using System;
using System.Collections;
using AZCL.Collections;

namespace AZCL
{
    /// <summary>
    /// Provides information about and access to an Enumeration type and its values.
    /// </summary><remarks>
    /// This non-generic interface is useful when handling multiple <see cref="IEnumValues"/> instances for different Enumeration types.
    /// </remarks>
    /// <seealso cref="IEnumValues{TEnumeration}"/>
    public interface IEnumValues : IEnumerable
    {
        /// <summary>
        /// Number of Enumeration values declared for the represented Enumeration type.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// All Enumeration values declared for the represented Enumeration type, in ordinal / declaration order (wrapped in a readonly array).
        /// </summary><remarks>
        /// Use <see cref="ReadOnlyArray{T}.CopyBacking"/> if a non-readonly copy is required.
        /// </remarks>
        ReadOnlyArray<Enumeration> Array { get; }

        /// <summary>
        /// Gets the Enumeration value from the represented Enumeration type that has the specified ordinal.
        /// </summary>
        /// <param name="ordinal">Ordinal of the Enumeration value.</param>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if <paramref name="ordinal"/> is less than zero or greater than or equal to <see cref="Count"/>.
        /// </exception>
        Enumeration this[int ordinal] { get; }

        /// <summary>
        /// Gets the first Enumeration value of the represented Enumeration type.
        /// </summary><remarks>
        /// If the Enumeration is empty (no values declared) this property is null.
        /// </remarks>
        Enumeration First { get; }

        /// <summary>
        /// Gets the last Enumeration value of the represented Enumeration type.
        /// </summary><remarks>
        /// If the Enumeration is empty (no values declared) this property is null.
        /// </remarks>
        Enumeration Last { get; }

        /// <summary>
        /// Indicates whether the represented Enumeration type is paired to a System.Enum type, i.e. if this is an Enumeration&lt;,&gt; type*.
        /// </summary><remarks>
        /// *This property will be false regardless if the represented Enumeration type is an empty enumeration, i.e. has no values.
        /// </remarks>
        /// <seealso cref="EnumValueType"/>
        bool HasEnumValues { get; }

        /// <summary>
        /// Gets the type of the System.Enum, or null if the represented Enumeration type isn't paired with a System.Enum.
        /// </summary>
        /// <seealso cref="HasEnumValues"/>
        Type EnumValueType { get; }

        /// <summary>
        /// Tries to find the Enumeration value with the specified <paramref name="name"/> among the values defined for the represented Enumeration type.
        /// </summary><remarks>
        /// This is an O(n) operation.
        /// <br/>
        /// <note type="inheritinfo">
        /// <b>Note To Enumeration Inheritors:</b><br/>
        /// This method requires all public static Enumeration fields inside the represented Enumeration class to have finished initializing
        /// before it can search them, so if those fields are not initialized yet it might not find the sought value even though it exists.
        /// </note>
        /// </remarks><returns>
        /// Returns the Enumeration value with the specified name, if it exists; otherwise null.
        /// </returns>
        /// <param name="name">The name of the enumeration value to find.</param>
        Enumeration TryParse(string name);

        /// <summary>
        /// Tries to find the Enumeration value paired with the specified <paramref name="enumValue"/>
        /// among the Enumeration values defined for the represented Enumeration type.
        /// </summary><remarks>
        /// This is either an O(1) or an O(n) operation.
        /// If the numerical value of all System.Enum values match the ordinal of the Enumeration value it is paired to* it becomes O(1); otherwise O(n).
        /// <br/><i>*This condition is evaluated during late initialization.</i>
        /// <para/>
        /// Conversions are only performed if <paramref name="allowConversion"/> is true and the specified <typeparamref name="TEnum"/> value can be
        /// converted to the System.Enum type paired to the represented Enumeration type without change in numerical value, including the sign.
        /// <br/>
        /// <note type="caution">
        /// The conversion option does not currently work to or from bool and char types!
        /// Support for this will be added in a future release, but for the time being these conversions should be considered U.B.
        /// </note>
        /// <note type="note">
        /// This method does not rely on nasty try-catch logic for its control flow.<br/>
        /// The bounds are properly tested using meta programming <i>before</i> any conversion and casting is attempted.<br/>
        /// (See the AZCL.Meta namespace.)
        /// </note>
        /// <note type="inheritinfo">
        /// <b>Note To Enumeration Inheritors:</b><br/>
        /// This method requires all public static Enumeration fields inside the represented Enumeration class to have finished initializing
        /// before it can search them, so if those fields are not initialized yet it might not find the sought value even though it exists.
        /// <br/>
        /// See also the protected property <see cref="Enumeration{TEnumeration}._IsInitialized"/>.
        /// </note>
        /// </remarks><returns>
        /// Returns the Enumeration value whose paired <see cref="Enumeration{TEnumeration, TEnum}.EnumValue"/> matches the specified
        /// <paramref name="enumValue"/>, if it exists and assuming the specified <paramref name="enumValue"/> was of the correct System.Enum
        /// type (or possibly of some other type that is holding a numerical value that can be converted to the correct System.Enum type without
        /// changing its numerical value); otherwise null.
        /// </returns>
        /// <param name="enumValue">The paired System.Enum value of the Enumeration value to find.</param>
        /// <param name="allowConversion">
        /// True to allow attempting a (numerically lossless) conversion from the specified value of <typeparamref name="TEnum"/> type to the System.Enum
        /// type that the represented Enumeration type is paired with. (No conversion is necessary nor attempted if it already is of correct type.)
        /// (Default: false)
        /// </param>
        Enumeration TryParse<TEnum>(TEnum enumValue, bool allowConversion = false)
            where TEnum : struct, IConvertible;

        /// <summary>
        /// Gets an enumerator for the enumeration values. This method allows <see cref="IEnumValues"/> instances to be used in foreach loops.
        /// </summary>
        /// <seealso cref="Array"/>
        new ReadOnlyArray<Enumeration>.Enumerator GetEnumerator();
    }
}
