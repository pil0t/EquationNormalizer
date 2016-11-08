using System;

namespace EquationNormalizer
{
    public class Variable
    {
        public Variable() { }
        public Variable(char name)
        {
            Name = name;
            Power = 1;
        }
        public Variable(char name, int power)
        {
            Name = name;
            Power = power;
        }

        public char Name { get; set; }
        public int Power { get; set; }
        
        public override string ToString()
        {
            if (Power == 0) return string.Empty;
            if (Power == 1) return Name.ToString();
            return $"{Name}^{Power}";
        }
    }
}