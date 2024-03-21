using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Core.Dtos
{
    public class RegisterUserRequestDto
    {
        [Required(ErrorMessage = "Correo de usuario es requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Contraseña de usuario es requerido")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Repetir contraseña de usuario es requerido")]
        public string PasswordRepeat { get; set; }
    }
}
