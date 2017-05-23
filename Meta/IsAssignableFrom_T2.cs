
namespace AZCL.Meta
{
    internal static class IsAssignableFrom<TFrom, TTo> // TODO: make doc then make public.
    {
        public static readonly bool value = typeof(TTo).IsAssignableFrom(typeof(TFrom));
    }
}
