using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Clientes
{
    public class ClienteFiltroRequest
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? DocumentoIdentidad { get; set; }
        public Guid? UsuarioId { get; set; }   // ← AGREGAR

        public int Pagina { get; set; } = 1;
        public int RegistrosPorPagina { get; set; } = 10;
    }
}
