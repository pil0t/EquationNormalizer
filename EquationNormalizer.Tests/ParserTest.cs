using System.Linq;
using NUnit.Framework;

namespace EquationNormalizer.Tests
{
    public class ParserTest
    {
        [Test]
        public void TestParserThrowsOnInvalid()
        {
            Assert.Throws<ParseException>(() => Parser.Parse("x=y=z"));
            Assert.Throws<ParseException>(() => Parser.Parse("xyz"));
            Assert.Throws<ParseException>(() => Parser.Parse("x/y=z"));
            Assert.Throws<ParseException>(() => Parser.Parse("(y=z"));
            Assert.Throws<ParseException>(() => Parser.Parse("y)=z"));
            Assert.Throws<ParseException>(() => Parser.Parse(")(y=z"));
        }

        [Test]
        public void TestParser()
        {
            var equation = Parser.Parse("x^2 + 3.5xy + y = y^2 - xy + y");
            
            Assert.That(equation.Left.Count, Is.EqualTo(3));
            Assert.That(equation.Left.Any(x => x.K == 3.5 && x.Variables.All(v => v.Name == 'x' || v.Name == 'y')));
            Assert.That(equation.Left.Any(x => x.Variables.All(v => v.Name == 'x' && v.Power == 2)));
            Assert.That(equation.Left.Any(x => x.Variables.All(v => v.Name == 'y' && v.Power == 1)));
            Assert.That(equation.Right.Count, Is.EqualTo(3));
            Assert.That(equation.Right.Any(x => x.Variables.All(v => v.Name == 'y' && v.Power == 2)));
            Assert.That(equation.Right.Any(x => x.K == -1 && x.Variables.All(v => v.Name == 'x' || v.Name == 'y')));
            Assert.That(equation.Right.Any(x => x.Variables.All(v => v.Name == 'y' && v.Power == 1)));
        }
    }
}