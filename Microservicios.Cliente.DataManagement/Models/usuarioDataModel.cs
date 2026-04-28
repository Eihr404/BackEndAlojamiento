using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Models
{
    public class usuariosDataModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}