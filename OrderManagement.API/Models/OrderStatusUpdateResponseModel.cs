namespace OrderManagement.API.Models
{
    public class OrderStatusUpdateResponseModel
    {
        public DateTime UpdatedAt { get; set; }

        public int Status { get; set; }

        public string ErrorMessage { get; set; }
    }
}
