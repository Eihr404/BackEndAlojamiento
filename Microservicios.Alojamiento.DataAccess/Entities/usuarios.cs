using System;
using System.Collections.Generic;

namespace Microservicios.Alojamiento.DataAccess.Entities
{
    public class usuarios
    {
        public Guid id { get; set; }
        public string email { get; set; } = string.Empty;
        public string password_hash { get; set; } = string.Empty;
        public bool activo { get; set; } = true;
        public DateTime fecha_creacion { get; set; } = DateTime.UtcNow;

        // Propiedades de navegación
        public virtual ICollection<usuario_roles> usuario_roles { get; set; } = new List<usuario_roles>();
        public virtual clientes? cliente { get; set; }
        public virtual administradores? administrador { get; set; }
    }
}