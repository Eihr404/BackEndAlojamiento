using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Models
{
    public class facturasDataModel
    {
        public Guid Id { get; set; }
        public Guid ReservaId { get; set; }
        public Guid MetodoPagoId { get; set; } // <--- Agregado
        public string NumFactura { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public string EstadoPago { get; set; } = string.Empty;
    }
}