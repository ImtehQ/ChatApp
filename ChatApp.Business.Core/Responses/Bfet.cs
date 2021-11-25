using ChatApp.Business.Domain.Responses;
using ChatApp.Domain.Enums.ResponseCodes;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace ChatApp.Business.Core.Responses
{
    public class Bfet : IResponse
    {
        public bool Valid { get; private set; }
        public HttpStatusCode Code { get; set; }
        public MethodCode MethodCode { get; init; }
        public LayerCode LayerCode { get; init; }
        public object Note { get; init; }
        public string Report { get; private set; }
        public object Echo { get; private set; }
        public List<IResponse> Children { get; private set; }

        public Bfet(Bfet Parent, MethodCode Method, LayerCode Layer, object Content)
        {
            Parent.Link(new Bfet(Method, Layer, Content));
        }
        public Bfet(MethodCode Method, LayerCode Layer, object Content)
        {
            MethodCode = Method;
            LayerCode = Layer;
            Note = Content;
        }

        public bool GetValid()
        {
            if (Valid == false)
                return false;
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i].Valid == false)
                    return false;
            }
            return true;
        }

        public T GetResponseObject<T>()
        {
            return (T)Echo;
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
            Report = message;
            return this;
        }

        public IResponse Successfull(object responseObject)
        {
            Valid = true;
            Code = HttpStatusCode.OK;
            Echo = responseObject;
            return this;
        }

        public IResponse Successfull(string message, object responseObject)
        {
            Valid = true;
            Code = HttpStatusCode.OK;
            Report = message;
            Echo = responseObject;
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
            Report = message;
            return this;
        }

        public IResponse Successfull(HttpStatusCode statusCode, object responseObject)
        {
            Valid = true;
            Code = statusCode;
            Echo = responseObject;
            return this;
        }
        public IResponse Successfull(HttpStatusCode statusCode, string message, object responseObject)
        {
            Valid = true;
            Code = statusCode;
            Echo = responseObject;
            Report = message;
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
            Report = message;
            return this;
        }

        public IResponse Link(IResponse response)
        {
            Children.Add(response);
            return this;
        }
        public IResponse Link(IResponse[] responses)
        {
            Children.AddRange(responses);
            return this;
        }
    }
}
