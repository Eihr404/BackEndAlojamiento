using Microservicios.Alojamiento.Business.DTOs.Auth;
using Microservicios.Alojamiento.DataManagement.Models;

namespace Microservicios.Alojamiento.Business.Mappers
{
    public static class AuthBusinessMapper
    {
        public static usuariosDataModel ToDataModel(LoginRequest request)
        {
            return new usuariosDataModel
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };
        }

        // ✅ Corregido para coincidir con la nueva estructura de LoginResponse
        public static LoginResponse ToLoginResponse(string token, string email, string rol)
        {
            return new LoginResponse
            {
                Token = token,
                CorreoElectronico = email,          // Mapeado a la nueva propiedad
                UserName = email,                   // UserName requerido por tu DTO
                NombreCompleto = string.Empty,      // Propiedad requerida por tu DTO
                Roles = new List<string> { rol },   // Convertimos el string individual a una Lista
                ExpirationUtc = DateTime.UtcNow,
    };
        }
    }
}