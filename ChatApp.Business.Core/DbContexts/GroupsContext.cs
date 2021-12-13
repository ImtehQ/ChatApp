using ChatApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Business.Core.DbContexts
{
    public class GroupsContext : DbContext
    {
        public DbSet<Group> set { get; set; }

        public GroupsContext(DbContextOptions<GroupsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
