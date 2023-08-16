using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public int InvoicesId { get; set; }
        public DateTime? InvoicesDate { get; set; }
        public int? CustomerId { get; set; }
        public bool? InvoicesStatus { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
