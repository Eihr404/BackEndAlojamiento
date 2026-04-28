using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Servicios_alojamiento;

namespace Microservicios.Alojamiento.Business.Validators
{
    public class CrearServicios_alojamientoValidator : AbstractValidator<CrearServicios_alojamientoRequest>
    {
        public CrearServicios_alojamientoValidator()
        {
            RuleFor(x => x.AlojamientoId).NotEmpty();
            RuleFor(x => x.ServicioId).NotEmpty();
            RuleFor(x => x.PrecioAdicional).GreaterThanOrEqualTo(0).WithMessage("El precio adicional no puede ser negativo.");
        }
    }
}