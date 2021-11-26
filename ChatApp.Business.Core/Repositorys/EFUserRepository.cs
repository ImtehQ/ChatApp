using ChapApp.Business.Domain.Interfaces;
using ChatApp.Business.Core.DbContexts;
using ChatApp.Business.Core.Responses;
using ChatApp.Domain.Enums.ResponseCodes;
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
    public class EFUserRepository : IUserRepository
    {
        private ChatAppContext context;

        public EFUserRepository(ChatAppContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users.ToList();
        }

        public IResponse GetUserByID(int id)
        {
            Response response = new Response(MethodCode.GetUserById, LayerCode.Repo,
                new object[] { id });

            User user = context.Users.Find(id);

            if (user == null)
            {
                return response.Failed(System.Net.HttpStatusCode.NotFound);
            }

            return response.Successfull();
        }

        public void InsertUser(User User)
        {
            context.Users.Add(User);
        }

        public void DeleteUser(int UserID)
        {
            User User = context.Users.Find(UserID);
            context.Users.Remove(User);
        }

        public void UpdateUser(User User)
        {
            context.Entry(User).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
