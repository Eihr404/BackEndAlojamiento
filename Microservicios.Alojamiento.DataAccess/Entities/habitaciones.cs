using System;
using System.Collections.Generic;

namespace Microservicios.Alojamiento.DataAccess.Entities
{
    public class habitaciones
    {
        public Guid id { get; set; }
        public string nombre_tipo { get; set; } = string.Empty;
        public int capacidad_personas { get; set; }
        public int numero_camas { get; set; }

        // Propiedad de navegación
        public virtual ICollection<alojamiento_habitacion> alojamientos_vinculados { get; set; } = new List<alojamiento_habitacion>();
    }
}