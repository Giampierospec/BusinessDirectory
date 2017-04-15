using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDirectory.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="El campo Email es requerido")]
        [EmailAddress(ErrorMessage = "El email es inválido")]
        public string Email{ get; set; }

        [Required(ErrorMessage ="El campo contraseña es requerido")]
        [StringLength(16, MinimumLength = 8,ErrorMessage ="La contraseña no debe ser menor a 8 carácteres y no mayor a 16")]
        public string Password { get; set; }
    }
}
