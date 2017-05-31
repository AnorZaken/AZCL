﻿using System;
using System.Collections.Generic;
using SCG = System.Collections.Generic;

namespace AZCL.Collections
{
    public abstract class Comparer<T> : SCG.Comparer<T> //TODO: doc
    {
        /* Order: primary, secondary, tertiary, quaternary, quinary, senary, septenary, octonary, nonary, and denary. */
        /* ********************************************************************************************************** */
        
        /// <summary>
        /// Composes two IComparer&lt;T&gt;, for use in sorting on a primary and secondary criteria.
        /// </summary><returns>
        /// A System.Collections.Generic.Comparer&lt;T&gt; instance, composed from the IComparer&lt;T&gt; parameters in order.
        /// </returns>
        /// <param name="primary">The primary sorting criteria implemented as an IComparer&lt;T&gt;.</param>
        /// <param name="secondary">The secondary sorting criteria implemented as an IComparer&lt;T&gt;.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        public static SCG.Comparer<T> Compose(IComparer<T> primary, IComparer<T> secondary)
        {
            if (primary == null)
                throw new ArgumentNullException(nameof(primary));
            if (secondary == null)
                throw new ArgumentNullException(nameof(secondary));

            return new Comparer2<T>(primary, secondary);
        }

        /// <summary>
        /// Composes three IComparer&lt;T&gt;, for use in sorting on a primary, secondary, and tertiary criteria.
        /// </summary><returns>
        /// A System.Collections.Generic.Comparer&lt;T&gt; instance, composed from the IComparer&lt;T&gt; parameters in order.
        /// </returns>
        /// <param name="primary">The primary sorting criteria implemented as an IComparer&lt;T&gt;.</param>
        /// <param name="secondary">The secondary sorting criteria implemented as an IComparer&lt;T&gt;.</param>
        /// <param name="tertiary">The tertiary* sorting criteria implemented as an IComparer&lt;T&gt;. <i>(*Third)</i></param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        public static SCG.Comparer<T> Compose(IComparer<T> primary, IComparer<T> secondary, IComparer<T> tertiary)
        {
            if (primary == null)
                throw new ArgumentNullException(nameof(primary));
            if (secondary == null)
                throw new ArgumentNullException(nameof(secondary));
            if (tertiary == null)
                throw new ArgumentNullException(nameof(tertiary));

            return new Comparer3<T>(primary, secondary, tertiary);
        }

        /// <summary>
        /// Composes four IComparer&lt;T&gt;, for use in sorting on a primary, secondary, tertiary, and quaternary criteria.
        /// </summary><returns>
        /// A System.Collections.Generic.Comparer&lt;T&gt; instance, composed from the IComparer&lt;T&gt; parameters in order.
        /// </returns>
        /// <param name="primary">The primary sorting criteria implemented as an IComparer&lt;T&gt;.</param>
        /// <param name="secondary">The secondary sorting criteria implemented as an IComparer&lt;T&gt;.</param>
        /// <param name="tertiary">The tertiary* sorting criteria implemented as an IComparer&lt;T&gt;. <i>(*Third)</i></param>
        /// <param name="quaternary">The quaternary* sorting criteria implemented as an IComparer&lt;T&gt;. <i>(*Fourth)</i></param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        public static SCG.Comparer<T> Compose(IComparer<T> primary, IComparer<T> secondary, IComparer<T> tertiary, IComparer<T> quaternary)
        {
            if (primary == null)
                throw new ArgumentNullException(nameof(primary));
            if (secondary == null)
                throw new ArgumentNullException(nameof(secondary));
            if (tertiary == null)
                throw new ArgumentNullException(nameof(tertiary));
            if (quaternary == null)
                throw new ArgumentNullException(nameof(quaternary));

            return new Comparer4<T>(primary, secondary, tertiary, quaternary);
        }

        /// <summary>
        /// Composes five IComparer&lt;T&gt;, for use in sorting on five ordered criteria.
        /// </summary><returns>
        /// A System.Collections.Generic.Comparer&lt;T&gt; instance, composed from the IComparer&lt;T&gt; parameters in order.
        /// </returns>
        /// <param name="primary">The primary sorting criteria implemented as an IComparer&lt;T&gt;.</param>
        /// <param name="secondary">The secondary sorting criteria implemented as an IComparer&lt;T&gt;.</param>
        /// <param name="tertiary">The tertiary* sorting criteria implemented as an IComparer&lt;T&gt;. <i>(*Third)</i></param>
        /// <param name="quaternary">The quaternary* sorting criteria implemented as an IComparer&lt;T&gt;. <i>(*Fourth)</i></param>
        /// <param name="quinary">The quinary* sorting criteria implemented as an IComparer&lt;T&gt;. <i>(*Fifth)</i></param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null.
        /// </exception>
        public static SCG.Comparer<T> Compose(IComparer<T> primary, IComparer<T> secondary, IComparer<T> tertiary, IComparer<T> quaternary, IComparer<T> quinary)
        {
            if (primary == null)
                throw new ArgumentNullException(nameof(primary));
            if (secondary == null)
                throw new ArgumentNullException(nameof(secondary));
            if (tertiary == null)
                throw new ArgumentNullException(nameof(tertiary));
            if (quaternary == null)
                throw new ArgumentNullException(nameof(quaternary));
            if (quinary == null)
                throw new ArgumentNullException(nameof(quinary));

            return new Comparer5<T>(primary, secondary, tertiary, quaternary, quinary);
        }

