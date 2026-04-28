using System;
using System.Collections.Generic;

namespace Microservicios.Alojamiento.DataAccess.Entities
{

    public class reservas
    {
        public Guid id { get; set; }
        public Guid cliente_id { get; set; }
        public Guid alojamiento_id { get; set; }
        public DateTime fecha_solicitud { get; set; } = DateTime.UtcNow;
        public DateTime fecha_check_in { get; set; }
        public DateTime fecha_check_out { get; set; }
        public string estado { get; set; } = "Pendiente";
        public decimal monto_total { get; set; }

        // Propiedades de navegación
        public virtual clientes cliente { get; set; } = null!;
        public virtual alojamientos alojamiento { get; set; } = null!;
        public virtual facturas? factura { get; set; }
        public virtual ICollection<reserva_detalles> detalles { get; set; } = new List<reserva_detalles>();
    }
}