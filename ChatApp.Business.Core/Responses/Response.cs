using ChatApp.Business.Domain.Responses;
using ChatApp.Domain.Enums.ResponseCodes;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace ChatApp.Business.Core.Responses
{
    public class Response : IResponse
    {
        public bool Valid { get; private set; }
        public HttpStatusCode Code { get; set; }
        public ResponseMethodCode MethodCode { get; init; }
        public ResponseLayerCode LayerCode { get; init; }
        public object Content { get; init; }
        public string Message { get; private set; }
        public object ResponseObject { get; private set; }

        public Response(ResponseMethodCode responseMethod, ResponseLayerCode responseLayer, object refContent)
        {
            MethodCode = responseMethod;
            LayerCode = responseLayer;
            Content = refContent;
        }

        public IResponse Successfull()
        {
            Valid = true;
            Code = HttpStatusCode.OK;
            return this;
        }

        public IResponse Successfull(string message)
        {
            Valid = true;
            Code = HttpStatusCode.OK;
            Message = message;
            return this;
        }

        public IResponse Successfull(object responseObject)
        {
            Valid = true;
            Code = HttpStatusCode.OK;
            ResponseObject = responseObject;
            return this;
        }

        public IResponse Successfull(string message, object responseObject)
        {
            Valid = true;
            Code = HttpStatusCode.OK;
            Message = message;
            ResponseObject = responseObject;
            return this;
        }

        public IResponse Successfull(HttpStatusCode statusCode)
        {
            Valid = true;
            Code = statusCode;
            return this;
        }

        public IResponse Successfull(HttpStatusCode statusCode, string message)
        {
            Valid = true;
            Code = statusCode;
            Message = message;
            return this;
        }

        public IResponse Successfull(HttpStatusCode statusCode, object responseObject)
        {
            Valid = true;
            Code = statusCode;
            ResponseObject = responseObject;
            return this;
        }
        public IResponse Successfull(HttpStatusCode statusCode, string message, object responseObject)
        {
            Valid = true;
            Code = statusCode;
            ResponseObject = responseObject;
            Message = message;
            return this;
        }

        public IResponse Failed(HttpStatusCode statusCode)
        {
            Valid = false;
            Code = statusCode;
            return this;
        }
        public IResponse Failed(HttpStatusCode statusCode, string message)
        {
            Valid = false;
            Code = statusCode;
            Message = message;
            return this;
        }
    }
}
