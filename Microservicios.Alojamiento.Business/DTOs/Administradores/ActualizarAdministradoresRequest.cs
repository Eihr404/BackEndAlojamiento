using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Administradores
{
    public class ActualizarAdministradoresRequest : CrearAdministradoresRequest
    {
        public Guid Id { get; set; }
    }
}
