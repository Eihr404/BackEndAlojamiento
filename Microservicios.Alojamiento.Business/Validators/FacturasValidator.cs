using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Facturas;

namespace Microservicios.Alojamiento.Business.Validators
{
    public class CrearFacturaValidator : AbstractValidator<CrearFacturaRequest>
    {
        public CrearFacturaValidator()
        {
            RuleFor(x => x.ReservaId).NotEmpty().WithMessage("La factura debe estar ligada a una reserva.");
            RuleFor(x => x.MetodoPagoId).NotEmpty().WithMessage("Debe especificar un método de pago.");
            RuleFor(x => x.NumFactura).NotEmpty().WithMessage("El número de factura es obligatorio.");
        }
    }
}