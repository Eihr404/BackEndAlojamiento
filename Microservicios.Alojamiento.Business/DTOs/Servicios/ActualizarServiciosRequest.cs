using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Servicios
{
    public class ActualizarServiciosRequest : CrearServiciosRequest
    {
        public Guid Id { get; set; }
    }
}
