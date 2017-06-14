
namespace AZCL.Meta
{
    internal static class IsEnumCompatible<T> // TODO: doc and public
    {
        public static readonly bool value = Evaluate.IsEnumCompatible<T>();
    }
}
