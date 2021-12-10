using ChatApp.Domain.Enums;

namespace ChatApp.Domain.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public GroupTypeEnum type { get; set; }
        public string Name { get; set; }
        public int MaxUsers { get; set; }
        public GroupVisibilityEnum VisibilityType { get; set; }
        public string Password { get; set; }
    }
}
