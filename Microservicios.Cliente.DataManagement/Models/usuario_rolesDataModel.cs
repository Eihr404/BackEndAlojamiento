using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Models
{
    public class usuario_rolesDataModel
    {
        public Guid UsuarioId { get; set; }
        public Guid RolId { get; set; }
        public string? NombreRol { get; set; }
    }
}
