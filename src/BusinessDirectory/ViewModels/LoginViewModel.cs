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
        [Required]
        public string Email{ get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
