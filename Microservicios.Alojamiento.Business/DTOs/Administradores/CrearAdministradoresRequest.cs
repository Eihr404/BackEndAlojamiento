using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Administradores
{
    public class CrearAdministradoresRequest
    {
        public Guid UsuarioId { get; set; } // Vinculación con la tabla Usuarios
        public string NombreComercial { get; set; } = string.Empty;
        public string NitTax { get; set; } = string.Empty;
        public string TelefonoSoporte { get; set; } = string.Empty;
    }
}
