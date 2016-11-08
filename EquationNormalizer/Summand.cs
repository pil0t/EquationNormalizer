using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquationNormalizer
{
    public class Summand
    {
        public Summand()
        {
            Variables = new List<Variable>();
        }

        public Summand(double k, params Variable[] variables)
        {
            K = k;
            Variables = variables.ToList();
        }

        public bool Positive => K > Equation.Epsilon;

        public bool IsOne => Math.Abs(Math.Abs(K) - 1) < Equation.Epsilon;

        public override string ToString()
        {
            if (Math.Abs(K) < Equation.Epsilon)
            {
                return "0";
            }

            var result = new StringBuilder();
            if (Positive)
            {
                result.Append("+");
            }
            else
            {
                result.Append("-");
            }

            if (!IsOne)
            {
                result.Append(K);
            }

            result.Append(Variables.Format());

            return result.ToString();
        }

        public List<Variable> Variables { get; set; }
        public double K { get; set; }

        public bool TryCombine(Summand other, out Summand result)
        {
            var thisSign = string.Join(",", this.Normalize().Variables);
            var otherSign = string.Join(",", other.Normalize().Variables);
            if (thisSign != otherSign)
            {
                result = null;
                return false;
            }

            result = new Summand(K + other.K, Variables.ToArray());
            return true;
        }

        public Summand Normalize()
        {
            var result = new List<Variable>();
            foreach (var variable in Variables.GroupBy(x => x.Name).OrderBy(x => x.Key))
            {
                result.Add(new Variable(variable.Key, variable.Sum(x => x.Power)));
            }

            return new Summand(K, result.ToArray());
        }
    }
}