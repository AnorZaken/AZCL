using System;

namespace AZCL
{
    // IndexFinderEquatable and IndexFinderNonEquatable code is identical but the Equals-calls will resolve differently!
    internal sealed class IndexFinderNonEquatable<T> : IIndexFinder<T>
    {
        public int IndexOf(T[,] array, ref T value)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int lenx = array.LengthX();
            int leny = array.LengthY();
            if (value == null)
            {
                for (int x = 0; x < lenx; ++x)
                    for (int y = 0; y < leny; ++y)
                        if (array[x, y] == null)
                            return x * leny + y;
            }
            else
            {
                for (int x = 0; x < lenx; ++x)
                    for (int y = 0; y < leny; ++y)
                        if ((array[x, y]?.Equals(value)).GetValueOrDefault())
                            return x * leny + y;
            }
            return -1;
        }

        public int IndexOf(T[,,] array, ref T value)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int lenx = array.LengthX();
            int leny = array.LengthY();
            int lenz = array.LengthZ();
            if (value == null)
            {
                for (int x = 0; x < lenx; ++x)
                    for (int y = 0; y < leny; ++y)
                        for (int z = 0; z < lenz; ++z)
                            if (array[x, y, z] == null)
                                return (x * leny + y) * lenz + z;
            }
            else
            {
                for (int x = 0; x < lenx; ++x)
                    for (int y = 0; y < leny; ++y)
                        for (int z = 0; z < lenz; ++z)
                            if ((array[x, y, z]?.Equals(value)).GetValueOrDefault())
                                return (x * leny + y) * lenz + z;
            }
            return -1;
        }

        public int IndexOf(T[,] array, ref T value, int startIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)startIndex > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));

            if (startIndex == array.Length)
                return -1;

            int lenx = array.LengthX();
            int leny = array.LengthY();
            int x = startIndex / leny; // Fast DivRem (div-part)
            int y = startIndex - x * leny; // Fast DivRem (mod-part)

            if (value == null)
            {
                do
                {
                    do
                    {
                        if (array[x, y] == null)
                            return x * leny + y;
                    }
                    while (++y < leny);
                    y = 0; // <-- reset after the above loop so the original y-argument is used exactly once.
                }
                while (++x < lenx);
            }
            else
            {
                do
                {
                    do
                    {
                        if ((array[x, y]?.Equals(value)).GetValueOrDefault())
                            return x * leny + y;
                    }
                    while (++y < leny);
                    y = 0; // <-- reset after the above loop so the original y-argument is used exactly once.
                }
                while (++x < lenx);
            }

            return -1;
        }

        public int IndexOf(T[,,] array, ref T value, int startIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)startIndex > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));

            if (startIndex == array.Length)
                return -1;

            int lenx = array.LengthX();
            int leny = array.LengthY();
            int lenz = array.LengthZ();
            int lenm = leny * lenz;
            int x = startIndex / lenm; // Fast DivRem (div-part 1)
            int m = startIndex - x * lenm; // Fast DivRem (mod-part 1)
            int y = m / lenz; // Fast DivRem (div-part 2)
            int z = m - y * lenz;  // Fast DivRem (mod-part 2)

            if (value == null)
            {
                do
                {
                    do
                    {
                        do
                        {
                            if (array[x, y, z] == null)
                                return x * lenm + y * leny + z;
                        }
                        while (++z < lenz);
                        z = 0; // <-- reset after the above loop so the original z-argument is used exactly once.
                    }
                    while (++y < leny);
                    y = 0; // <-- reset after the above loop so the original y-argument is used exactly once.
                }
                while (++x < lenx);
            }
            else
            {
                do
                {
                    do
                    {
                        do
                        {
                            if ((array[x, y, z]?.Equals(value)).GetValueOrDefault())
                                return x * lenm + y * leny + z;
                        }
                        while (++z < lenz);
                        z = 0; // <-- reset after the above loop so the original z-argument is used exactly once.
                    }
                    while (++y < leny);
                    y = 0; // <-- reset after the above loop so the original y-argument is used exactly once.
                }
                while (++x < lenx);
            }

            return -1;
        }

        public int IndexOf(T[,] array, ref T value, int startIndex, int count)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)startIndex > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (unchecked((uint)count > (uint)(array.Length - startIndex)))
                throw new ArgumentOutOfRangeException(nameof(count));

            if (count == 0)
                return -1;

            int lenx = array.LengthX();
            int leny = array.LengthY();
            int x = startIndex / leny; // Fast DivRem (div-part)
            int y = startIndex - x * leny; // Fast DivRem (mod-part)

            if (value == null)
            {
                do
                {
                    do
                    {
                        if (array[x, y] == null)
                            return x * leny + y;
                        if (--count == 0)
                            return -1;
                    }
                    while (++y < leny);
                    y = 0; // <-- reset after the above loop so the original y-argument is used exactly once.
                }
                while (++x < lenx);
            }
            else
            {
                do
                {
                    do
                    {
                        if ((array[x, y]?.Equals(value)).GetValueOrDefault())
                            return x * leny + y;
                        if (--count == 0)
                            return -1;
                    }
                    while (++y < leny);
                    y = 0; // <-- reset after the above loop so the original y-argument is used exactly once.
                }
                while (++x < lenx);
            }

            return -1;
        }

        public int IndexOf(T[,,] array, ref T value, int startIndex, int count)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)startIndex > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (unchecked((uint)count > (uint)(array.Length - startIndex)))
                throw new ArgumentOutOfRangeException(nameof(count));

            if (count == 0)
                return -1;

            int lenx = array.LengthX();
            int leny = array.LengthY();
            int lenz = array.LengthZ();
            int lenm = leny * lenz;
            int x = startIndex / lenm; // Fast DivRem (div-part 1)
            int m = startIndex - x * lenm; // Fast DivRem (mod-part 1)
            int y = m / lenz; // Fast DivRem (div-part 2)
            int z = m - y * lenz;  // Fast DivRem (mod-part 2)

            if (value == null)
            {
                do
                {
                    do
                    {
                        do
                        {
                            if (array[x, y, z] == null)
                                return x * lenm + y * leny + z;
                            if (--count == 0)
                                return -1;
                        }
                        while (++z < lenz);
                        z = 0; // <-- reset after the above loop so the original z-argument is used exactly once.
                    }
                    while (++y < leny);
                    y = 0; // <-- reset after the above loop so the original y-argument is used exactly once.
                }
                while (++x < lenx);
            }
            else
            {
                do
                {
                    do
                    {
                        do
                        {
                            if ((array[x, y, z]?.Equals(value)).GetValueOrDefault())
                                return x * lenm + y * leny + z;
                            if (--count == 0)
                                return -1;
                        }
                        while (++z < lenz);
                        z = 0; // <-- reset after the above loop so the original z-argument is used exactly once.
                    }
                    while (++y < leny);
                    y = 0; // <-- reset after the above loop so the original y-argument is used exactly once.
                }
                while (++x < lenx);
            }

            return -1;
        }

        public int LastIndexOf(T[,] array, ref T value)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int leny = array.LengthY();
            if (value == null)
            {
                for (int x = array.LengthX() - 1; x >= 0; --x)
                    for (int y = leny - 1; y >= 0; --y)
                        if (array[x, y] == null)
                            return x * leny + y;
            }
            else
            {
                for (int x = array.LengthX() - 1; x >= 0; --x)
                    for (int y = leny - 1; y >= 0; --y)
                        if ((array[x, y]?.Equals(value)).GetValueOrDefault())
                            return x * leny + y;
            }

            return -1;
        }

        public int LastIndexOf(T[,,] array, ref T value)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            int leny = array.LengthY();
            int lenz = array.LengthZ();
            if (value == null)
            {
                for (int x = array.LengthX() - 1; x >= 0; --x)
                    for (int y = leny - 1; y >= 0; --y)
                        for (int z = lenz - 1; z >= 0; --z)
                            if (array[x, y, z] == null)
                                return (x * leny + y) * lenz + z;
            }
            else
            {
                for (int x = array.LengthX() - 1; x >= 0; --x)
                    for (int y = leny - 1; y >= 0; --y)
                        for (int z = lenz - 1; z >= 0; --z)
                            if ((array[x, y, z]?.Equals(value)).GetValueOrDefault())
                                return (x * leny + y) * lenz + z;
            }

            return -1;
        }

        public int LastIndexOf(T[,] array, ref T value, int startIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)startIndex > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));

            if (startIndex == array.Length)
                return -1;

            int leny = array.LengthY();
            int x = startIndex / leny; // Fast DivRem (div-part)
            int y = startIndex - x * leny; // Fast DivRem (mod-part)

            if (value == null)
            {
                do
                {
                    do
                    {
                        if (array[x, y] == null)
                            return x * leny + y;
                    }
                    while (--y >= 0);
                    y += leny; // <-- reset after the above loop so the original y-argument is used exactly once.
                }
                while (--x >= 0);
            }
            else
            {
                do
                {
                    do
                    {
                        if ((array[x, y]?.Equals(value)).GetValueOrDefault())
                            return x * leny + y;
                    }
                    while (--y >= 0);
                    y += leny; // <-- reset after the above loop so the original y-argument is used exactly once.
                }
                while (--x >= 0);
            }

            return -1;
        }

        public int LastIndexOf(T[,,] array, ref T value, int startIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)startIndex > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));

            if (startIndex == array.Length)
                return -1;

            int leny = array.LengthY();
            int lenz = array.LengthZ();
            int lenm = leny * lenz;
            int x = startIndex / lenm; // Fast DivRem (div-part 1)
            int m = startIndex - x * lenm; // Fast DivRem (mod-part 1)
            int y = m / lenz; // Fast DivRem (div-part 2)
            int z = m - y * lenz;  // Fast DivRem (mod-part 2)

            if (value == null)
            {
                do
                {
                    do
                    {
                        do
                        {
                            if (array[x, y, z] == null)
                                return x * leny + y;
                        }
                        while (--z >= 0);
                        z += lenz; // <-- reset after the above loop so the original y-argument is used exactly once.
                    }
                    while (--y >= 0);
                    y += leny; // <-- reset after the above loop so the original y-argument is used exactly once.
                }
                while (--x >= 0);
            }
            else
            {
                do
                {
                    do
                    {
                        do
                        {
                            if ((array[x, y, z]?.Equals(value)).GetValueOrDefault())
                                return x * leny + y;
                        }
                        while (--z >= 0);
                        z += lenz; // <-- reset after the above loop so the original y-argument is used exactly once.
                    }
                    while (--y >= 0);
                    y += leny; // <-- reset after the above loop so the original y-argument is used exactly once.
                }
                while (--x >= 0);
            }

            return -1;
        }

        public int LastIndexOf(T[,] array, ref T value, int startIndex, int count)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)startIndex > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (unchecked((uint)count > (uint)(array.Length - startIndex)))
                throw new ArgumentOutOfRangeException(nameof(count));

            if (count == 0)
                return -1;

            int leny = array.LengthY();
            int x = startIndex / leny; // Fast DivRem (div-part)
            int y = startIndex - x * leny; // Fast DivRem (mod-part)

            if (value == null)
            {
                do
                {
                    do
                    {
                        if (array[x, y] == null)
                            return x * leny + y;
                        if (--count == 0)
                            return -1;
                    }
                    while (--y >= 0);
                    y += leny; // <-- reset after the above loop so the original y-argument is used exactly once.
                }
                while (--x >= 0);
            }
            else
            {
                do
                {
                    do
                    {
                        if ((array[x, y]?.Equals(value)).GetValueOrDefault())
                            return x * leny + y;
                        if (--count == 0)
                            return -1;
                    }
                    while (--y >= 0);
                    y += leny; // <-- reset after the above loop so the original y-argument is used exactly once.
                }
                while (--x >= 0);
            }

            return -1;
        }

        public int LastIndexOf(T[,,] array, ref T value, int startIndex, int count)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (unchecked((uint)startIndex > (uint)array.Length))
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (unchecked((uint)count > (uint)(array.Length - startIndex)))
                throw new ArgumentOutOfRangeException(nameof(count));

            if (count == 0)
                return -1;

            int leny = array.LengthY();
            int lenz = array.LengthZ();
            int lenm = leny * lenz;
            int x = startIndex / lenm; // Fast DivRem (div-part 1)
            int m = startIndex - x * lenm; // Fast DivRem (mod-part 1)
            int y = m / lenz; // Fast DivRem (div-part 2)
            int z = m - y * lenz;  // Fast DivRem (mod-part 2)

            if (value == null)
            {
                do
                {
                    do
                    {
                        do
                        {
                            if (array[x, y, z] == null)
                                return x * leny + y;
                            if (--count == 0)
                                return -1;
                        }
                        while (--z >= 0);
                        z += lenz; // <-- reset after the above loop so the original y-argument is used exactly once.
                    }
                    while (--y >= 0);
                    y += leny; // <-- reset after the above loop so the original y-argument is used exactly once.
                }
                while (--x >= 0);
            }
            else
            {
                do
                {
                    do
                    {
                        do
                        {
                            if ((array[x, y, z]?.Equals(value)).GetValueOrDefault())
                                return x * leny + y;
                            if (--count == 0)
                                return -1;
                        }
                        while (--z >= 0);
                        z += lenz; // <-- reset after the above loop so the original y-argument is used exactly once.
                    }
                    while (--y >= 0);
                    y += leny; // <-- reset after the above loop so the original y-argument is used exactly once.
                }
                while (--x >= 0);
            }

            return -1;
        }
    }
}
