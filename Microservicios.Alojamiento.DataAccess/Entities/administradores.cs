using System;
using System.Collections.Generic;

namespace Microservicios.Alojamiento.DataAccess.Entities
{
    public class administradores
    {
        public Guid id { get; set; }
        public Guid usuario_id { get; set; }
        public string? nombre_comercial { get; set; }
        public string? nit_tax { get; set; }
        public string? telefono_soporte { get; set; }

        // Propiedades de navegación
        public virtual usuarios usuario { get; set; } = null!;
        public virtual ICollection<alojamientos> alojamientos { get; set; } = new List<alojamientos>();
    }
}