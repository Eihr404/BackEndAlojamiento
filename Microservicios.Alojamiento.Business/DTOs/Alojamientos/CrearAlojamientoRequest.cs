using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Alojamientos
{
    public class CrearAlojamientoRequest
    {
        public Guid AdminId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty; // Ej: Hotel, Apartamento, Casa, Hostal
        public string? Descripcion { get; set; }
        public string Ciudad { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public int PoliticaCancelacionHoras { get; set; } = 48;
        public string? NormasAdicionales { get; set; }

        // Amenities
        public bool TieneWifi { get; set; }
        public bool TienePiscina { get; set; }
        public bool AdmiteMascotas { get; set; }
        public bool TieneCocina { get; set; }
    }
}
