using System.Net;

namespace FluentResponses.Interfaces
{
    public interface IResponse
    {
        object GetAttachment();
        T GetAttachment<T>();
        bool GetValid();
        Response SetAttachment(object value, bool autoValidate = true);
        Response SetValid(bool value);
        T SetAttachmentReturn<T>(T value, bool autoValidate = true);
        HttpStatusCode GetStatusCode();
    }
}
