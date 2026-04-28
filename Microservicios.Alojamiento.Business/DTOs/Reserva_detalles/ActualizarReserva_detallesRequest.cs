using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Reserva_detalles
{
    public class ActualizarReserva_detallesRequest : CrearReserva_detallesRequest
    {
        public Guid Id { get; set; }
    }
}
