
namespace AZCL
{
    // Shared error strings used in exceptions.
    internal static class ERR
    {
        // this is just to keep it in an easy to find place for now, it should be made into resource strings eventually.
        internal const string

            POPULATE_INNER = "Inner array can't be populated because it's null.",

            CONVERT_INNER = "Inner array can't be converted because it's null.",

            CLEAR_INNER = "Inner array can't be cleared because it's null.",

            CREATE_INNER = "One or more inner array already exist.",

            EXCLUDE_INNER = "Inner array found at the specified index(es) is null.",

            SOURCE_EMPTY = "The source sequence is empty.",


            AZCL_INTERNAL_ERROR = "Seems AZCL encountered an internal error, sorry about that :(\n" // <-- 61 char (+newline)
            + "Please report it on AZCL's GitHub: https://github.com/AnorZaken/AZCL/issues"; // <-- 76 char (<80)

    }
}
