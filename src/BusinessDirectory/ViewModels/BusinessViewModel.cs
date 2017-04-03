using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDirectory.ViewModels
{
    public class BusinessViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Address { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string Phone { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [Required]
        public string Description { get; set; }

        public string UserName { get; set; }
    }
}
