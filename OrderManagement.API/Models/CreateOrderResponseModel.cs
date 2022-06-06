namespace OrderManagement.API.Models
{
    public class CreateOrderResponseModel
    {
        public List<CreateOrderResponseItem> CreateOrderResponseItems { get; set; } = new List<CreateOrderResponseItem> { };
    }

    public class CreateOrderResponseItem
    {
        public string CustomerOrderNo { get; set; }
        public string SystemOrderNo { get; set; }
        public int Status { get; set; }
        public string ErrorMessage { get; set; }
    }
}
