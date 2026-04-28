using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Models
{
    public class reserva_detallesDataModel
    {
        public Guid Id { get; set; }
        public Guid ReservaId { get; set; }
        public string TipoItem { get; set; } = string.Empty; // "Habitacion" o "Servicio"
        public Guid ItemId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioCapturado { get; set; } // Valor del Snapshot
    }
}
