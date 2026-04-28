using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Metodos_pago
{
    public class Metodo_pagoResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
