
namespace AZCL.Collections
{
    // Shared error strings used in exceptions.
    internal static partial class ERR
    {
        internal const string

            AZCL_INTERNAL_ERROR = "Seems AZCL encountered an internal error, sorry about that :(\n" // <-- 61 char (+newline)
            + "Please report it on AZCL's GitHub: https://github.com/AnorZaken/AZCL/issues", // <-- 76 char (<80)

            CURRENT_INVALID = "Enumerator's current position is before the first or past the last element to iterate.",
            
            START_PLUS_LENGTH = "Starting index + length exceeds the length of the array argument.",

            ARRAY_ARG_ABSENT = "Array argument is absent / backing field is null.",
            
            BACKING_ARRAY_ABSENT = "Backing array is absent.";

    }
}
