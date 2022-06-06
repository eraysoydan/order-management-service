namespace OrderManagement.API.Models
{
    public class GetOrdersResponseModel
    {
        public int Id { get; set; }
        public string CustomerOrderNo { get; set; }
        public string OutputAddress { get; set; }
        public string DestinationAddress { get; set; }
        public decimal Quantity { get; set; }
        public string QuantityUnit { get; set; }
        public decimal Weight { get; set; }
        public string WeightUnit { get; set; }
        public string ItemName { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
    }
}
