using System.ComponentModel;

namespace OrderManagement.API.Common.Enums
{
    public enum OrderStatus
    {
        [Description("Sipariş Alındı")]
        OrderReceived = 1,

        [Description("Yola Çıktı")]
        OnTheRoad = 2,

        [Description("Dağıtım Merkezinde")]
        AtDistributionCenter = 3,

        [Description("Dağıtıma Çıktı")]
        InDistribution = 4,

        [Description("Teslim Edildi")]
        Delivered = 5,

        [Description("Teslim Edilemedi")]
        NotDelivered = 6
    }
}
