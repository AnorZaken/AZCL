using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using AZCL.Collections;

/* An example implementation:

    public sealed class ExampleEnum : Enumeration<ExampleEnum>
    {
        private ExampleEnum()
        { }

        public static readonly ExampleEnum Foo = new ExampleEnum();
        public static readonly ExampleEnum Bar = new ExampleEnum();
        public static readonly ExampleEnum Bas = new ExampleEnum();
    }
*/

/* Lets try and answer some important questions:
 *  1. Is this language specification compliant / portable?
 *  2. Are these types thread safe?
 *  3. Does it require full trust?
 *  4. How does the ordinals get assigned in textual order?
 * 
 * The short answer to #1 and #2 is "Yes", and the short answer to #3 is "No" - but it hasn't been verified yet. (This is still a wip feature on a dev branch!)
 * 
 * As for backing up those claims, we will need to dig into some specifications.
 * There are two major concerns for the initialization code of the Enumeration<> class we need to scrutinize:
 *  - How can we guarantee thread safety?
 *  - How can we guarantee that ALL user defined values are initialized before any of them are used (outside of the user defined enumeration type itself)?
 *  
 * The first is simple: ALL of our initialization code, including ALL constructor calls, is run during type initialization, after which all values are readonly.
 * Because ALL our constructor calls are made (during type initialization - thus in a thread-safe way) we can also be sure that name initialization will occur.
 * That sounds good *assuming* ALL constructor calls are made during type initialization. But wait...
 * How do we know that ALL constructor calls, i.e. for ALL the public static fields in a user defined enumeration type, take place during type initialization?
 * There are two cases here, either:
 *  a) The user defined enumeration class has a static constructor.
 *  b) The user defined enumeration class doesn't have a static constructor.
 * 
 * For case a), static constructor present (the class thus not having a "beforeFieldInit" flag), it is quite straightforward and we can derive the outcome from:
 * ECMA-334 (C# language specification), Static constructors, section 17.11 http://www.ecma-international.org/publications/files/ECMA-ST/Ecma-334.pdf
 * "
 *  The static constructor for a class executes at most once in a given application domain.
 *  The execution of a static constructor is triggered by the first of the following events to occur within an application domain:
 *   - An instance of the class is created.
 *   - Any of the static members of the class are referenced.
 *  If a class contains the Main method (Section 3.1) in which execution begins, the static constructor for that class executes before the Main method is called.
 *  If a class contains any static fields with initializers, those initializers are executed in textual order immediately prior to executing the static constructor.
 * "
 * Any use involving the (public static) enumeration values - all stored in static fields - will satisfy:
 * " - Any of the static members of the class are referenced."
 * which means the static constructor of the user defined enumeration type has to run, before which the following also applies:
 * "If a class contains any static fields with initializers, those initializers are executed in textual order immediately prior to executing the static constructor."
 * Which means the constructor calls on ALL the fields of the user defined type will take place, one for each field, at the end of which the names are initialized.
 * (Assuming the author of the user defined enumeration type didn't forget to initialize one of their values, that would be a bug on their part, but not dangerous).
 * At the first of these constructor calls we also satisfy:
 * " - An instance of the class is created."
 * on the Enumeration<TEnumeration> class, which does have a static constructor, which means that the two static fields 'fields' and 'values' are initialized.
 * Only after all these constructors have run and all internal initialization has completed will the static constructor of the user defined type run,
 * meaning that all enumeration values are safe to use inside the user defined static constructor. (Again assuming no bugs on part of its author.)
 * (See also ECMA-335 (CLI specification), Class type definition, part I, section 8.9.5 http://www.ecma-international.org/publications/files/ECMA-ST/ECMA-335.pdf )
 * 
 * For case b) things are ever so slightly different, and we need to look at another part of the specifications:
 * ECMA-334 (C# language specification), Static field initialization, section 17.4.5.1 http://www.ecma-international.org/publications/files/ECMA-ST/Ecma-334.pdf
 * "
 *  If a static constructor (§17.11) exists in the class, execution of the static field initializers occurs immediately prior to executing that static constructor.
 *  Otherwise, the static field initializers are executed at an implementation-dependent time prior to the first use of a static field of that class.
 * "
 * Again, any use of the (public static) enumeration values - all stored in static fields - will satisfy:
 * "...the static field initializers are executed at [or prior to] first use of a static field of that class."
 * Notice especially the use of plural for "initializers" and the use of singular for "a static field" with regards to when it triggers.
 * This again means that the constructor calls on ALL the fields of the user defined type will take place.
 * See case a) for the rest as it is identical.
 * 
 * As for #3, the code doesn't require full trust because we aren't doing anything nasty (such as accessing members we shouldn't have access to).
 * We are reading constructor metadata, and reading metadata about and getting values from public fields.
 * This should all be fine assuming the user defined enumeration type is either publicly accessible or provides a GetStaticFieldValue delegate.
 * For more info see MSDN - Security Considerations for Reflection https://msdn.microsoft.com/en-us/library/stfy7tfc.aspx
 *
 * Finally: (#4) How do ordinals get assigned in textual order?
 * ECMA-334 (C# language specification), Variable Initializers, section 17.4.5 http://www.ecma-international.org/publications/files/ECMA-ST/Ecma-334.pdf
 * "
 *  The default value initialization described in §17.4.3 occurs for all fields, including fields that have variable
 *  initializers. Thus, when a class is initialized, all static fields in that class are first initialized to their default
 *  values, and then the static field initializers are executed in textual order. Likewise, when an instance of a
 *  class is created, all instance fields in that instance are first initialized to their default values, and then the
 *  instance field initializers are executed in textual order. When there are field declarations in multiple partial
 *  type declarations for the same type, the order of the parts is unspecified. However, within each part the field
 *  initializers are executed in order. 
 * "
 * Since as soon as one of the static fields in the user defined enumeration type are accessed / referenced all static fields
 * are initialized (see ECMA-344 17.11, ECMA-334 17.4.5.1, and ECMA-335 part I 8.9.5) and since section 17.4.5 above states:
 * "...the static field initializers are executed in textual order."
 * This means that we can set the ordinal in the ctor using a simple counter - and it will corresponds to the textual order in which the user
 * has defined their enumeration values - mirroring the default behavior of standard C# enums AND making ordinals suitable for serialization.
 * (Again assuming the author of the user defined enumeration type hasn't introduced a bug in their code; but if they have it will be caught.)
 * 
 * Regarding thread safety again, there are some methods and properties which need all of the fields to be initialized before they can operate.
 * While these are "normally" thread safe, calling them from within the type initialization itself can cause them to fail (and throw), since
 * it's impossible to wait for initialization to finish if initialization depends on one of these methods / properties.
 * Just to be clear: It will NOT dead-lock - it trigger initialization, which will do nothing if initialization is already in progress, after
 * which it checks if initialization was completed, which of course it wasn't since we are executing in the middle of it, and thus throw.
 * The exception thrown in such cases is unfortunately a little vague since there are multiple possible causes and it's impossible to know
 * which one of them is the culprit. See the code comments inside TriggerInitialization() for more details.
 * Also see the Remarks section of one of these methods or properties, e.g. the Values property, for all the gory details, including references
 * to the relevant parts of the specification.
 * But here is a quick rule of thumb to keep it simpler:
 * Don't call those methods or properties (nor any code inside or outside the class which might indirectly call them) during type initialization
 * of your enumeration. Unless you *really* need to make a very advanced Enumeration type it should be no problem to just avoid that stuff.
 * Otherwise happy specification reading! ;)
 * (Currently it's only the static TryParse method and the static Values property that require this extra carefulness.) [07/06/2017] [dd/mm/yyyy]
 * (All the aforementioned methods and properties are clearly marked with an inheritors note in their Remarks section.)
 */

