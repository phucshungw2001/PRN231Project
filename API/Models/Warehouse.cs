using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            Products = new HashSet<Product>();
        }

        public int WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
