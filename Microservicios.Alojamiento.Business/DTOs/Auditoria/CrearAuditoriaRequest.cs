using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Auditoria
{
    public class CrearAuditoriaRequest
    {
        public Guid? UsuarioId { get; set; }
        public string Accion { get; set; } = string.Empty; // INSERT, UPDATE, DELETE 
        public string TablaAfectada { get; set; } = string.Empty;
        public Guid? RegistroId { get; set; }
        public string? DatosAnteriores { get; set; } // Formato JSON 
    }
}
