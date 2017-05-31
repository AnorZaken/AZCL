
namespace AZCL.Collections
{
    // Shared error strings used in exceptions.
    internal static class ERR
    {
        internal const string

            AZCL_INTERNAL_ERROR = AZCL.ERR.AZCL_INTERNAL_ERROR,

            CURRENT_INVALID = "Enumerator's current position is before the first or past the last element to iterate.",
            
            START_PLUS_LENGTH = "Starting index + length exceeds the length of the array argument.",

            ARRAY_ARG_ABSENT = "Array argument / backing field is absent.",

            PARAMS_CONTAINS_NULL = "One or more of the (params-)parameters are null.",
            
            BACKING_ARRAY_ABSENT = "Backing array is absent.";
        
    }
}
