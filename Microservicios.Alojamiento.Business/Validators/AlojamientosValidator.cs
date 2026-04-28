using Microservicios.Alojamiento.Business.DTOs.Alojamientos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.Validators
{
    public class CrearAlojamientoValidator : AbstractValidator<CrearAlojamientoRequest>
    {
        public CrearAlojamientoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del alojamiento es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.Tipo)
                .NotEmpty().WithMessage("El tipo de alojamiento es obligatorio (Hotel, Hostal, etc.).");

            RuleFor(x => x.Ciudad)
                .NotEmpty().WithMessage("La ciudad es obligatoria.");

            RuleFor(x => x.Direccion)
                .NotEmpty().WithMessage("La dirección es obligatoria.");

            RuleFor(x => x.AdminId)
                .NotEmpty().WithMessage("Debe asignarse un administrador válido.");

            // Regla de negocio: El check-out no puede ser a la misma hora que el check-in
            RuleFor(x => x.CheckOut)
                .NotEqual(x => x.CheckIn).WithMessage("La hora de Check-out no puede ser igual a la de Check-in.");
        }
    }
}
