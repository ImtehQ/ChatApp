namespace FluentResponses.Models
{
    public class Status
    {
        internal bool Value { get; set; }
        internal Status(bool value)
        {
            Value = value;
        }

    }
}
