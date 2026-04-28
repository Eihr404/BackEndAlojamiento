using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Reserva_detalles
{
    public class CrearReserva_detallesRequest
    {
        public Guid ReservaId { get; set; }
        public Guid ItemId { get; set; } // ID de Habitacion o Servicio
        public string TipoItem { get; set; } = string.Empty; // 'Habitacion' o 'Servicio'
        public int Cantidad { get; set; } = 1;
        public decimal PrecioCapturado { get; set; } // Snapshot del precio actual
    }
}
