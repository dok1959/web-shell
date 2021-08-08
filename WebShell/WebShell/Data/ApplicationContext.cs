using Microsoft.EntityFrameworkCore;
using WebShell.Models;

namespace WebShell.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Command> Registrations { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
