using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Deliverymvc1.Models;

namespace Deliverymvc1.Data
{
    public class DeliverymvcContext : DbContext
    {
        public DeliverymvcContext (DbContextOptions<DeliverymvcContext> options)
            : base(options)
        {
        }

        public DbSet<Deliverymvc1.Models.Admin> Admin { get; set; }

        public DbSet<Deliverymvc1.Models.Login> Login { get; set; }

        public DbSet<Deliverymvc1.Models.Executiveres> Executiveres { get; set; }
    }
}
