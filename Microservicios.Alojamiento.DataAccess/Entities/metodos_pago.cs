using System;
using System.Collections.Generic;

namespace Microservicios.Alojamiento.DataAccess.Entities
{
    public class metodos_pago
    {
        public Guid id { get; set; }
        public string nombre { get; set; } = string.Empty;

        // Propiedad de navegación
        public virtual ICollection<facturas> facturas_asociadas { get; set; } = new List<facturas>();
    }
}