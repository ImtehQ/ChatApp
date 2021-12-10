using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace FluentResponses.Interfaces
{
    public interface IResponse
    {
        object GetAttachment();
        T GetAttachment<T>();
        bool GetValid();
        Response SetAttachment(object value);
        Response SetAttachment<T>(T value);
        Response SetValid(bool value);
        T SetAttachmentReturn<T>(T value);
        HttpStatusCode GetStatusCode();
    }
}
