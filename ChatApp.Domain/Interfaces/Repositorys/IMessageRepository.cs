using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Interfaces
{
    public interface IMessageRepository
    {
        void DeleteMessage(int MessageID);
        Message GetMessageByID(int id);
        IEnumerable<Message> GetMessages();
        void InsertMessage(Message Message);
        void Save();
        void UpdateMessage(Message Message);
    }
}