#pragma warning disable CS0660, CS0661
namespace AZCL
{
    /// <summary>
    /// Represents a Java-style pure enumeration, providing static properties such as Count and Values
    /// (in addition to the base features of the non-generic <see cref="Enumeration"/> class).
    /// </summary><remarks>
    /// <note type="inheritinfo">
    /// Inherit directly from this type if you do not need to pair values with an equivalent System.Enum type.
    /// Pairing with a System.Enum type is useful for example if you want to be able to switch on its values (since switch case
    /// statements only accept const values and literals). Switching without a System.Enum type is still possible (in C# 6) using the
    /// 'nameof' operator since it returns string literals - but switching on strings is not as efficient as switching on Enum values.
    /// </note>
    /// <note type="inheritinfo">
    /// To inherit from this class, to create a pure Enumeration,type, there are several important but simple contract requirements.
    /// <br/>
    /// To simplify the description of the contract (by means of example) lets assume we are implementing an Enumeration class
    /// called MyEnum and look at what requirements the MyEnum class must satisfy.
    /// <br/>
    /// First lets look at the contract for the MyEnum type declaration:
    /// <list type="bullet">
    ///     <item>The MyEnum class must inherit directly from the Enumeration&lt;&gt; class, i.e. Enumeration&lt;&gt; must be its base class.</item>
    ///     <item>The MyEnum class must be sealed, preventing any other class from inheriting from MyEnum.</item>
    ///     <item>The MyEnum class must specify itself as the generic type parameter to Enumeration&lt;&gt;.</item>
    /// </list>
    /// In other words the MyEnum class declaration should look something like this:
    /// <code>
    /// sealed class MyEnum : Enumeration&lt;MyEnum&gt;
    /// {
    ///     // ...
    /// }
    /// </code>
    /// Second lets look at the contract details to follow when declaring Enumeration values inside the MyEnum class:
    /// <list type="bullet">
    ///     <item>All values defined for an Enumeration type must be declared as public static readonly.</item>
    ///     <item>All values defined for an Enumeration type must be of the same type as the type they are declared in.</item>
    ///     <item>All values defined for an Enumeration type must be initialized, either with initializers or in a static constructor.</item>
    /// </list>
    /// Again looking at the MyEnum example, our code should now either look something like this:
    /// <code>
    /// sealed class MyEnum : Enumeration&lt;MyEnum&gt;
    /// {
    ///     public static readonly MyEnum Foo = new MyEnum();
    ///     public static readonly MyEnum Bar = new MyEnum();
    ///     public static readonly MyEnum Bas = new MyEnum();
    /// }
    /// </code>
    /// Or this:
    /// <code>
    /// sealed class MyEnum : Enumeration&lt;MyEnum&gt;
    /// {
    ///     public static readonly MyEnum Foo;
    ///     public static readonly MyEnum Bar;
    ///     public static readonly MyEnum Bas;
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
    /// have ordinal 0, Bar will have ordinal 1, and Bas will have ordinal 2 (matching the default behavior of System.Enum types).
    /// <br/>
    /// <b>It is VERY important that all Enumeration values are statically initialized!</b>
    /// Only once Count initializations have been performed, where Count is the number of public static MyEnum fields declared
    /// inside the MyEnum class, will data such as the Name properties of the Enumeration values be initialized. This is referred
    /// to throughout the Enumeration documentation as "Late initialization". Furthermore the above three contract requirements
    /// are not asserted / evaluated until late initialization takes place. This could make a missing initialization error or
    /// other breach of the three contract requirements for Enumeration value declaration potentially easy to miss.
    /// <br/>
    /// To mitigate the above risks the Enumeration class provides the protected property <see cref="_IsInitialized"/>.
    /// It is recommended to assert this property in the Enumeration type's static constructor:
    /// <code>
    /// sealed class MyEnum : Enumeration&lt;MyEnum&gt;
    /// {
    ///     public static readonly MyEnum Foo = new MyEnum();
    ///     public static readonly MyEnum Bar = new MyEnum();
    ///     public static readonly MyEnum Bas = new MyEnum();
    ///     
    ///     static MyEnum()
    ///     {
    ///         Debug.Assert(_IsInitialized);
    ///     }
    /// }
    /// </code>
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
    /// In conclusion, a properly implemented Enumeration type should ideally look something like this:
    /// <code>
    /// sealed class MyEnum : Enumeration&lt;MyEnum&gt;
    /// {
    ///     private MyEnum() { }
    ///     
    ///     public static readonly MyEnum Foo = new MyEnum();
    ///     public static readonly MyEnum Bar = new MyEnum();
    ///     public static readonly MyEnum Bas = new MyEnum();
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
    /// enumeration, complicated validation can sometimes be required since C# provides no guarantee an Enum has a valid value belonging
    /// to this fixed set, and no elegant and performant way of verifying it either.
    /// <para/>
    /// System.Enum is an old C++ legacy that should never have been "ported over to" (hacked into) a strongly typed language,
    /// something that the designers of C# themselves said was one of their biggest mistakes in designing the language.
    /// Conceptually Enumerations and Flags are different and should have been two different types:
    /// Whereas an enumeration value is always a single plain value, a flag represents a set of values - possibly the empty set.
    /// </remarks>
    /// <typeparam name="TEnumeration">
    /// A sealed user defined enumeration type that inherits directly from this abstract class (or directly from the <see cref="Enumeration{TEnumeration, TEnum}"/> class).
    /// </typeparam>
    /// <seealso cref="Enumeration"/>
    /// <seealso cref="Enumeration{TEnumeration, TEnum}"/>
    public class Enumeration<TEnumeration> // TODO: do some more tests and finally proofread all doc & code one last time.
        : Enumeration, IEquatable<TEnumeration>, IEquatable<Enumeration<TEnumeration>>
        where TEnumeration : Enumeration<TEnumeration>
    {
        /// <summary>
        /// Initializes the Ordinal and Name* of this enumeration value.
        /// </summary><remarks>
        /// Ordinal gets determined here, and will be assigned such that it corresponds to the textual order in which the enumeration values are declared.<br/>
        /// (See comments in the Enumeration&lt;TEnumeration&gt; source code for more on how and why this works, along with relevant ECMA specification quotes.)
        /// <para/>
        /// *Name is late initialized:
        /// Names for all instances are initialized during the constructor call for the initialization of the last (public static) TEnumeration field.
        /// </remarks>
        /// <exception cref="Exception">
        /// Thrown if this operation causes the number of created instances to exceed the number of public static <typeparamref name="TEnumeration"/> fields
        /// declared in the <typeparamref name="TEnumeration"/> class. (If running a DEBUG build this will also trigger an assertion failure.)
        /// </exception>
        /// <seealso cref="_IsInitialized"/>
        protected Enumeration()
            : base(counter)
        {
            if (counter == values.Length)
            {
                Debug.Assert(false, ERR_COUNTER);
                throw new Exception(ERR_COUNTER);
            }

            TEnumeration val = this;
            values[counter] = val;

            if (++counter == values.Length)
                InitializeInfoAndNames(val);
        }

        /// <summary>
        /// Override this method in case you want to create a non-public Enumeration type for use in a low-trust or other restricted environment. See Remarks.
        /// </summary><remarks>
        /// <br/>The code inside this method should be:
        /// <code>
        /// return (TEnumeration)field.GetValue(null);
        /// </code>
        /// Even though this is the exact same code as in the default implementation of this method, it still needs to be overridden as a member of the
        /// <typeparamref name="TEnumeration"/> class to ensure that the GetValue call is made with sufficient access rights to get the value of the field.
        /// </remarks>
        protected virtual TEnumeration GetStaticFieldValue(FieldInfo field)
            => (TEnumeration)field.GetValue(null);

        /// <summary>
        /// Next Enumeration value, in Ordinal order, or null if this is the last value.
        /// </summary><remarks><note type="inheritinfo">
        /// <b>Note To Inheritors:</b><br/>
        /// If called during type initialization the next value might not be instantiated yet,
        /// in which case this property can incorrectly return null even if this isn't the last value.
        /// </note></remarks>
        new public TEnumeration Next
        {
            get
            {
                int next = Ordinal + 1;
                return next == values.Length ? null : values[next];
            }
        }

        /// <summary>
        /// Previous Enumeration value, in Ordinal order, or null if this is the first value.
        /// </summary>
        new public TEnumeration Prev
            => Ordinal == 0 ? null : values[Ordinal - 1];

        /// <summary>
        /// EnumInfo instance that provides information about the Enumeration values of this Enumeration type.
        /// </summary><remarks><note type="inheritinfo">
        /// <b>Note To Inheritors:</b><br/>
        /// This property is initially null. It is late initialized - meaning that only when type initialization has reached the point where all the
        /// public static <typeparamref name="TEnumeration"/> fields inside the <typeparamref name="TEnumeration"/> class have been initialized will
        /// this property be initialized.
        /// </note></remarks>
        new public IEnumValues<TEnumeration> Values
            => ev_instance;

        /// <summary>
        /// Number of enumeration values declared for this enumeration type.
        /// </summary><remarks>
        /// This is the same as the number of public static <typeparamref name="TEnumeration"/> fields declared in the <typeparamref name="TEnumeration"/> class.
        /// <br/>
        /// <note type="inheritinfo">
        /// <b>Note To Inheritors:</b><br/>
        /// This property is safe to use during type initialization. It can thus be used in those special cases where <see cref="GetValues"/> would
        /// throw and <see cref="Values"/> would return null. (See Note To Inheritors in the Remarks section of each of those properties respectively.)
        /// </note>
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static int Count
            => values.Length;

        /// <summary>
        /// <see cref="IEnumValues{TEnumeration}"/> instance that provides information about the Enumeration values of this Enumeration type.
        /// </summary><remarks>
        /// The leading underscore in the name of this property might be slightly confusing, but it is there to ensure that this property doesn't
        /// mingle in with the public static <typeparamref name="TEnumeration"/> values declared in the <typeparamref name="TEnumeration"/> class.
        /// <note type="inheritinfo">
        /// <b>Note To Inheritors:</b><br/>
        /// This property requires all <typeparamref name="TEnumeration"/> fields inside the <typeparamref name="TEnumeration"/> class to have finished
        /// initializing before it can return them, so if those fields are not initialized it will attempt to trigger their initialization.
        /// However if this call executes during type initialization for <typeparamref name="TEnumeration"/>, either from the same thread or from another
        /// thread with a circular initialization dependency*, then the attempt to perform initialization will return without having any effect.**
        /// In such cases at least one of these fields could be observed as null***. Since this proves that <typeparamref name="TEnumeration"/> is
        /// incorrectly implemented / in violation of the contract for types inheriting from Enumeration an Exception will be thrown****.
        /// <br/><br/><i>
        /// *If called from another thread, and assuming no circular dependency, the thread will simply block until initialization has finished, see
        /// ECMA-335 (CIL Specification), part II, section 10.5.3.3.
        /// <br/>
        /// **Because type initialization is only allowed to occur once, see ECMA-335 (CIL Specification), part II, section 10.5.3.
        /// <br/>
        /// ***Any value observed is guaranteed to at least be default initialized, i.e. null, see ECMA-334 (C# Specification), section 17.4.5.
        /// <br/>
        ///****This exception will be swallowed by the type initializer and a TypeInitializationException will be thrown on access later, however if
        /// running a debug build an Assertion message will also be displayed (prior to throwing) to help expose the flawed Enumeration implementation.
        /// </i><br/><br/>
        /// To avoid the risks mentioned above, all code that can execute during type initialization for the <typeparamref name="TEnumeration"/> class,
        /// specifically before or during initialization of the public static <typeparamref name="TEnumeration"/> fields*, should avoid calling any code
        /// outside the class if at all possible. To call outside code during initialization is to risk accidentally introducing a circular initialization
        /// dependency.
        /// <br/><br/><i>
        /// *The code locations in question would be: the code inside the <typeparamref name="TEnumeration"/> instance constructors,
        /// expressions called to provide arguments for those instance constructors, static field initializers that textually precede the last of the
        /// public static fields of <typeparamref name="TEnumeration"/> type, as well as all code called from instance field initializers (because
        /// instance field initializers are executed immediately before the call to the base class constructor, see ECMA-334 (C# Specification),
        /// section 17.10.2).
        /// </i><br/><br/>
        /// Note that this method is safe to call from inside a static constructor in <typeparamref name="TEnumeration"/> assuming that all the
        /// <typeparamref name="TEnumeration"/> fields inside the <typeparamref name="TEnumeration"/> class have already been initialized.
        /// See also <see cref="_IsInitialized"/>.
        /// </note>
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static IEnumValues<TEnumeration> GetValues()
        {
            TriggerInitialization();
            return ev_instance;
        }

        /// <summary>
        /// Indicates whether this enumeration value is equal to another enumeration value.
        /// </summary><returns>
        /// True if these enumeration values are reference equal; otherwise false.
        /// </returns>
        /// <param name="other">An enumeration value to compare against.</param>
        public bool Equals(TEnumeration other)
            => ReferenceEquals(this, other);

        /// <inheritdoc cref="Equals(TEnumeration)"/>
        bool IEquatable<Enumeration<TEnumeration>>.Equals(Enumeration<TEnumeration> other)
            => ReferenceEquals(this, other);

        /// <summary>
        /// Provides implicit downcasting.
        /// </summary>
        public static implicit operator TEnumeration(Enumeration<TEnumeration> value)
        {
            // An explicit cast is required here! (Otherwise it becomes circular / recursive and ends up causing a StackOverflow.)
            return (TEnumeration)value;
        }

        // -----

        /// <summary>
        /// Indicates whether the names (as accessed from the <see cref="Enumeration.Name"/> property) for this enumeration type have been initialized yet.
        /// </summary><remarks>
        /// The names are late-initialized (when the last of the enumeration values declared in the enumeration type is instantiated).
        /// Thus the name is not guaranteed to be initialized yet if accessed from an instance constructor of the user defined enumeration type.
        /// (The Name property will return null before it is initialized.)<br/>
        /// However all names are guaranteed to be initialized before entering a static constructor - assuming no field initializer was accidentally omitted.
        /// The easiest way to verify that all fields have an initializer is thus for the user defined enumeration type to assert this property in a static constructor.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected internal static bool _IsInitialized
            => ev_instance != null;

        // -----

        private static readonly TEnumeration[] values; // <-- early initialized.
        private static FieldInfo[] fields; // <-- early initialized, late de-initialized.
        private static int counter; // <-- this grows during type initialization, until it reaches values.Length.
        private static EnumValues<TEnumeration> ev_instance; // <-- late initialized (or early initialized if the Enumeration is empty).

        static Enumeration()
        {
            var t = typeof(TEnumeration);
            var f = t.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly);
            int len = f.Length;
            if (len == 0)
            {
                values = Empty<TEnumeration>.Array;
                ev_instance = new EnumValues<TEnumeration>(values);
            }
            else
            {
                values = new TEnumeration[len];
                fields = f;
            }

            // Verify requirement: Direct inheritance & Sealed.
            var tBase = t.BaseType;
            bool baseIsValid = tBase == typeof(Enumeration<TEnumeration>) || tBase.IsGenericType && tBase.GetGenericTypeDefinition() == typeof(Enumeration<,>);

            if (!baseIsValid)
            {
                Debug.Assert(false, ErrPrefix + ERR_BASE);
                throw new Exception(ErrPrefix + ERR_BASE);
            }
            if (!t.IsSealed)
            {
                Debug.Assert(false, ErrPrefix + ERR_SEALED);
                throw new Exception(ErrPrefix + ERR_SEALED);
            }

            //VerifyCctorsPrivate(t); // scraping this as a requirement because it's not technically needed (even though it is VERY HIGHLY recommended!)
        }

