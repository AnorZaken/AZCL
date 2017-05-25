using System;

namespace AZCL
{
    /// <summary>
    /// Extensions for use on System.Type instances.
    /// </summary>
    public static class TypeExtensions
    {
        // cached generic type definition
        private static readonly object tdef_Nullable = typeof(Nullable<>);

        /// <summary>
        /// Returns whether the System.Type is a Nullable&lt;?&gt; type.
        /// </summary><returns>
        /// True if the type is a Nullable&lt;X&gt; for some other type X (or the generic type
        /// definition of Nullable&lt;&gt;); otherwise false.
        /// </returns>
        public static bool IsNullable(this Type t)
        {
            return t.IsGenericType && ReferenceEquals(tdef_Nullable, t.GetGenericTypeDefinition());
        }
    }
}
