using NUnit.Framework;

namespace EquationNormalizer.Tests
{
    public class VariableTests
    {
        [Test]
        public void DisplayVariableTest()
        {
            var var1 = new Variable('a', 2);
            Assert.That(var1.ToString(), Is.EqualTo("a^2"));

            var var2 = new Variable('b', 123);
            Assert.That(var2.ToString(), Is.EqualTo("b^123"));
        }

        [Test]
        public void VariableCompareTest()
        {
            var varA = new Variable('a', 42);
            var varA2 = new Variable('a', 42);
            var varB = new Variable('b', 42);
            Assert.That(varA, Is.Not.EqualTo(varB));
            Assert.That(varA, Is.EqualTo(varA2));

            Assert.That(varA == varA2);
            Assert.That(varA != varB);
        }
    }
}