using System;

namespace AZCL.Bits
{
    /// <summary>
    /// Represents an Int32 hash value and provides convenience methods for hashing.
    /// </summary><remarks>
    /// None of the methods and operators of this type are cryptographically safe.
    /// They should NOT be used for any security related purpose!
    /// </remarks>
    internal struct Hash : IEquatable<Hash>, IEquatable<int> // TODO: public when ready
    {
        /// <summary>
        /// The hash value.
        /// </summary>
        public readonly int value;

        /// <summary>
        /// Instantiates a Hash with the specified value.
        /// </summary>
        /// <param name="hash">The hash value.</param>
        public Hash(int hash)
        {
            this.value = hash;
        }

        // ---

        /// <summary>
        /// Implicitly returns the hash value.
        /// </summary>
        public static implicit operator int(Hash hash)
            => hash.value;

        /// <summary>
        /// Implicitly creates a Hash instance from an integer value.
        /// </summary>
        public static implicit operator Hash(int hash)
            => new Hash(hash);

        // ---

        public static int Combine(int hash1, int hash2)
            => hash1.HashCombine(hash2);

        public static Hash Combine(int hash1, Hash hash2)
            => new Hash(hash1.HashCombine(hash2));

        public static Hash Combine(Hash hash1, int hash2)
            => new Hash(hash1.value.HashCombine(hash2));

        public static Hash Combine(Hash hash1, Hash hash2)
            => new Hash(hash1.value.HashCombine(hash2));

        // ---

        public static int Combine(int hash1, int hash2, int hash3)
            => hash1.HashCombine(hash2).HashCombine(hash3);

        public static Hash Combine(int hash1, int hash2, Hash hash3)
            => new Hash(hash1.HashCombine(hash2).HashCombine(hash3));

        public static Hash Combine(int hash1, Hash hash2, int hash3)
            => new Hash(hash1.HashCombine(hash2).HashCombine(hash3));

        public static Hash Combine(int hash1, Hash hash2, Hash hash3)
            => new Hash(hash1.HashCombine(hash2).HashCombine(hash3));

        public static Hash Combine(Hash hash1, int hash2, int hash3)
            => new Hash(hash1.value.HashCombine(hash2).HashCombine(hash3));

        public static Hash Combine(Hash hash1, int hash2, Hash hash3)
            => new Hash(hash1.value.HashCombine(hash2).HashCombine(hash3));

        public static Hash Combine(Hash hash1, Hash hash2, int hash3)
            => new Hash(hash1.value.HashCombine(hash2).HashCombine(hash3));

        public static Hash Combine(Hash hash1, Hash hash2, Hash hash3)
            => new Hash(hash1.value.HashCombine(hash2).HashCombine(hash3));

        // ---

        public static int Combine(int hash1, int hash2, int hash3, int hash4)
            => hash1.HashCombine(hash2).HashCombine(hash3).HashCombine(hash4);

        public static Hash Combine(int hash1, int hash2, int hash3, Hash hash4)
            => new Hash(hash1.HashCombine(hash2).HashCombine(hash3).HashCombine(hash4));

        public static Hash Combine(int hash1, int hash2, Hash hash3, int hash4)
            => new Hash(hash1.HashCombine(hash2).HashCombine(hash3).HashCombine(hash4));

        public static Hash Combine(int hash1, int hash2, Hash hash3, Hash hash4)
            => new Hash(hash1.HashCombine(hash2).HashCombine(hash3).HashCombine(hash4));

        public static Hash Combine(int hash1, Hash hash2, int hash3, int hash4)
            => new Hash(hash1.HashCombine(hash2).HashCombine(hash3).HashCombine(hash4));

        public static Hash Combine(int hash1, Hash hash2, int hash3, Hash hash4)
            => new Hash(hash1.HashCombine(hash2).HashCombine(hash3).HashCombine(hash4));

        public static Hash Combine(int hash1, Hash hash2, Hash hash3, int hash4)
            => new Hash(hash1.HashCombine(hash2).HashCombine(hash3).HashCombine(hash4));

        public static Hash Combine(int hash1, Hash hash2, Hash hash3, Hash hash4)
            => new Hash(hash1.HashCombine(hash2).HashCombine(hash3).HashCombine(hash4));

