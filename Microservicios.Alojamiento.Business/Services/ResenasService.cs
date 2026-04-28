using Microservicios.Alojamiento.Business.DTOs.Resenas; // Ajusta según tu namespace
using Microservicios.Alojamiento.Business.DTOs.Reservas;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.Business.Mappers;
using Microservicios.Alojamiento.DataAccess.Common;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microservicios.Alojamiento.DataManagement.Mappers;
using System.ComponentModel.DataAnnotations;
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;


namespace Microservicios.Alojamiento.Business.Services
{
    public class ResenasService : IResenasService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResenasService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ResenaResponse>> ObtenerResenasPorAlojamientoAsync(Guid alojamientoId)
        {
            // Usamos tu método paginado pasándole valores por defecto (página 1, límite 1000)
            var pagedEntities = await _unitOfWork.ResenasQueryRepository.GetByAlojamientoPagedAsync(
                alojamientoId,
                1,
                1000);

            // Mapeamos de entidad a modelo y luego a DTO de respuesta
            return pagedEntities.Items
                .Select(e => ResenasBusinessMapper.ToResponse(e.ToModel()))
                .ToList();
        }

        public async Task<ResenaResponse> ObtenerPorIdAsync(Guid id)
        {
            var entity = await _unitOfWork.ResenasQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró la reseña con ID: {id}");

            return ResenasBusinessMapper.ToResponse(entity.ToModel());
        }

        // ✅ CORREGIDO: Renombrado de CrearResenaAsync a PublicarResenaAsync
        public async Task<ResenaResponse> PublicarResenaAsync(CrearResenaRequest request)
        {
            if (request.Estrellas < 1 || request.Estrellas > 5)
                throw new ValidationException("La calificación debe estar entre 1 y 5 estrellas.");

            var dataModel = ResenasBusinessMapper.ToDataModel(request);
            dataModel.Id = Guid.NewGuid();

            var entity = dataModel.ToEntity();
            entity.cliente_id = request.ClienteId;
            entity.alojamiento_id = request.AlojamientoId;
            entity.estrellas = request.Estrellas;
            entity.comentario = request.Comentario;
            entity.fecha = DateTime.UtcNow;

            await _unitOfWork.ResenasRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            // ── NUEVO: recalcular calificacion_avg del alojamiento ──
            var todasLasResenas = await _unitOfWork.ResenasQueryRepository
                .GetByAlojamientoPagedAsync(request.AlojamientoId, 1, 10000);

            var promedio = todasLasResenas.Items.Average(r => (double)r.estrellas);

            var alojamiento = await _unitOfWork.AlojamientosQueryRepository
                .ObtenerPorIdAsync(request.AlojamientoId);

            if (alojamiento != null)
            {
                alojamiento.calificacion_avg = (decimal)Math.Round(promedio, 2);
                _unitOfWork.AlojamientosRepository.Update(alojamiento);
                await _unitOfWork.SaveChangesAsync();
            }
            // ────────────────────────────────────────────────────────

            return ResenasBusinessMapper.ToResponse(dataModel);
        }

        public async Task<ResenaResponse> ActualizarResenaAsync(ActualizarResenasRequest request)
        {
            if (request.Estrellas < 1 || request.Estrellas > 5)
                throw new ValidationException("La calificación debe estar entre 1 y 5 estrellas.");

            var entity = await _unitOfWork.ResenasQueryRepository.ObtenerPorIdAsync(request.Id);
            if (entity == null) throw new KeyNotFoundException("Reseña no encontrada.");

            // El cliente o alojamiento no se cambian, solo se actualiza la calificación y el comentario
            entity.estrellas = request.Estrellas;
            entity.comentario = request.Comentario;

            _unitOfWork.ResenasRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return ResenasBusinessMapper.ToResponse(entity.ToModel());
        }

        public async Task EliminarResenaAsync(Guid id)
        {
            var entity = await _unitOfWork.ResenasQueryRepository.ObtenerPorIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"No se encontró la reseña con ID: {id}");

            _unitOfWork.ResenasRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}