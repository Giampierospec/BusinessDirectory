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
        [Required]
        public string Email { get; set; }

        public string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string LastName { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
