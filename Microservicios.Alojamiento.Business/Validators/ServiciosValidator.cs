using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Servicios;

namespace Microservicios.Alojamiento.Business.Validators
{
    public class CrearServiciosValidator : AbstractValidator<CrearServiciosRequest>
    {
        public CrearServiciosValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del servicio es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre del servicio es demasiado largo.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(500).WithMessage("La descripción no debe exceder los 500 caracteres.");
        }
    }
}