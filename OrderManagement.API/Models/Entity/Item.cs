using System;
using System.Collections.Generic;

namespace OrderManagement.API.Models.Entity
{
    public partial class Item
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