        /*
        [Conditional("DEBUG")] // Debug only: For Release counting instantiations is sufficient.
        private static void VerifyCctorsPrivate(Type t)
        {
            AZAssert.NotNullInternal(t, nameof(t));

            foreach (var cctor in t.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                Debug.Assert(cctor.IsPrivate, ERR_CTOR);
        }
        */

        // this is NOT a copy! this does NOT trigger full initialization!
        internal static TEnumeration[] ValArrayInternal
            => values;

        // this is NOT a copy!
        internal sealed override Enumeration[] GetValuesInternal(bool triggerInitialization)
        {
            if (triggerInitialization)
                TriggerInitialization();
            return values;
        }

        internal sealed override IEnumValues EVInternal
            => ev_instance;

        internal virtual TEnumeration TryParse<TEnum>(TEnum enumValue, bool allowConversion = false)
            where TEnum : struct, IConvertible
        {
            return null;
        }

        // lastVal is the instance that is calling InitializeNames (from its ctor). (lastVal == values[values.Length - 1])
        private static void InitializeInfoAndNames(TEnumeration lastVal)
        {
            AZAssert.NotNullInternal(lastVal, nameof(lastVal));
            AZAssert.NotNullInternal(fields, nameof(fields));

            string err = null;
            var t = typeof(TEnumeration);
            FieldInfo[] fieldArray = fields;
            for (int i = 0; i < fieldArray.Length; ++i)
            {
                var f = fieldArray[i];
                if (!f.IsInitOnly || f.FieldType != t)
                {
                    err = ErrPrefix + "." + f.Name + ERR_FIELD_TYPE;
                    break;
                }

                var val = lastVal.GetStaticFieldValue(f);
                if (val == null) // we expect exactly one field to be null: the last field in textual order (i.e. the field that corresponds to the instance that is calling this method).
                {
                    val = lastVal; // if this happens more than once it means that not all fields have initializers, but then TryInitializeName will fail below, so it will get caught.
                }

                if (!val.TryInitializeName(f.Name)) // TryInitializeName fails if name is already set, and since values.Length == fields.Length this ensures uniqueness!
                {
                    err = ErrPrefix + "." + f.Name + ERR_FIELD_VALUE;
                    break;
                }
            }

            if (err != null)
            {
                Debug.Assert(false, err);
                throw new Exception(err);
            }

            fields = null; // fields array not needed anymore.

            ev_instance = new EnumValues<TEnumeration>(values); // info init.
            // late initialization completed successfully.
        }

