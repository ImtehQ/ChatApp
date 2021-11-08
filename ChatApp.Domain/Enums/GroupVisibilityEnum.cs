using ChatApp.Domain.Attributes;

namespace ChatApp.Domain.Enums
{
    public enum GroupVisibilityEnum
    {
        [StringValue("public")]
        OptionPublic = 0,
        [StringValue("private")]
        OptionPrivate = 1,
        [StringValue("hidden")]
        OptionHidden = 2
    }
}
