
namespace AZCL
{
    /// <summary>
    /// Determines how much capacity to add when growing a collection,
    /// and when to and by how much to reduce capacity when shrinking a collection.
    /// </summary>
    public enum ResizeBehavior
    {
        /// <summary>
        /// Allow roughly 50 percent spill when resizing.
        /// <para/>(Minimum capacity of a non-empty collection is 8)
        /// </summary>
        Balanced = 0,

        /// <summary>
        /// Allow roughly 100 percent spill when resizing.
        /// <para/>(Minimum capacity of a non-empty array is 32)
        /// </summary>
        Spacious,

        /// <summary>
        /// Keep the collection as small as possible (i.e. Capacity == Count).
        /// <para/>(Minimum capacity of a non-empty collection is 1)
        /// </summary>
        Trimmed,
    }
}
