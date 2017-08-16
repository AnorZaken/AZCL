
// Copyright 2005-2014 Daniel James.
// Distributed under the Boost Software License, Version 1.0.
// (See accompanying file BOOST_LICENSE_1_0.txt or copy at
// http://www.boost.org/LICENSE_1_0.txt)

//  Based on Peter Dimov's proposal
//  http://www.open-std.org/JTC1/SC22/WG21/docs/papers/2005/n1756.pdf
//  issue 6.18.


/*  <ABOVE IS THE RELEVANT FILE HEADER AND COPYRIGHT FROM "boost/functional/hash/hash.hpp"> */

// "hash_combine_impl" translated from C++ to C# by Nicklas Damgren 2017.

//  This file falls under the "Boost Software License - Version 1.0 - August 17th, 2003"
//  since translations from one language to another is considered a Derivative Work.

/*
*  Original C++ code from "boost/functional/hash/hash.hpp" :
*
*   template <typename SizeT>
*   inline void hash_combine_impl(SizeT& seed, SizeT value)
*   {
*       seed ^= value + 0x9e3779b9 + (seed<<6) + (seed>>2);
*   }
*/

//  The "hash_combine_impl" method is not in any way related to (nor a derivative work
//  of) the public domain MurmurHash3 code mentioned in "boost/functional/hash/hash.hpp".
//  Regardless, here is the Murmurhash3 header:

// MurmurHash3 was written by Austin Appleby, and is placed in the public
// domain. The author hereby disclaims copyright to this source code.

using System.ComponentModel;

namespace AZCL.Bits
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static partial class HashExtensions // TODO: public when ready
    {
        private const int GOLDEN = unchecked((int)0x9e3779b9); // (from golden ratio)

        /// <summary>
        /// Combines one hash value with another.
        /// </summary>
        /// <param name="hash1">The first hash.</param>
        /// <param name="hash2">A second hash.</param>
        /// <remarks>
        /// Call repeatedly to incrementally create a hash from several other hashes.
        /// <note type="note">
        /// The order of combination is important!<br/>
        /// <c>a.HashCombine(b)</c> does not generally produce the same result as <c>b.HashCombine(a)</c>.
        /// </note>
        /// <note type="security">Not suitable for cryptography / security.</note>
        /// </remarks>
        /// <returns>
        /// The combined hash value.
        /// </returns>
        public static int HashCombine(this int hash1, int hash2)
            => hash1 ^ (hash2 + GOLDEN + (hash1 << 6) + (hash1 >> 2));
    }
}
