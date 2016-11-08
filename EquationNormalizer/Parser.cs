using System;
using System.Collections.Generic;

namespace EquationNormalizer
{
    public class Parser
    {
        public Equation Parse(string source)
        {
            var splitted = RemoveWhitespace(source).Split('=');
            if (splitted.Length < 2) throw new ParseException("Уравнение должно содержать знак '='");
            if (splitted.Length > 2) throw new ParseException("Уравнение должно содержать только один знак '='");

            var left = splitted[0];
            var right = splitted[1];
            var equation = new Equation() {Left = ParseSide(left), Right = ParseSide(right)};
            return equation;
        }

        private static string RemoveWhitespace(string input)
        {
            return string.Join("", input.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }

        private int ParseInt(string source, ref int position)
        {
            char c;
            c = source[position];
            var cl = new List<char>();
            while (char.IsDigit(c))
            {
                cl.Add(c);
                position++;
                if (position >= source.Length)
                    break;
                c = source[position];
            }
            position--;
            return int.Parse(new string(cl.ToArray()));
        }
        private double ParseDouble(string source, ref int position)
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
            return double.Parse(new string(cl.ToArray()));
        }

        private List<Summand> ParseSide(string source)
        {
            var result = new List<Summand>();
            var currSummand = new Summand() {K = 1};
            double curNum = 0;
            Variable currVariable = null;
            int pos = 0;
            bool inPower = false;
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
                        currVariable.Power = ParseInt(source, ref pos);
                    }
                    else
                    {
                        currSummand.K = currSummand.K * ParseDouble(source, ref pos);
                    }
                }
                else if (c == '+' || c == '-')
                {
                    if(pos >0)
                        result.Add(currSummand);
                    currSummand = new Summand() {K = c == '+' ? 1 : -1};
                }
                else if (c == '^')
                {
                    inPower = true;
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

            result.Add(currSummand);
            return result;
        }
    }
}