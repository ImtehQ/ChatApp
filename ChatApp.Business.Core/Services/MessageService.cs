using ChapApp.Business.Domain.Interfaces;
using ChatApp.Business.Core.Responses;
using ChatApp.Business.Core.Validator;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Enums.ResponseCodes;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Response response = new Response(ResponseMethodCode.GetMessages, ResponseLayerCode.Service,
               new object[] { groupId, pageNr });

            List<Message> messages = _messageRepository.GetMessages()
                .Where(m => m.GroupId == groupId)
                .Skip(((pageNr - 1) * 10))
                .Take(10)
                .ToList();

            return response.Successfull(messages);
        }

        public IResponse GetAllMessages(int groupId, int pageNr, string Query)
        {
            Response response = new Response(ResponseMethodCode.GetMessages, ResponseLayerCode.Service,
               new object[] { groupId, pageNr });

            List<Message> messages = _messageRepository.GetMessages()
                .Where(m => m.GroupId == groupId && m.Content.Contains(Query))
                .Skip(((pageNr - 1) * 10))
                .Take(10).ToList();

            return response.Successfull(messages);
        }

        public IResponse SendMessage(string message, User sender, GroupTypeEnum groupType, int groupId)
        {
            Response response = new Response(ResponseMethodCode.SendMessage, ResponseLayerCode.Service,
                new object[] { message, sender, groupType, groupId });

            var messageValidator = MessageContentValidator.CheckContent(message);
            if (messageValidator.Valid == false) return messageValidator;

            Message m = new Message() { Content = message, SenderId = sender, GroupId = groupId };
            _messageRepository.InsertMessage(m);
            _messageRepository.Save();

            return response.Successfull();
        }

        
    }
}
