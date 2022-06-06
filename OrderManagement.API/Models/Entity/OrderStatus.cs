using System;
using System.Collections.Generic;

namespace OrderManagement.API.Models.Entity
{
    public partial class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
