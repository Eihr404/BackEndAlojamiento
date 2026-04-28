using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Alojamientos
{
    public class AlojamientoResponse
    {
        public Guid Id { get; set; }
        public Guid AdminId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string Ciudad { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public int PoliticaCancelacionHoras { get; set; }
        public string? NormasAdicionales { get; set; }
        public decimal CalificacionAvg { get; set; }

        // Amenities
        public bool TieneWifi { get; set; }
        public bool TienePiscina { get; set; }
        public bool AdmiteMascotas { get; set; }
        public bool TieneCocina { get; set; }
    }
}
