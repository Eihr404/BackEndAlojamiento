using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Alojamiento_habitacion
{
    public class Alojamiento_habitacionResponse
    {
        public Guid Id { get; set; }
        public Guid AlojamientoId { get; set; }
        public Guid HabitacionId { get; set; }
        public decimal PrecioNoche { get; set; }
        public int CantidadTotal { get; set; }
    }
}
