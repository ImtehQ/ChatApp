using ChapApp.Business.Domain.Interfaces;
using ChapApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapApp.Business.Core.Repositorys
{
    public class DataContext<T> : DbContext where T : class
    {
        public DbSet<T> Set { get; set; }

        public DataContext(DbContextOptions<DataContext<T>> options) : base(options){}
    }

    public class EFRepository<T> : IRepository<T> where T : DataContext<T>
    {
        public T Context { get; set; }
        public EFRepository(T dataContext)
        {
            Context = dataContext;
        }
    }

    public class UserService : IUserService
    {
        public IRepository<DataContext<User>> UserRepository;
        public UserService(IRepository<DataContext<User>> Repository)
        {
            UserRepository = Repository;
        }

        public bool Exist(string username, string password)
        {
            return UserRepository.Context.Set.Any(user => 
            user.UserName == username &&
            user.PasswordHash == password) ? true : false;
        }
    }
    public class GroupService : IGroupService
    {
        public IRepository<DataContext<Group>> UserRepository;
        public GroupService(IRepository<DataContext<Group>> Repository)
        {
            UserRepository = Repository;
        }

        public bool Exist(string groupName)
        {
            return UserRepository.Context.Set.Any(group =>
            group.Name == groupName) ? true : false;
        }
    }
}
