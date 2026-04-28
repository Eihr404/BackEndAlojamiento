using System;

namespace Microservicios.Alojamiento.DataAccess.Entities
{

    public class facturas
    {
        public Guid id { get; set; }
        public Guid reserva_id { get; set; }
        public string num_factura { get; set; } = string.Empty;
        public DateTime fecha_emision { get; set; } = DateTime.UtcNow;
        public Guid metodo_pago_id { get; set; }
        public string estado_pago { get; set; } = "Pendiente"; // Ya no es el Enum, ahora es string
        // Propiedades de navegación
        public virtual reservas reserva { get; set; } = null!;
        public virtual metodos_pago metodo_pago { get; set; } = null!;
    }
}