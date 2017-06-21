using System.ComponentModel;

namespace AZCL
{
    /// <summary>
    /// Extensions for use on Char values.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class CharExtensions
    {
        /// <summary>
        /// True if the char value is less than or equal to '\u007f'.
        /// </summary>
        public static bool IsAscii(this char c)
            => c <= '\u007f';

        /// <summary>
        /// True if the char value is less than or equal to '\u00ff'.
        /// </summary><remarks>
        /// Also known as ASCII + Latin-1 Supplement.
        /// </remarks>
        public static bool IsLatin1(this char c)
            => c <= '\u00ff';

        /// <summary>
        /// True if the char is a whitespace in the ASCII + Latin-1 Supplement character set.
        /// </summary><remarks>
        /// More specifically the following characters are considered whitespace:
        /// <br/>ASCII:
        /// <br/>U+0009 = &lt;Cc&gt; HORIZONTAL TAB
        /// <br/>U+000a = &lt;Cc&gt; LINE FEED
        /// <br/>U+000b = &lt;Cc&gt; VERTICAL TAB
        /// <br/>U+000c = &lt;Cc&gt; FORM FEED
        /// <br/>U+000d = &lt;Cc&gt; CARRIAGE RETURN
        /// <br/>U+0020 = &lt;Zs&gt; SPACE
        /// <br/>Latin1:
        /// <br/>U+0085 = &lt;Cc&gt; NEXT LINE
        /// <br/>U+00a0 = &lt;Zs&gt; NO-BREAK SPACE
        /// </remarks>
        public static bool IsLatin1WhiteSpace(this char c)
            => IsLatin1(c) && (c == '\u0020' || (c >= '\u0009' && c <= '\u000d') || c == '\u0085' || c == '\u00a0');
    }
}
