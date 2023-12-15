using Entities.Models;
using Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> opts):base(opts)
        {

        }

        public UserContext() { }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
