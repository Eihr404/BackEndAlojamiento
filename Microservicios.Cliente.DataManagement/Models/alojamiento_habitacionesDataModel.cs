using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Models
{
    public class alojamiento_habitacionesDataModel
    {
        public Guid Id { get; set; } // Agregamos el Id que está en tu Entity
        public Guid AlojamientoId { get; set; }
        public Guid HabitacionId { get; set; }
        public string? NombreHabitacion { get; set; }
        public decimal PrecioNoche { get; set; }
        public int CantidadTotal { get; set; } // Antes StockDisponible
    }
}