        public static Hash Combine(Hash hash1, int hash2, int hash3, int hash4)
            => new Hash(hash1.value.HashCombine(hash2).HashCombine(hash3).HashCombine(hash4));

        public static Hash Combine(Hash hash1, int hash2, int hash3, Hash hash4)
            => new Hash(hash1.value.HashCombine(hash2).HashCombine(hash3).HashCombine(hash4));

        public static Hash Combine(Hash hash1, int hash2, Hash hash3, int hash4)
            => new Hash(hash1.value.HashCombine(hash2).HashCombine(hash3).HashCombine(hash4));

        public static Hash Combine(Hash hash1, int hash2, Hash hash3, Hash hash4)
            => new Hash(hash1.value.HashCombine(hash2).HashCombine(hash3).HashCombine(hash4));

        public static Hash Combine(Hash hash1, Hash hash2, int hash3, int hash4)
            => new Hash(hash1.value.HashCombine(hash2).HashCombine(hash3).HashCombine(hash4));

        public static Hash Combine(Hash hash1, Hash hash2, int hash3, Hash hash4)
            => new Hash(hash1.value.HashCombine(hash2).HashCombine(hash3).HashCombine(hash4));

        public static Hash Combine(Hash hash1, Hash hash2, Hash hash3, int hash4)
            => new Hash(hash1.value.HashCombine(hash2).HashCombine(hash3).HashCombine(hash4));

        public static Hash Combine(Hash hash1, Hash hash2, Hash hash3, Hash hash4)
            => new Hash(hash1.value.HashCombine(hash2).HashCombine(hash3).HashCombine(hash4));

        // ---

        public static int CombineMany(int hash, params int[] hashes)
        {
            if (hashes == null)
                throw new ArgumentNullException(nameof(hashes));

            int i = 0;
            for (; i + 8 <= hashes.Length; i += 8) // unrolling 8 at a time
            {
                hash = hash // taking advantage of the fact that we are unrolling a power of 2:
                    .HashCombine(hashes[i | 0])
                    .HashCombine(hashes[i | 1])
                    .HashCombine(hashes[i | 2])
                    .HashCombine(hashes[i | 3])
                    .HashCombine(hashes[i | 4])
                    .HashCombine(hashes[i | 5])
                    .HashCombine(hashes[i | 6])
                    .HashCombine(hashes[i | 7])
                    ;
            }
            for (; i < hashes.Length; ++i)
                hash = hash.HashCombine(hashes[i]);

            return hash;
        }

        public static Hash CombineMany(params Hash[] hashes)
        {
            if (hashes == null)
                throw new ArgumentNullException(nameof(hashes));

            if (hashes.Length == 0) // or throw?
                return default(Hash);

            int i = 0, hash = 0;
            for (; i + 8 <= hashes.Length; i += 8) // unrolling 8 at a time
            {
                hash = hash // taking advantage of the fact that we are unrolling a power of 2:
                    .HashCombine(hashes[i | 0].value)
                    .HashCombine(hashes[i | 1].value)
                    .HashCombine(hashes[i | 2].value)
                    .HashCombine(hashes[i | 3].value)
                    .HashCombine(hashes[i | 4].value)
                    .HashCombine(hashes[i | 5].value)
                    .HashCombine(hashes[i | 6].value)
                    .HashCombine(hashes[i | 7].value)
                    ;
            }
            for (; i < hashes.Length; ++i)
                hash = hash.HashCombine(hashes[i].value);

            return new Hash(hash);
        }

        public static Hash CombineMany(Hash hash, params int[] hashes)
            => new Hash(CombineMany(hash.value, hashes));

        // ---

        public static Hash operator ^(int hash1, Hash hash2)
            => Combine(hash1, hash2);
        public static Hash operator ^(Hash hash1, int hash2)
            => Combine(hash1, hash2);
        public static Hash operator ^(Hash hash1, Hash hash2)
            => Combine(hash1, hash2);

        public override bool Equals(object obj)
            => obj is Hash ? Equals((Hash)obj):
                obj is int ? Equals((int)obj):
                 false;

        public bool Equals(Hash other)
            => this.value == other.value;

        public bool Equals(int other)
            => value == other;

        public override int GetHashCode()
            => value;

        public override string ToString()
            => value.ToStrHex(true);
    }
}
