using ChatApp.Business.Domain.Responses;
using ChatApp.Domain.Enums.ResponseCodes;
using System.Collections.Generic;
using System.Net;

namespace ChatApp.Domain.Interfaces
{
    public interface IResponse
    {
        bool Valid { get; }
        HttpStatusCode Code { get; set; }
        object Echo { get; }
        string Report { get; }
        object Note { get; init; }
        LayerCode LayerCode { get; init; }
        MethodCode MethodCode { get; init; }

        public IResponse Link(IResponse response);
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
        T GetResponseObject<T>();
        bool GetValid();
    }
}
