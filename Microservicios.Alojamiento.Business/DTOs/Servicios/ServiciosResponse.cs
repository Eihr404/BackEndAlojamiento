using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Servicios
{
    public class ServiciosResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
}
