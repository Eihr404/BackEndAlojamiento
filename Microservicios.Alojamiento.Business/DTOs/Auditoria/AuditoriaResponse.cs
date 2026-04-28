using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Auditoria
{
    public class AuditoriaResponse
    {
        public int Id { get; set; } // En auditoría se usa serial/int 
        public Guid? UsuarioId { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string TablaAfectada { get; set; } = string.Empty;
        public Guid? RegistroId { get; set; }
        public string? DatosAnteriores { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
