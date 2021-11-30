using ChatApp.Domain.Attributes;

namespace ChatApp.Domain.Enums
{
    public enum AccountRoleEnum
    {
        [StringValue("User")]
        RoleUser = 0,
        [StringValue("Moderator")]
        RoleModerator = 1,
        [StringValue("Admin")]
        RoleAdmin = 2,

    }
}
