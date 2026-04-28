using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Usuarios
{
    public class CrearUsuariosRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // Se procesará a Hash en el servicio
        public bool Activo { get; set; } = true;
    }
}
