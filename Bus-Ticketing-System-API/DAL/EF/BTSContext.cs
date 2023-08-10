using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class BTSContext:DbContext
    {
        public DbSet<admin> admin { get; set; }
        public DbSet<bus> bus { get; set; }
        public DbSet<busProvider> busProvider { get; set; }
        public DbSet<customer> customer { get; set; }
        public DbSet<discountCupon> discountCupon { get; set; }
        public DbSet<employee> employee { get; set; }
        public DbSet<notice> notice { get; set; }
        public DbSet<place> place { get; set; }
        public DbSet<ticket> ticket { get; set; }
        public DbSet<token> token { get; set; }
        public DbSet<trip> trip { get; set; }
        public DbSet<user> user { get; set; }
    }
}
