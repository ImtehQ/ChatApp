using ChatApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Business.Core.DbContexts
{
    public class UsersContext : DbContext
    {
        public DbSet<User> set { get; set; }

        public UsersContext(DbContextOptions<UsersContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
