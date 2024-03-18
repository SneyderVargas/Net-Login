using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Core.Dtos
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Nombre de usuario es requerido")]
        public string NameUser { get; set; }
        [Required(ErrorMessage = "Contraseña es requerido ")]
        public string PasswordUser { get; set; }
    }
}
