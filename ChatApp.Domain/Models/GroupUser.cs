using ChatApp.Domain.Enums;

namespace ChatApp.Domain.Models
{
    public class GroupUser
    {
        public int Id { get; set; }
        public Group Group { get; set; }
        public User User { get; set; }
        public AccountRoleEnum AccountRole { get; set; }
    }
}
