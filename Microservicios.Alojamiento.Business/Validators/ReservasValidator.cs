using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Reservas;

namespace Microservicios.Alojamiento.Business.Validators
{
    public class CrearReservaValidator : AbstractValidator<CrearReservaRequest>
    {
        public CrearReservaValidator()
        {
            RuleFor(x => x.ClienteId).NotEmpty();
            RuleFor(x => x.AlojamientoId).NotEmpty();

            RuleFor(x => x.FechaCheckIn)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)  // ← UtcNow.Date en lugar de Today
                .WithMessage("La fecha de Check-in no puede ser en el pasado.");

            RuleFor(x => x.FechaCheckOut)
                .GreaterThan(x => x.FechaCheckIn)
                .WithMessage("La fecha de Check-out debe ser posterior a la de Check-in.");

            RuleFor(x => x.MontoTotal)
                .GreaterThan(0)
                .WithMessage("El monto total de la reserva debe ser mayor a cero.");
        }
    }
}