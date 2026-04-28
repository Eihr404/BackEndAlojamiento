using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Metodos_pago
{
    public class CrearMetodo_pagoRequest
    {
        public string Nombre { get; set; } = string.Empty; // Tarjeta, Efectivo, etc.
    }
}