        private static void TriggerInitialization()
        {
            if (_IsInitialized)
                return;

            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(TEnumeration).TypeHandle);

            if (_IsInitialized)
                return;

            // ...might as well give it a last desperate shot before throwing in the towel.
            System.Threading.Thread.MemoryBarrier();

            if (_IsInitialized)
                return;

            // This code should be unreachable if all field initialization requirements are met...
            if (values[values.Length - 1] == null)
            {
                // At least one field lacks initializer OR is initialized to the value of another field OR is initialized to null ...
                // ... OR this method was called during type initialization (in which case there exists a circular initialization dependency).
                Debug.Assert(false, ErrPrefix + ERR_FIELD_VALUE);
                throw new Exception(ErrPrefix + ERR_FIELD_VALUE);
            }
            else // This should never ever happen!
            {
                Debug.Assert(false, ErrPrefix + ERR_UNKNOWN);
                throw new Exception(ErrPrefix + ERR_UNKNOWN);
            }
        }

        // Simple assembly name + simple type name. For prefixing error messages.
        internal static string ErrPrefix
        {
            get
            {
                var t = typeof(TEnumeration);
                return t.Assembly.GetName().Name + ">" + t.Name;
            }
        }

        internal const string
            ERR_BASE = ": Generic type-parameter TEnumeration must derive directly from Enumeration<TEnumeration> or Enumeration<TEnumeration, TEnum>.",
            ERR_SEALED = ": Generic type-parameter TEnumeration must be a sealed class.",
            ERR_TENUM = ": Generic type-parameter TEnum must be a System.Enum type.",
            ERR_CTOR = ": All TEnumeration constructors must be private.",
            ERR_FIELD_TYPE = ": All public static fields of TEnumeration must be readonly and of type TEnumeration.",
            ERR_FIELD_VALUE = ": All public static fields of TEnumeration must have a unique non-null instance assigned during type initialization. (Alternatively caused by a cyclic initialization dependency.)",
            ERR_COUNTER = ": The required number of instances for this enumeration type has already been created. No further instantiation is allowed!",
            ERR_ENUM_MISSING = ": For each TEnumeration value / field there must exist a TEnum value with exactly the same name.",
            ERR_ENUM_UNIQUE = ": Every TEnum Value paired to an TEnumeration value must be (numerically) unique among all TEnum values paired to that specific TEnumeration type.",
            ERR_UNKNOWN = ": Enumeration initialization failed for mysterious unknown reasons. (Good luck!)";
    }
}
#pragma warning restore CS0660, CS0661
