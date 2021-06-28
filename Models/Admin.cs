﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Deliverymvc1.Models
{
    public class Admin
    {
       
        [Key]
        public int AdminID { get; set; }
        [MaxLength(length: 30, ErrorMessage = "*Only 30 characters allowed")]
        [Display(Name = "User Name")]
        public string Name { get; set; }
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password atleast have 1 Uppercase, 1 Lowercase, 1 Number and 1 Special character")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
