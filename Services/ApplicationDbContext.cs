using Microsoft.EntityFrameworkCore;
using WATCHWAWE.Models;

namespace WATCHWAWE.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        { 
        
        }

        public DbSet<Upcoming> Upcomings { get; set; }
    }
}
