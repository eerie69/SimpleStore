using KazahStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;

namespace KazahStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Store> Items { get; set; }
        public DbSet<Category> categories { get; set; }
    }
}
