using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Alojamientos
{
    public class AlojamientoFiltroRequest
    {
        public Guid? AdminId { get; set; } // ← agregar

        // Filtros geográficos y básicos
        public string? Ciudad { get; set; }
        public string? Tipo { get; set; } // Hotel, Apartamento, etc.
        public string? Nombre { get; set; }

        // Filtros de calificación
        public decimal? CalificacionMinima { get; set; }

        // Filtros de Amenities (para búsquedas específicas)
        public bool? TieneWifi { get; set; }
        public bool? TienePiscina { get; set; }
        public bool? AdmiteMascotas { get; set; }
        public bool? TieneCocina { get; set; }

        // Paginación (Sugerencia técnica para la capa de negocio)
        public int Pagina { get; set; } = 1;
        public int RegistrosPorPagina { get; set; } = 10;
    }
}
