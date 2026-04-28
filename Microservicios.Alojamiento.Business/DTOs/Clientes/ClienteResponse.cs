using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Clientes
{
    public class ClienteResponse
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? DocumentoIdentidad { get; set; }
    }
}
