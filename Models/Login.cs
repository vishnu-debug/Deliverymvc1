﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Deliverymvc1.Models
{
    public class Login
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }
    }
}
