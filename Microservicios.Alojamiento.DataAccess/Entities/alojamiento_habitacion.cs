using System;

namespace Microservicios.Alojamiento.DataAccess.Entities
{
    public class alojamiento_habitacion
    {
        public Guid id { get; set; }
        public Guid alojamiento_id { get; set; }
        public Guid habitacion_id { get; set; }
        public decimal precio_noche { get; set; }
        public int cantidad_total { get; set; }

        // Propiedades de navegación
        public virtual alojamientos alojamiento { get; set; } = null!;
        public virtual habitaciones habitacion { get; set; } = null!;
    }
}