namespace OrderManagement.API.Models
{
    public class CreateOrderRequestModel
    {
        public IEnumerable<CreateOrderRequestItem> CreateOrderRequestItems { get; set; }
    }

    public class CreateOrderRequestItem
    {
        public string CustomerOrderNo { get; set; }

        public string OutputAddress { get; set; }

        public string DestinationAddress { get; set; }

        public decimal Quantity { get; set; }

        public string QuantityUnit { get; set; }

        public decimal Weight { get; set; }

        public string WeightUnit { get; set; }

        public OrderItem OrderItem { get; set; }

        public string Note { get; set; }
    }

    public class OrderItem
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
