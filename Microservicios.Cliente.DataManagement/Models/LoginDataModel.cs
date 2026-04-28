using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataManagement.Models
{
    public class LoginDataModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}