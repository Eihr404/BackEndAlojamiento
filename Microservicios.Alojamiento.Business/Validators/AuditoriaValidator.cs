using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Auditoria;

namespace Microservicios.Alojamiento.Business.Validators
{
    public class CrearAuditoriaValidator : AbstractValidator<CrearAuditoriaRequest>
    {
        public CrearAuditoriaValidator()
        {
            RuleFor(x => x.Accion).NotEmpty().MaximumLength(50);
            RuleFor(x => x.TablaAfectada).NotEmpty().MaximumLength(50);
            RuleFor(x => x.RegistroId).NotEmpty().WithMessage("Debe indicarse el ID del registro afectado.");
        }
    }
}