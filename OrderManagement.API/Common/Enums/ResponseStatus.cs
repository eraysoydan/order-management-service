using System.ComponentModel;

namespace OrderManagement.API.Common.Enums
{
    public enum ResponseStatus
    {
        [Description("Başarılı")]
        Success = 0,

        [Description("Başarısız")]
        Fail = 1
    }
}
