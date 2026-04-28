using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Roles;

namespace Microservicios.Alojamiento.Business.Validators
{
    public class CrearRolesValidator : AbstractValidator<CrearRolesRequest>
    {
        public CrearRolesValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Descripcion).MaximumLength(250);
        }
    }
}