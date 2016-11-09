using System;
using System.Collections.Generic;

namespace EquationNormalizer
{
    public class Parser
    {
        public static Equation Parse(string source)
        {
            var splitted = source.RemoveWhitespace().Split('=');
            if (splitted.Length < 2) throw new ParseException("Уравнение должно содержать знак '='");
            if (splitted.Length > 2) throw new ParseException("Уравнение должно содержать только один знак '='");

            var left = splitted[0];
            var right = splitted[1];
            var equation = new Equation() { Left = ParseSide(left), Right = ParseSide(right) };
            return equation;
        }

        private static TNumber ParseNumber<TNumber>(string source, Func<string, TNumber> parser, ref int position)
        {
            char c;
            c = source[position];
            var cl = new List<char>();
            while (char.IsDigit(c) || c == '.')
            {
                cl.Add(c);
                position++;
                if (position >= source.Length)
                    break;
                c = source[position];
            }
            position--;
            return parser(new string(cl.ToArray()));
        }

        private static List<Summand> ParseSide(string source)
        {
            var parstack = new Stack<int>();
            var result = new List<Summand>();
            var currSummand = new Summand() { K = 1 };
            double curNum = 0;
            Variable currVariable = null;
            int pos = 0;
            bool inPower = false;
            parstack.Push(1);
            while (pos < source.Length)
            {
                var c = source[pos];

                if (inPower && !char.IsDigit(c))
                {
                    currVariable = null;
                    inPower = false;
                }

                if (char.IsDigit(c))
                {
                    if (inPower)
                    {
                        currVariable.Power = ParseNumber(source, int.Parse, ref pos);
                    }
                    else
                    {
                        currSummand.K = currSummand.K * ParseNumber(source, double.Parse, ref pos);
                    }
                }
                else if (c == '+' || c == '-')
                {
                    if (pos > 0)
                        result.Add(currSummand);
                    var mult = parstack.Peek();
                    currSummand = new Summand() { K = c == '+' ? mult : -mult };
                }
                else if (c == '^')
                {
                    inPower = true;
                }
                else if (c == '(')
                {
                    var mult = parstack.Peek();
                    if (pos != 0 && source[pos - 1] == '-')
                    {
                        mult = -mult;
                    }

                    parstack.Push(mult);
                }
                else if (c == ')')
                {
                    if (parstack.Count == 1) throw new ParseException("Скобки не сбалансированы!");
                    parstack.Pop();
                }
                else if (char.IsLetter(c))
                {
                    currVariable = new Variable(c);
                    currSummand.Variables.Add(currVariable);
                }
                else
                {
                    throw new ParseException($"Ошибка разбора выражения в позиции {pos} символ '{c}'");
                }

                pos++;
            }

            if (parstack.Count > 1) throw new ParseException("Скобки не сбалансированы!");
            result.Add(currSummand);
            return result;
        }
    }
}