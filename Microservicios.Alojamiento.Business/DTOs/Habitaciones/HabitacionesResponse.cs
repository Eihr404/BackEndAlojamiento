using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Habitaciones
{
    public class HabitacionesResponse
    {
        public Guid Id { get; set; }
        public string NombreTipo { get; set; } = string.Empty;
        public int CapacidadPersonas { get; set; }
        public int NumeroCamas { get; set; }
    }
}
