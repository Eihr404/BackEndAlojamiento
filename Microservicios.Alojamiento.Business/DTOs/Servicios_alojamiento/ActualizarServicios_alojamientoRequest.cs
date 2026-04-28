using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Servicios_alojamiento
{
    public class ActualizarServicios_alojamientoRequest : CrearServicios_alojamientoRequest
    {
        public Guid Id { get; set; }
    }
}
