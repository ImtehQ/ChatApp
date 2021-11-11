using ChatApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Interfaces
{
    public interface IDataContext
    {
        DbSet<User> UserSet { get; set; }
        DbSet<Group> GroupSet { get; set; }
        DbSet<Message> MessageSet { get; set; }
    }
}