        /// <summary>
        /// Composes multiple IComparer&lt;T&gt;, for use in sorting on multiple ordered criteria.
        /// </summary><remarks>
        /// If no arguments are given (or an empty array is given) the result will be a comparer that always returns zero.
        /// Dubious usefulness aside, it is theoretically sound and thus not sufficient cause for raising an exception.
        /// </remarks><returns>
        /// A System.Collections.Generic.Comparer&lt;T&gt; instance, composed from the IComparer&lt;T&gt; parameters in order.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments / any value found inside the params-array are null; or if a null array is given as argument.
        /// </exception>
        public static SCG.Comparer<T> Compose(params IComparer<T>[] comparers)
        {
            if (comparers == null)
                throw new ArgumentNullException(nameof(comparers));

            int len = comparers.Length;
            if (len <= 5)
                switch (len)
                {
                    case 2:
                        return Compose(comparers[0], comparers[1]);
                    case 3:
                        return Compose(comparers[0], comparers[1], comparers[2]);
                    case 4:
                        return Compose(comparers[0], comparers[1], comparers[2], comparers[3]);
                    case 5:
                        return Compose(comparers[0], comparers[1], comparers[2], comparers[3], comparers[4]);
                }

            if (ArrayHelper.ExistsNull(comparers))
                throw new ArgumentNullException(paramName: nameof(comparers), message: ERR.PARAMS_CONTAINS_NULL);


            return new ComparerN<T>(comparers);
        }
    }

    internal sealed class Comparer2<T> : SCG.Comparer<T>
    {
        private readonly IComparer<T> primary, secondary;

        // assumes non-null references!!
        internal Comparer2(IComparer<T> primary, IComparer<T> secondary)
        {
            this.primary = primary;
            this.secondary = secondary;
        }

        public sealed override int Compare(T x, T y)
        {
            int r = primary.Compare(x, y);
            return r == 0 ? secondary.Compare(x, y) : r;
        }
    }

    internal sealed class Comparer3<T> : SCG.Comparer<T>
    {
        private readonly IComparer<T> primary, secondary, tertiary;

        // assumes non-null references!!
        internal Comparer3(IComparer<T> primary, IComparer<T> secondary, IComparer<T> tertiary)
        {
            this.primary = primary;
            this.secondary = secondary;
            this.tertiary = tertiary;
        }

        public sealed override int Compare(T x, T y)
        {
            int r = primary.Compare(x, y);
            if (r == 0)
            {
                r = secondary.Compare(x, y);
                if (r == 0)
                {
                    r = tertiary.Compare(x, y);
                }
            }
            return r;
        }
    }

    internal sealed class Comparer4<T> : SCG.Comparer<T>
    {
        private readonly IComparer<T> primary, secondary, tertiary, quaternary;

        // assumes non-null references!!
        internal Comparer4(IComparer<T> primary, IComparer<T> secondary, IComparer<T> tertiary, IComparer<T> quaternary)
        {
            this.primary = primary;
            this.secondary = secondary;
            this.tertiary = tertiary;
            this.quaternary = quaternary;
        }

        public sealed override int Compare(T x, T y)
        {
            int r = primary.Compare(x, y);
            if (r == 0)
            {
                r = secondary.Compare(x, y);
                if (r == 0)
                {
                    r = tertiary.Compare(x, y);
                    if (r == 0)
                    {
                        r = quaternary.Compare(x, y);
                    }
                }
            }
            return r;
        }
    }

    internal sealed class Comparer5<T> : SCG.Comparer<T>
    {
        private readonly IComparer<T> primary, secondary, tertiary, quaternary, quinary;

        // assumes non-null references!!
        internal Comparer5(IComparer<T> primary, IComparer<T> secondary, IComparer<T> tertiary, IComparer<T> quaternary, IComparer<T> quinary)
        {
            this.primary = primary;
            this.secondary = secondary;
            this.tertiary = tertiary;
            this.quaternary = quaternary;
            this.quinary = quinary;
        }

        public sealed override int Compare(T x, T y)
        {
            int r = primary.Compare(x, y);
            if (r == 0)
            {
                r = secondary.Compare(x, y);
                if (r == 0)
                {
                    r = tertiary.Compare(x, y);
                    if (r == 0)
                    {
                        r = quaternary.Compare(x, y);
                        if (r == 0)
                        {
                            r = quinary.Compare(x, y);
                        }
                    }
                }
            }
            return r;
        }
    }

    internal sealed class ComparerN<T> : SCG.Comparer<T>
    {
        private readonly IComparer<T>[] comparers;

        // assumes non-null array with non-null elements!!
        internal ComparerN(IComparer<T>[] comparers)
        {
            this.comparers = comparers;
        }

        public sealed override int Compare(T x, T y)
        {
            for (int i = 0; i < comparers.Length; ++i)
            {
                int r = comparers[i].Compare(x, y);
                if (r != 0)
                    return r;
            }
            return 0;
        }
    }
}
