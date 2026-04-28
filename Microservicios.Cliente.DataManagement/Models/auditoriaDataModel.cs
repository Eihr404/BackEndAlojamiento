using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Models
{
    public class auditoriaDataModel
    {
        public int Id { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string TablaAfectada { get; set; } = string.Empty;
        public Guid RegistroId { get; set; }

        public Guid? UsuarioId { get; set; }

        public DateTime FechaHora { get; set; }
        public string? DatosAnteriores { get; set; }
    }
}