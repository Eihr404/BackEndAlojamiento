using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Habitaciones
{
    public class CrearHabitacionesRequest
    {
        public string NombreTipo { get; set; } = string.Empty; // Ej: Suite, Doble
        public int CapacidadPersonas { get; set; }
        public int NumeroCamas { get; set; }
    }
}
