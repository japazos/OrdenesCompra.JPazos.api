namespace OrdenesCompra.JPazos.application.IServices
{
    public interface IJwtService
    {
        string GenerateToken(string customerId, string email, string roleId);
    }
}
