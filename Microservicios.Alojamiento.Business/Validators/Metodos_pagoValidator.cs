using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Metodos_pago;

namespace Microservicios.Alojamiento.Business.Validators
{
    public class CrearMetodo_pagoValidator : AbstractValidator<CrearMetodo_pagoRequest>
    {
        public CrearMetodo_pagoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del método de pago es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre es demasiado largo.");
        }
    }
}