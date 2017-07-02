using System.ComponentModel;

/* TODO:
 * The Combine formula is taken from the C++ Boost Library (2011),
 * Need to read through the license details, but probably need to include a Boost copyright and license.
 * (The license is open for all use, but requires that copyright and a copy of the license is included.)
 */

namespace AZCL.Bits
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class HashExtensions // TODO: public when ready
    {
        private const int GOLDEN = unchecked((int)0x9e3779b9);

        public static int HashCombine(this int hash, int hash2)
            => hash ^ (hash2 + GOLDEN + (hash << 6) + (hash >> 2));
    }
}
