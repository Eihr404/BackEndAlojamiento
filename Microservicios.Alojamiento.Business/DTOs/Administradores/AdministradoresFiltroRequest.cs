using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Administradores
{
    public class AdministradoresFiltroRequest
    {
        public string? NombreComercial { get; set; }
        public string? NitTax { get; set; }
        public int Pagina { get; set; } = 1;
        public int RegistrosPorPagina { get; set; } = 10;
    }
}
