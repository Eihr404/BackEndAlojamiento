using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Alojamiento_habitacion;

namespace Microservicios.Alojamiento.Business.Validators
{
    public class CrearAlojamiento_habitacionValidator : AbstractValidator<CrearAlojamiento_habitacionRequest>
    {
        public CrearAlojamiento_habitacionValidator()
        {
            RuleFor(x => x.AlojamientoId).NotEmpty();
            RuleFor(x => x.HabitacionId).NotEmpty();
            RuleFor(x => x.PrecioNoche).GreaterThan(0).WithMessage("El precio por noche debe ser mayor a 0.");
            RuleFor(x => x.CantidadTotal).GreaterThanOrEqualTo(0).WithMessage("La cantidad total no puede ser negativa.");
        }
    }
}