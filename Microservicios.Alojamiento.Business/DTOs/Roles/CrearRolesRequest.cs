using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microservicios.Alojamiento.Business.DTOs.Roles
{
    public class CrearRolesRequest
    {
        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "La descripción no puede superar los 255 caracteres")]
        public string? Descripcion { get; set; }
    }
}
