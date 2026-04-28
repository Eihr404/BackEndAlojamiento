using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Models
{
    public class alojamientosFiltroDataModel
    {
        public string? Ciudad { get; set; }
        public decimal? PrecioMaximo { get; set; }
        public string? TipoAlojamiento { get; set; }
        public int Pagina { get; set; } = 1;
        public int RegistrosPorPagina { get; set; } = 10;
    }
}
