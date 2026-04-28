namespace Microservicios.Alojamiento.API.Models.Common
{
    public class ApiErrorResponse
    {
        public string Title { get; set; } = "Uno o más errores han ocurrido.";
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public IEnumerable<string>? Errors { get; set; }
        public string? TraceId { get; set; }

        public ApiErrorResponse() { }

        public ApiErrorResponse(string message, int status, IEnumerable<string>? errors = null)
        {
            Status = status;
            Message = message;
            Errors = errors;
        }
    }
}
