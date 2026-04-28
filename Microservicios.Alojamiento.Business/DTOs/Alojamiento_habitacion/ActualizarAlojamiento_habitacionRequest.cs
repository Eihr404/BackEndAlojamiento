using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Alojamiento_habitacion
{
    public class ActualizarAlojamiento_habitacionRequest : CrearAlojamiento_habitacionRequest
    {
        public Guid Id { get; set; }
    }
}
