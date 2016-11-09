using NUnit.Framework;

namespace EquationNormalizer.Tests
{
    public class NormalizationIntegrationTest
    {
        [TestCase("x=y","x-y=0")]
        [TestCase("x^2 + y=x^2","y=0")]
        [TestCase("xy=-yx+x^2","-x^2+2xy=0")]
        [TestCase("xy=-yx+x^2","-x^2+2xy=0")]
        [TestCase("y + 2 + 7 + a + b=b+a+x + 2 + 3 + 4","y-x=0")]
        [TestCase("0=0","0=0")]
        [TestCase("x+y+z=x+y+z + 2","-2=0")]
        [TestCase("-(x+y)=x-y", "-2x = 0")]
        [TestCase("(2-(x+y))=x-y", "-2x +2= 0")]
        public void TestNormalization(string source, string expected)
        {
            var normalized = Parser.Parse(source).Normalize().ToString();

            var expectedTrim = expected.RemoveWhitespace();
            var normalizedTrim = normalized.RemoveWhitespace();
            Assert.That(normalizedTrim, Is.EqualTo(expectedTrim));
        }
    }
}