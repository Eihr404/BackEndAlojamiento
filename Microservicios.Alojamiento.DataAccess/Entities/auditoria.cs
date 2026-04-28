using System;

using System;

namespace Microservicios.Alojamiento.DataAccess.Entities
{
    public class auditoria
    {
        public int id { get; set; }
        public string accion { get; set; } = string.Empty;
        public string tabla_afectada { get; set; } = string.Empty;
        public Guid? registro_id { get; set; }
        public Guid? usuario_id { get; set; } // El ? es vital aquí
        public DateTime? fecha_hora { get; set; }
        public string? datos_anteriores { get; set; } // Representa el JSONB
    }
}