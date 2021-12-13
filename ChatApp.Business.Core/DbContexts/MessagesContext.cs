using ChatApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Business.Core.DbContexts
{
    public class MessagesContext : DbContext
    {
        public DbSet<Message> set { get; set; }

        public MessagesContext(DbContextOptions<MessagesContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
