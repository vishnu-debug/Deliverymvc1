using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Deliverymvc1.Models
{
    public class New
    {
        [Key]
        public int ExrequestID { get; set; }
        public bool Status { get; set; }
        public double Price { get; set; }
        public int RequestID { get; set; }
        public int CustomerID { get; set; }

    }
}
