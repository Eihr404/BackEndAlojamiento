using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Models
{
    public class alojamientosDataModel
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public decimal CalificacionAvg { get; set; }
        public bool TieneWifi { get; set; }
        public bool TienePiscina { get; set; }
        public bool AdmiteMascotas { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public Guid AdminId { get; set; }
    }
}
