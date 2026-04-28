using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Resenas
{
    public class ActualizarResenasRequest
    {
        public Guid Id { get; set; }
        public int Estrellas { get; set; }
        public string? Comentario { get; set; }
    }
}
