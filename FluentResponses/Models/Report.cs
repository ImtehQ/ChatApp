namespace FluentResponses.Models
{
    public class Report
    {
        internal string Content { get; init; }
        internal Report(string content)
        {
            Content = content;
        }
    }
}
