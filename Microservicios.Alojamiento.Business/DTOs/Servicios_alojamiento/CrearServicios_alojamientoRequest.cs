using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Servicios_alojamiento
{
    public class CrearServicios_alojamientoRequest
    {
        public Guid AlojamientoId { get; set; }
        public Guid ServicioId { get; set; }
        public decimal PrecioAdicional { get; set; }
        public bool EstaActivo { get; set; } = true;
    }
}
