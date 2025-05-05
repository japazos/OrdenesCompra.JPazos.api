using Mapster;
using OrdenesCompra.JPazos.domain.Entities;
using OrdenesCompra.JPazos.application.Dto.Usuario;

namespace OrdenesCompra.JPazos.application.Mappings
{
    public class ModelToDtoMapping
    {
        public static void Build(TypeAdapterConfig config)
        {
            config.NewConfig<Usuario, UsuarioDto>()
                .Map(dst => dst.NombreCompleto, src => $"{src.Apellidos} {src.Nombres}")
                .Map(dst => dst.FechaCreacion, src => src.FechaCreacion.ToString("dd/MM/yyyy"));
        }
    }
}
