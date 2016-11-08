using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquationNormalizer
{
    public class Equation
    {
        internal const double Epsilon = 0.00001d;
        public List<Summand> Left { get; set; }
        public List<Summand> Right { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            RenderSummands(sb, Left);
            sb.Append(" = ");
            if (Right.Count > 0)
                RenderSummands(sb, Right);
            else
                sb.Append("0");

            return sb.ToString();
        }

        public Equation Normalize()
        {
            var allSummands = Left.Select(x => x.Normalize()).ToList();
            foreach (var summand in Right)
            {
                allSummands.Add(new Summand() {K = -summand.K, Variables = summand.Normalize().Variables.ToList()});
            }

            var newSummands = new List<Summand>();
            foreach (var sg in allSummands.GroupBy(x=>string.Join(",", x.Variables.Select(v=>v.ToString()))).OrderByDescending(x=>x.FirstOrDefault()?.Variables.Order()))
            {
                newSummands.Add(new Summand() {Variables = sg.First().Variables, K = sg.Sum(s => s.K)});
            }
            return new Equation() {Left = newSummands, Right = new List<Summand>()};
        }

        private void RenderSummands(StringBuilder sb, List<Summand> summands)
        {
            foreach (var summand in summands)
            {
                if (Math.Abs(summand.K) > Epsilon)
                {
                    if (sb.Length > 0 && summand.K > Epsilon)
                    {
                        sb.Append("+");
                    }

                    if (!(Math.Abs(summand.K - 1) < Epsilon))
                    {
                        if ((Math.Abs(summand.K + 1) < Epsilon))
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