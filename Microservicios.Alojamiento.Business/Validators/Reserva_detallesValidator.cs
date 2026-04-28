using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Reserva_detalles;

namespace Microservicios.Alojamiento.Business.Validators
{
    public class CrearReserva_detallesValidator : AbstractValidator<CrearReserva_detallesRequest>
    {
        public CrearReserva_detallesValidator()
        {
            RuleFor(x => x.ReservaId).NotEmpty();
            RuleFor(x => x.TipoItem).Must(x => x == "Habitacion" || x == "Servicio")
                .WithMessage("El tipo de ítem debe ser 'Habitacion' o 'Servicio'.");
            RuleFor(x => x.ItemId).NotEmpty();
            RuleFor(x => x.Cantidad).GreaterThan(0).WithMessage("La cantidad debe ser al menos 1.");
            RuleFor(x => x.PrecioCapturado).GreaterThanOrEqualTo(0).WithMessage("El precio capturado no puede ser negativo.");
        }
    }
}