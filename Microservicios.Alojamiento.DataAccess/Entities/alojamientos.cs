using System;
using System.Collections.Generic;

namespace Microservicios.Alojamiento.DataAccess.Entities
{

    public class alojamientos
    {
        public Guid id { get; set; }
        public Guid admin_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public String tipo { get; set; }
        public string? descripcion { get; set; }
        public string ciudad { get; set; } = string.Empty;
        public string direccion { get; set; } = string.Empty;
        public decimal? latitud { get; set; }
        public decimal? longitud { get; set; }
        public TimeSpan check_in { get; set; }
        public TimeSpan check_out { get; set; }
        public int politica_cancelacion_horas { get; set; } = 48;
        public string? normas_adicionales { get; set; }
        public decimal calificacion_avg { get; set; } = 0.0m;

        // Amenities (Boleanos para filtros rápidos)
        public bool tiene_wifi { get; set; }
        public bool tiene_piscina { get; set; }
        public bool admite_mascotas { get; set; }
        public bool tiene_cocina { get; set; }

        // Propiedades de navegación
        public virtual administradores administrador { get; set; } = null!;
        public virtual ICollection<alojamiento_habitacion> habitaciones_configuradas { get; set; } = new List<alojamiento_habitacion>();
        public virtual ICollection<servicios_alojamiento> servicios_ofertados { get; set; } = new List<servicios_alojamiento>();
        public virtual ICollection<reservas> reservas { get; set; } = new List<reservas>();
        public virtual ICollection<resenas> resenas { get; set; } = new List<resenas>();
    }
}