namespace Microservicios.Alojamiento.Business.DTOs.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpirationUtc { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public Guid UsuarioId { get; set; } // ← agrega esto

    }
}