using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Roles
{
    public class ActualizarRolesRequest : CrearRolesRequest
    {
        public Guid Id { get; set; }
    }
}
