using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Facturas
{
    public class CrearFacturaRequest
    {
        public Guid ReservaId { get; set; }
        public string NumFactura { get; set; } = string.Empty;
        public Guid MetodoPagoId { get; set; }
    }
}
