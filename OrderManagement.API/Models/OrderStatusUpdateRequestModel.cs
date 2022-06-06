namespace OrderManagement.API.Models
{
    public class OrderStatusUpdateRequestModel
    {
        public string CustomerOrderNo { get; set; }

        public int StatusId { get; set; }
    }
}
