using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Clientes
{
    public class ActualizarClienteRequest : CrearClienteRequest
    {
        public Guid Id { get; set; }
    }
}
