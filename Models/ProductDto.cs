using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Week_1.Models
{
    public class ProductDto
    {
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name should be between 1-50 characters")]
        public string Name { get; set; }

        [Range(0, 1000)]
        public double Price { get; set; }

        [Required]
        public int UnitsInStock { get; set; }

        [MaxLength(200, ErrorMessage = "Maximum length should be 200 character")]
        public string Description { get; set; }
    }
}
