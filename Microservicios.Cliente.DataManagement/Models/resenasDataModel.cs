using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Models
{
    public class resenasDataModel
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public Guid AlojamientoId { get; set; }
        public string? NombreCliente { get; set; }
        public int Estrellas { get; set; }
        public string? Comentario { get; set; }
        public DateTime Fecha { get; set; }
    }
}
