
namespace AZCL.Meta
{
    /// <summary>
    /// Static meta class that indicates whether <typeparamref name="TTarget"/> is assignable from <typeparamref name="TFrom"/>.
    /// </summary>
    public static class IsAssignableFrom<TTarget, TFrom>
    {
        /// <summary>
        /// Indicates whether <typeparamref name="TTarget"/> is assignable from <typeparamref name="TFrom"/>
        /// </summary>
        public static readonly bool value = Evaluate.IsAssignableFrom<TTarget, TFrom>();
    }
}
