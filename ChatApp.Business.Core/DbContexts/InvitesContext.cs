using ChatApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Business.Core.DbContexts
{
    public class InvitesContext : DbContext
    {
        public DbSet<Invite> set { get; set; }

        public InvitesContext(DbContextOptions<InvitesContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
