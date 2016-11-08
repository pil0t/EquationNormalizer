using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquationNormalizer
{
    public static class Extensions
    {
        public static string Format(this List<Variable> varList)
        {
            return string.Join("", varList.OrderByDescending(x => x.Power).ThenBy(x => x.Name));
        }

        public static int Order(this List<Variable> varList)
        {
            return varList == null || varList.Count == 0 ? 0 : varList.Max(x => x.Power);
        }

        public static string Sign(this double value)
        {
            return value > Equation.Epsilon ? "+" : "-";
        }
    }
}