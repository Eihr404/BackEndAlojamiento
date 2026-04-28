using Microservicios.Alojamiento.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Interfaces
{
    public interface IusuariosDataService
    {
        Task<usuariosDataModel?> GetByEmailAsync(string email);
        Task<bool> RegistrarUsuarioAsync(usuariosDataModel usuario, string password);
        Task<bool> UpdatePerfilClienteAsync(clientesDataModel cliente);
        Task<bool> UpdatePerfilAdminAsync(administradoresDataModel admin);
    }
}
