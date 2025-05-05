using System;

namespace OrdenesCompra.JPazos.application.generics
{
    public class ExceptionResponse
    {
        public string ErrorCode { get; set; } = null!;
        public string ErrorMessage { get; set; } = null!;

        public ExceptionResponse(string message, string errorCode = "00000003") 
        {
            ErrorCode = errorCode;
            ErrorMessage = message;
        }
    }
}
