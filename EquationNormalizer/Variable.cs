using System;

namespace EquationNormalizer
{
    public class Variable
    {
        protected bool Equals(Variable other)
        {
            return Name == other.Name && Power == other.Power;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Variable) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Name.GetHashCode()*397) ^ Power;
            }
        }

        public static bool operator ==(Variable left, Variable right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Variable left, Variable right)
        {
            return !Equals(left, right);
        }

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