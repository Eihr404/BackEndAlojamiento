namespace Microservicios.Alojamiento.Business.Exceptions
{
    // Clase base para todas las excepciones de negocio
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }
}