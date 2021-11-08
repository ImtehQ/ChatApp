using System;
namespace ChatApp.Domain.Models
{
    public class User
    {
        public int Id {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email {  get; set; }
        public string PasswordHash {  get; set; }
        public DateTime Created {  get; set; }
        public DateTime LastUpdated { get; set; }
        public int RoleId {  get; set; }
        public bool isBlocked {  get; set; }
        public bool RequiresVerification { get; set; }
        public Message[] messagesIds {  get; set; }
    }
}
