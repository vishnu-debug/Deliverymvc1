using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Deliverymvc1.Models
{
    public class Executive
    {
        public int ExecutiveID { get; set; }

        [Required]
        [MaxLength(length: 20, ErrorMessage = "Only 20 characters allowed.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(length: 20, ErrorMessage = "*Only 20 characters allowed")]
        public string Name { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 8)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public int Age { get; set; }

        [Required]
        [MaxLength(length: 10, ErrorMessage = "*Only 10 numbers allowed")]
        [MinLength(length: 10, ErrorMessage = "*Must have 10 numbers")]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public bool IsVerified { get; set; }
        public string Type { get; set; }
    }
}
