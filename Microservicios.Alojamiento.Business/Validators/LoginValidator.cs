using FluentValidation;
using Microservicios.Alojamiento.Business.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.Business.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
