using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Deliverymvc1.Models
{
    public class Userreq
    {
        [Key]
        public int RequestID { get; set; }
        public DateTime DTofPickup { get; set; }       
        public double WeightOfPackage { get; set; }
        public string Address { get; set; }
        public int ExecutiveID { get; set; }
        public int CustomerID { get; set; }
        public string City{ get; set; }
        public string Name { get; set; }

    }
}
