using Microservicios.Alojamiento.Business.DTOs.Auth;

namespace Microservicios.Alojamiento.Business.Interfaces
{
    public interface IAuthService
    {
        // ✅ Añadimos el CancellationToken para que coincida con el Servicio y el Controlador
        Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
        Task RegistrarUsuarioAsync(LoginRequest request);
        Task RegistrarSocioAsync(LoginRequest request);
    }
}