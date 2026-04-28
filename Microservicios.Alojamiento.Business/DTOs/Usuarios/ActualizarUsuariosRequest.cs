using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Usuarios
{
    public class ActualizarUsuariosRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool Activo { get; set; }
        // La actualización de password suele ir en un DTO separado por seguridad, 
        // pero aquí lo incluimos según la estructura base.
        public string? Password { get; set; }
    }
}
