using System;

namespace Microservicios.Alojamiento.DataAccess.Entities
{
    public class usuario_roles
    {
        public Guid usuario_id { get; set; }
        public virtual usuarios usuario { get; set; } = null!;

        public Guid rol_id { get; set; }
        public virtual roles rol { get; set; } = null!;
    }
}