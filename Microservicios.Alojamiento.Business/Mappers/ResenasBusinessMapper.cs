using Microservicios.Alojamiento.Business.DTOs.Resenas;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class ResenasBusinessMapper
    {
        // De DataModel a Response (Lectura para la API)
        public static ResenaResponse ToResponse(resenasDataModel model)
        {
            return new ResenaResponse
            {
                Id = model.Id,
                ClienteId = model.ClienteId,
                AlojamientoId = model.AlojamientoId,
                Estrellas = model.Estrellas,
                Comentario = model.Comentario,
                Fecha = model.Fecha
                // Nota: Si agregaste NombreCliente al DTO ResenaResponse, mapealo aquí:
                // NombreCliente = model.NombreCliente
            };
        }

        // De Request a DataModel (Creación de nueva reseña)
        public static resenasDataModel ToDataModel(CrearResenaRequest request)
        {
            return new resenasDataModel
            {
                ClienteId = request.ClienteId,
                AlojamientoId = request.AlojamientoId,
                Estrellas = request.Estrellas,
                Comentario = request.Comentario,
                Fecha = DateTime.UtcNow // La fecha se captura al momento de crear 
            };
        }

        // De Request a DataModel (Actualización de reseña existente)
        public static resenasDataModel ToDataModel(ActualizarResenasRequest request)
        {
            return new resenasDataModel
            {
                Id = request.Id,
                Estrellas = request.Estrellas,
                Comentario = request.Comentario
                // Los IDs de cliente y alojamiento no suelen cambiar en una actualización
            };
        }
    }
}