using ChapApp.Business.Domain.Interfaces;
using ChatApp.Business.Core.DbContexts;
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
    public class EFGroupRepository : IGroupRepository
    {
        private ChatAppContext context;

        public EFGroupRepository(ChatAppContext context)
        {
            this.context = context;
        }

        public IEnumerable<Group> GetGroups()
        {
            return context.Groups.ToList();
        }

        public Group GetGroupByID(int id)
        {
            return context.Groups.Find(id);
        }

        public void InsertGroup(Group Group)
        {
            context.Groups.Add(Group);
        }

        public void DeleteGroup(int GroupID)
        {
            Group Group = context.Groups.Find(GroupID);
            context.Groups.Remove(Group);
        }

        public void UpdateGroup(Group Group)
        {
            context.Entry(Group).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}