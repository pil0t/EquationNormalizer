using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquationNormalizer
{
    public class Equation
    {
        public List<Summand> Left { get; set; }
        public List<Summand> Right { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            RenderSummands(sb, Left);
            sb.Append(" = ");
            RenderSummands(sb, Right);
            return sb.ToString();
        }

        public Equation Normalize()
        {
            // normalize single summands and move in left side
            var allSummands = Left.Concat(Right.Select(r => r.Inverse)).Select(x => x.Normalize()).ToList();
            var newSummands = Normalizer.Normalize(allSummands);
            
            return new Equation() {Left = newSummands, Right = new List<Summand>()};
        }

        private void RenderSummands(StringBuilder sb, List<Summand> summands)
        {
            if (summands.Count == 0 || summands.All(x => x.K.IsZero()))
            {
                sb.Append("0");
                return;
            }

            foreach (var summand in summands
                .OrderByDescending(x => x.Variables.Sum(s => s.Power))
                .ThenBy(x=>x.Variables.Distinct().Count())
                .ThenByDescending(x => x.Variables.Count()))
            {
                if (!summand.K.IsZero())
                {
                    // плюс пишем не в начале строки и для положительных чисел
                    if (sb.Length > 0 && summand.K.IsPositive())
                    {
                        sb.Append("+");
                    }

                    if (!summand.K.Is(1)) // для +1 пропускаем
                    {
                        if (summand.K.Is(-1)) // для -1 только добавить '-'
                        {
                            sb.Append("-");
                        }
                        else
                        {
                            sb.Append(summand.K);
                        }
                    }

                    sb.Append(string.Join("", summand.Variables.Select(x => x.ToString())));
                }
            }
        }
    }
}