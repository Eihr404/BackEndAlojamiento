using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Models
{
    public class metodos_pagoDataModel
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        // Se elimina la propiedad Activo
    }
}