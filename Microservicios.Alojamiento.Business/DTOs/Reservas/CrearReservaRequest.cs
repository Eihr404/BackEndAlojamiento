using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Reservas
{
    public class CrearReservaRequest
    {
        public Guid ClienteId { get; set; }
        public Guid AlojamientoId { get; set; }
        public DateTime FechaCheckIn { get; set; }
        public DateTime FechaCheckOut { get; set; }
        public Guid HabitacionId { get; set; }  // ← agregar

        public decimal MontoTotal { get; set; }
    }
}
