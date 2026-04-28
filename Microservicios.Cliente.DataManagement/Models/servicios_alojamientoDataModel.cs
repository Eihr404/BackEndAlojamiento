using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Models
{
    public class servicios_alojamientoDataModel
    {
        public Guid Id { get; set; } // Agregado para coincidir con la PK del SQL
        public Guid AlojamientoId { get; set; }
        public Guid ServicioId { get; set; }
        public string? NombreServicio { get; set; }
        public decimal PrecioAdicional { get; set; } // Antes era Precio
        public bool EstaActivo { get; set; }
    }
}
