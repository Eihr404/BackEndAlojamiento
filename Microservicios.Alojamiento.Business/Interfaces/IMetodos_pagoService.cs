using Microservicios.Alojamiento.Business.DTOs.Metodos_pago;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.Interfaces
{
    public interface IMetodos_pagoService
    {
        Task<Metodo_pagoResponse> CrearMetodoPagoAsync(CrearMetodo_pagoRequest request);
        Task<Metodo_pagoResponse> ActualizarMetodoPagoAsync(ActualizarMetodos_pagoRequest request);
        Task EliminarMetodoPagoAsync(Guid id);
        Task<Metodo_pagoResponse?> ObtenerPorIdAsync(Guid id);
        Task<IEnumerable<Metodo_pagoResponse>> ObtenerTodosAsync();
    }
}
