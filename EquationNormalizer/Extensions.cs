using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquationNormalizer
{
    public static class Extensions
    {
        internal const double Epsilon = 0.00001d;

        public static string Format(this List<Variable> varList)
        {
            return String.Join("", varList.OrderByDescending(x => x.Power).ThenBy(x => x.Name));
        }

        public static string RemoveWhitespace(this string input)
        {
            return String.Join("", input.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }

        public static bool IsZero(this double number)
        {
            return Math.Abs(number) < Epsilon;
        }

        public static bool Is(this double number, double compareTo)
        {
            return Math.Abs(number - compareTo) < Epsilon;
        }

        public static bool IsPositive(this double number)
        {
            return number >= Epsilon;
        }
        public static bool IsNegative(this double number)
        {
            return number <= Epsilon;
        }
    }
}