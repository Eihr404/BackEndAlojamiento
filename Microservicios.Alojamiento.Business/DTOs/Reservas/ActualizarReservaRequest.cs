using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Reservas
{
    public class ActualizarReservaRequest : CrearReservaRequest
    {
        public Guid Id { get; set; }
        public string Estado { get; set; } = "Pendiente"; // Corresponde al ENUM estado_reserva 
    }
}
