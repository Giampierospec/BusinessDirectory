using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDirectory.ViewModels
{
    public class CategoryViewModel
    {
        [Required]
        [StringLength(25, MinimumLength = 5)]
        public string Name { get; set; }
    }
}
