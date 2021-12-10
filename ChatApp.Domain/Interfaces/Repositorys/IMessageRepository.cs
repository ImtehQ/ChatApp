using ChatApp.Domain.Models;
using System.Collections.Generic;

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
