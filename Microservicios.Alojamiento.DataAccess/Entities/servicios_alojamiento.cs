using System;

namespace Microservicios.Alojamiento.DataAccess.Entities
{
    public class servicios_alojamiento
    {
        public Guid id { get; set; }
        public Guid alojamiento_id { get; set; }
        public Guid servicio_id { get; set; }
        public decimal precio_adicional { get; set; }
        public bool esta_activo { get; set; } = true;

        // Propiedades de navegación
        public virtual alojamientos alojamiento { get; set; } = null!;
        public virtual servicios servicio { get; set; } = null!;
    }
}