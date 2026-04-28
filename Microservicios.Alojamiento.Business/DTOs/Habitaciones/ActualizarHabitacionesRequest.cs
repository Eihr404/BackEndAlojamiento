using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Habitaciones
{
    public class ActualizarHabitacionesRequest : CrearHabitacionesRequest
    {
        public Guid Id { get; set; }
    }
}
