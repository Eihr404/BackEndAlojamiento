using Microservicios.Alojamiento.Business.DTOs.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.Interfaces
{
    public interface IUsuariosService
    {
        Task<UsuariosResponse> CrearUsuarioAsync(CrearUsuariosRequest request);
        Task<UsuariosResponse?> ObtenerPorEmailAsync(string email);
        Task<UsuariosResponse?> ObtenerPorIdAsync(Guid id);
        Task<UsuariosResponse> ActualizarUsuarioAsync(ActualizarUsuariosRequest request);
        Task CambiarEstadoAsync(Guid id, bool activo);

        Task EliminarUsuarioAsync(Guid id);
    }
}
