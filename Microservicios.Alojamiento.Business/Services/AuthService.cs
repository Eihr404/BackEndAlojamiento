using Microservicios.Alojamiento.Business.DTOs.Auth;
using Microservicios.Alojamiento.Business.Interfaces;
using Microservicios.Alojamiento.DataAccess.Entities;
using Microservicios.Alojamiento.DataManagement.Interfaces;
using Microsoft.Extensions.Configuration;
using NotFoundException = Microservicios.Alojamiento.Business.Exceptions.NotFoundException;
using ValidationException = Microservicios.Alojamiento.Business.Exceptions.ValidationException;


namespace Microservicios.Alojamiento.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default)
        {
            var usuario = await _unitOfWork.UsuarioQueryRepository.ObtenerPorEmailAsync(request.Email);

            if (usuario == null || usuario.password_hash != request.Password)
                throw new UnauthorizedAccessException("Credenciales incorrectas.");

            // Usar el método que ya hace JOIN con la tabla roles
            var claims = await _unitOfWork.UsuarioQueryRepository
                .GetUserClaimsDataAsync(usuario.id);

            // Extraer los nombres de roles del objeto anónimo
            var listaRoles = new List<string>();
            if (claims != null)
            {
                var rolesProperty = claims.GetType().GetProperty("Roles");
                var roles = rolesProperty?.GetValue(claims) as IEnumerable<object>;
                if (roles != null)
                    listaRoles = roles.Select(r => r.ToString()!).ToList();
            }

            if (listaRoles.Count == 0)
                listaRoles.Add("Invitado");

            return new LoginResponse
            {
                UsuarioId = usuario.id,
                UserName = usuario.email,
                CorreoElectronico = usuario.email,
                NombreCompleto = "Usuario de Alojamiento",
                Roles = listaRoles,
                Token = string.Empty,
                ExpirationUtc = DateTime.UtcNow
            };
        }

        public async Task RegistrarUsuarioAsync(LoginRequest request)
        {
            var existente = await _unitOfWork.UsuarioQueryRepository.ObtenerPorEmailAsync(request.Email);
            if (existente != null)
                throw new ValidationException("El correo electrónico ya se encuentra registrado.");

            // 1. Crear usuario
            var entity = new usuarios
            {
                email = request.Email,
                password_hash = request.Password,
                activo = true,
                fecha_creacion = DateTime.UtcNow
            };

            await _unitOfWork.UsuarioRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            // 2. Asignar rol Cliente
            var rolCliente = new usuario_roles
            {
                usuario_id = entity.id,
                rol_id = Guid.Parse("a3b95c74-f10c-4c8c-af84-91c05f2bad41")
            };
            await _unitOfWork.Usuario_rolesRepository.AddAsync(rolCliente);

            // 3. Crear perfil de cliente
            var cliente = new clientes
            {
                usuario_id = entity.id,
                nombre = request.Email.Split('@')[0],
                apellido = "Usuario"
            };
            await _unitOfWork.ClientesRepository.AddAsync(cliente);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RegistrarSocioAsync(LoginRequest request)
        {
            var existente = await _unitOfWork.UsuarioQueryRepository.ObtenerPorEmailAsync(request.Email);
            if (existente != null)
                throw new ValidationException("El correo electrónico ya se encuentra registrado.");

            // 1. Crear usuario
            var entity = new usuarios
            {
                email = request.Email,
                password_hash = request.Password,
                activo = true,
                fecha_creacion = DateTime.UtcNow
            };

            await _unitOfWork.UsuarioRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync(); // ← guardar para obtener el id real

            // 2. Asignar rol Socio
            var rolSocio = new usuario_roles
            {
                usuario_id = entity.id,
                rol_id = Guid.Parse("fb31148b-6c36-4057-a109-5d783a3200cf")
            };
            await _unitOfWork.Usuario_rolesRepository.AddAsync(rolSocio);

            // 3. Crear registro en administradores
            var admin = new administradores
            {
                usuario_id = entity.id,
                nombre_comercial = request.Email, // valor inicial, el socio lo puede editar después
                nit_tax = "",
                telefono_soporte = ""
            };
            await _unitOfWork.AdministradoresRepository.AddAsync(admin);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}