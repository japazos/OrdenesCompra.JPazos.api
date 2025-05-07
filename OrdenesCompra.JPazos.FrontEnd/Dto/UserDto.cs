namespace OrdenesCompra.JPazos.FrontEnd.Dto
{
    public class UserDto 
    {
        public string Token { get; set; }
        public string UsuarioId { get; set; }
        public string NombreCompleto { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
        public string FechaCreacion { get; set; }
    }
}
