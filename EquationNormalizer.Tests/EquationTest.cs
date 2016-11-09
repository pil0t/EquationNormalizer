using System.Collections.Generic;
using NUnit.Framework;

namespace EquationNormalizer.Tests
{
    public class EquationTest
    {
        [Test]
        public void TestEquationNormalization()
        {
            var equation = new Equation()
                           {
                               Left = new List<Summand>() { new Summand(1,new Variable('x'))},
                               Right = new List<Summand>() { new Summand(1,new Variable('y'))}

                           };
            var normalized = equation.Normalize();
            Assert.That(normalized.Left.Count, Is.EqualTo(2));
            Assert.That(normalized.ToString(), Is.EqualTo("x-y = 0"));


            var equation2 = new Equation()
                            {
                                Left = new List<Summand>() { new Summand(1,new Variable('x'))},
                                Right = new List<Summand>() { new Summand(-1,new Variable('x'))}

                            };
            var normalized2 = equation2.Normalize();
            Assert.That(normalized2.Left.Count, Is.EqualTo(1));
            Assert.That(normalized2.ToString(), Is.EqualTo("2x = 0"));
        }
    }
}