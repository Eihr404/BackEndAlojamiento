using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Models
{
    public class habitacionesDataModel
    {
        public Guid Id { get; set; }
        public string NombreTipo { get; set; } = string.Empty;
        public int CapacidadPersonas { get; set; }
        public int NumeroCamas { get; set; }
    }
}
