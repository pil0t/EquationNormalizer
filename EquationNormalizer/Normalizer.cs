using System.Collections.Generic;
using System.Linq;

namespace EquationNormalizer
{
    public class Normalizer
    {
        public static List<Summand> Normalize(List<Summand> source)
        {
            var newSummands = new List<Summand>();
            foreach (var summandGroup in source.GroupBy(x => x.Variables, new EnumerableComparer<Variable>()))
            {
                newSummands.Add(new Summand(summandGroup.Sum(s => s.K), summandGroup.Key.ToArray()));
            }

            return newSummands;
        }
    }
}