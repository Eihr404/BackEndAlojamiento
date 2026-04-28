using Microservicios.Alojamiento.Business.DTOs.Resenas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microservicios.Alojamiento.Business.Interfaces
{
    public interface IResenasService
    {
        Task<ResenaResponse> PublicarResenaAsync(CrearResenaRequest request);
        Task<IEnumerable<ResenaResponse>> ObtenerResenasPorAlojamientoAsync(Guid alojamientoId);
        Task<ResenaResponse> ObtenerPorIdAsync(Guid id);
        Task<ResenaResponse> ActualizarResenaAsync(ActualizarResenasRequest request);
        Task EliminarResenaAsync(Guid id);
    }
}
