using ChapApp.Business.Domain.Extensions;
using ChatApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.DBContexts
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }


        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            for (int i = 1; i < 10; i++)
            {
                User user = new User();
                modelBuilder.Entity<User>().HasData(user.New(i));
            }

            

            modelBuilder.Entity<User>().ToTable("Users").HasKey(x => x.Id);
        }

    }
}
