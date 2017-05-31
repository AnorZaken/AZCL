using AZCL.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AZCL
{
    public abstract class Enumeration // TODO: doc!
    {
        /* This constraint: "where TEnumeration : Enumeration, IEnumerationMarker"; thanks to Enumeration ctor being internal,
         * Enumeration<T1,T2> requiring T1 to implement IEnumerationMarker, and Enumeration<,> type initialization asserting
         * direct inheritance; guarantees that TEnumeration will be a user declared type inheriting from Enumeration<,>. */
        internal static class _Internal<TEnumeration> where TEnumeration : Enumeration, IEnumerationMarker
        {
            internal static TEnumeration[] values;
            internal static FieldInfo[] fields;
            internal static int initCountDown;
        }

        public static int Count<TEnumeration>() where TEnumeration : Enumeration, IEnumerationMarker
        {
            return _Internal<TEnumeration>.values.Length;
        }

        public static void Values<TEnumeration>(out TEnumeration[] values) where TEnumeration : Enumeration, IEnumerationMarker
        {
            var vsource = _Internal<TEnumeration>.values;
            var vcopy = new TEnumeration[vsource.Length];
            Array.Copy(vsource, vcopy, vsource.Length);
            values = vcopy;
        }

        public static void Values<TEnumeration>(out ReadOnlyArray<TEnumeration> values) where TEnumeration : Enumeration, IEnumerationMarker
        {
            values = _Internal<TEnumeration>.values;
        }

        public static IEnumerable<TEnumeration> Values<TEnumeration>() where TEnumeration : Enumeration, IEnumerationMarker
        {
            var values = _Internal<TEnumeration>.values;
            for (int i = 0; i < values.Length; ++i)
                yield return values[i];
        }

        internal static void Touch<TEnumeration>() where TEnumeration : Enumeration, IEnumerationMarker
        {
            var fields = _Internal<TEnumeration>.fields;
            if (fields != null)
                foreach (var f in fields)
                    f.GetValue(null);
        }

        public static bool IsInitialized<TEnumeration>() where TEnumeration : Enumeration, IEnumerationMarker
        {
            return _Internal<TEnumeration>.values != null && _Internal<TEnumeration>.fields == null;
        }

        public static bool TryInitialize<TEnumeration>() where TEnumeration : Enumeration, IEnumerationMarker
        {
            if (IsInitialized<TEnumeration>())
                return true;

            Touch<TEnumeration>();
            return IsInitialized<TEnumeration>();
        }

        /// <summary>
        /// The ordinal of this (strong) enumeration value.
        /// </summary><remarks>
        /// IMPORTANT:<br/>
        /// The ordinal IS NOT guaranteed to be the same between executions!
        /// Do not use this value in serialization or any other context where it might persist beyond the execution of the program!
        /// <br/>
        /// However at runtime the ordinal IS guaranteed to be fixed, unique among the ordinals of enumerations values of the same type,
        /// and part of a the zero-based continuous range 0 to (Count - 1).
        /// (The Count property is static and exists on all types derived from Enumeration.)
        /// <para/>
        /// For serialization purposes it is best to use the <see cref="Name"/> property (or simply <see cref="ToString()"/>).
        /// </remarks>
        public int Ordinal
        {
            get { return ordinal; }
        }

        /// <summary>
        /// Name of this enumeration value.
        /// </summary><remarks>
        /// Guaranteed to be unique among the names of enumeration values of the same type. (Suitable for serialization.)
        /// </remarks>
        public string Name
        {
            get { return name; }
        }

        public sealed override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }

        public sealed override int GetHashCode()
        {
            return ordinal;
        }

        /// <summary>
        /// Returns the name of this enumeration value.
        /// </summary>
        public sealed override string ToString()
        {
            return Name;
        }

        // Must be internal: This ensures that no external library can inherit directly from this type.
        internal Enumeration()
        { }

        internal int ordinal;
        internal string name;
    }
}
