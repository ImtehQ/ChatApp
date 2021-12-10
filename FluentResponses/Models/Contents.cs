namespace FluentResponses.Models
{
    public class Contents
    {
        internal string Message { get; set; }
        internal object Content { get; init; }
        internal Contents(object content, string message = "")
        {
            Content = content;
            Message = message;
        }
    }
}
