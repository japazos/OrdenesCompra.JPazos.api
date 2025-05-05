using System;

namespace OrdenesCompra.JPazos.application.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException() { }

        public BusinessException(string message) : base(message) { }
    }
}
