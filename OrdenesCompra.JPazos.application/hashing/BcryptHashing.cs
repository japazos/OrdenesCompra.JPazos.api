using BCryptNet = BCrypt.Net.BCrypt;

namespace OrdenesCompra.JPazos.application.hashing
{
    internal static class BcryptHashing
    {
        public static string Hash(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            return BCryptNet.HashPassword(text);
        }
        
        public static bool Verify(string text, string hash)
        {
            if (string.IsNullOrEmpty(text)) return false;
            if (string.IsNullOrEmpty(hash)) return false;

            return BCryptNet.Verify(text, hash);
        }
    }
}
