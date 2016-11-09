using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace EquationNormalizer.Tests
{
    public class NormalizerTest
    {
        [Test]
        public void NormalizeTest()
        {
            var summandList = new List<Summand>()
                              {
                                  new Summand(2, new Variable('x')),
                                  new Summand(3, new Variable('x')),
                                  new Summand(4, new Variable('y'))
                              };

            var normalized = Normalizer.Normalize(summandList);

            Assert.That(normalized.Count, Is.EqualTo(2));
            Assert.That(normalized.Any(s => s.K == 5 && s.Variables.All(v => v.Name == 'x')));
            Assert.That(normalized.Any(s => s.K == 4 && s.Variables.All(v => v.Name == 'y')));
        }
    }
}