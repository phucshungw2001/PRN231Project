using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int SuppliersId { get; set; }
        public string? SuppliersName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
