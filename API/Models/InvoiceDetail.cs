using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class InvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        public int? InvoiceId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public double? Discount { get; set; }

        public virtual Invoice? Invoice { get; set; }
        public virtual Product? Product { get; set; }
    }
}
