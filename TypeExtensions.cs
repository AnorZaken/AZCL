using System;

namespace AZCL
{
    /// <summary>
    /// Extensions for use on System.Type instances.
    /// </summary>
    public static class TypeExtensions
    {
        // cached generic type definition
        private static readonly object typeof_Nullable = typeof(Nullable<>);

        /// <summary>
        /// Returns true if the System.Type is a Nullable&lt;&gt; type.
        /// </summary><remarks>
        /// Also returns true if the type is the generic type definition for Nullable&lt;&gt;,
        /// i.e. the result of the expression: <code>typeof(Nullable&lt;&gt;)</code>
        /// </remarks>
        public static bool IsNullable(this Type t)
        {
            return t.IsGenericType && Object.ReferenceEquals(typeof_Nullable, t.IsGenericTypeDefinition ? t : t.GetGenericTypeDefinition());
        }
    }
}
