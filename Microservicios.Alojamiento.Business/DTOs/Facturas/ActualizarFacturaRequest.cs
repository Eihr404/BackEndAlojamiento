using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Facturas
{
    public class ActualizarFacturaRequest
    {
        public Guid Id { get; set; }
        public string EstadoPago { get; set; } = "Exitoso"; // Corresponde al ENUM estado_factura [cite: 44, 58]
        public Guid MetodoPagoId { get; set; }
    }
}
