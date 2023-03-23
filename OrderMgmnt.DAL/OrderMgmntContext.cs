using Microsoft.EntityFrameworkCore;
using OrderMgmnt.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgmnt.DAL
{
    public class OrderMgmntContext : DbContext
    {
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Vender> Venders { get; set; }


    }
}
