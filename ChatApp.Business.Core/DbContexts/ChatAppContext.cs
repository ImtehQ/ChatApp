using ChatApp.Business.Core.Cryptography;
using ChatApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using RandomNameGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.DbContexts
{
    public class ChatAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Invite> Invites { get; set; }

        public ChatAppContext(DbContextOptions<ChatAppContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var pg = new PersonNameGenerator();
            //for (int i = 0; i < 10; i++)
            //{
            //    _userRepository.InsertUser(new User
            //    {
            //        FirstName = pg.GenerateRandomFirstName(),
            //        LastName = pg.GenerateRandomLastName(),
            //        Email = pg.GenerateRandomFirstAndLastName() + "@gmail.com",
            //        UserName = pg.GenerateRandomFirstName(),
            //        isBlocked = false,
            //        Created = DateTime.Now,
            //        LastUpdated = DateTime.Now,
            //        Role = 0,
            //        PasswordHash = Rfc2898.Convert("aaaaaaaaaa", "bbbbbbbbbb")
            //    });
            //    _userRepository.Save();
            //}
        }
    }
}
