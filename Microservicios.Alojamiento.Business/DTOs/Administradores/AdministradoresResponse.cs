using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Administradores
{
    public class AdministradoresResponse
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string NombreComercial { get; set; } = string.Empty;
        public string NitTax { get; set; } = string.Empty;
        public string TelefonoSoporte { get; set; } = string.Empty;
    }
}
