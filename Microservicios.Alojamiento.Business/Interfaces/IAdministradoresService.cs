using Microservicios.Alojamiento.Business.DTOs.Administradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.Interfaces
{
    public interface IAdministradoresService
    {
        Task<AdministradoresResponse> RegistrarAdministradorAsync(CrearAdministradoresRequest request);
        Task<AdministradoresResponse?> ObtenerPorUsuarioIdAsync(Guid usuarioId);
        Task<AdministradoresResponse> ActualizarAdministradorAsync(ActualizarAdministradoresRequest request);
        Task<AdministradoresResponse?> ObtenerPorIdAsync(Guid id);
        Task<AdministradoresResponse> CrearAdministradorAsync(CrearAdministradoresRequest request);

        // 🚨 Asegúrate de que esta línea exista:
        Task EliminarAdministradorAsync(Guid id);
    }
}
