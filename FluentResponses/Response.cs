using FluentResponses.Interfaces;
using System;
using System.Net;
using System.Reflection;

namespace FluentResponses
{
    public class Response : IResponse
    {
        internal Type Source { get; init; }
        internal ParameterInfo[] Parameters { get; init; }
        internal MethodInfo Invoker { get; init; }

        internal string SourceString { get; set; }
        internal HttpStatusCode StatusCode { get; set; }
        internal string Message { get; set; }
        internal object Attachment { get; set; }
        internal bool Valid { get; set; }

        internal Response(object Caller, string MethodName)
        {
            Type Source = Caller.GetType();
            Invoker = Source.GetMethod(MethodName);
            SourceString = Source.Name;
            if (Invoker != null)
                Parameters = Invoker.GetParameters();
        }
        internal void Copy(Response copyFrom)
        {
            Message = copyFrom.Message;
            Attachment = copyFrom.Attachment;
            StatusCode = copyFrom.StatusCode;
            SourceString += "-" + copyFrom.SourceString;
            Valid = copyFrom.Valid;
        }

        public HttpStatusCode GetStatusCode()
        {
            return StatusCode;
        }
        public bool GetValid()
        {
            return this.Valid;
        }
        public Response SetValid(bool value)
        {
            this.Valid = value;
            return this;
        }
        public object GetAttachment()
        {
            return Attachment;
        }
        public T GetAttachment<T>()
        {
            return (T)Attachment;
        }

        public Response SetAttachment(object value, bool autoValidate = true)
        {
            Attachment = value;
            if (autoValidate)
                ValidateAttachment();
            return this;
        }

        public T SetAttachmentReturn<T>(T value, bool autoValidate = true)
        {
            SetAttachment(value, autoValidate);
            return value;
        }

        private void ValidateAttachment()
        {
            if (Attachment == null)
                Valid = false;
            else
                Valid = true;
        }
    }
}
