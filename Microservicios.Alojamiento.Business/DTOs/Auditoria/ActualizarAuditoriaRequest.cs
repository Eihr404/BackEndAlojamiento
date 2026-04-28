using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Auditoria
{
    namespace Microservicios.Alojamiento.Business.DTOs.Auditoria
    {
        public class ActualizarAuditoriaRequest : CrearAuditoriaRequest
        {
            /// <summary>
            /// El ID de auditoría es de tipo int (SERIAL en PostgreSQL).
            /// </summary>
            public int Id { get; set; }
        }
    }
}
