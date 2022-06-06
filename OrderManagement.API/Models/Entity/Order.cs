using System;
using System.Collections.Generic;

namespace OrderManagement.API.Models.Entity
{
    public partial class Order
    {
        public int Id { get; set; }
        public string SystemOrderNo { get; set; }
        public string CustomerOrderNo { get; set; }
        public string OutputAddress { get; set; }
        public string DestinationAddress { get; set; }
        public decimal Quantity { get; set; }
        public string QuantityUnit { get; set; }
        public decimal Weight { get; set; }
        public string WeightUnit { get; set; }
        public string ItemCode { get; set; }
        public string Note { get; set; }
        public int StatusId { get; set; }
        public DateTime StatusUpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
