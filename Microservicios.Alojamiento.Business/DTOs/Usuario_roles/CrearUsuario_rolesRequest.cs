using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Usuario_roles
{
    public class CrearUsuario_rolesRequest
    {
        public Guid UsuarioId { get; set; }
        public Guid RolId { get; set; }
    }
}
