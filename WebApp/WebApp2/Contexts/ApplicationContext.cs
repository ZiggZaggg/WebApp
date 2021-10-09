using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp2.Models;

namespace WebApp2.Contexts
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
    }
}
