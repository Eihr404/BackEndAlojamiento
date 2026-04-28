using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Metodos_pago
{
    public class ActualizarMetodos_pagoRequest : CrearMetodo_pagoRequest
    {
        public Guid Id { get; set; }
    }
}
