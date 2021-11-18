using ChatApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChapApp.Business.Domain.Extensions;

namespace ChatApp.Business.Core.DbContexts
{
    public class ChatAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ChatAppContext(DbContextOptions<ChatAppContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //for (int i = 0; i < 5; i++)
            //{
            //    modelBuilder.Entity<User>().HasData(UserExtensions.New(i));
            //}

        }
    }
}
