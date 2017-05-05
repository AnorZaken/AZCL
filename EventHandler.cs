using System;

namespace AZCL
{
    /* Rather than depending on AZCL, consider copy-pasting this type into your project.
     * Thanks to contravariance there shouldn't be any issues (with .net3.0 and up).
     * >> Don't forget to change the namespace if you copy! << */

    /// <summary>
    /// Represents a method that handles general events.
    /// </summary><remarks>
    /// See https://msdn.microsoft.com/en-us/library/windows/apps/br225997.aspx and
    /// http://stackoverflow.com/questions/1046016/event-signature-in-net-using-a-strong-typed-sender
    /// </remarks>
    /// <param name="sender">The event source.</param>
    /// <param name="e">The event data. If there is no event data, this parameter might be null.</param>
    [Serializable]
    public delegate void EventHandler<TSender, TEventArgs>(
        TSender sender, TEventArgs e
    )
#if ENFORCE_EVENTARGS_CONSTRAINT
    where TEventArgs : EventArgs
#endif
        ;
}