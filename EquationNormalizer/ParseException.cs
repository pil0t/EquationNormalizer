using System;

namespace EquationNormalizer
{
    public class ParseException : Exception
    {
        public ParseException(string message) : base(message)
        {
        }
    }
}