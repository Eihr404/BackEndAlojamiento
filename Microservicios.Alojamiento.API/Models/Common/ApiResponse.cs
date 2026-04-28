namespace Microservicios.Alojamiento.API.Models.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        // Constructor vacío para serialización
        public ApiResponse() { }

        // Método estático para respuestas exitosas
        public static ApiResponse<T> Ok(T data, string message = "Operación exitosa")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message,
                Errors = null
            };
        }

        // Método estático para fallos (usado por el Middleware)
        public static ApiResponse<T> Fail(string message, IEnumerable<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Data = default,
                Message = message,
                Errors = errors
            };
        }
    }
}
