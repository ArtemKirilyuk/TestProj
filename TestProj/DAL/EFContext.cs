using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestProj.Models;

namespace TestProj.DAL
{
    public class EFContext : DbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<EndUser> EndUsers { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<User> Users { get; set; }

        public EFContext()
            : base("DefaultConnection")
        {

        }
    }
}