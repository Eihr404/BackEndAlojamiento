using System;
using System.Collections.Generic;

namespace Microservicios.Alojamiento.DataAccess.Entities
{
    public class roles
    {
        public Guid id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string? descripcion { get; set; }

        // Propiedad de navegación
        public virtual ICollection<usuario_roles> usuario_roles { get; set; } = new List<usuario_roles>();
    }
}