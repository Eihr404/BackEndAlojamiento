namespace Microservicios.Alojamiento.Business.Exceptions
{
    public class UnauthorizedBusinessException : BusinessException
    {
        public UnauthorizedBusinessException(string message) : base(message) { }
    }
}