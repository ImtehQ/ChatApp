using ChatApp.Business.Domain.Responses;
using System.Collections.Generic;
using System.Net;

namespace ChatApp.Domain.Interfaces
{
    public interface IResponse
    {
        bool Valid { get; }
        HttpStatusCode Code { get; set; }
        public IResponse Successfull();
        public IResponse Successfull(string message);
        public IResponse Successfull(object responseObject);
        public IResponse Successfull(string message, object responseObject);
        public IResponse Successfull(HttpStatusCode statusCode);
        public IResponse Successfull(HttpStatusCode statusCode, string message);
        public IResponse Successfull(HttpStatusCode statusCode, object responseObject);
        public IResponse Successfull(HttpStatusCode statusCode, string message, object responseObject);
        public IResponse Failed(HttpStatusCode statusCode);
        public IResponse Failed(HttpStatusCode statusCode, string message);
    }
}
