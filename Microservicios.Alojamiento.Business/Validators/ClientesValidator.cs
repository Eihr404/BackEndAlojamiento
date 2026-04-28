using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Clientes;

namespace Microservicios.Alojamiento.Business.Validators
{
    public class CrearClienteValidator : AbstractValidator<CrearClienteRequest>
    {
        public CrearClienteValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres.");

            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El apellido es obligatorio.");

            RuleFor(x => x.DocumentoIdentidad)
                .NotEmpty().WithMessage("El documento de identidad es requerido para el registro legal.");

            RuleFor(x => x.UsuarioId)
                .NotEmpty().WithMessage("El cliente debe estar vinculado a un UsuarioId.");
        }
    }
}