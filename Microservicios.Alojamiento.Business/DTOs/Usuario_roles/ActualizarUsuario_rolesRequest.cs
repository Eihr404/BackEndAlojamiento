using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Usuario_roles
{
    public class ActualizarUsuario_rolesRequest
    {
        // Al ser una tabla de relación con clave primaria compuesta (usuario_id, rol_id), 
        // la actualización suele implicar borrar y crear o modificar el rol asociado.
        public Guid UsuarioId { get; set; }
        public Guid RolIdAnterior { get; set; }
        public Guid RolIdNuevo { get; set; }
    }
}
