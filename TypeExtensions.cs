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
            return t.IsGenericType && t.GetGenericTypeDefinition() == tdef_Nullable;
        }

        /// <summary>
        /// Gets the type of the current object, or the declared type of the object if the object is null.
        /// </summary><remarks>
        /// This is the same as calling GetType() on the object when it isn't null,
        /// and the closest you can get to the correct type information when it is.
        /// </remarks><returns>
        /// The runtime type of the current object, or the declared type (i.e. compile-time type) of the object if the object is null.
        /// </returns>
        /// <typeparam name="T">Declared type of the object.</typeparam>
        /// <param name="obj">The object to get the type for.</param>
        public static Type GetTypeOrDeclared<T>(this T obj)
        {
            if (obj == null)
                return typeof(T);
            else
                return obj.GetType();
        }
    }
}
