using Microservicios.Alojamiento.API.Extensions;
using Microservicios.Alojamiento.API.Middleware;
using Microservicios.Alojamiento.API.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de Settings (JWT)
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// 2. Registro de Servicios mediante Extensions
builder.Services.AddControllers();

// ✅ Corregido: Agregamos el prefijo "Add" que faltaba
builder.Services.AddApiVersioningExtension();

// ✅ Corregido: Nombre exacto de la extensión de Swagger
builder.Services.AddSwaggerExtension();

// ✅ Corregido: Nombre exacto de la extensión de Auth
builder.Services.AddAuthenticationExtension(builder.Configuration);

// ✅ Corregido: Nombre exacto de la extensión de CORS
builder.Services.AddCorsExtension();

// ❌ Eliminado: "AddServiceCollectionExtension" no existe, 
// ya que el método real es el de abajo:
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// 3. Configuración del Pipeline de Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservicios Alojamiento API V1");
    // Opcional: Esto hace que Swagger sea la página de inicio (https://url.com/)
    // c.RoutePrefix = string.Empty; 
});

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
