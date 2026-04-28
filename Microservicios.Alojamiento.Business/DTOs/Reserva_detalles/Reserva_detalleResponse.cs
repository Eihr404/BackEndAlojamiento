using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Reserva_detalles
{
    public class Reserva_detalleResponse
    {
        public Guid Id { get; set; }
        public Guid ReservaId { get; set; }
        public Guid ItemId { get; set; }
        public string TipoItem { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioCapturado { get; set; }
    }
}
