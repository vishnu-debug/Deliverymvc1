using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Deliverymvc1.Models
{
    public class Executiveres
    {
        [Key]
        public int ExrequestID { get; set; }
        public bool Status { get; set; }
        public int ExecutiveID { get; set; }
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public int RequestID { get; set; }
        public string Address { get; set; }
        



        public double Price { get; set; }




        public Userreq Userreq { get; set; }




       
    }
}
