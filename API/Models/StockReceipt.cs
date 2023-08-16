using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class StockReceipt
    {
        public StockReceipt()
        {
            ReceiptDetails = new HashSet<ReceiptDetail>();
        }

        public int ReceiptId { get; set; }
        public DateTime? DateReceipt { get; set; }
        public int? WarehouseId { get; set; }
        public int? SupplierId { get; set; }

        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
}
