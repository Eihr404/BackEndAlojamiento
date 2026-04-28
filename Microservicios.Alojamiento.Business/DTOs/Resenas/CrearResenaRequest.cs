using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Resenas
{
    public class CrearResenaRequest
    {
        public Guid ClienteId { get; set; }
        public Guid AlojamientoId { get; set; }
        public int Estrellas { get; set; } // Validación: 1 a 5 
        public string? Comentario { get; set; }
    }
}
