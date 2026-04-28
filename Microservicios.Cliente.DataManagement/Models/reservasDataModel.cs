using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Models
{
    public class reservasDataModel
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public Guid AlojamientoId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaCheckIn { get; set; }
        public DateTime FechaCheckOut { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal MontoTotal { get; set; }
        public List<reserva_detallesDataModel> Detalles { get; set; } = new();
    }
}
