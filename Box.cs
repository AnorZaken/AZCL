
namespace AZCL
{
    /// <summary>
    /// A simple generic boxing class.
    /// </summary>
	public class Box<T>
	{
        /// <summary>
        /// The value stored in the box.
        /// </summary>
        public T value;
        
        /// <summary>
        /// Creates a new Box (with the default value of <typeparamref name="T"/>).
        /// </summary>
        public Box()
        { }

        /// <summary>
        /// Creates a new Box containing <paramref name="value"/>.
        /// </summary>
        public Box(T value)
        {
            this.value = value;
        }
	}
}
