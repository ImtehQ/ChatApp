using ChatApp.Business.Core.Validator;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.MarkExtentions;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ChatApp.Business.Core.Services
{
    public class MessageService : IMessageService
    {
        IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public IResponse GetAllMessages(int groupId, int pageNr)
        {
            IResponse response = this.CreateResponse();

            List<Message> messages = _messageRepository.GetMessages()
                .Where(m => m.GroupId == groupId)
                .Skip(((pageNr - 1) * 10))
                .Take(10)
                .ToList();

            response.SetAttachment(messages);
            return response.Successfull();
        }

        public IResponse GetAllMessages(int groupId, int pageNr, string query)
        {
            IResponse response = this.CreateResponse();

            List<Message> messages = _messageRepository.GetMessages()
                .Where(m => m.GroupId == groupId && m.Content.Contains(query))
                .Skip(((pageNr - 1) * 10))
                .Take(10).ToList();

            response.SetAttachment(messages);
            return response.Successfull();
        }

        public IResponse SendMessage(string message, User sender, GroupTypeEnum groupType, int groupId)
        {
            IResponse response = this.CreateResponse();

            response.Include(MessageContentValidator.CheckContent(message));
            if (response.GetValid() == false)
                return response.Failed();

            Message m = new Message() { Content = message, SenderId = sender, GroupId = groupId };
            _messageRepository.InsertMessage(m);
            _messageRepository.Save();


            return response.Successfull();
        }


    }
}
