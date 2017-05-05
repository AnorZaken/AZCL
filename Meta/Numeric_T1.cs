using System;

namespace AZCL.Meta
{
    /// <summary>
    /// Static meta class that holds a <see cref="NumericInfo"/> instance for type <typeparamref name="T"/>.
    /// </summary>
    public static class Numeric<T>
    {
        /// <summary>
        /// Numeric info about type <typeparamref name="T"/>.
        /// </summary>
        public static readonly NumericInfo info = new NumericInfo(Type.GetTypeCode(typeof(T)));
    }
}
