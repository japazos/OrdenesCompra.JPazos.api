using Mapster;
using OrdenesCompra.JPazos.application.Mappings;

namespace OrdenesCompra.JPazos.application.Configs
{
    public class MappingConfig
    {
        public static void RegisterMappings(TypeAdapterConfig config)
        {
            ModelToDtoMapping.Build(config);
        }
    }
}
