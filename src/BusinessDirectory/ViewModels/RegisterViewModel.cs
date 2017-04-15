using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDirectory.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="El campo email es requerido")]
        [EmailAddress(ErrorMessage ="El email es invalido")]
        public string Email { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage ="El campo nombre es requerido")]
        [StringLength(50, MinimumLength = 5,ErrorMessage ="El nombre debe ser por lo menos 5 carácteres y un máximo de 50")]
        public string Name { get; set; }

        [Required(ErrorMessage ="El campo apellido es requerido")]
        [StringLength(50, MinimumLength = 5, ErrorMessage ="El apellido debe contener mínimo 5 carácteres y máximo 50")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="El campo contraseña es requerido")]
        [StringLength(16, MinimumLength = 8, ErrorMessage ="La contraseña debe tener un mínimo de 8 carácteres y un máximo de  16")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "El numero de teléfono no es válido")]
        public string PhoneNumber { get; set; }
    }
}
