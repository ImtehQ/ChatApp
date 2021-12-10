namespace ChatApp.Domain.Models
{
    public class AuthenticationModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
