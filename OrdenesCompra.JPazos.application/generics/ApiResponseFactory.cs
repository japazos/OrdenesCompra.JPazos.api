namespace OrdenesCompra.JPazos.application.generics
{
    public static class ApiResponseFactory
    {
        public static ApiResponse<T> SuccessWithData<T>(T data) => new() { Success = true, Message = string.Empty, Data = data };
        public static ApiResponse SuccessWithoutData(bool success, string message) => new() { Success = success, Message = message };
    }
}
