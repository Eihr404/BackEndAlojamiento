using System;

namespace Microservicios.Alojamiento.DataAccess.Entities
{
    public enum TipoItemDetalle { Habitacion, Servicio }

    public class reserva_detalles
    {
        public Guid id { get; set; }
        public Guid reserva_id { get; set; }
        public Guid item_id { get; set; } // Puede ser el ID de habitacion o servicio
        public string tipo_item { get; set; } = string.Empty;
        public int cantidad { get; set; } = 1;
        public decimal precio_capturado { get; set; } // ¡Importante! El precio histórico

        // Propiedad de navegación
        public virtual reservas reserva { get; set; } = null!;
    }
}