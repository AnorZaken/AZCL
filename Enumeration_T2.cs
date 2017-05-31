using System;
using System.Diagnostics;
using System.Reflection;

namespace AZCL
{
    /* An example implementation could look like this:

        public sealed class ExampleEnum : Enumeration<ExampleEnum, ExampleEnum.Const>, IEnumerationMarker
        {
            private ExampleEnum()
		    { }

            public static readonly ExampleEnum Foo = new ExampleEnum();
            public static readonly ExampleEnum Bar = new ExampleEnum();
            public static readonly ExampleEnum Bas = new ExampleEnum();

            public enum Const
            {
                Foo,
                Bar,
                Bas,
            }
        }
    */
#pragma warning disable CS0660, CS0661
    public abstract class Enumeration<TStrongEnumeration, TEnum> // TODO: doc!
        : Enumeration, IEquatable<TStrongEnumeration>, IEquatable<Enumeration<TStrongEnumeration, TEnum>>
        where TStrongEnumeration : Enumeration<TStrongEnumeration, TEnum>, IEnumerationMarker
        where TEnum : struct
    {
        protected Enumeration()
        {
            if (--_Internal<TStrongEnumeration>.initCountDown == 0)
                __Late__Init((TStrongEnumeration)this);
        }

        public static int Count
        {
            get { return _Internal<TStrongEnumeration>.values.Length; }
        }

        public TEnum Value
        {
            get { return value; }
        }

        public bool Equals(TStrongEnumeration other)
        {
            return ReferenceEquals(this, other);
        }
        public bool Equals(Enumeration<TStrongEnumeration, TEnum> other)
        {
            return ReferenceEquals(this, other);
        }

        public static implicit operator Enumeration<TStrongEnumeration, TEnum>(TStrongEnumeration derived)
        {
            return derived;
        }

        public static bool operator ==(Enumeration<TStrongEnumeration, TEnum> left, Enumeration<TStrongEnumeration, TEnum> right)
        {
            return ReferenceEquals(left, right);
        }
        public static bool operator !=(Enumeration<TStrongEnumeration, TEnum> left, Enumeration<TStrongEnumeration, TEnum> right)
        {
            return !ReferenceEquals(left, right);
        }

        private TEnum value;

#if DEBUG
        const string // TODO: improve this stuff...
            THIS = nameof(Enumeration<TStrongEnumeration, TEnum>), // "Enumeration"
            ERR1 = THIS + ": Generic type-parameter TStrongEnumeration must derive directly from " + THIS + ".",
            ERR2 = THIS + ": Generic type-parameter TStrongEnumeration must be a sealed class.",
            ERR3 = THIS + ": Generic type-parameter TEnum must be a System.Enum type.",
            ERR4 = THIS + ": All TStrongEnumeration constructors must be private.",
            ERR5 = THIS + ": All public static fields of TStrongEnumeration must be readonly and of type TStrongEnumeration.",
            ERR6 = THIS + ": All public static fields of TStrongEnumeration must have a unique non-null instance assigned during type initialization.",
            ERR7 = THIS + ": For each public static TStrongEnumeration field there must exist a TEnum field/value with exactly the same name.";
#endif

        static Enumeration()
        {
#if DEBUG
            var tStrong = typeof(TStrongEnumeration);
            Debug.Assert(tStrong.BaseType == typeof(Enumeration<TStrongEnumeration, TEnum>), ERR1);
            Debug.Assert(tStrong.IsSealed, ERR2);
            Debug.Assert(typeof(TEnum).IsEnum, ERR3);
            foreach (var ctor in tStrong.GetConstructors())
                Debug.Assert(ctor.IsPrivate, ERR4);
#endif
            FieldInfo[] fields = typeof(TStrongEnumeration).GetFields(BindingFlags.Static | BindingFlags.Public);
            _Internal<TStrongEnumeration>.fields = fields;
            _Internal<TStrongEnumeration>.initCountDown = fields.Length;
            _Internal<TStrongEnumeration>.values = new TStrongEnumeration[fields.Length];
        }

        private static void __Late__Init(TStrongEnumeration missingPiece)
        {
            var fields = _Internal<TStrongEnumeration>.fields;
            var values = _Internal<TStrongEnumeration>.values;
            var tStrong = typeof(TStrongEnumeration);
            var tWeak = typeof(TEnum);

            for (int i = 0; i < fields.Length; ++i)
            {
                var instance = __Field__Init(fields[i], i, ref missingPiece, tStrong, tWeak);
#if DEBUG
                Debug.Assert(Array.IndexOf(values, instance, 0, i) == -1, ERR6);
#endif
                values[i] = instance;
            }

            _Internal<TStrongEnumeration>.fields = null;
        }

        private static TStrongEnumeration __Field__Init(FieldInfo field, int ordinal, ref TStrongEnumeration missingPiece, Type tStrong, Type tEnum)
        {
#if DEBUG
            Debug.Assert(field.IsInitOnly && field.FieldType == tStrong, ERR5);
#endif
            var obj = field.GetValue(null);
            TStrongEnumeration instance;
            if (obj == null)
            {
                instance = missingPiece;
#if DEBUG
                Debug.Assert(instance != null, ERR6);
#endif
                missingPiece = null;
            }
            else
            {
                instance = (TStrongEnumeration)obj;
            }
            instance.ordinal = ordinal;
            instance.name = field.Name;

            // TryParse doesn't exist in .net 3.5 :(
            //bool bParse = Enum.TryParse(instance.name, out instance.value);
#if DEBUG
            //Debug.Assert(bParse, ERR7);
            try
            {
                instance.value = (TEnum)Enum.Parse(tEnum, instance.name);
            }
            catch
            {
                Debug.Assert(false, ERR7);
            }
#else
            instance.value = (TEnum)Enum.Parse(tEnum, instance.name);
#endif
            return instance;
        }
    }
#pragma warning restore CS0660, CS0661
}
