using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Alojamientos
{
    public class ActualizarAlojamientoRequest : CrearAlojamientoRequest
    {
        public Guid Id { get; set; }
    }
}
