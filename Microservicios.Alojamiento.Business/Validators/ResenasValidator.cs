using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Resenas;

namespace Microservicios.Alojamiento.Business.Validators
{
    public class CrearResenaValidator : AbstractValidator<CrearResenaRequest>
    {
        public CrearResenaValidator()
        {
            RuleFor(x => x.ClienteId).NotEmpty();
            RuleFor(x => x.AlojamientoId).NotEmpty();

            RuleFor(x => x.Estrellas)
                .InclusiveBetween(1, 5).WithMessage("La calificación debe estar entre 1 y 5 estrellas.");

            RuleFor(x => x.Comentario)
                .MaximumLength(1000).WithMessage("El comentario no puede exceder los 1000 caracteres.");
        }
    }
}