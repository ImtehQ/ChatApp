namespace ChatApp.Domain.Models
{
    public class Invite
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Message { get; set; }
    }
}
