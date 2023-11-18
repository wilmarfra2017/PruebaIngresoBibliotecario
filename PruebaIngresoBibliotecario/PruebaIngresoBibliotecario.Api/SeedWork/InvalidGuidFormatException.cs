using System;

namespace PruebaIngresoBibliotecario.Api
{
    public class InvalidGuidFormatException : Exception
    {
        public InvalidGuidFormatException(string message) : base(message)
        {
        }
    }

}
