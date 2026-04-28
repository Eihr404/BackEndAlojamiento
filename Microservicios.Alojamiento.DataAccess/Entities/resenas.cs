using System;

namespace Microservicios.Alojamiento.DataAccess.Entities
{
    public class resenas
    {
        public Guid id { get; set; }
        public Guid cliente_id { get; set; }
        public Guid alojamiento_id { get; set; }
        public int estrellas { get; set; } // Validación 1-5 se hará en Configuration
        public string? comentario { get; set; }
        public DateTime fecha { get; set; } = DateTime.UtcNow;

        // Propiedades de navegación
        public virtual clientes cliente { get; set; } = null!;
        public virtual alojamientos alojamiento { get; set; } = null!;
    }
}