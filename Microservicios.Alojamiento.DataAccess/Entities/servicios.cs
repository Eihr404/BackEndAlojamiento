using System;
using System.Collections.Generic;

namespace Microservicios.Alojamiento.DataAccess.Entities
{
    public class servicios
    {
        public Guid id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string? descripcion { get; set; }

        // Propiedad de navegación
        public virtual ICollection<servicios_alojamiento> alojamientos_que_lo_ofrecen { get; set; } = new List<servicios_alojamiento>();
    }
}