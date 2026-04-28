using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Habitaciones;

namespace Microservicios.Alojamiento.Business.Validators
{
    public class CrearHabilitacionesValidator : AbstractValidator<CrearHabitacionesRequest>
    {
        public CrearHabilitacionesValidator()
        {
            RuleFor(x => x.NombreTipo)
                .NotEmpty().WithMessage("El nombre del tipo de habitación es obligatorio (ej: Suite, Doble).")
                .MaximumLength(50).WithMessage("El nombre del tipo no puede exceder los 50 caracteres.");

            RuleFor(x => x.CapacidadPersonas)
                .GreaterThan(0).WithMessage("La capacidad de personas debe ser al menos 1.");

            RuleFor(x => x.NumeroCamas)
                .GreaterThanOrEqualTo(0).WithMessage("El número de camas no puede ser negativo.");
        }
    }
}