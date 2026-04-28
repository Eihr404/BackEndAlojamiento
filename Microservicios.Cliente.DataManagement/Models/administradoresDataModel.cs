using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Models
{
    public class administradoresDataModel
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string? NombreComercial { get; set; } // Antes NombreEmpresa
        public string? NitTax { get; set; }          // Antes Ruc
        public string? TelefonoSoporte { get; set; }
    }
}
