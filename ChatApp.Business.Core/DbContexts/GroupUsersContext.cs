using ChatApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Business.Core.DbContexts
{
    public class GroupUsersContext : DbContext
    {
        public DbSet<GroupUser> set { get; set; }

        public GroupUsersContext(DbContextOptions<GroupUsersContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
