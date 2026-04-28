using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Administradores;

namespace Microservicios.Alojamiento.Business.Validators
{
    public class CrearAdministradoresValidator : AbstractValidator<CrearAdministradoresRequest>
    {
        public CrearAdministradoresValidator()
        {
            RuleFor(x => x.UsuarioId).NotEmpty().WithMessage("El administrador debe estar ligado a un usuario.");
            RuleFor(x => x.NombreComercial).MaximumLength(150).WithMessage("El nombre comercial no puede exceder los 150 caracteres.");
            RuleFor(x => x.NitTax).NotEmpty().WithMessage("El NIT/Tax ID es obligatorio para fines legales.");
            RuleFor(x => x.TelefonoSoporte).Matches(@"^\+?\d{7,15}$").WithMessage("El formato del teléfono de soporte no es válido.");
        }
    }
}