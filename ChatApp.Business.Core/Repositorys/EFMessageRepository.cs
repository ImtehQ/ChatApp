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
    public class EFMessageRepository : IMessageRepository
    {
        private ChatAppContext context;

        public EFMessageRepository(ChatAppContext context)
        {
            this.context = context;
        }

        public IEnumerable<Message> GetMessages()
        {
            return context.Messages.ToList();
        }

        public Message GetMessageByID(int id)
        {
            return context.Messages.Find(id);
        }

        public void InsertMessage(Message Message)
        {
            context.Messages.Add(Message);
        }

        public void DeleteMessage(int MessageID)
        {
            Message Message = context.Messages.Find(MessageID);
            context.Messages.Remove(Message);
        }

        public void UpdateMessage(Message Message)
        {
            context.Entry(Message).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}