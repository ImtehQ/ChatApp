using ChatApp.Domain.Attributes;

namespace ChatApp.Domain.Enums
{
    public enum GroupTypeEnum
    {
        [StringValue("algemeen")]
        OptionGeneral = 0,
        [StringValue("1-op-1")]
        OptionPrivate = 1,
        [StringValue("groep")]
        OptionGroup = 2
    }
}
