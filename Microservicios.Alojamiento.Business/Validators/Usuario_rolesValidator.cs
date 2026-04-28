using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Usuario_roles;

namespace Microservicios.Alojamiento.Business.Validators
{
    public class CrearUsuario_rolesValidator : AbstractValidator<CrearUsuario_rolesRequest>
    {
        public CrearUsuario_rolesValidator()
        {
            RuleFor(x => x.UsuarioId).NotEmpty().WithMessage("Se requiere un ID de usuario.");
            RuleFor(x => x.RolId).NotEmpty().WithMessage("Se requiere un ID de rol.");
        }
    }
}