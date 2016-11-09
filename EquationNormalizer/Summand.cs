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

        public Summand Inverse => new Summand(-K, Variables.ToArray());

        public bool Positive => K > Extensions.Epsilon;

        public bool IsOne => Math.Abs(Math.Abs(K) - 1) < Extensions.Epsilon;

        public override string ToString()
        {
            return this.ToString(false);
        }

        public string ToString(bool alwaysWithSign)
        {
            if (Math.Abs(K) < Extensions.Epsilon)
            {
                return "0";
            }

            var result = new StringBuilder();
            if (Positive)
            {
                if(alwaysWithSign)
                    result.Append("+");
            }
            else if (IsOne)
            {
                result.Append("-");
            }

            if (!IsOne || !Variables.Any())
            {
                result.Append(K);
            }

            result.Append(Variables.Format());

            return result.ToString();
        }

        public List<Variable> Variables { get; set; }

        public double K { get; set; }
        
        /// <summary> Normalize summand
        /// From xxyyy to x^2y^3 </summary>
        /// <returns></returns>
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