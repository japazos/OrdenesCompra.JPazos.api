using FluentValidation;
using OrdenesCompra.JPazos.application.Dto.Orden;
using System.Linq;

namespace OrdenesCompra.JPazos.application.Validators
{
    public class OrdenCreateDtoValidator : AbstractValidator<OrdenCreateDto>
    {
        public OrdenCreateDtoValidator()
        {
            RuleFor(d => d.OrdenDetalle)
                .NotEmpty()
                .WithMessage("La orden debe tener al menos un detalle.");

            RuleForEach(dto => dto.OrdenDetalle)
                .ChildRules(detalle => 
                {
                    detalle.RuleFor(d => d.Cantidad)
                        .GreaterThanOrEqualTo(0)
                        .WithMessage("La cantidad no puede ser negativa.");

                    detalle.RuleFor(d => d.PrecioUnitario)
                        .GreaterThanOrEqualTo(0)
                        .WithMessage("El precio unitario no puede ser negativo.");

                });

        }
    }
}
