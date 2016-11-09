using NUnit.Framework;

namespace EquationNormalizer.Tests
{
    public class SummandTest
    {
        [Test]
        public void SummandDisplayTest()
        {
            var summand1 = new Summand(1);
            Assert.That(summand1.ToString(), Is.EqualTo("1"));

            var summand2 = new Summand(1, new Variable('x'));
            Assert.That(summand2.ToString(), Is.EqualTo("x"));

            var summand3 = new Summand(1, new Variable('x', 2));
            Assert.That(summand3.ToString(), Is.EqualTo("x^2"));

            var summand4 = new Summand(-1, new Variable('x', 2));
            Assert.That(summand4.ToString(), Is.EqualTo("-x^2"));

            var summand5 = new Summand(-2, new Variable('x', 2));
            Assert.That(summand5.ToString(), Is.EqualTo("-2x^2"));
        }

        [Test]
        public void SummandNormalizeTest()
        {
            var summand1 = new Summand(1, new Variable('x'), new Variable('x'));
            var normalized = summand1.Normalize();
            Assert.That(normalized.ToString(), Is.EqualTo("x^2"));

            var summand2 = new Summand(1, new Variable('x'), new Variable('x'), new Variable('y'), new Variable('y'), new Variable('x'));
            var normalized2 = summand2.Normalize();
            Assert.That(normalized2.ToString(), Is.EqualTo("x^3y^2"));
        }
    }
}