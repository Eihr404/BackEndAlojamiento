using System;
using System.Collections.Generic;

namespace Microservicios.Alojamiento.DataAccess.Entities
{
    public class clientes
    {
        public Guid id { get; set; }
        public Guid usuario_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string apellido { get; set; } = string.Empty;
        public string? telefono { get; set; }
        public string? documento_identidad { get; set; }

        // Propiedades de navegación
        public virtual usuarios usuario { get; set; } = null!;
        public virtual ICollection<reservas> reservas { get; set; } = new List<reservas>();
        public virtual ICollection<resenas> resenas { get; set; } = new List<resenas>();
    }
}