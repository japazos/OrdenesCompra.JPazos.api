using Mapster;
using OrdenesCompra.JPazos.domain.Entities;
using OrdenesCompra.JPazos.application.Dto.Usuario;
using OrdenesCompra.JPazos.application.Dto.Orden;
using OrdenesCompra.JPazos.application.dto.security;

namespace OrdenesCompra.JPazos.application.Mappings
{
    internal class DtoToModelMapping
    {
        public static void Build(TypeAdapterConfig config)
        {
            config.NewConfig<AddUsuarioDto, Usuario>();
            config.NewConfig<OrdenCreateDto, Orden>();
            config.NewConfig<SignUpDto, Usuario>();
        }
    }
}
