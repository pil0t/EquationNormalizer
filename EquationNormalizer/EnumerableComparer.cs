using System.Collections.Generic;
using System.Linq;

namespace EquationNormalizer
{
    class EnumerableComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode(IEnumerable<T> obj)
        {
            return obj.Aggregate(string.Empty, (s, i) => s + i.GetHashCode(), s => s.GetHashCode());
        }
    }
}