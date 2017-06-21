using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AZCL.Collections
{
    /// <summary>
    /// An EqualityComparer for testing reference equality.
    /// </summary><remarks>
    /// This implementation is primarily intended for .Net4.0+ (where it becomes universal thanks to contravariance in generics).
    /// </remarks>
    /// <seealso cref="T:AZCL.Collections.ReferenceEqualityComparer{T}"/>
    public sealed class ReferenceEqualityComparer : IEqualityComparer, IEqualityComparer<object>
    {
        /// <summary>
        /// The default (singleton) instance.
        /// </summary>
        public static readonly ReferenceEqualityComparer Default = new ReferenceEqualityComparer();

        private ReferenceEqualityComparer()
        { }

        /// <inheritdoc/>
        public new bool Equals(object x, object y)
        {
            return x == y; // This is reference equality! (See language spec for equals operator.)
        }

        /// <inheritdoc/>
        public int GetHashCode(object obj)
            => RuntimeHelpers.GetHashCode(obj);
    }

    /// <summary>
    /// An EqualityComparer for testing reference equality.
    /// </summary><remarks>
    /// This implementation is primarily intended for .Net3.5 or earlier (before the IEqualityComparer&lt;T&gt; interface had contravariance).
    /// </remarks>
    /// <seealso cref="T:AZCL.Collections.ReferenceEqualityComparer"/>
    public sealed class ReferenceEqualityComparer<T> : IEqualityComparer, IEqualityComparer<T>
        where T : class
    {
        /// <summary>
        /// The default (singleton) instance.
        /// </summary>
        public static readonly ReferenceEqualityComparer<T> Default = new ReferenceEqualityComparer<T>();

        private ReferenceEqualityComparer()
        { }

        bool IEqualityComparer<T>.Equals(T x, T y)
            => Equals(x, y);
        
        int IEqualityComparer<T>.GetHashCode(T obj)
            => RuntimeHelpers.GetHashCode(obj);
        
        /// <inheritdoc/>
        public new bool Equals(object x, object y)
        {
            return x == y; // This is reference equality! (See language spec for equals operator.)
        }

        /// <inheritdoc/>
        public int GetHashCode(object obj)
            => RuntimeHelpers.GetHashCode(obj);
    }
}
